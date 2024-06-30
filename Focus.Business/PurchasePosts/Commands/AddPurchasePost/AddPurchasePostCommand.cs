using Focus.Business.Interface;
using Focus.Business.Inventories.Queries.GetLatestInventory;
using Focus.Business.Transactions.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.PurchasePosts.Queries.GetPurchasePostList;
using Focus.Business.Purchases.Queries.GetPurchaseRegistrationNo;
using Focus.Business.Sales.Commands.SaleOrderPayment;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.EmailConfigurationSetup.Model;
using Focus.Business.Models;
using System.Text;
using Focus.Business.CreditNotes.Commands;
using Focus.Domain.Interface;

namespace Focus.Business.PurchasePosts.Commands.AddPurchasePost
{
    public class AddPurchasePostCommand : IRequest<Message>
    {
        public PurchasePostLookupModel PurchasePost { get; set; }
        public decimal Payments { get; set; }

        public class Handler : IRequestHandler<AddPurchasePostCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private readonly IUserHttpContextProvider _contextProvider;
            private string _code;
            private readonly ISendEmail sendEmail;
            public Handler(IApplicationDbContext context, ILogger<AddPurchasePostCommand> logger, IMediator mediator, IUserHttpContextProvider contextProvider, ISendEmail _sendEmail)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
                _contextProvider = contextProvider;
                sendEmail = _sendEmail;
            }

            public async Task<Message> Handle(AddPurchasePostCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }


                    var period = await _context.CompanySubmissionPeriods.FirstOrDefaultAsync(x => x.PeriodStart <= request.PurchasePost.Date.Date && x.PeriodEnd >= request.PurchasePost.Date.Date, cancellationToken: cancellationToken);
                    if (period == null)
                        throw new NotFoundException("Financial Year Not Found", "");

                    if (period.IsPeriodClosed)
                        throw new NotFoundException("Financial year period closed", "");

                    string invoiceNumber;
                    var invoiceNo = await _mediator.Send(new GetPurchaseRegistrationNoQuery()
                    {
                        BranchId = request.PurchasePost.BranchId,
                    }, cancellationToken);

                    if (request.PurchasePost.IsPurchaseReturn)
                    {
                        invoiceNumber = invoiceNo.PurchaseReturn;
                    }
                    else if (request.PurchasePost.IsPurchasePost)
                    {
                        invoiceNumber = invoiceNo.Post;
                    }
                    else
                    {
                        invoiceNumber = invoiceNo.Draft;
                    }

                    if (request.PurchasePost.IsPurchaseReturn)
                    {
                        var itemList = new List<PurchasePostItem>();

                        foreach (var item in request.PurchasePost.PurchasePostItems)
                        {
                            itemList.Add(new PurchasePostItem
                            {
                                UnitName = item.UnitName,
                                ProductId = item.ProductId,
                                TaxRateId = item.TaxRateId,
                                TaxMethod = item.TaxMethod,
                                Description = item.Description,
                                IsService = item.IsService,
                                Discount = item.Discount,
                                FixDiscount = Math.Round(item.FixDiscount, 2),
                                Quantity = request.PurchasePost.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity,
                                ExpiryDate = item.ExpiryDate,
                                SerialNo = item.SerialNo,
                                GuaranteeDate = item.GuaranteeDate,
                                BatchNo = item.BatchNo,
                                RemainingQuantity = Convert.ToDecimal(item.RemainingQuantity) - (request.PurchasePost.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity),
                                HighQty = item.HighQty,
                                UnitPerPack = item.UnitPerPack,
                                UnitPrice = Math.Round(item.UnitPrice, 2),
                                WareHouseId = request.PurchasePost.WareHouseId,
                                TotalAmount = item.TotalAmount,
                                TotalWithOutAmount = item.GrossAmount,
                                VatAmount = item.VatAmount,
                                DiscountAmount = item.DiscountAmount,
                            });
                        }
                        request.PurchasePost.ApprovalDate = DateTime.UtcNow;
                        var purchasePost = new PurchasePost()
                        {
                            NationalId = request.PurchasePost.NationalId,
                            BillingId = request.PurchasePost.BillingId,
                            ShippingId = request.PurchasePost.ShippingId,
                            DeliveryId = request.PurchasePost.DeliveryId,
                            Date = request.PurchasePost.Date,
                            PurchaseOrderNo = request.PurchasePost.PurchaseOrderNo,
                            InvoiceDate = request.PurchasePost.InvoiceDate,
                            InvoiceNo = request.PurchasePost.RegistrationNo,
                            RegistrationNo = invoiceNumber,
                            SupplierId = request.PurchasePost.SupplierId,
                            WareHouseId = request.PurchasePost.WareHouseId,
                            TaxMethod = request.PurchasePost.TaxMethod,
                            TaxRateId = request.PurchasePost.TaxRateId,
                            IsPurchaseReturn = request.PurchasePost.IsPurchaseReturn,
                            Raw = request.PurchasePost.IsRaw,
                            PurchaseInvoiceId = request.PurchasePost.PurchaseInvoiceId,
                            PurchasePostItems = itemList,
                            PeriodId = period.Id,
                            TransactionLevelDiscount = request.PurchasePost.TransactionLevelDiscount,
                            IsDiscountOnTransaction = request.PurchasePost.IsDiscountOnTransaction,
                            IsFixed = request.PurchasePost.IsFixed,
                            IsBeforeTax = request.PurchasePost.IsBeforeTax,
                            Discount = request.PurchasePost.Discount,
                            TotalAmount = request.PurchasePost.TotalAmount,
                            TotalWithOutAmount = request.PurchasePost.GrossAmount,
                            VatAmount = request.PurchasePost.VatAmount,
                            DiscountAmount = request.PurchasePost.DiscountAmount,
                            BranchId = request.PurchasePost.BranchId,
                            SupplierQuotationId = request.PurchasePost.SupplierQuotationId,
                            TotalAfterDiscount = request.PurchasePost.TotalAfterDiscount,
                            Note = request.PurchasePost.Note,
                            Reference = request.PurchasePost.Reference,
                            poNumberAndDate = request.PurchasePost.PoNumberAndDate,
                            GoodsRecieveNumberAndDate = request.PurchasePost.GoodsRecieveNumberAndDate,
                    };

                        await _context.PurchasePosts.AddAsync(purchasePost, cancellationToken);


                        if (request.PurchasePost.AttachmentList != null && request.PurchasePost.AttachmentList.Any())
                        {
                            foreach (var item in request.PurchasePost.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = purchasePost.Id,
                                    Description = item.Description,
                                    FileName = item.FileName,
                                    Path = item.Path
                                };
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);

                            }
                        }


                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            BranchId = request.PurchasePost.BranchId,
                            DocumentNo=purchasePost.RegistrationNo
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        var savePurchasePost = await _context.PurchasePosts
                            .AsNoTracking()
                            .Include(x => x.Supplier)
                            .Include(x => x.PurchasePostItems)
                            .ThenInclude(x => x.TaxRate)
                            .Include(x => x.PurchasePostItems)
                            .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Category)
                            .FirstOrDefaultAsync(x => x.Id == purchasePost.Id, cancellationToken: cancellationToken);

                        //var stocks = _context.Stocks.AsNoTracking().AsQueryable();

                        var purchaseInvoice = _context.PurchasePostItems
                            .AsNoTracking()
                            .Where(x => x.PurchasePostId == request.PurchasePost.PurchaseInvoiceId)
                            .AsQueryable();


                        foreach (var item in request.PurchasePost.PurchasePostItems)
                        {
                            if (item.ProductId == null)
                            {
                                //update remaining quantity
                                var purchaseInvoiceItem = purchaseInvoice.AsNoTracking().FirstOrDefault(x => x.Description == item.Description && x.PurchasePostId == request.PurchasePost.PurchaseInvoiceId);
                                purchaseInvoiceItem.RemainingQuantity = Convert.ToDecimal(purchaseInvoiceItem.RemainingQuantity) - Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity);

                                _context.PurchasePostItems.Update(purchaseInvoiceItem);
                            }
                            else
                            {
                                //update remaining quantity
                                var purchaseInvoiceItem = purchaseInvoice.AsNoTracking().FirstOrDefault(x => x.Id == item.Id && x.PurchasePostId == request.PurchasePost.PurchaseInvoiceId);
                                purchaseInvoiceItem.RemainingQuantity = Convert.ToDecimal(purchaseInvoiceItem.RemainingQuantity) - Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity);

                                _context.PurchasePostItems.Update(purchaseInvoiceItem);
                            }
                        }






                        Guid directPurchaseAccountId = default;
                        var costCenterDirectPurchase = await _context.CostCenters
                            .AsNoTracking()
                            .Include(x => x.Accounts)
                            .FirstOrDefaultAsync(x => x.Code == "190000", cancellationToken: cancellationToken);
                        if (costCenterDirectPurchase != null)
                        {
                            directPurchaseAccountId = costCenterDirectPurchase.Accounts.FirstOrDefault(x => x.Code == "19000001").Id;
                        }
                        else
                        {
                            var accountType = await _context.AccountTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Assets", cancellationToken: cancellationToken);

                            if (accountType != null)
                            {
                                var newCostCenter = new CostCenter()
                                {
                                    Code = "190000",
                                    Name = "Direct Purchase W/O Inventory",
                                    NameArabic = "Direct Purchase W/O Inventory",
                                    Description = "Direct Purchase W/O Inventory",
                                    IsActive = true,
                                    AccountTypeId = accountType.Id
                                };
                                await _context.CostCenters.AddAsync(newCostCenter, cancellationToken);

                                var newAccount = new Account
                                {
                                    Code = "19000001",
                                    Name = "Direct Purchase W/O Inventory",
                                    NameArabic = "Direct Purchase W/O Inventory",
                                    Description = "Direct Purchase W/O Inventory",
                                    IsActive = true,
                                    CostCenterId = newCostCenter.Id
                                };
                                await _context.Accounts.AddAsync(newAccount, cancellationToken);
                                directPurchaseAccountId = newAccount.Id;
                            }
                        }

                        //var stocks = new Collection<Stock>();
                        var transactions = new Collection<TransactionLookupModel>();
                        var ledgerTransactions = new Collection<LedgerTransaction>();
                        decimal inventoryAccountVal = 0;
                        decimal purchaseAccountVal = 0;
                        var isPos = _context.CompanyPermissions.AsNoTracking().Any(x => x.Value == "PosWithTransactionlevel");
                        foreach (var item in savePurchasePost.PurchasePostItems)
                        {
                            if (!item.IsService)
                            {
                                var stock = savePurchasePost.BranchId==null ? await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == item.WareHouseId, cancellationToken) : await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == item.WareHouseId && x.BranchId==savePurchasePost.BranchId, cancellationToken);

                                if (stock == null)
                                {
                                    var newStock = new Stock()
                                    {
                                        ProductId = item.ProductId.Value,
                                        WareHouseId = request.PurchasePost.WareHouseId,
                                        BranchId = savePurchasePost.BranchId
                                    };
                                    newStock.CurrentQuantity -= item.Quantity;
                                    await _context.Stocks.AddAsync(newStock, cancellationToken);

                                    stock = newStock;
                                }
                                else
                                {
                                    stock.CurrentQuantity -= item.Quantity;
                                    stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);
                                    stock.AveragePrice = Math.Round(stock.CurrentStockValue / stock.CurrentQuantity, 2);
                                }

                                //var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                                //{
                                //    ProductId = item.ProductId.Value,
                                //    StockId = stock.Id,
                                //    WareHouseId = item.WareHouseId
                                //}, cancellationToken);
                                if (stock.CurrentQuantity < 0)
                                    throw new ApplicationException("Current Qty is Less in " + item.Product.EnglishName);

                                var returnInvoiceValue = item.Quantity * item.UnitPrice;

                                if (!string.IsNullOrEmpty(item.BatchNo))
                                {
                                    var batchInventory =
                                        _context.Inventories.FirstOrDefault(
                                            x => x.BatchNumber == item.BatchNo && x.ProductId == item.ProductId);
                                    if (batchInventory != null)
                                    {
                                        if (batchInventory.RemainingQuantity > item.Quantity)
                                        {
                                            batchInventory.RemainingQuantity -= item.Quantity;
                                        }
                                        else if (batchInventory.RemainingQuantity == item.Quantity)
                                        {
                                            batchInventory.RemainingQuantity = 0;
                                            batchInventory.IsActive = false;
                                            batchInventory.IsOpen = false;
                                        }
                                        else
                                        {
                                            throw new ObjectAlreadyExistsException("Remaining Quantity of Product " + item.Product.EnglishName + " is: " + batchInventory.RemainingQuantity);
                                        }
                                    }
                                }

                                var inv = new Inventory()
                                {
                                    Date = savePurchasePost.Date,
                                    DocumentId = savePurchasePost.Id,
                                    DocumentNumber = savePurchasePost.RegistrationNo,
                                    Quantity = Convert.ToDecimal(item.Quantity),
                                    Price = Math.Round(item.UnitPrice, 2),
                                    ProductId = item.ProductId.Value,
                                    WarrantyDate = item.GuaranteeDate,
                                    Serial = item.SerialNo,
                                    StockId = stock.Id,
                                    WareHouseId = stock.WareHouseId,
                                    TransactionType = TransactionType.PurchaseReturn,
                                    //AveragePrice = averagePrice,
                                    //CurrentQuantity = currentInventory.CurrentQuantity - item.Quantity,
                                    //CurrentStockValue = (stockValue - returnInvoiceValue),
                                    SalePrice = item.Product.SalePrice,
                                    BranchId = savePurchasePost.BranchId
                                };

                                await _context.Inventories.AddAsync(inv, cancellationToken);

                                inventoryAccountVal = inventoryAccountVal + Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))
                                        : (item.Quantity * item.UnitPrice), 4);
                                if (isPos)
                                {
                                    //inventory Account
                                    ledgerTransactions.Add(new LedgerTransaction
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.PurchasePost.Date,
                                        ApprovalDate = request.PurchasePost.ApprovalDate,
                                        ContactId = savePurchasePost.SupplierId,
                                        AccountId = item.Product.InventoryAccountId,
                                        Debit = 0,
                                        Credit = Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))
                                        : (item.Quantity * item.UnitPrice), 4),
                                        Description = TransactionType.PurchaseReturn.ToString(),
                                        DocumentId = savePurchasePost.Id,
                                        TransactionType = TransactionType.PurchaseReturn,
                                        DocumentNumber = savePurchasePost.RegistrationNo,
                                        Year = savePurchasePost.Date.Year.ToString(),
                                        ProductId = item.ProductId,
                                        PeriodId = period.Id,
                                        BranchId = savePurchasePost.BranchId
                                    });
                                }
                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = _context.SyncLog(),
                                    Code = _code,
                                    BranchId = request.PurchasePost.BranchId,
                                    DocumentNo=purchasePost.RegistrationNo
                                }, cancellationToken);
                                await _context.SaveChangesAsync(cancellationToken);
                            }
                            else
                            {
                                //Direct Purchase Account
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.PurchasePost.Date,
                                    ApprovalDate = request.PurchasePost.ApprovalDate,
                                    ContactId = savePurchasePost.SupplierId,
                                    AccountId = directPurchaseAccountId,
                                    Debit = 0,
                                    Credit = Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))
                                    : (item.Quantity * item.UnitPrice), 4),
                                    Description = TransactionType.PurchasePost.ToString(),
                                    DocumentId = savePurchasePost.Id,
                                    TransactionType = TransactionType.PurchasePost,
                                    DocumentNumber = savePurchasePost.RegistrationNo,
                                    Year = savePurchasePost.Date.Year.ToString(),
                                    ProductId = item.ProductId,
                                    PeriodId = period.Id,
                                    BranchId = savePurchasePost.BranchId
                                });
                            }

                        }
                        var accounts = await _context.Accounts.Where(x => x.Name == "VAT on Purchase Return" || x.Code == "42600001" || x.Code == "11100001").ToListAsync(cancellationToken);

                        transactions.Add(new TransactionLookupModel
                        {
                            Date = DateTime.UtcNow,
                            DocumentDate = request.PurchasePost.Date,
                            ApprovalDate = request.PurchasePost.ApprovalDate,
                            ContactId = savePurchasePost.SupplierId,
                            AccountId = accounts.FirstOrDefault(x => x.Code == "11100001").Id,
                            Debit = 0,
                            Credit = inventoryAccountVal,
                            Description = TransactionType.PurchaseReturn.ToString(),
                            DocumentId = savePurchasePost.Id,
                            TransactionType = TransactionType.PurchaseReturn,
                            DocumentNumber = savePurchasePost.RegistrationNo,
                            Year = savePurchasePost.Date.Year.ToString(),
                            ProductId = Guid.Empty,
                            PeriodId = period.Id,
                            BranchId = savePurchasePost.BranchId
                        });

                        Guid vatAccountId;
                        var vatAccount = accounts.FirstOrDefault(x => x.Name == "VAT on Purchase Return");
                        if (vatAccount == null)
                        {
                            var vatCostCenter = await _context.CostCenters.AsNoTracking().Include(x => x.Accounts).FirstOrDefaultAsync(x => x.Code == "130000", cancellationToken: cancellationToken);
                            var code = vatCostCenter.Accounts.MaxBy(x => x.Code)?.Code;
                            var account = new Account()
                            {
                                Code = (Convert.ToInt64(code) + 1).ToString(),
                                Name = "VAT on Purchase Return",
                                NameArabic = "ضريبة القيمة المضافة على عائد الشراء",
                                Description = "VAT on Purchase Return",
                                IsActive = true,
                                CostCenterId = vatCostCenter.Id
                            };
                            await _context.Accounts.AddAsync(account, cancellationToken);
                            vatAccountId = account.Id;
                        }
                        else
                        {
                            vatAccountId = vatAccount.Id;
                        }


                        #region Transaction Level Discount

                        var sumQuantity = savePurchasePost.PurchasePostItems.Sum(product => product.Quantity);


                        var totalVat = savePurchasePost.PurchasePostItems.Sum(prod => ((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? ((((prod.Quantity * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - ((savePurchasePost.IsBeforeTax ? (((prod.Quantity * prod.UnitPrice) * savePurchasePost.TransactionLevelDiscount) / 100) : 0))) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
                        ((((prod.Quantity * prod.UnitPrice) - prod.FixDiscount - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - ((savePurchasePost.IsBeforeTax && !savePurchasePost.IsFixed && savePurchasePost.IsDiscountOnTransaction ? (((prod.Quantity * prod.UnitPrice) * savePurchasePost.TransactionLevelDiscount) / 100) : (savePurchasePost.IsBeforeTax && savePurchasePost.IsFixed && savePurchasePost.IsDiscountOnTransaction ? (savePurchasePost.TransactionLevelDiscount / sumQuantity * prod.Quantity) : 0)))) * prod.TaxRate.Rate) / 100));


                        //vat
                        transactions.Add(new TransactionLookupModel
                        {
                            Date = DateTime.UtcNow,
                            DocumentDate = request.PurchasePost.Date,
                            ApprovalDate = request.PurchasePost.ApprovalDate,
                            ContactId = savePurchasePost.SupplierId,
                            AccountId = vatAccountId,
                            Credit = Math.Abs(Math.Round((totalVat), 4)),
                            Debit = 0,
                            Description = TransactionType.PurchasePost.ToString(),
                            DocumentId = savePurchasePost.Id,
                            TransactionType = TransactionType.PurchasePost,
                            DocumentNumber = savePurchasePost.RegistrationNo,
                            Year = savePurchasePost.Date.Year.ToString(),
                            PeriodId = period.Id,
                            BranchId = savePurchasePost.BranchId
                        });


                        var totalVatWithoutFree = savePurchasePost.PurchasePostItems.Sum(prod => ((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? ((((prod.Quantity * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (savePurchasePost.IsBeforeTax ? (((prod.Quantity * prod.UnitPrice) * savePurchasePost.TransactionLevelDiscount) / 100) : 0)) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
                        ((((prod.Quantity * prod.UnitPrice) - prod.FixDiscount - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (savePurchasePost.IsBeforeTax && !savePurchasePost.IsFixed && savePurchasePost.IsDiscountOnTransaction ? (((prod.Quantity * prod.UnitPrice) * savePurchasePost.TransactionLevelDiscount) / 100) : (savePurchasePost.IsBeforeTax && savePurchasePost.IsFixed && savePurchasePost.IsDiscountOnTransaction ? (savePurchasePost.TransactionLevelDiscount / sumQuantity * prod.Quantity) : 0))) * prod.TaxRate.Rate) / 100));

                        // Discount
                        decimal transactionLevelTotalDiscount = 0;
                        if (savePurchasePost.IsDiscountOnTransaction)
                        {
                            transactionLevelTotalDiscount = (savePurchasePost.IsBeforeTax && savePurchasePost.IsDiscountOnTransaction) ? (savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? (savePurchasePost.TransactionLevelDiscount * (savePurchasePost.PurchasePostItems.Sum(x => (x.UnitPrice * x.Quantity) - (x.UnitPrice * x.Quantity * x.TaxRate.Rate / (100 + x.TaxRate.Rate)))) / 100) : (savePurchasePost.IsFixed ? savePurchasePost.TransactionLevelDiscount : savePurchasePost.TransactionLevelDiscount * savePurchasePost.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity) / 100) : (savePurchasePost.IsFixed ? savePurchasePost.TransactionLevelDiscount : savePurchasePost.TransactionLevelDiscount * (savePurchasePost.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity) + ((savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? 0 : totalVatWithoutFree)) / 100);

                            if (transactionLevelTotalDiscount != 0)
                            {
                                //Discount  
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.PurchasePost.Date,
                                    ApprovalDate = request.PurchasePost.ApprovalDate,
                                    ContactId = savePurchasePost.SupplierId,
                                    AccountId = accounts.FirstOrDefault(x => x.Code == "42600001")?.Id ?? Guid.Empty,
                                    Credit = 0,
                                    Debit = Math.Abs(Math.Round((transactionLevelTotalDiscount), 4)),

                                    Description = TransactionType.PurchasePost.ToString(),
                                    DocumentId = savePurchasePost.Id,
                                    TransactionType = TransactionType.PurchasePost,
                                    DocumentNumber = savePurchasePost.RegistrationNo,
                                    Year = savePurchasePost.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = savePurchasePost.BranchId
                                });
                            }


                        }

                        else
                        {
                            if (savePurchasePost.PurchasePostItems.Sum(x => Math.Round((x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : (savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100) : x.FixDiscount), 2)) != 0)
                            {
                                //Discount  
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.PurchasePost.Date,
                                    ApprovalDate = request.PurchasePost.ApprovalDate,
                                    ContactId = savePurchasePost.SupplierId,
                                    AccountId = accounts.FirstOrDefault(x => x.Code == "42600001")?.Id ?? Guid.Empty,
                                    Credit = 0,
                                    Debit = savePurchasePost.PurchasePostItems.Sum(x => Math.Round((x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : (savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100) : x.FixDiscount), 2)),

                                    Description = TransactionType.PurchasePost.ToString(),
                                    DocumentId = savePurchasePost.Id,
                                    TransactionType = TransactionType.PurchasePost,
                                    DocumentNumber = savePurchasePost.RegistrationNo,
                                    Year = savePurchasePost.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = savePurchasePost.BranchId
                                });
                            }

                        }
                        #endregion
                        #region Discount/Adjustment

                        if (savePurchasePost.Discount != 0)
                        {
                            var account = await _context.Accounts.AsNoTracking()
                                           .FirstOrDefaultAsync(x => x.Name == "Other Expense");
                            Guid accountId = account == null ? Guid.Empty : account.Id;
                            if (account == null)
                            {
                                var lastInventoryCode = await _context.Accounts.AsNoTracking().Include(x => x.CostCenter)
                                       .OrderBy(x => x.CostCenter.Name == "Expense Adjustment").LastOrDefaultAsync();

                                var newAccount = new Account
                                {
                                    Code = (Convert.ToInt32(lastInventoryCode.Code) + 1).ToString(),
                                    Name = "Other Expense",
                                    Description = "Other Expense",
                                    IsActive = true,
                                    CostCenterId = lastInventoryCode.CostCenterId
                                };
                                await _context.Accounts.AddAsync(newAccount);
                                accountId = newAccount.Id;
                            }
                            transactions.Add(new TransactionLookupModel
                            {
                                Date = DateTime.UtcNow,
                                ContactId = savePurchasePost.SupplierId,
                                AccountId = accountId,
                                Debit = savePurchasePost.Discount < 0 ? Math.Abs(Math.Round((savePurchasePost.Discount), 4)) : 0,
                                Credit = savePurchasePost.Discount > 0 ? Math.Abs(Math.Round((savePurchasePost.Discount), 4)) : 0,
                                Description = "Discount/ Adjustment",
                                DocumentId = savePurchasePost.Id,
                                TransactionType = TransactionType.SaleInvoice,
                                DocumentNumber = savePurchasePost.RegistrationNo,
                                Year = savePurchasePost.Date.Year.ToString(),
                                PeriodId = period.Id,
                                BranchId = savePurchasePost.BranchId
                            });


                        }

                        #endregion



                        //account Payable
                        decimal total = 0;
                        if (savePurchasePost.IsDiscountOnTransaction)
                        {
                            var saleTotal = savePurchasePost.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity);
                            var disc = (savePurchasePost.IsBeforeTax && savePurchasePost.IsDiscountOnTransaction) ? (savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? (savePurchasePost.TransactionLevelDiscount * (savePurchasePost.PurchasePostItems.Sum(x => (x.UnitPrice * x.Quantity))) / 100) : (savePurchasePost.IsFixed ? savePurchasePost.TransactionLevelDiscount : savePurchasePost.TransactionLevelDiscount * savePurchasePost.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity) / 100) : (savePurchasePost.IsFixed ? savePurchasePost.TransactionLevelDiscount : savePurchasePost.TransactionLevelDiscount * (savePurchasePost.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity) + ((savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? 0 : totalVatWithoutFree)) / 100);

                            total = Math.Round(saleTotal + ((savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? 0 : totalVatWithoutFree) - disc + savePurchasePost.Discount, 4);
                        }
                        else
                        {
                            total = (savePurchasePost.PurchasePostItems.Sum(x => Math.Round(((((((x.UnitPrice * x.Quantity) - (x.Discount != 0 ? (x.UnitPrice * x.Quantity * x.Discount) / 100 : (savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل" ? x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100) : x.FixDiscount))) * ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : (100 + x.TaxRate.Rate))) / ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : 100)))), 2))) + savePurchasePost.Discount;

                        }
                        //account Payable
                        transactions.Add(new TransactionLookupModel
                        {
                            Date = DateTime.UtcNow,
                            DocumentDate = request.PurchasePost.Date,
                            ApprovalDate = request.PurchasePost.ApprovalDate,
                            ContactId = savePurchasePost.SupplierId,
                            AccountId = savePurchasePost.Supplier.AccountId.Value,
                            Credit = 0,
                            Debit = Math.Abs(Math.Round(total, 4)),
                            Description = TransactionType.PurchasePost.ToString(),
                            DocumentId = savePurchasePost.Id,
                            TransactionType = TransactionType.PurchasePost,
                            DocumentNumber = savePurchasePost.RegistrationNo,
                            Year = savePurchasePost.Date.Year.ToString(),
                            PeriodId = period.Id,
                            BranchId = savePurchasePost.BranchId
                        });

                        //transactions.Add(new TransactionLookupModel
                        //{
                        //    Date = DateTime.UtcNow,
                        //    DocumentDate = request.PurchasePost.Date,
                        //    ApprovalDate = request.PurchasePost.ApprovalDate,
                        //    ContactId = savePurchasePost.SupplierId,
                        //    AccountId = savePurchasePost.Supplier.AccountId.Value,
                        //    Credit = savePurchasePost.PurchasePostItems.Where(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل").Sum(x => Math.Round(x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : x.FixDiscount * Convert.ToDecimal(x.Quantity), 2)),
                        //    Debit = 0,
                        //    Description = TransactionType.PurchasePost.ToString(),
                        //    DocumentId = savePurchasePost.Id,
                        //    TransactionType = TransactionType.PurchasePost,
                        //    DocumentNumber = savePurchasePost.RegistrationNo,
                        //    Year = savePurchasePost.Date.Year.ToString(),
                        //    PeriodId = period.Id
                        //});



                        foreach (var transaction in transactions)
                        {
                            await _mediator.Send(new AddTransactionCommand { Transaction = transaction, Code= _code }, cancellationToken);
                        }

                        var isClose = await _context.PurchasePostItems.AnyAsync(x => x.PurchasePostId == request.PurchasePost.PurchaseInvoiceId && x.RemainingQuantity > 0, cancellationToken: cancellationToken);
                        if (!isClose)
                        {
                            var po = _context.PurchasePosts.FirstOrDefault(x =>
                                x.Id == request.PurchasePost.PurchaseInvoiceId);
                            if (po != null)
                            {
                                po.IsClose = true;
                                po.PartiallyInvoices = PartiallyInvoices.Return;
                            }
                        }
                        else
                        {
                            var po = _context.PurchasePosts.FirstOrDefault(x =>
                                x.Id == request.PurchasePost.PurchaseInvoiceId);
                            if (po != null)
                            {
                                po.PartiallyInvoices = PartiallyInvoices.Return;
                            }

                        }
                        await _context.LedgerTransactions.AddRangeAsync(ledgerTransactions, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            BranchId = request.PurchasePost.BranchId,
                            DocumentNo=purchasePost.RegistrationNo
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        scope.Complete();

                        // Return Product id after successfully updating data
                        var message = new Message
                        {
                            Id = savePurchasePost.Id,
                            IsAddUpdate = "Data has been added successfully"
                        };
                        return message;
                    }

                    if (request.PurchasePost.Id != Guid.Empty)
                    {
                        var purchase = await _context.PurchasePosts.FindAsync(request.PurchasePost.Id);

                        if (purchase == null)
                            throw new NotFoundException("Purchase invoice not found.", "");

                        //if (purchase.PurchaseOrder != null)
                        //{
                        //    if (purchase.PurchaseOrder.IsClose)
                        //        throw new NotFoundException("Purchase Order Already Close", "");
                        //}


                        var itemList = request.PurchasePost.PurchasePostItems.Select(item => new PurchasePostItem
                        {
                            UnitName = item.UnitName,
                            ProductId = item.ProductId,
                            TaxRateId = item.TaxRateId,
                            TaxMethod = item.TaxMethod,
                            Discount = item.Discount,
                            Description = item.Description,
                            FixDiscount = Math.Round(item.FixDiscount, 2),
                            Quantity = item.ProductId == null ? item.Quantity : request.PurchasePost.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity,
                            RemainingQuantity = item.ProductId == null ? item.Quantity : request.PurchasePost.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity,
                            ExpiryDate = item.ExpiryDate,
                            SerialNo = item.SerialNo,
                            GuaranteeDate = item.GuaranteeDate,
                            BatchNo = item.BatchNo,
                            HighQty = item.HighQty,
                            UnitPerPack = item.UnitPerPack,
                            WarrantyTypeId = item.WarrantyTypeId,
                            IsService = item.IsService,
                            UnitPrice = Math.Round(item.UnitPrice, 2),
                            WareHouseId = request.PurchasePost.WareHouseId,
                            ColorId = item.ColorId,
                            TotalAmount = item.TotalAmount,
                            TotalWithOutAmount = item.GrossAmount,
                            VatAmount = item.VatAmount,
                            DiscountAmount = item.DiscountAmount,
                            SaleSizeAssortments = item.SaleSizeAssortment?.Select(size => new SaleSizeAssortment() { SizeId = size.SizeId, Quantity = size.Quantity }).ToList()
                        })
                            .ToList();

                        _context.PurchasePostItems.RemoveRange(purchase.PurchasePostItems);


                        purchase.NationalId = request.PurchasePost.NationalId;
                        purchase.BillingId = request.PurchasePost.BillingId;
                        purchase.ShippingId = request.PurchasePost.ShippingId;
                        purchase.DeliveryId = request.PurchasePost.DeliveryId;



                        purchase.RegistrationNo = request.PurchasePost.IsPurchasePost ? invoiceNumber : request.PurchasePost.RegistrationNo;
                        purchase.Date = request.PurchasePost.Date;
                        purchase.PurchaseOrderNo = request.PurchasePost.PurchaseOrderNo;
                        purchase.SupplierId = request.PurchasePost.SupplierId;
                        purchase.IsPurchaseReturn = false;
                        purchase.InvoiceNo = request.PurchasePost.InvoiceNo;
                        purchase.InvoiceDate = request.PurchasePost.InvoiceDate;
                        purchase.WareHouseId = request.PurchasePost.WareHouseId;
                        purchase.InvoiceFixDiscount = request.PurchasePost.InvoiceFixDiscount;
                        purchase.PurchaseOrderId = request.PurchasePost.PurchaseOrderId;
                        purchase.GoodReceiveNoteId = request.PurchasePost.GoodReceiveNoteId;
                        purchase.TaxMethod = request.PurchasePost.TaxMethod;
                        purchase.TaxRateId = request.PurchasePost.TaxRateId;
                        purchase.Raw = request.PurchasePost.IsRaw;
                        purchase.IsPurchasePost = request.PurchasePost.IsPurchasePost;
                        purchase.PurchasePostItems = itemList;
                        purchase.PeriodId = period.Id;
                        purchase.BankCashAccountId = request.PurchasePost.BankCashAccountId;
                        purchase.PaymentType = request.PurchasePost.PaymentType;
                        purchase.IsCredit = request.PurchasePost.IsCredit;
                        purchase.TransactionLevelDiscount = request.PurchasePost.TransactionLevelDiscount;
                        purchase.IsDiscountOnTransaction = request.PurchasePost.IsDiscountOnTransaction;
                        purchase.IsFixed = request.PurchasePost.IsFixed;
                        purchase.IsBeforeTax = request.PurchasePost.IsBeforeTax;
                        purchase.Discount = request.PurchasePost.Discount;
                        purchase.TotalAmount = request.PurchasePost.TotalAmount;
                        purchase.TotalWithOutAmount = request.PurchasePost.GrossAmount;
                        purchase.VatAmount = request.PurchasePost.VatAmount;
                        purchase.DiscountAmount = request.PurchasePost.DiscountAmount;
                        purchase.poNumberAndDate = request.PurchasePost.PoNumberAndDate;
                        purchase.GoodsRecieveNumberAndDate = request.PurchasePost.GoodsRecieveNumberAndDate;
                        purchase.TotalAfterDiscount = request.PurchasePost.TotalAfterDiscount;
                        purchase.Note = request.PurchasePost.Note;
                        purchase.Reference = request.PurchasePost.Reference;


                        if (!request.PurchasePost.IsCredit && request.PurchasePost.IsPurchasePost)
                        {
                            purchase.PartiallyInvoices = PartiallyInvoices.Fully;
                        }




                        //Product Variation
                        if (request.PurchasePost.IsPurchasePost && request.PurchasePost.ColorVariants)
                        {
                            foreach (var item in request.PurchasePost.PurchasePostItems.Where(x => !x.IsService))
                            {
                                foreach (var size in item.SaleSizeAssortment.Where(size => size.Quantity > 0))
                                {
                                    var variation = await _context.VariationInventories.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.ColorId == item.ColorId && x.SizeId == size.SizeId, cancellationToken: cancellationToken);
                                    if (variation == null)
                                    {
                                        var variationInventory = new VariationInventory()
                                        {
                                            ProductId = item.ProductId.Value,
                                            ColorId = item.ColorId,
                                            SizeId = size.SizeId,
                                            Quantity = size.Quantity,
                                            BranchId = purchase.BranchId
                                        };
                                        await _context.VariationInventories.AddAsync(variationInventory, cancellationToken);
                                    }
                                    else
                                    {
                                        variation.Quantity += size.Quantity;
                                        _context.VariationInventories.Update(variation);
                                    }

                                    var variationInventoryForReporting = new VariationInventoryForReporting()
                                    {
                                        DocumentId = purchase.Id,
                                        TransactionType = TransactionType.PurchasePost,
                                        ProductId = item.ProductId.Value,
                                        ColorId = item.ColorId.Value,
                                        SizeId = size.SizeId,
                                        Quantity = size.Quantity,
                                        BranchId = purchase.BranchId
                                    };
                                    await _context.VariationInventoryForReportings.AddAsync(variationInventoryForReporting, cancellationToken);
                                }
                            }
                        }



                        if (request.PurchasePost.AttachmentList != null && request.PurchasePost.AttachmentList.Count>0)
                        {
                            var attachments = _context.Attachments
                                .AsNoTracking()
                                .Where(x => x.DocumentId == purchase.Id)
                                .AsQueryable();

                            if (attachments.Any())
                            {
                                _context.Attachments.RemoveRange(attachments);
                            }

                            foreach (var item in request.PurchasePost.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = purchase.Id,
                                    Description = item.Description,
                                    FileName = item.FileName,
                                    Path = item.Path
                                };
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);

                            }
                        }

                       

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            BranchId = request.PurchasePost.BranchId,
                            DocumentNo= purchase.RegistrationNo
                        }, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);

                        

                        bool isClose = false;
                        if (purchase.PurchaseOrder != null)
                        {
                            isClose = purchase.PurchaseOrder.IsClose;

                        }

                        if (request.PurchasePost.PurchaseOrderId != null && request.PurchasePost.IsPurchasePost && !isClose)
                        {
                            // Get PO Details
                            var po = await _context.PurchaseOrders
                                .Include(x => x.PurchaseOrderActions)
                                .Include(x => x.PurchaseAttachments)
                                .Include(x => x.Supplier)
                                .Include(x => x.PurchasePosts)
                                .ThenInclude(x => x.PurchasePostItems)
                                .ThenInclude(x => x.TaxRate)
                                .Include(x => x.PurchaseOrderItems)
                                .Include(x => x.PurchaseOrderPayments)
                                .ThenInclude(x => x.PaymentVoucher)
                                .Include(x => x.PurchaseOrderExpenses)
                                .ThenInclude(x => x.PaymentVoucher)
                                .FirstOrDefaultAsync(x => x.Id == request.PurchasePost.PurchaseOrderId, cancellationToken);
                            if (po == null)
                                throw new NotFoundException("Purchase Order Was not found", "");
                            if (po.IsClose)
                                throw new NotFoundException("Purchase Order Was not found", "");

                            //Add Expense
                            if (request.PurchasePost.PurchasePostExpense != null && request.PurchasePost.InternationalPurchase)
                            {
                                var purchasePostExpenses = new List<PurchasePostExpense>();
                                foreach (var expense in request.PurchasePost.PurchasePostExpense)
                                {
                                    var poExpense = po.PurchaseOrderExpenses.FirstOrDefault(x => x.Id == expense.Id);
                                    poExpense.UsedAmount = poExpense.UsedAmount + expense.Amount;
                                    _context.PurchaseOrderExpenses.Update(poExpense);

                                    purchasePostExpenses.Add(new PurchasePostExpense
                                    {
                                        Date = expense.Date,
                                        PurchasePostId = purchase.Id,
                                        VoucherNumber = expense.VoucherNumber,
                                        Narration = expense.Narration,
                                        ChequeNumber = expense.ChequeNumber,
                                        ApprovalStatus = expense.ApprovalStatus,
                                        PaymentVoucherType = expense.PaymentVoucherType,
                                        BankCashAccountId = expense.BankCashAccountId,
                                        ContactAccountId = expense.ContactAccountId,
                                        Amount = expense.Amount,
                                        ApprovedDate = DateTime.Now,
                                        PaymentMethod = expense.PaymentMethod,
                                        PaymentMode = expense.PaymentMode,
                                        TaxMethod = expense.TaxMethod,
                                        TaxRateId = expense.TaxRateId,
                                        VatAccountId = expense.VatAccountId
                                    });
                                }
                                await _context.PurchasePostExpenses.AddRangeAsync(purchasePostExpenses, cancellationToken);
                            }

                            //Add Action
                            if (po.PurchaseOrderActions != null && request.PurchasePost.InternationalPurchase)
                            {
                                var purchaseInvoiceAction = new List<PurchaseInvoiceAction>();
                                foreach (var action in po.PurchaseOrderActions)
                                {
                                    purchaseInvoiceAction.Add(new PurchaseInvoiceAction
                                    {
                                        PurchaseInvoicePostId = purchase.Id,
                                        ProcessId = action.ProcessId,
                                        CurrentDate = DateTime.UtcNow,
                                        Date = action.Date,
                                        Description = action.Description
                                    });
                                }
                                await _context.PurchaseInvoiceActions.AddRangeAsync(purchaseInvoiceAction, cancellationToken);
                            }

                            // Add Attachment
                            if (po.PurchaseAttachments != null && request.PurchasePost.InternationalPurchase)
                            {
                                var purchaseInvoiceAttachments = new List<PurchaseInvoiceAttachment>();
                                foreach (var attachment in po.PurchaseAttachments)
                                {
                                    purchaseInvoiceAttachments.Add(new PurchaseInvoiceAttachment
                                    {
                                        PurchaseInvoicePostId = purchase.Id,
                                        Path = attachment.Path,
                                        Date = attachment.Date,
                                        Description = attachment.Description
                                    });
                                }
                                await _context.PurchaseInvoiceAttachments.AddRangeAsync(purchaseInvoiceAttachments, cancellationToken);
                            }

                            //Update PO Item Remaining Quantity
                            //foreach (var item in request.PurchasePost.PurchasePostItems)
                            //{
                            //    if (item.ProductId == null)
                            //    {
                            //        var poDetail = po.PurchaseOrderItems.FirstOrDefault(x => x.Description == item.Description);
                            //        if (poDetail != null)
                            //        {
                            //            poDetail.RemainingQuantity = poDetail.RemainingQuantity - (request.PurchasePost.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity);
                            //            _context.PurchaseOrderItems.Update(poDetail);
                            //        }
                            //    }
                            //    else
                            //    {
                            //        var poDetail = po.PurchaseOrderItems.FirstOrDefault(x => x.Id == item.Id);
                            //        if (poDetail != null)
                            //        {
                            //            poDetail.RemainingQuantity = poDetail.RemainingQuantity - (request.PurchasePost.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity);
                            //            _context.PurchaseOrderItems.Update(poDetail);
                            //        }
                            //    }

                            //}

                            //Close PO
                            //if (request.PurchasePost.PartiallyPurchase)
                            //{
                            //    var close = po.PurchaseOrderItems.FirstOrDefault(x => x.PurchaseOrderId == request.PurchasePost.PurchaseOrderId && x.RemainingQuantity > 0);
                            //    if (close == null)
                            //    {
                            //        po.IsClose = true;
                            //    }
                            //}
                            //else
                            //{
                            //    po.IsClose = true;
                            //}


                            //po.IsProcessed = true;
                            //if (String.IsNullOrEmpty(po.Reason))
                            //{
                            //    po.Reason = request.PurchasePost.RegistrationNo + " " + request.PurchasePost.Date.ToString("dd/MM/yyyy");

                            //}
                            //else

                            //{
                            //    po.Reason = po.Reason + "     ||     " + request.PurchasePost.RegistrationNo + " " + request.PurchasePost.Date.ToString("dd/MM/yyyy");

                            //}

                            // Auto Generate Pay Voucher
                            if (request.PurchasePost.AutoPurchaseVoucher && request.PurchasePost.InternationalPurchase && request.PurchasePost.IsCredit)
                            {
                                var purchaseOrderList = new List<PurchasePostLookUpModel>();
                                foreach (var purchaseOrder in po.PurchasePosts)
                                {
                                    var netAmount = new TotalAmount();
                                    var lookUpModel = new PurchasePostLookUpModel
                                    {
                                        Id = purchaseOrder.Id,
                                        NetAmount = netAmount.CalculateTotalAmount(purchaseOrder)
                                    };
                                    purchaseOrderList.Add(lookUpModel);
                                }

                                var payments = purchaseOrderList.Sum(x => x.NetAmount);
                                var totalPay = po.PurchaseOrderPayments.Sum(x => x.Amount);
                                var result = request.PurchasePost.DueAmount - (totalPay - payments);
                                if (totalPay > 0)
                                {
                                    await _mediator.Send(new AddPaymentVoucherCommand
                                    {
                                        IsPurchase = true,
                                        PurchaseInvoice = purchase.Id,
                                        ContactAccountId = po.Supplier?.AccountId ?? Guid.Empty,
                                        ContactAdvanceAccountId = po.Supplier?.AdvanceAccountId ?? Guid.Empty,
                                        Payment = payments,
                                        DueAmount = request.PurchasePost.DueAmount,
                                        PurchaseOrderPayments = po.PurchaseOrderPayments,
                                        BranchId = purchase.BranchId
                                    }, cancellationToken);
                                }


                                if (result <= 0)
                                {
                                    purchase.PartiallyInvoices = PartiallyInvoices.Fully;
                                }
                                else if (totalPay - payments > 0)
                                {
                                    purchase.PartiallyInvoices = PartiallyInvoices.Partially;
                                }
                            }
                        }



                        if (request.PurchasePost.IsPurchasePost)
                        {
                            Guid directPurchaseAccountId = default;
                            var costCenterDirectPurchase = await _context.CostCenters
                                .AsNoTracking()
                                .Include(x => x.Accounts)
                                .FirstOrDefaultAsync(x => x.Code == "190000", cancellationToken: cancellationToken);
                            if (costCenterDirectPurchase != null)
                            {
                                directPurchaseAccountId = costCenterDirectPurchase.Accounts.FirstOrDefault(x => x.Code == "19000001").Id;
                            }
                            else
                            {
                                var accountType = await _context.AccountTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Assets", cancellationToken: cancellationToken);

                                if (accountType != null)
                                {
                                    var newCostCenter = new CostCenter()
                                    {
                                        Code = "190000",
                                        Name = "Direct Purchase W/O Inventory",
                                        NameArabic = "Direct Purchase W/O Inventory",
                                        Description = "Direct Purchase W/O Inventory",
                                        IsActive = true,
                                        AccountTypeId = accountType.Id
                                    };
                                    await _context.CostCenters.AddAsync(newCostCenter, cancellationToken);

                                    var newAccount = new Account
                                    {
                                        Code = "19000001",
                                        Name = "Direct Purchase W/O Inventory",
                                        NameArabic = "Direct Purchase W/O Inventory",
                                        Description = "Direct Purchase W/O Inventory",
                                        IsActive = true,
                                        CostCenterId = newCostCenter.Id
                                    };
                                    await _context.Accounts.AddAsync(newAccount, cancellationToken);
                                    directPurchaseAccountId = newAccount.Id;
                                }
                            }

                            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                            var savePurchasePost = _context.PurchasePosts
                                .AsNoTracking()
                                .Include(x => x.Supplier)
                                .Include(x => x.PurchasePostItems)
                                .ThenInclude(x => x.TaxRate)
                                .Include(x => x.PurchasePostItems)
                                .ThenInclude(x => x.Product)
                                .ThenInclude(x => x.Category)
                                .FirstOrDefault(x => x.Id == purchase.Id);

                            if (savePurchasePost == null)
                                throw new NotFoundException("Purchase Invoice", "Not Found");

                            var transactions = new Collection<TransactionLookupModel>();
                            var ledgerTransactions = new Collection<LedgerTransaction>();
                            decimal inventoryAccountVal = 0;
                            decimal cogsAccountVal = 0;
                            decimal purchaseAccountVal = 0;
                            var isPos = _context.CompanyPermissions.AsNoTracking().Any(x => x.Value == "PosWithTransactionlevel");

                            var autoNumber = _context.Inventories.OrderByDescending(x => x.AutoNumbering).FirstOrDefault(x => x.AutoNumbering != 0)?.AutoNumbering;
                            foreach (var item in savePurchasePost.PurchasePostItems)
                            {
                                if (item.ProductId != null && !item.IsService)
                                {
                                    var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == item.WareHouseId, cancellationToken);

                                    if (stock == null)
                                    {
                                        var newStock = new Stock()
                                        {
                                            ProductId = item.ProductId.Value,
                                            WareHouseId = request.PurchasePost.WareHouseId,
                                            BranchId = savePurchasePost.BranchId
                                        };

                                        if (!request.PurchasePost.ExpenseToGst)
                                            newStock.CurrentQuantity = newStock.CurrentQuantity + Convert.ToDecimal(item.Quantity);
                                        await _context.Stocks.AddAsync(newStock, cancellationToken);

                                        stock = newStock;
                                    }
                                    else if (!request.PurchasePost.ExpenseToGst)
                                    {

                                        stock.CurrentQuantity = stock.CurrentQuantity + Convert.ToDecimal(item.Quantity);
                                    }

                                    var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                                    {
                                        ProductId = item.ProductId.Value,
                                        StockId = stock.Id,
                                        WareHouseId = item.WareHouseId
                                    }, cancellationToken);

                                    //if (item.ExpiryDate < CurrentInventory.ExpiryDate)
                                    //    throw new ApplicationException(item.Product.EnglishName + "Expiry date must be greter than Last inventory expiry date.");

                                    if (!request.PurchasePost.ExpenseToGst)
                                    {
                                        var inv = new Inventory()
                                        {
                                            Date = savePurchasePost.Date,
                                            DocumentNumber = savePurchasePost.RegistrationNo,
                                            DocumentId = savePurchasePost.Id,
                                            Quantity = Convert.ToDecimal(item.Quantity),
                                            SalePrice = item.Product.SalePrice,
                                            Price = Math.Round(item.UnitPrice, 2),
                                            ProductId = item.ProductId.Value,
                                            StockId = stock.Id,
                                            WareHouseId = stock.WareHouseId,
                                            WarrantyDate = item.GuaranteeDate,
                                            Serial = item.SerialNo,
                                            TransactionType = TransactionType.PurchasePost,
                                            AveragePrice = currentInventory.CurrentQuantity == 0 ? Math.Round(item.UnitPrice, 2) : Math.Round((currentInventory.AveragePrice + item.UnitPrice) / 2, 2),
                                            ExpiryDate = item.ExpiryDate ?? currentInventory.ExpiryDate,
                                            CurrentQuantity = currentInventory.CurrentQuantity + Convert.ToDecimal(item.Quantity),
                                            CurrentStockValue = (currentInventory.CurrentQuantity + Convert.ToDecimal(item.Quantity)) * (currentInventory.CurrentQuantity == 0 ? Math.Round(item.UnitPrice, 2) : Math.Round((currentInventory.AveragePrice + item.UnitPrice) / 2, 2)),
                                            IsActive = true,
                                            IsOpen = true,
                                            BatchNumber = item.BatchNo,
                                            AutoNumbering = (int)(autoNumber == null ? 1 : ++autoNumber),
                                            RemainingQuantity = Convert.ToDecimal(item.Quantity),
                                            BranchId = savePurchasePost.BranchId
                                        };
                                        await _context.Inventories.AddAsync(inv, cancellationToken);
                                    }

                                    inventoryAccountVal = inventoryAccountVal + Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))
                                  : (item.Quantity * item.UnitPrice), 4);

                                    if (isPos)
                                    {
                                        transactions.Add(new TransactionLookupModel
                                        {
                                            Date = DateTime.UtcNow,
                                            DocumentDate = request.PurchasePost.Date,
                                            ApprovalDate = request.PurchasePost.ApprovalDate,
                                            ContactId = savePurchasePost.SupplierId,
                                            AccountId = item.Product.Category.InventoryAccountId,
                                            Credit = 0,
                                            Debit = Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))
                                  : (item.Quantity * item.UnitPrice), 4),
                                            Description = TransactionType.PurchasePost.ToString(),
                                            DocumentId = savePurchasePost.Id,
                                            TransactionType = TransactionType.PurchasePost,
                                            DocumentNumber = savePurchasePost.RegistrationNo,
                                            Year = savePurchasePost.Date.Year.ToString(),
                                            ProductId = item.ProductId,
                                            PeriodId = period.Id,
                                            BranchId = savePurchasePost.BranchId
                                        });
                                    }
                                    //inventory Account

                                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                                    {
                                        SyncRecordModels = _context.SyncLog(),
                                        Code = _code,
                                        BranchId = request.PurchasePost.BranchId,
                                        DocumentNo=purchase.RegistrationNo
                                    }, cancellationToken);
                                    //Save changes to database
                                    await _context.SaveChangesAsync(cancellationToken);
                                }
                                else
                                {
                                    //Direct Purchase Account
                                    transactions.Add(new TransactionLookupModel
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.PurchasePost.Date,
                                        ApprovalDate = request.PurchasePost.ApprovalDate,
                                        ContactId = savePurchasePost.SupplierId,
                                        AccountId = directPurchaseAccountId,
                                        Credit = 0,
                                        Debit = Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))
                                    : (item.Quantity * item.UnitPrice), 4),
                                        Description = TransactionType.PurchasePost.ToString(),
                                        DocumentId = savePurchasePost.Id,
                                        TransactionType = TransactionType.PurchasePost,
                                        DocumentNumber = savePurchasePost.RegistrationNo,
                                        Year = savePurchasePost.Date.Year.ToString(),
                                        ProductId = item.ProductId,
                                        PeriodId = period.Id,
                                        BranchId = savePurchasePost.BranchId
                                    });
                                }
                            }

                            var accounts = await _context.Accounts.Where(x => x.Code == "1300001" || x.Code == "42600001").ToListAsync(cancellationToken);

                            #region Transaction Level Discount

                            var sumQuantity = savePurchasePost.PurchasePostItems.Sum(product => product.Quantity);


                            var totalVat = savePurchasePost.PurchasePostItems.Sum(prod => ((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? ((((prod.Quantity * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - ((savePurchasePost.IsBeforeTax ? (((prod.Quantity * prod.UnitPrice) * savePurchasePost.TransactionLevelDiscount) / 100) : 0))) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
                            ((((prod.Quantity * prod.UnitPrice) - prod.FixDiscount - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - ((savePurchasePost.IsBeforeTax && !savePurchasePost.IsFixed && savePurchasePost.IsDiscountOnTransaction ? (((prod.Quantity * prod.UnitPrice) * savePurchasePost.TransactionLevelDiscount) / 100) : (savePurchasePost.IsBeforeTax && savePurchasePost.IsFixed && savePurchasePost.IsDiscountOnTransaction ? (savePurchasePost.TransactionLevelDiscount / sumQuantity * prod.Quantity) : 0)))) * prod.TaxRate.Rate) / 100));


                            //vat
                            transactions.Add(new TransactionLookupModel
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = request.PurchasePost.Date,
                                ApprovalDate = request.PurchasePost.ApprovalDate,
                                ContactId = savePurchasePost.SupplierId,
                                AccountId = accounts.FirstOrDefault(x => x.Code == "1300001").Id,
                                Credit = 0,
                                Debit = Math.Abs(Math.Round((totalVat), 4)),
                                Description = TransactionType.PurchasePost.ToString(),
                                DocumentId = savePurchasePost.Id,
                                TransactionType = TransactionType.PurchasePost,
                                DocumentNumber = savePurchasePost.RegistrationNo,
                                Year = savePurchasePost.Date.Year.ToString(),
                                PeriodId = period.Id,
                                BranchId = savePurchasePost.BranchId
                            });


                            var totalVatWithoutFree = savePurchasePost.PurchasePostItems.Sum(prod => ((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? ((((prod.Quantity * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (savePurchasePost.IsBeforeTax ? (((prod.Quantity * prod.UnitPrice) * savePurchasePost.TransactionLevelDiscount) / 100) : 0)) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
                            ((((prod.Quantity * prod.UnitPrice) - prod.FixDiscount - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (savePurchasePost.IsBeforeTax && !savePurchasePost.IsFixed && savePurchasePost.IsDiscountOnTransaction ? (((prod.Quantity * prod.UnitPrice) * savePurchasePost.TransactionLevelDiscount) / 100) : (savePurchasePost.IsBeforeTax && savePurchasePost.IsFixed && savePurchasePost.IsDiscountOnTransaction ? (savePurchasePost.TransactionLevelDiscount / sumQuantity * prod.Quantity) : 0))) * prod.TaxRate.Rate) / 100));

                            // Discount
                            decimal transactionLevelTotalDiscount = 0;
                            if (savePurchasePost.IsDiscountOnTransaction)
                            {
                                transactionLevelTotalDiscount = (savePurchasePost.IsBeforeTax && savePurchasePost.IsDiscountOnTransaction) ? (savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? (savePurchasePost.TransactionLevelDiscount * (savePurchasePost.PurchasePostItems.Sum(x => (x.UnitPrice * x.Quantity) - (x.UnitPrice * x.Quantity * x.TaxRate.Rate / (100 + x.TaxRate.Rate)))) / 100) : (savePurchasePost.IsFixed ? savePurchasePost.TransactionLevelDiscount : savePurchasePost.TransactionLevelDiscount * savePurchasePost.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity) / 100) : (savePurchasePost.IsFixed ? savePurchasePost.TransactionLevelDiscount : savePurchasePost.TransactionLevelDiscount * (savePurchasePost.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity) + ((savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? 0 : totalVatWithoutFree)) / 100);

                                if (transactionLevelTotalDiscount != 0)
                                {
                                    //Discount  
                                    transactions.Add(new TransactionLookupModel
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.PurchasePost.Date,
                                        ApprovalDate = request.PurchasePost.ApprovalDate,
                                        ContactId = savePurchasePost.SupplierId,
                                        AccountId = accounts.FirstOrDefault(x => x.Code == "42600001").Id,
                                        Credit = Math.Abs(Math.Round((transactionLevelTotalDiscount), 4)),
                                        Debit = 0,

                                        Description = TransactionType.PurchasePost.ToString(),
                                        DocumentId = savePurchasePost.Id,
                                        TransactionType = TransactionType.PurchasePost,
                                        DocumentNumber = savePurchasePost.RegistrationNo,
                                        Year = savePurchasePost.Date.Year.ToString(),
                                        PeriodId = period.Id,
                                        BranchId = savePurchasePost.BranchId
                                    });
                                }


                            }

                            else
                            {
                                if (savePurchasePost.PurchasePostItems.Sum(x => Math.Round((x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : (savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100) : x.FixDiscount), 2)) != 0)
                                {
                                    //Discount  
                                    transactions.Add(new TransactionLookupModel
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.PurchasePost.Date,
                                        ApprovalDate = request.PurchasePost.ApprovalDate,
                                        ContactId = savePurchasePost.SupplierId,
                                        AccountId = accounts.FirstOrDefault(x => x.Code == "42600001").Id,
                                        Credit = savePurchasePost.PurchasePostItems.Sum(x => Math.Round((x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : (savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100) : x.FixDiscount), 2)),
                                        Debit = 0,

                                        Description = TransactionType.PurchasePost.ToString(),
                                        DocumentId = savePurchasePost.Id,
                                        TransactionType = TransactionType.PurchasePost,
                                        DocumentNumber = savePurchasePost.RegistrationNo,
                                        Year = savePurchasePost.Date.Year.ToString(),
                                        PeriodId = period.Id,
                                        BranchId = savePurchasePost.BranchId
                                    });
                                }

                            }
                            #endregion
                            #region Discount/Adjustment

                            if (savePurchasePost.Discount != 0)
                            {
                                var account = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Other Expense", cancellationToken: cancellationToken);
                                Guid accountId = account?.Id ?? Guid.Empty;
                                if (account == null)
                                {
                                    var lastInventoryCode = await _context.Accounts.AsNoTracking().Include(x => x.CostCenter)
                                           .OrderBy(x => x.CostCenter.Name == "Expense Adjustment").LastOrDefaultAsync(cancellationToken: cancellationToken);

                                    var newAccount = new Account
                                    {
                                        Code = (Convert.ToInt32(lastInventoryCode.Code) + 1).ToString(),
                                        Name = "Other Expense",
                                        Description = "Other Expense",
                                        IsActive = true,
                                        CostCenterId = lastInventoryCode.CostCenterId
                                    };
                                    await _context.Accounts.AddAsync(newAccount, cancellationToken);
                                    accountId = newAccount.Id;
                                }
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = savePurchasePost.Date,
                                    ContactId = savePurchasePost.SupplierId,
                                    AccountId = accountId,
                                    Debit = savePurchasePost.Discount > 0 ? Math.Abs(Math.Round((savePurchasePost.Discount), 4)) : 0,
                                    Credit = savePurchasePost.Discount < 0 ? Math.Abs(Math.Round((savePurchasePost.Discount), 4)) : 0,
                                    Description = "Discount/ Adjustment",
                                    DocumentId = savePurchasePost.Id,
                                    TransactionType = TransactionType.SaleInvoice,
                                    DocumentNumber = savePurchasePost.RegistrationNo,
                                    Year = savePurchasePost.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = savePurchasePost.BranchId
                                });


                            }

                            #endregion
                            decimal total = 0;
                            if (savePurchasePost.IsDiscountOnTransaction)
                            {
                                var saleTotal = savePurchasePost.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity);
                                var disc = (savePurchasePost.IsBeforeTax && savePurchasePost.IsDiscountOnTransaction) ? (savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? (savePurchasePost.TransactionLevelDiscount * (savePurchasePost.PurchasePostItems.Sum(x => (x.UnitPrice * x.Quantity))) / 100) : (savePurchasePost.IsFixed ? savePurchasePost.TransactionLevelDiscount : savePurchasePost.TransactionLevelDiscount * savePurchasePost.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity) / 100) : (savePurchasePost.IsFixed ? savePurchasePost.TransactionLevelDiscount : savePurchasePost.TransactionLevelDiscount * (savePurchasePost.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity) + ((savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? 0 : totalVatWithoutFree)) / 100);

                                total = Math.Round(saleTotal + ((savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? 0 : totalVatWithoutFree) - disc + savePurchasePost.Discount, 4);
                            }
                            else
                            {
                                total = (savePurchasePost.PurchasePostItems.Sum(x => Math.Round(((((((x.UnitPrice * x.Quantity) - (x.Discount != 0 ? (x.UnitPrice * x.Quantity * x.Discount) / 100 : (savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل" ? x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100) : x.FixDiscount))) * ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : (100 + x.TaxRate.Rate))) / ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : 100)))), 2))) + savePurchasePost.Discount;

                            }
                            if (request.PurchasePost.IsCredit)
                            {

                                //account Payable
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.PurchasePost.Date,
                                    ApprovalDate = request.PurchasePost.ApprovalDate,
                                    ContactId = savePurchasePost.SupplierId,
                                    AccountId = savePurchasePost.Supplier.AccountId.Value,
                                    Credit = Math.Abs(Math.Round(total, 4)),
                                    Debit = 0,
                                    Description = TransactionType.PurchasePost.ToString(),
                                    DocumentId = savePurchasePost.Id,
                                    TransactionType = TransactionType.PurchasePost,
                                    DocumentNumber = savePurchasePost.RegistrationNo,
                                    Year = savePurchasePost.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = savePurchasePost.BranchId
                                });
                                //transactions.Add(new TransactionLookupModel
                                //{
                                //    Date = DateTime.UtcNow,
                                //    DocumentDate = request.PurchasePost.Date,
                                //    ApprovalDate = request.PurchasePost.ApprovalDate,
                                //    ContactId = savePurchasePost.SupplierId,
                                //    AccountId = savePurchasePost.Supplier.AccountId.Value,
                                //    Debit = savePurchasePost.PurchasePostItems.Where(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل").Sum(x => Math.Round(x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100), 2)),
                                //    Credit = 0,
                                //    Description = TransactionType.PurchasePost.ToString(),
                                //    DocumentId = savePurchasePost.Id,
                                //    TransactionType = TransactionType.PurchasePost,
                                //    DocumentNumber = savePurchasePost.RegistrationNo,
                                //    Year = savePurchasePost.Date.Year.ToString(),
                                //    PeriodId = period.Id
                                //});

                            }
                            else
                            {
                                var supplier = await _context.Contacts
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Id == savePurchasePost.SupplierId, cancellationToken: cancellationToken);

                                var costCenter = await _context.CostCenters
                                    .AsNoTracking()
                                    .Include(x => x.Accounts)
                                    .FirstOrDefaultAsync(x => x.Code == "200000", cancellationToken: cancellationToken);

                                var cashAccount = costCenter.Accounts.FirstOrDefault(x => x.Name == "Cash Purchase");

                                if (cashAccount == null)
                                {
                                    var newCashAccount = costCenter.Accounts.MaxBy(x => x.Code);
                                    var account = new Account()
                                    {
                                        Code = (Convert.ToInt64(newCashAccount.Code) + 1).ToString(),
                                        Name = "Cash Purchase",
                                        NameArabic = "شراء نقدا",
                                        Description = "Cash Purchase",
                                        IsActive = true,
                                        CostCenterId = costCenter.Id
                                    };
                                    await _context.Accounts.AddAsync(account, cancellationToken);
                                    supplier.SupplierCashAccountId = account.Id;
                                    _context.Contacts.Update(supplier);
                                }
                                else
                                {
                                    if (savePurchasePost.Supplier.SupplierCashAccountId == null || savePurchasePost.Supplier.SupplierCashAccountId == Guid.Empty)
                                    {
                                        supplier.SupplierCashAccountId = cashAccount.Id;
                                        _context.Contacts.Update(supplier);
                                    }
                                }


                                //supplier account Credit
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.PurchasePost.Date,
                                    ApprovalDate = request.PurchasePost.ApprovalDate,
                                    ContactId = savePurchasePost.SupplierId,
                                    AccountId = supplier.SupplierCashAccountId,
                                    Credit = Math.Abs(Math.Round(total, 4)),

                                    Debit = 0,
                                    Description = TransactionType.PurchasePost.ToString(),
                                    DocumentId = savePurchasePost.Id,
                                    TransactionType = TransactionType.PurchasePost,
                                    DocumentNumber = savePurchasePost.RegistrationNo,
                                    Year = savePurchasePost.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = savePurchasePost.BranchId
                                });

                                //supplier account Debit
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.PurchasePost.Date,
                                    ApprovalDate = request.PurchasePost.ApprovalDate,
                                    ContactId = savePurchasePost.SupplierId,
                                    AccountId = supplier.SupplierCashAccountId,
                                    Debit = Math.Abs(Math.Round(total, 4)),
                                    Credit = 0,
                                    Description = TransactionType.PurchasePost.ToString(),
                                    DocumentId = savePurchasePost.Id,
                                    TransactionType = TransactionType.PurchasePost,
                                    DocumentNumber = savePurchasePost.RegistrationNo,
                                    Year = savePurchasePost.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = savePurchasePost.BranchId
                                });

                                ////supplier account Debit Discount

                                //transactions.Add(new TransactionLookupModel
                                //{
                                //    Date = DateTime.UtcNow,
                                //    DocumentDate = request.PurchasePost.Date,
                                //    ApprovalDate = request.PurchasePost.ApprovalDate,
                                //    ContactId = savePurchasePost.SupplierId,
                                //    AccountId = supplier.SupplierCashAccountId,
                                //    Debit = savePurchasePost.PurchasePostItems.Where(x => x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل").Sum(x => Math.Round(x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100), 2)),
                                //    Credit = 0,
                                //    Description = TransactionType.PurchasePost.ToString(),
                                //    DocumentId = savePurchasePost.Id,
                                //    TransactionType = TransactionType.PurchasePost,
                                //    DocumentNumber = savePurchasePost.RegistrationNo,
                                //    Year = savePurchasePost.Date.Year.ToString(),
                                //    PeriodId = period.Id
                                //});



                                //Bank/Cash account
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.PurchasePost.Date,
                                    ApprovalDate = request.PurchasePost.ApprovalDate,
                                    ContactId = savePurchasePost.SupplierId,
                                    AccountId = savePurchasePost.BankCashAccountId.Value,
                                    Credit = Math.Abs(Math.Round(total, 4)),
                                    Debit = 0,
                                    Description = TransactionType.PurchasePost.ToString(),
                                    DocumentId = savePurchasePost.Id,
                                    TransactionType = TransactionType.PurchasePost,
                                    DocumentNumber = savePurchasePost.RegistrationNo,
                                    Year = savePurchasePost.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = savePurchasePost.BranchId
                                });
                            }


                            foreach (var transaction in transactions)
                            {
                                await _mediator.Send(new AddTransactionCommand { Transaction = transaction, Code = _code }, cancellationToken);
                            }
                            await _context.LedgerTransactions.AddRangeAsync(ledgerTransactions, cancellationToken);


                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                BranchId = request.PurchasePost.BranchId,
                                DocumentNo=purchase.RegistrationNo
                            }, cancellationToken);
                            //Save changes to database
                            await _context.SaveChangesAsync(cancellationToken);

                            scope.Complete();
                        }

                       
                        var message = new Message
                        {
                            Id = purchase.Id,
                            IsAddUpdate = "Data has been added successfully"
                        };
                        return message;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(invoiceNumber))
                        {
                            throw new ApplicationException("Registration no is not found");
                        }

                        var itemList = new List<PurchasePostItem>();
                        foreach (var item in request.PurchasePost.PurchasePostItems)
                        {
                            if (item.Serial && !string.IsNullOrEmpty(item.SerialNo))
                            {
                                var qty = request.PurchasePost.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity;

                                for (int i = 0; i < qty; i++)
                                {
                                    string serialNo = item.SerialNo;
                                    string lastFragment = serialNo.Split('-').Last();
                                    string firstFragment = serialNo.Substring(0, serialNo.Length - lastFragment.Length);
                                    Int32 autoNo = Convert.ToInt32(lastFragment) + i;
                                    var format = "";

                                    for (int j = 0; j < lastFragment.Length; j++)
                                    {
                                        format += "0";
                                    }

                                    var prefix = firstFragment;
                                    var newCode = prefix + autoNo.ToString(format);

                                    itemList.Add(new PurchasePostItem
                                    {
                                        UnitName = item.UnitName,
                                        ProductId = item.ProductId,
                                        TaxRateId = item.TaxRateId,
                                        TaxMethod = item.TaxMethod,
                                        Discount = item.Discount,
                                        Description = item.Description,
                                        FixDiscount = Math.Round(item.FixDiscount, 2),
                                        Quantity = 1,
                                        RemainingQuantity = 1,
                                        SerialNo = Convert.ToString(newCode),
                                        ExpiryDate = item.ExpiryDate,
                                        BatchNo = item.BatchNo,
                                        HighQty = 0,
                                        GuaranteeDate = item.GuaranteeDate,
                                        UnitPerPack = item.UnitPerPack,
                                        WarrantyTypeId = item.WarrantyTypeId,
                                        IsService = item.IsService,
                                        UnitPrice = Math.Round(item.UnitPrice, 2),
                                        WareHouseId = request.PurchasePost.WareHouseId,
                                        TotalAmount = item.TotalAmount,
                                        TotalWithOutAmount = item.GrossAmount,
                                        VatAmount = item.VatAmount,
                                        DiscountAmount = item.DiscountAmount,
                                    });
                                }
                            }
                            else
                            {
                                itemList.Add(new PurchasePostItem
                                {
                                    UnitName = item.UnitName,
                                    ProductId = item.ProductId,
                                    TaxRateId = item.TaxRateId,
                                    TaxMethod = item.TaxMethod,
                                    Discount = item.Discount,
                                    Description = item.Description,
                                    FixDiscount = Math.Round(item.FixDiscount, 2),
                                    Quantity = item.ProductId == null ? item.Quantity : request.PurchasePost.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity,
                                    RemainingQuantity = item.ProductId == null ? item.Quantity : request.PurchasePost.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity,
                                    ExpiryDate = item.ExpiryDate,
                                    SerialNo = item.SerialNo,
                                    GuaranteeDate = item.GuaranteeDate,
                                    BatchNo = item.BatchNo,
                                    HighQty = item.HighQty,
                                    UnitPerPack = item.UnitPerPack,
                                    WarrantyTypeId = item.WarrantyTypeId,
                                    IsService = item.IsService,
                                    UnitPrice = Math.Round(item.UnitPrice, 2),
                                    WareHouseId = request.PurchasePost.WareHouseId,
                                    ColorId = item.ColorId,
                                    TotalAmount = item.TotalAmount,
                                    TotalWithOutAmount = item.GrossAmount,
                                    VatAmount = item.VatAmount,
                                    DiscountAmount = item.DiscountAmount,
                                    SaleSizeAssortments = item.SaleSizeAssortment?.Select(size => new SaleSizeAssortment()
                                    {
                                        SizeId = size.SizeId,
                                        Quantity = size.Quantity
                                    }).ToList()
                                });
                            }
                        }



                        var purchasePost = new PurchasePost()
                        {
                            NationalId = request.PurchasePost.NationalId,
                            BillingId = request.PurchasePost.BillingId,
                            ShippingId = request.PurchasePost.ShippingId,
                            DeliveryId = request.PurchasePost.DeliveryId,
                            poNumberAndDate = request.PurchasePost.PoNumberAndDate,
                            GoodsRecieveNumberAndDate = request.PurchasePost.GoodsRecieveNumberAndDate,
                            Date = request.PurchasePost.Date,
                            PurchaseOrderNo = request.PurchasePost.PurchaseOrderNo,
                            RegistrationNo = invoiceNumber,
                            SupplierId = request.PurchasePost.SupplierId,
                            IsPurchaseReturn = false,
                            InvoiceNo = request.PurchasePost.InvoiceNo,
                            InvoiceDate = request.PurchasePost.InvoiceDate,
                            WareHouseId = request.PurchasePost.WareHouseId,
                            InvoiceFixDiscount = request.PurchasePost.InvoiceFixDiscount,
                            PurchaseOrderId = request.PurchasePost.PurchaseOrderId,
                            GoodReceiveNoteId = request.PurchasePost.GoodReceiveNoteId,
                            TaxMethod = request.PurchasePost.TaxMethod,
                            TaxRateId = request.PurchasePost.TaxRateId,
                            Raw = request.PurchasePost.IsRaw,
                            IsPurchasePost = request.PurchasePost.IsPurchasePost,
                            PartiallyInvoices = PartiallyInvoices.UnPaid,
                            PurchasePostItems = itemList,
                            BankCashAccountId = request.PurchasePost.BankCashAccountId,
                            PaymentType = request.PurchasePost.PaymentType,
                            IsCredit = request.PurchasePost.IsCredit,
                            PeriodId = period.Id,
                            TransactionLevelDiscount = request.PurchasePost.TransactionLevelDiscount,
                            IsDiscountOnTransaction = request.PurchasePost.IsDiscountOnTransaction,
                            IsFixed = request.PurchasePost.IsFixed,
                            IsBeforeTax = request.PurchasePost.IsBeforeTax,
                            Discount = request.PurchasePost.Discount,
                            TotalAmount = request.PurchasePost.TotalAmount,
                            TotalWithOutAmount = request.PurchasePost.GrossAmount,
                            VatAmount = request.PurchasePost.VatAmount,
                            DiscountAmount = request.PurchasePost.DiscountAmount,
                            BranchId = request.PurchasePost.BranchId,
                            SupplierQuotationId = request.PurchasePost.SupplierQuotationId,
                            TotalAfterDiscount = request.PurchasePost.TotalAfterDiscount,
                            Note = request.PurchasePost.Note,
                            Reference = request.PurchasePost.Reference,
                    };


                        if ( request.PurchasePost.GoodReceiveNoteId != null)
                        {

                            var goodsReceive = await _context.GoodReceives.FindAsync(request.PurchasePost.GoodReceiveNoteId);


                            {
                                foreach (var item in request.PurchasePost.PurchasePostItems)
                                {

                                    if (item.ProductId == null)
                                    {
                                        var poDetail = goodsReceive.GoodReceiveNoteItems.FirstOrDefault(x => x.Description == item.Description);
                                        if (poDetail != null)
                                        {
                                            poDetail.RemainingQuantity = poDetail.RemainingQuantity - (request.PurchasePost.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity);
                                            _context.GoodReceiveNoteItems.Update(poDetail);

                                        }
                                    }
                                    else
                                    {
                                        var poDetail = goodsReceive.GoodReceiveNoteItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                                        if (poDetail != null)
                                        {
                                            poDetail.RemainingQuantity = poDetail.RemainingQuantity - (request.PurchasePost.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity);
                                            _context.GoodReceiveNoteItems.Update(poDetail);

                                        }
                                    }



                                    
                                }
                            }
                            goodsReceive.IsProcessed = true;
                            if (String.IsNullOrEmpty(goodsReceive.Reason))
                            {
                                goodsReceive.Reason = request.PurchasePost.RegistrationNo + " " + request.PurchasePost.Date.ToString("dd/MM/yyyy");

                            }
                            else

                            {
                                goodsReceive.Reason = goodsReceive.Reason + "     ||     " + request.PurchasePost.RegistrationNo + " " + request.PurchasePost.Date.ToString("dd/MM/yyyy");

                            }

                            var close = goodsReceive.GoodReceiveNoteItems.FirstOrDefault(x => x.GoodReceiveNoteId == request.PurchasePost.GoodReceiveNoteId && x.RemainingQuantity > 0);
                            if (close == null)
                            {
                                goodsReceive.IsClose = true;
                                goodsReceive.PartiallyReceived = Domain.Enum.PartiallyInvoices.Fully;
                            }
                            else

                            {
                                goodsReceive.PartiallyReceived = Domain.Enum.PartiallyInvoices.Partially;
                            }
                        }


                        if (request.PurchasePost.IsPurchasePost)
                        {
                            request.PurchasePost.ApprovalDate = DateTime.UtcNow;
                        }

                        if (!request.PurchasePost.IsCredit && request.PurchasePost.IsPurchasePost)
                        {
                            purchasePost.PartiallyInvoices = PartiallyInvoices.Fully;
                        }

                        await _context.PurchasePosts.AddAsync(purchasePost, cancellationToken);

                        if (request.PurchasePost.AttachmentList is { Count: > 0 })
                        {
                            foreach (var item in request.PurchasePost.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = purchasePost.Id,
                                    Description = item.Description,
                                    FileName = item.FileName,
                                    Path = item.Path
                                };
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);

                            }
                        }

                        //Product Variation
                        if (request.PurchasePost.IsPurchasePost && request.PurchasePost.ColorVariants)
                        {
                            var variationInventoryForReporting = new List<VariationInventoryForReporting>();
                            foreach (var item in request.PurchasePost.PurchasePostItems.Where(x => !x.IsService))
                            {
                                foreach (var size in item.SaleSizeAssortment.Where(size => size.Quantity > 0))
                                {
                                    var variation = await _context.VariationInventories.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.ColorId == item.ColorId && x.SizeId == size.SizeId, cancellationToken: cancellationToken);
                                    if (variation == null)
                                    {
                                        var variationInventory = new VariationInventory()
                                        {
                                            ProductId = item.ProductId.Value,
                                            ColorId = item.ColorId,
                                            SizeId = size.SizeId,
                                            Quantity = size.Quantity,
                                            BranchId = purchasePost.BranchId
                                        };
                                        await _context.VariationInventories.AddAsync(variationInventory, cancellationToken);
                                    }
                                    else
                                    {
                                        variation.Quantity += size.Quantity;
                                        _context.VariationInventories.Update(variation);
                                    }


                                    variationInventoryForReporting.Add(new VariationInventoryForReporting()
                                    {
                                        DocumentId = purchasePost.Id,
                                        TransactionType = 0,
                                        ProductId = item.ProductId ?? Guid.Empty,
                                        ColorId = item.ColorId,
                                        SizeId = size.SizeId,
                                        Quantity = size.Quantity,
                                        BranchId = purchasePost.BranchId
                                    });
                                }
                            }
                            await _context.VariationInventoryForReportings.AddRangeAsync(variationInventoryForReporting, cancellationToken);
                        }

                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        if ( request.PurchasePost.PurchaseOrderId != null)
                        {
                            // Get PO Details
                            var po = await _context.PurchaseOrders
                                .Include(x => x.Supplier)
                                .Include(x => x.PurchaseOrderItems)
                                .Include(x => x.PurchaseOrderPayments)
                                .ThenInclude(x => x.PaymentVoucher)
                                .Include(x => x.PurchaseOrderExpenses)
                                .ThenInclude(x => x.PaymentVoucher)
                                .FirstOrDefaultAsync(x => x.Id == request.PurchasePost.PurchaseOrderId, cancellationToken);

                            if (po == null)
                                throw new NotFoundException("Purchase Order", "Was not found");

                            //Update PO Item Remaining Quantity
                            foreach (var item in request.PurchasePost.PurchasePostItems)
                            {
                                if (item.ProductId == null)
                                {
                                    var poDetail = po.PurchaseOrderItems.FirstOrDefault(x => x.Description == item.Description);
                                    if (poDetail != null)
                                    {
                                        poDetail.RemainingQuantity = poDetail.RemainingQuantity - (request.PurchasePost.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity);
                                        _context.PurchaseOrderItems.Update(poDetail);

                                    }
                                }
                                else
                                {
                                    var poDetail = po.PurchaseOrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                                    if (poDetail != null)
                                    {
                                        poDetail.RemainingQuantity = poDetail.RemainingQuantity - (request.PurchasePost.IsMultiUnit ? Convert.ToDecimal(((item.HighQty == null || item.HighQty == 0) ? 0 : item.HighQty * item.UnitPerPack) + item.Quantity) : item.Quantity);
                                        _context.PurchaseOrderItems.Update(poDetail);

                                    }
                                }

                            }

                            //Close PO
                            if (request.PurchasePost.PartiallyPurchase)
                            {
                                var close = po.PurchaseOrderItems.FirstOrDefault(x => x.PurchaseOrderId == request.PurchasePost.PurchaseOrderId && x.RemainingQuantity > 0);
                                if (close == null)
                                {
                                    po.IsClose = true;
                                    po.PartiallyReceived = PartiallyInvoices.Fully;
                                }
                                else
                                {
                                    po.PartiallyReceived = PartiallyInvoices.Partially;

                                }
                            }
                            else
                            {
                                po.IsClose = true;
                                po.PartiallyReceived = PartiallyInvoices.Fully;

                            }

                            po.IsProcessed = true;
                            if (String.IsNullOrEmpty(po.Reason))
                            {
                                po.Reason = request.PurchasePost.RegistrationNo + " " + request.PurchasePost.Date.ToString("dd/MM/yyyy");

                            }
                            else

                            {
                                po.Reason = po.Reason + "     ||     " + request.PurchasePost.RegistrationNo + " " + request.PurchasePost.Date.ToString("dd/MM/yyyy");

                            }

                            //Add Expense
                            if (request.PurchasePost.PurchasePostExpense != null && request.PurchasePost.InternationalPurchase)
                            {
                                var purchasePostExpenses = new List<PurchasePostExpense>();
                                foreach (var expense in request.PurchasePost.PurchasePostExpense)
                                {
                                    var poExpense = po.PurchaseOrderExpenses.FirstOrDefault(x => x.Id == expense.Id);
                                    poExpense.UsedAmount += expense.Amount;
                                    _context.PurchaseOrderExpenses.Update(poExpense);

                                    purchasePostExpenses.Add(new PurchasePostExpense
                                    {
                                        Date = expense.Date,
                                        PurchasePostId = purchasePost.Id,
                                        VoucherNumber = expense.VoucherNumber,
                                        Narration = expense.Narration,
                                        ChequeNumber = expense.ChequeNumber,
                                        ApprovalStatus = expense.ApprovalStatus,
                                        PaymentVoucherType = expense.PaymentVoucherType,
                                        BankCashAccountId = expense.BankCashAccountId,
                                        ContactAccountId = expense.ContactAccountId,
                                        Amount = expense.Amount,
                                        ApprovedDate = DateTime.UtcNow,
                                        PaymentMethod = expense.PaymentMethod,
                                        PaymentMode = expense.PaymentMode,
                                        TaxMethod = expense.TaxMethod,
                                        TaxRateId = expense.TaxRateId,
                                        VatAccountId = expense.VatAccountId
                                    });
                                }
                                await _context.PurchasePostExpenses.AddRangeAsync(purchasePostExpenses, cancellationToken);
                            }

                            if (po.PurchaseOrderActions != null && request.PurchasePost.InternationalPurchase)
                            {
                                var purchaseInvoiceAction = new List<PurchaseInvoiceAction>();
                                foreach (var action in po.PurchaseOrderActions)
                                {
                                    purchaseInvoiceAction.Add(new PurchaseInvoiceAction
                                    {
                                        PurchaseInvoicePostId = purchasePost.Id,
                                        ProcessId = action.ProcessId,
                                        CurrentDate = DateTime.UtcNow,
                                        Date = action.Date,
                                        Description = action.Description
                                    });
                                }
                                await _context.PurchaseInvoiceActions.AddRangeAsync(purchaseInvoiceAction, cancellationToken);
                            }

                            // Add Attachment
                            if (po.PurchaseAttachments != null && request.PurchasePost.InternationalPurchase)
                            {
                                var purchaseInvoiceAttachments = new List<PurchaseInvoiceAttachment>();
                                foreach (var attachment in po.PurchaseAttachments)
                                {
                                    purchaseInvoiceAttachments.Add(new PurchaseInvoiceAttachment
                                    {
                                        PurchaseInvoicePostId = purchasePost.Id,
                                        Path = attachment.Path,
                                        Date = attachment.Date,
                                        Description = attachment.Description
                                    });
                                }
                                await _context.PurchaseInvoiceAttachments.AddRangeAsync(purchaseInvoiceAttachments, cancellationToken);
                            }

                            // Auto Generate Pay Voucher
                            if (request.PurchasePost.AutoPurchaseVoucher && request.PurchasePost.InternationalPurchase && request.PurchasePost.IsCredit)
                            {
                                if (po.PurchaseOrderPayments.Sum(x => x.Amount) > 0)
                                {
                                    await _mediator.Send(new AddPaymentVoucherCommand
                                    {
                                        IsPurchase = true,
                                        PurchaseInvoice = purchasePost.Id,
                                        ContactAccountId = po.Supplier?.AccountId ?? Guid.Empty,
                                        ContactAdvanceAccountId = po.Supplier?.AdvanceAccountId ?? Guid.Empty,
                                        Payment = request.Payments,
                                        DueAmount = request.PurchasePost.DueAmount,
                                        PurchaseOrderPayments = po.PurchaseOrderPayments,
                                        BranchId = purchasePost.BranchId
                                    }, cancellationToken);
                                }


                                var totalPay = po.PurchaseOrderPayments.Sum(x => x.Amount);
                                var result = request.PurchasePost.DueAmount - (totalPay - request.Payments);

                                if (result <= 0)
                                {
                                    purchasePost.PartiallyInvoices = PartiallyInvoices.Fully;
                                }
                                else if (totalPay - request.Payments > 0)
                                {
                                    purchasePost.PartiallyInvoices = PartiallyInvoices.Partially;
                                }
                            }
                        }


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            BranchId = request.PurchasePost.BranchId,
                            DocumentNo= purchasePost.RegistrationNo
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);


                        if (request.PurchasePost.IsPurchasePost)
                        {
                            var savePurchasePost = await _context.PurchasePosts
                                .AsNoTracking()
                                .Include(x => x.Supplier)
                                .Include(x => x.PurchasePostItems)
                                .ThenInclude(x => x.TaxRate)
                                .Include(x => x.PurchasePostItems)
                                .ThenInclude(x => x.Product)
                                .ThenInclude(x => x.Category)
                                .FirstOrDefaultAsync(x => x.Id == purchasePost.Id, cancellationToken: cancellationToken);

                            if (savePurchasePost == null)
                                throw new NotFoundException("Purchase Invoice", "Not Found");

                            Guid directPurchaseAccountId = default;
                            var costCenterDirectPurchase = await _context.CostCenters
                                .AsNoTracking()
                                .Include(x => x.Accounts)
                                .FirstOrDefaultAsync(x => x.Code == "190000", cancellationToken: cancellationToken);
                            if (costCenterDirectPurchase != null)
                            {
                                directPurchaseAccountId = costCenterDirectPurchase.Accounts.FirstOrDefault(x => x.Code == "19000001").Id;
                            }
                            else
                            {
                                var accountType = await _context.AccountTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Assets", cancellationToken: cancellationToken);

                                if (accountType != null)
                                {
                                    var newCostCenter = new CostCenter()
                                    {
                                        Code = "190000",
                                        Name = "Direct Purchase W/O Inventory",
                                        NameArabic = "Direct Purchase W/O Inventory",
                                        Description = "Direct Purchase W/O Inventory",
                                        IsActive = true,
                                        AccountTypeId = accountType.Id
                                    };
                                    await _context.CostCenters.AddAsync(newCostCenter, cancellationToken);

                                    var newAccount = new Account
                                    {
                                        Code = "19000001",
                                        Name = "Direct Purchase W/O Inventory",
                                        NameArabic = "Direct Purchase W/O Inventory",
                                        Description = "Direct Purchase W/O Inventory",
                                        IsActive = true,
                                        CostCenterId = newCostCenter.Id
                                    };
                                    await _context.Accounts.AddAsync(newAccount, cancellationToken);
                                    directPurchaseAccountId = newAccount.Id;
                                }
                            }


                            var transactions = new Collection<TransactionLookupModel>();
                            var ledgerTransactions = new Collection<LedgerTransaction>();
                            decimal inventoryAccount = 0;
                            decimal cogsAccount = 0;
                            decimal purchaseAccount = 0;
                            var isPos = _context.CompanyPermissions.AsNoTracking().Any(x => x.Value == "PosWithTransactionlevel");

                            var autoNumber = _context.Inventories.OrderByDescending(x => x.AutoNumbering).FirstOrDefault(x => x.AutoNumbering != 0)?.AutoNumbering;
                            foreach (var item in savePurchasePost.PurchasePostItems)
                            {
                                if (item.ProductId != null && !item.IsService)
                                {
                                    if (!request.PurchasePost.ExpenseToGst)
                                    {
                                        var stock = savePurchasePost.BranchId==null ? await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == item.WareHouseId, cancellationToken) : await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == item.WareHouseId && x.BranchId == savePurchasePost.BranchId, cancellationToken);

                                        if (stock == null)
                                        {
                                            var newStock = new Stock()
                                            {
                                                ProductId = item.ProductId.Value,
                                                WareHouseId = request.PurchasePost.WareHouseId,
                                                BranchId = savePurchasePost.BranchId
                                            };


                                            newStock.CurrentQuantity = Convert.ToDecimal(item.Quantity);
                                            newStock.AveragePrice = Math.Round(item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل" ? item.UnitPrice * 100 / (100 + item.TaxRate.Rate) : item.UnitPrice, 2);
                                            newStock.CurrentStockValue = Math.Round(newStock.CurrentQuantity * newStock.AveragePrice, 2);


                                            await _context.Stocks.AddAsync(newStock, cancellationToken);
                                            stock = newStock;
                                        }
                                        else
                                        {
                                            var qty = stock.CurrentQuantity;
                                            stock.CurrentQuantity += Convert.ToDecimal(item.Quantity);
                                            stock.AveragePrice = Math.Round(((stock.AveragePrice * qty) + ((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل" ? item.UnitPrice * 100 / (100 + item.TaxRate.Rate) : item.UnitPrice) * item.Quantity)) / stock.CurrentQuantity, 2);
                                            stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);

                                        }

                                        var inv = new Inventory()
                                        {
                                            Date = savePurchasePost.Date,
                                            DocumentNumber = savePurchasePost.RegistrationNo,
                                            DocumentId = savePurchasePost.Id,
                                            Quantity = Convert.ToDecimal(item.Quantity),
                                            SalePrice = item.Product.SalePrice,
                                            Price = Math.Round(item.UnitPrice, 2),
                                            ProductId = item.ProductId.Value,
                                            WareHouseId = item.WareHouseId,
                                            TransactionType = TransactionType.PurchasePost,
                                            WarrantyDate = item.GuaranteeDate,
                                            Serial = item.SerialNo,
                                            StockId = stock.Id,
                                            //AveragePrice = currentInventory.CurrentQuantity == 0 ? Math.Round(item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل" ? item.UnitPrice * 100 / (100 + item.TaxRate.Rate) : item.UnitPrice, 2) : Math.Round(((currentInventory.AveragePrice * currentInventory.CurrentQuantity) + ((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل" ? item.UnitPrice * 100 / (100 + item.TaxRate.Rate) : item.UnitPrice) * item.Quantity)) / (currentInventory.CurrentQuantity + item.Quantity), 2),
                                            //ExpiryDate = item.ExpiryDate ?? currentInventory.ExpiryDate,
                                            //CurrentQuantity = currentInventory.CurrentQuantity + Convert.ToDecimal(item.Quantity),
                                            //CurrentStockValue = (currentInventory.CurrentQuantity + Convert.ToDecimal(item.Quantity)) * (currentInventory.CurrentQuantity == 0 ? Math.Round(item.UnitPrice, 2) : Math.Round((currentInventory.AveragePrice + item.UnitPrice) / 2, 2)),
                                            IsOpen = true,
                                            IsActive = true,
                                            BatchNumber = item.BatchNo,
                                            AutoNumbering = (int)(autoNumber == null ? 1 : ++autoNumber),
                                            RemainingQuantity = Convert.ToDecimal(item.Quantity),
                                            BranchId = savePurchasePost.BranchId
                                        };
                                        await _context.Inventories.AddAsync(inv, cancellationToken);
                                    }

                                    inventoryAccount += Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))
                                        : (item.Quantity * item.UnitPrice), 4);

                                    if (isPos)
                                    {
                                        //inventory Account
                                        ledgerTransactions.Add(new LedgerTransaction
                                        {
                                            Date = DateTime.UtcNow,
                                            DocumentDate = request.PurchasePost.Date,
                                            ApprovalDate = request.PurchasePost.ApprovalDate,
                                            ContactId = savePurchasePost.SupplierId,
                                            AccountId = item.Product.InventoryAccountId,
                                            Credit = 0,
                                            Debit = Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))
                                        : (item.Quantity * item.UnitPrice), 4),
                                            Description = TransactionType.PurchasePost.ToString(),
                                            DocumentId = savePurchasePost.Id,
                                            TransactionType = TransactionType.PurchasePost,
                                            DocumentNumber = savePurchasePost.RegistrationNo,
                                            Year = savePurchasePost.Date.Year.ToString(),
                                            ProductId = item.ProductId,
                                            PeriodId = period.Id,
                                            BranchId = savePurchasePost.BranchId
                                        });
                                    }
                                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                                    {
                                        SyncRecordModels = _context.SyncLog(),
                                        Code = _code,
                                        BranchId = request.PurchasePost.BranchId,
                                        DocumentNo=purchasePost.RegistrationNo
                                    }, cancellationToken);

                                    //Save changes to database
                                    await _context.SaveChangesAsync(cancellationToken);
                                }
                                else
                                {
                                    //Direct Purchase Account
                                    transactions.Add(new TransactionLookupModel
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.PurchasePost.Date,
                                        ApprovalDate = request.PurchasePost.ApprovalDate,
                                        ContactId = savePurchasePost.SupplierId,
                                        AccountId = directPurchaseAccountId,
                                        Credit = 0,
                                        Debit = Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))
                                    : (item.Quantity * item.UnitPrice), 4),
                                        Description = TransactionType.PurchasePost.ToString(),
                                        DocumentId = savePurchasePost.Id,
                                        TransactionType = TransactionType.PurchasePost,
                                        DocumentNumber = savePurchasePost.RegistrationNo,
                                        Year = savePurchasePost.Date.Year.ToString(),
                                        ProductId = item.ProductId,
                                        PeriodId = period.Id,
                                        BranchId = savePurchasePost.BranchId
                                    });
                                }
                            }



                            var accounts = await _context.Accounts.Where(x =>
                                x.Code == "1300001" ||
                                x.Code == "42600001" ||
                                x.Code == "11100001" ||
                                x.Code == "60500101").ToListAsync(cancellationToken);
                            //Inventpry Sum Account
                            transactions.Add(new TransactionLookupModel
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = request.PurchasePost.Date,
                                ApprovalDate = request.PurchasePost.ApprovalDate,
                                ContactId = savePurchasePost.SupplierId,
                                AccountId = accounts.FirstOrDefault(x => x.Code == "11100001").Id,
                                Credit = 0,
                                Debit = inventoryAccount,
                                Description = TransactionType.PurchasePost.ToString(),
                                DocumentId = savePurchasePost.Id,
                                TransactionType = TransactionType.PurchasePost,
                                DocumentNumber = savePurchasePost.RegistrationNo,
                                Year = savePurchasePost.Date.Year.ToString(),
                                ProductId = Guid.Empty,
                                PeriodId = period.Id,
                                BranchId = savePurchasePost.BranchId
                            });




                            #region Transaction Level Discount

                            var sumQuantity = savePurchasePost.PurchasePostItems.Sum(product => product.Quantity);


                            var totalVat = savePurchasePost.PurchasePostItems.Sum(prod => ((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? ((((prod.Quantity * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - ((savePurchasePost.IsBeforeTax ? (((prod.Quantity * prod.UnitPrice) * savePurchasePost.TransactionLevelDiscount) / 100) : 0))) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
                            ((((prod.Quantity * prod.UnitPrice) - prod.FixDiscount - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - ((savePurchasePost.IsBeforeTax && !savePurchasePost.IsFixed && savePurchasePost.IsDiscountOnTransaction ? (((prod.Quantity * prod.UnitPrice) * savePurchasePost.TransactionLevelDiscount) / 100) : (savePurchasePost.IsBeforeTax && savePurchasePost.IsFixed && savePurchasePost.IsDiscountOnTransaction ? (savePurchasePost.TransactionLevelDiscount / sumQuantity * prod.Quantity) : 0)))) * prod.TaxRate.Rate) / 100));


                            //vat
                            transactions.Add(new TransactionLookupModel
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = request.PurchasePost.Date,
                                ApprovalDate = request.PurchasePost.ApprovalDate,
                                ContactId = savePurchasePost.SupplierId,
                                AccountId = accounts.FirstOrDefault(x => x.Code == "1300001").Id,
                                Credit = 0,
                                Debit = Math.Abs(Math.Round((totalVat), 4)),
                                Description = TransactionType.PurchasePost.ToString(),
                                DocumentId = savePurchasePost.Id,
                                TransactionType = TransactionType.PurchasePost,
                                DocumentNumber = savePurchasePost.RegistrationNo,
                                Year = savePurchasePost.Date.Year.ToString(),
                                PeriodId = period.Id,
                                BranchId = savePurchasePost.BranchId
                            });


                            var totalVatWithoutFree = savePurchasePost.PurchasePostItems.Sum(prod => ((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? ((((prod.Quantity * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (savePurchasePost.IsBeforeTax ? (((prod.Quantity * prod.UnitPrice) * savePurchasePost.TransactionLevelDiscount) / 100) : 0)) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
                            ((((prod.Quantity * prod.UnitPrice) - prod.FixDiscount - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (savePurchasePost.IsBeforeTax && !savePurchasePost.IsFixed && savePurchasePost.IsDiscountOnTransaction ? (((prod.Quantity * prod.UnitPrice) * savePurchasePost.TransactionLevelDiscount) / 100) : (savePurchasePost.IsBeforeTax && savePurchasePost.IsFixed && savePurchasePost.IsDiscountOnTransaction ? (savePurchasePost.TransactionLevelDiscount / sumQuantity * prod.Quantity) : 0))) * prod.TaxRate.Rate) / 100));

                            // Discount
                            decimal transactionLevelTotalDiscount = 0;
                            if (savePurchasePost.IsDiscountOnTransaction)
                            {
                                transactionLevelTotalDiscount = (savePurchasePost.IsBeforeTax && savePurchasePost.IsDiscountOnTransaction) ? (savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? (savePurchasePost.TransactionLevelDiscount * (savePurchasePost.PurchasePostItems.Sum(x => (x.UnitPrice * x.Quantity) - (x.UnitPrice * x.Quantity * x.TaxRate.Rate / (100 + x.TaxRate.Rate)))) / 100) : (savePurchasePost.IsFixed ? savePurchasePost.TransactionLevelDiscount : savePurchasePost.TransactionLevelDiscount * savePurchasePost.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity) / 100) : (savePurchasePost.IsFixed ? savePurchasePost.TransactionLevelDiscount : savePurchasePost.TransactionLevelDiscount * (savePurchasePost.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity) + ((savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? 0 : totalVatWithoutFree)) / 100);

                                //Discount  
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.PurchasePost.Date,
                                    ApprovalDate = request.PurchasePost.ApprovalDate,
                                    ContactId = savePurchasePost.SupplierId,
                                    AccountId = accounts.FirstOrDefault(x => x.Code == "42600001").Id,
                                    Credit = Math.Abs(Math.Round((transactionLevelTotalDiscount), 4)),
                                    Debit = 0,

                                    Description = TransactionType.PurchasePost.ToString(),
                                    DocumentId = savePurchasePost.Id,
                                    TransactionType = TransactionType.PurchasePost,
                                    DocumentNumber = savePurchasePost.RegistrationNo,
                                    Year = savePurchasePost.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = savePurchasePost.BranchId
                                });

                            }

                            else
                            {
                                //Discount  
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.PurchasePost.Date,
                                    ApprovalDate = request.PurchasePost.ApprovalDate,
                                    ContactId = savePurchasePost.SupplierId,
                                    AccountId = accounts.FirstOrDefault(x => x.Code == "42600001").Id,
                                    Credit = savePurchasePost.PurchasePostItems.Sum(x => Math.Round((x.Discount != 0 ? (x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100 : (savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100) : x.FixDiscount), 2)),
                                    Debit = 0,

                                    Description = TransactionType.PurchasePost.ToString(),
                                    DocumentId = savePurchasePost.Id,
                                    TransactionType = TransactionType.PurchasePost,
                                    DocumentNumber = savePurchasePost.RegistrationNo,
                                    Year = savePurchasePost.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = savePurchasePost.BranchId
                                });
                            }
                            #endregion
                            #region Discount/Adjustment

                            if (savePurchasePost.Discount != 0)
                            {
                                var account = await _context.Accounts.AsNoTracking()
                                               .FirstOrDefaultAsync(x => x.Name == "Other Expense", cancellationToken: cancellationToken);
                                Guid accountId = account == null ? Guid.Empty : account.Id;
                                if (account == null)
                                {
                                    var lastInventoryCode = await _context.Accounts.AsNoTracking().Include(x => x.CostCenter)
                                           .OrderBy(x => x.CostCenter.Name == "Expense Adjustment").LastOrDefaultAsync();

                                    var newAccount = new Account
                                    {
                                        Code = (Convert.ToInt32(lastInventoryCode.Code) + 1).ToString(),
                                        Name = "Other Expense",
                                        Description = "Other Expense",
                                        IsActive = true,
                                        CostCenterId = lastInventoryCode.CostCenterId
                                    };
                                    await _context.Accounts.AddAsync(newAccount);
                                    accountId = newAccount.Id;
                                }
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = savePurchasePost.Date,
                                    ContactId = savePurchasePost.SupplierId,
                                    AccountId = accountId,
                                    Debit = savePurchasePost.Discount > 0 ? Math.Abs(Math.Round((savePurchasePost.Discount), 4)) : 0,
                                    Credit = savePurchasePost.Discount < 0 ? Math.Abs(Math.Round((savePurchasePost.Discount), 4)) : 0,
                                    Description = "Discount/ Adjustment",
                                    DocumentId = savePurchasePost.Id,
                                    TransactionType = TransactionType.SaleInvoice,
                                    DocumentNumber = savePurchasePost.RegistrationNo,
                                    Year = savePurchasePost.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = savePurchasePost.BranchId
                                });


                            }

                            #endregion
                            decimal total = 0;
                            if (savePurchasePost.IsDiscountOnTransaction)
                            {
                                var saleTotal = savePurchasePost.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity);
                                var disc = (savePurchasePost.IsBeforeTax && savePurchasePost.IsDiscountOnTransaction) ? (savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? (savePurchasePost.TransactionLevelDiscount * (savePurchasePost.PurchasePostItems.Sum(x => (x.UnitPrice * x.Quantity))) / 100) : (savePurchasePost.IsFixed ? savePurchasePost.TransactionLevelDiscount : savePurchasePost.TransactionLevelDiscount * savePurchasePost.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity) / 100) : (savePurchasePost.IsFixed ? savePurchasePost.TransactionLevelDiscount : savePurchasePost.TransactionLevelDiscount * (savePurchasePost.PurchasePostItems.Sum(x => x.UnitPrice * x.Quantity) + ((savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? 0 : totalVatWithoutFree)) / 100);

                                total = Math.Round(saleTotal + ((savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? 0 : totalVatWithoutFree) - disc + savePurchasePost.Discount, 4);
                            }
                            else
                            {
                                total = (savePurchasePost.PurchasePostItems.Sum(x => Math.Round(((((((x.UnitPrice * x.Quantity) - (x.Discount != 0 ? (x.UnitPrice * x.Quantity * x.Discount) / 100 : (savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل" ? x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100) : x.FixDiscount))) * ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : (100 + x.TaxRate.Rate))) / ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : 100)))), 2))) + savePurchasePost.Discount;

                            }
                            if (request.PurchasePost.IsCredit)
                            {

                                //account Payable
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.PurchasePost.Date,
                                    ApprovalDate = request.PurchasePost.ApprovalDate,
                                    ContactId = savePurchasePost.SupplierId,
                                    AccountId = savePurchasePost.Supplier.AccountId.Value,
                                    Credit = Math.Abs(Math.Round(total, 4)),
                                    Debit = 0,
                                    Description = TransactionType.PurchasePost.ToString(),
                                    DocumentId = savePurchasePost.Id,
                                    TransactionType = TransactionType.PurchasePost,
                                    DocumentNumber = savePurchasePost.RegistrationNo,
                                    Year = savePurchasePost.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = savePurchasePost.BranchId
                                });

                            }
                            else
                            {
                                var supplier = await _context.Contacts
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(x => x.Id == savePurchasePost.SupplierId, cancellationToken: cancellationToken);

                                var costCenter = await _context.CostCenters
                                    .AsNoTracking()
                                    .Include(x => x.Accounts)
                                    .FirstOrDefaultAsync(x => x.Code == "200000", cancellationToken: cancellationToken);

                                var cashAccount = costCenter.Accounts.FirstOrDefault(x => x.Name == "Cash Purchase");

                                if (cashAccount == null)
                                {
                                    var newCashAccount = costCenter.Accounts.OrderBy(x => x.Code).LastOrDefault();
                                    var account = new Account()
                                    {
                                        Code = (Convert.ToInt64(newCashAccount.Code) + 1).ToString(),
                                        Name = "Cash Purchase",
                                        NameArabic = "شراء نقدا",
                                        Description = "Cash Purchase",
                                        IsActive = true,
                                        CostCenterId = costCenter.Id
                                    };
                                    await _context.Accounts.AddAsync(account, cancellationToken);
                                    supplier.SupplierCashAccountId = account.Id;
                                    _context.Contacts.Update(supplier);
                                }
                                else
                                {
                                    if (savePurchasePost.Supplier.SupplierCashAccountId == null || savePurchasePost.Supplier.SupplierCashAccountId == Guid.Empty)
                                    {
                                        supplier.SupplierCashAccountId = cashAccount.Id;
                                        _context.Contacts.Update(supplier);
                                    }
                                }


                                //supplier account Credit
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.PurchasePost.Date,
                                    ApprovalDate = request.PurchasePost.ApprovalDate,
                                    ContactId = savePurchasePost.SupplierId,
                                    AccountId = supplier.SupplierCashAccountId,
                                    Credit = Math.Abs(Math.Round(total, 4)),

                                    Debit = 0,
                                    Description = TransactionType.PurchasePost.ToString(),
                                    DocumentId = savePurchasePost.Id,
                                    TransactionType = TransactionType.PurchasePost,
                                    DocumentNumber = savePurchasePost.RegistrationNo,
                                    Year = savePurchasePost.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = savePurchasePost.BranchId
                                });

                                //supplier account Debit
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.PurchasePost.Date,
                                    ApprovalDate = request.PurchasePost.ApprovalDate,
                                    ContactId = savePurchasePost.SupplierId,
                                    AccountId = supplier.SupplierCashAccountId,
                                    Debit = Math.Abs(Math.Round(total, 4)),
                                    Credit = 0,
                                    Description = TransactionType.PurchasePost.ToString(),
                                    DocumentId = savePurchasePost.Id,
                                    TransactionType = TransactionType.PurchasePost,
                                    DocumentNumber = savePurchasePost.RegistrationNo,
                                    Year = savePurchasePost.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = savePurchasePost.BranchId
                                });

                                //Bank/Cash account
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.PurchasePost.Date,
                                    ApprovalDate = request.PurchasePost.ApprovalDate,
                                    ContactId = savePurchasePost.SupplierId,
                                    AccountId = savePurchasePost.BankCashAccountId.Value,
                                    Credit = Math.Abs(Math.Round(total, 4)),
                                    Debit = 0,
                                    Description = TransactionType.PurchasePost.ToString(),
                                    DocumentId = savePurchasePost.Id,
                                    TransactionType = TransactionType.PurchasePost,
                                    DocumentNumber = savePurchasePost.RegistrationNo,
                                    Year = savePurchasePost.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = savePurchasePost.BranchId
                                });
                            }


                            foreach (var transaction in transactions)
                            {
                                await _mediator.Send(new AddTransactionCommand
                                {
                                    Transaction = transaction, 
                                    Code = _code,
                                    BranchId = savePurchasePost.BranchId
                                }, cancellationToken);
                            }
                            await _context.LedgerTransactions.AddRangeAsync(ledgerTransactions, cancellationToken);

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                BranchId = savePurchasePost.BranchId,
                                DocumentNo=purchasePost.RegistrationNo
                            }, cancellationToken);
                            //Save changes to database
                            await _context.SaveChangesAsync(cancellationToken);

                        }



                      

                        scope.Complete();

                        var message = new Message
                        {
                            Id = purchasePost.Id,
                            IsAddUpdate = "Data has been added successfully"
                        };
                        return message;
                    }
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }

            //public decimal CalculateVat(ICollection<PurchasePostItem> purchasePostItems)
            //{
            //    var Debit = purchasePostItems.Sum(x => Math.Round((((x.UnitPrice * Convert.ToDecimal(x.Quantity)) - ((x.Discount != 0 ? ((x.UnitPrice * Convert.ToDecimal(x.Quantity) * x.Discount) / 100) : (x.FixDiscount * Convert.ToDecimal(x.Quantity))))) * x.TaxRate.Rate) / ((savePurchasePost.TaxMethod == "Inclusive" || savePurchasePost.TaxMethod == "شامل") ? (100 + x.TaxRate.Rate) : 100), 2)),
            //    return 0;
            //}
        }
    }
}
