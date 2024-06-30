using Focus.Business.Interface;
using Focus.Business.Transactions.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Domain.Interface;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.Sales.Commands.AddSaleReturn
{
    public class AddSaleReturnCommand : IRequest<Message>
    {
        public SaleLookupModel Sale { get; set; }
        public string CounterId { get; set; }
        public bool FromTouchInvoice { get; set; }
        public Guid NewSaleId { get; set; }
        public bool IsPayment { get; set; }
        public decimal InvoiceAmount { get; set; }
        public string InvoiceNumber { get; set; }

        public class Handler : IRequestHandler<AddSaleReturnCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private readonly IUserHttpContextProvider _contextProvider;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddSaleReturnCommand> logger, IMediator mediator, IUserHttpContextProvider contextProvider)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
                _contextProvider = contextProvider;
            }

            public async Task<Message> Handle(AddSaleReturnCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var period = await _context.CompanySubmissionPeriods.FirstOrDefaultAsync(x => x.PeriodStart <= request.Sale.Date.Date && x.PeriodEnd >= request.Sale.Date.Date, cancellationToken: cancellationToken);

                    if (period == null)
                        throw new ApplicationException("Financial year not created");

                    var random = new Random();
                    string barcode = string.Empty;
                    for (int i = 0; i < 11; i++)
                        barcode = String.Concat(barcode, random.Next(10).ToString());


                    if (request.Sale.Id != Guid.Empty)
                    {
                        if (request.Sale.InvoiceType != InvoiceType.SaleReturn)
                            throw new ApplicationException("Sale type must be sale return");

                        var saleReturn = await _context.Sales.FirstOrDefaultAsync(x => x.Id == request.Sale.Id, cancellationToken: cancellationToken);
                        if (saleReturn == null)
                            throw new NotFoundException("Sale Return ", request.Sale.Code);

                        var sale = new Sale()
                        {
                            Date = request.Sale.Date,
                            TaxMethod = request.Sale.TaxMethod,
                            TaxRateId = request.Sale.TaxRateId,
                            DocumentType = request.Sale.DocumentType,
                            SaleInvoiceId = request.Sale.SaleInvoiceId,
                            IsSaleReturnPost = request.Sale.IsSaleReturnPost,
                            RegistrationNo = request.Sale.RegistrationNo,
                            WareHouseId = request.Sale.WareHouseId,
                            BarCode = barcode,
                            InvoiceType = request.Sale.InvoiceType,
                            RefrenceNo = request.Sale.RefrenceNo,
                            IsCredit = request.Sale.IsCredit,
                            IsService = request.Sale.IsService,
                            PeriodId = period.Id,
                            TermAndCondition = request.Sale.TermAndCondition,
                            TermAndConditionAr = request.Sale.TermAndConditionAr,
                            WarrantyLogoPath = request.Sale.WarrantyLogoPath,
                            Discount = request.Sale.Discount,
                            IsBeforeTax = request.Sale.IsBeforeTax,
                            TransactionLevelDiscount = request.Sale.TransactionLevelDiscount,
                            IsDiscountOnTransaction = request.Sale.IsDiscountOnTransaction,
                            IsFixed = request.Sale.IsFixed,
                            TotalAmount = request.Sale.TotalAmount,
                            VatAmount = request.Sale.VatAmount,
                            DiscountAmount = request.Sale.DiscountAmount,
                            TotalWithOutAmount = request.Sale.GrossAmount,
                            BranchId = request.Sale.BranchId,
                            CreatedBy = _contextProvider.GetUserId() ?? Guid.Empty,
                            SaleItems = request.FromTouchInvoice ? request.Sale.SaleItems.Where(x => x.Quantity < 0).Select(x =>
                                      new SaleItem()
                                      {
                                          ProductId = x.ProductId,
                                          TaxRateId = x.TaxRateId,
                                          TaxMethod = x.TaxMethod,
                                          Discount = x.Discount,
                                          FixDiscount = x.FixDiscount,
                                          Quantity = x.Quantity * (-1),
                                          UnitPrice = x.UnitPrice,
                                          IsFree = x.IsFree,
                                          ServiceItem = x.ServiceItem,
                                          WareHouseId = request.Sale.WareHouseId,
                                          StyleNumber = x.StyleNumber,
                                          TotalAmount = x.TotalAmount,
                                          VatAmount = x.VatAmount,
                                          Description = x.Description,
                                          DiscountAmount = x.DiscountAmount,
                                          TotalWithOutAmount = x.GrossAmount,
                                      }).ToList() : request.Sale.SaleItems.Select(x =>
                                      new SaleItem()
                                      {
                                          ProductId = x.ProductId,
                                          TaxRateId = x.TaxRateId,
                                          TaxMethod = x.TaxMethod,
                                          Discount = x.Discount,
                                          FixDiscount = x.FixDiscount,
                                          Quantity = x.Quantity,
                                          UnitPrice = x.UnitPrice,
                                          ServiceItem = x.ServiceItem,
                                          IsFree = x.IsFree,
                                          StyleNumber = x.StyleNumber,
                                          WareHouseId = request.Sale.WareHouseId,
                                          TotalAmount = x.TotalAmount,
                                          VatAmount = x.VatAmount,
                                          Description = x.Description,
                                          DiscountAmount = x.DiscountAmount,
                                          TotalWithOutAmount = x.GrossAmount,
                                      }).ToList()
                        };

                        if (request.Sale.IsCredit)
                        {
                            if (request.Sale.CustomerId == Guid.Empty)
                                throw new ApplicationException("Customer is required.");

                            if (request.Sale.DueDate == default)
                                throw new ApplicationException("Due date is required.");

                            sale.CustomerId = request.Sale.CustomerId;
                            sale.DueDate = request.Sale.DueDate;
                        }
                        else
                        {
                            if (request.Sale.CustomerId != Guid.Empty && request.Sale.CustomerId != null)
                            {
                                sale.CustomerId = request.Sale.CustomerId;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(request.Sale.CashCustomer))
                                    throw new ApplicationException("Cash Customer is required.");

                                var cashCustomerExist = await _context.CashCustomer.AsNoTracking().FirstOrDefaultAsync(x =>
                                    x.Id == Guid.Parse(request.Sale.CashCustomerId), cancellationToken: cancellationToken);
                                if (cashCustomerExist == null)
                                {
                                    sale.CashCustomer = new CashCustomer
                                    {
                                        Name = request.Sale.CashCustomer,
                                        Mobile = request.Sale.Mobile,
                                        Code = request.Sale.Code,
                                        Email = request.Sale.Email,
                                        CustomerId = request.Sale.CashCustomerId
                                    };
                                }
                                else
                                {
                                    sale.CashCustomerId = cashCustomerExist.Id;
                                }
                            }
                        }
                        await _context.Sales.AddAsync(sale, cancellationToken);



                        if (request.IsPayment)
                        {
                            var paymentList = new List<SalePayment>
                            {
                                new SalePayment()
                                {

                                    Received = request.Sale.ReturnInvoiceAmount,
                                    //PaymentOptionId = request.Sale.SalePayment.PaymentOptionId,
                                    Name = "Return Invoice(" + sale.RegistrationNo + ") إعادة الفاتورة",
                                    SaleId = request.NewSaleId,
                                },
                                new SalePayment()
                                {

                                    Received = request.InvoiceAmount,
                                    //PaymentOptionId = request.Sale.SalePayment.PaymentOptionId,
                                    Name = "New Invoice(" + request.InvoiceNumber + ") فاتورة جديدة",
                                    SaleId = sale.Id,
                                }
                            };

                            await _context.SalePayments.AddRangeAsync(paymentList, cancellationToken);
                        }



                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = sale.BranchId,
                            Code = _code,
                            DocumentNo = sale.RegistrationNo
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        if (request.Sale.IsSaleReturnPost)
                        {
                            await AddTransaction(sale.Id, request.CounterId, request.Sale.Date, request.Sale.DayStart,
                                request.InvoiceAmount, request.FromTouchInvoice, request.Sale, request.Sale.IsFifo);
                            var isClose = await _context.SaleItems.AnyAsync(x => x.SaleId == request.Sale.SaleInvoiceId && x.RemainingQuantity > 0, cancellationToken: cancellationToken);
                            if (!isClose)
                            {
                                var si = await _context.Sales.FirstOrDefaultAsync(x => x.Id == request.Sale.SaleInvoiceId, cancellationToken: cancellationToken);
                                if (si != null)
                                {
                                    si.IsSaleReturn = true;
                                }
                            }
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = sale.BranchId,
                            Code = _code,
                            DocumentNo = sale.RegistrationNo
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        scope.Complete();

                        // Return Product id after successfully updating data
                        var message = new Message
                        {
                            Id = sale.Id,
                            IsAddUpdate = "Data has been updated successfully",
                        };
                        return message;
                    }
                    else
                    {
                        if (request.Sale.InvoiceType != InvoiceType.SaleReturn)
                            throw new ApplicationException("Sale type must be sale return");

                        var sale = new Sale()
                        {
                            Date = request.Sale.Date,
                            TaxMethod = request.Sale.TaxMethod,
                            TaxRateId = request.Sale.TaxRateId,
                            SaleInvoiceId = request.Sale.SaleInvoiceId,
                            DocumentType = request.Sale.DocumentType,
                            BarCode = barcode,
                            IsSaleReturnPost = request.Sale.IsSaleReturnPost,
                            RegistrationNo = request.Sale.RegistrationNo,
                            WareHouseId = request.Sale.WareHouseId,
                            InvoiceType = request.Sale.InvoiceType,
                            RefrenceNo = request.Sale.RefrenceNo,
                            IsCredit = request.Sale.IsCredit,

                            IsService = request.Sale.IsService,
                            PeriodId = period.Id,
                            TermAndCondition = request.Sale.TermAndCondition,
                            TermAndConditionAr = request.Sale.TermAndConditionAr,
                            WarrantyLogoPath = request.Sale.WarrantyLogoPath,
                            Discount = request.Sale.Discount,
                            IsBeforeTax = request.Sale.IsBeforeTax,
                            TransactionLevelDiscount = request.Sale.TransactionLevelDiscount,
                            IsDiscountOnTransaction = request.Sale.IsDiscountOnTransaction,
                            IsFixed = request.Sale.IsFixed,
                            TotalAmount = request.Sale.TotalAmount,
                            VatAmount = request.Sale.VatAmount,
                            DiscountAmount = request.Sale.DiscountAmount,
                            TotalWithOutAmount = request.Sale.GrossAmount,
                            BranchId = request.Sale.BranchId,
                            CreatedBy = _contextProvider.GetUserId() ?? Guid.Empty,
                            SaleItems = request.Sale.SaleItems.Select(x =>
                                new SaleItem
                                {
                                    ProductId = x.ProductId,
                                    TaxMethod = x.TaxMethod,
                                    TaxRateId = x.TaxRateId,
                                    Discount = x.Discount,
                                    FixDiscount = x.FixDiscount,
                                    Quantity = x.Quantity,
                                    UnitPrice = x.UnitPrice,
                                    Serial = x.Serial,
                                    GuaranteeDate = x.GuaranteeDate,
                                    WareHouseId = request.Sale.WareHouseId,
                                    BatchExpiry = x.BatchExpiry,
                                    BatchNo = x.BatchNo,
                                    ServiceItem = x.ServiceItem,
                                    IsFree = x.IsFree,
                                    StyleNumber = x.StyleNumber,
                                    Description = x.Description,
                                    TotalAmount = x.TotalAmount,
                                    VatAmount = x.VatAmount,
                                    DiscountAmount = x.DiscountAmount,
                                    TotalWithOutAmount = x.GrossAmount
                                }).ToList()
                        };

                        if (request.Sale.IsCredit)
                        {
                            if (request.Sale.CustomerId == Guid.Empty)
                                throw new ApplicationException("Customer is required.");

                            if (request.Sale.DueDate == default)
                                throw new ApplicationException("Due date is required.");

                            sale.CustomerId = request.Sale.CustomerId;
                            sale.DueDate = request.Sale.DueDate;
                        }
                        else
                        {
                            if (request.Sale.CustomerId != Guid.Empty && request.Sale.CustomerId != null)
                            {
                                sale.CustomerId = request.Sale.CustomerId;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(request.Sale.CashCustomer))
                                    throw new ApplicationException("Cash Customer is required.");

                                var cashCustomerExist = await _context.CashCustomer.AsNoTracking().FirstOrDefaultAsync(x =>
                                    x.Id == Guid.Parse(request.Sale.CashCustomerId), cancellationToken: cancellationToken);
                                if (cashCustomerExist == null)
                                {
                                    sale.CashCustomer = new CashCustomer
                                    {
                                        Name = request.Sale.CashCustomer,
                                        Mobile = request.Sale.Mobile,
                                        Code = request.Sale.Code,
                                        Email = request.Sale.Email,
                                        CustomerId = request.Sale.CashCustomerId
                                    };
                                }
                                else
                                {
                                    sale.CashCustomerId = cashCustomerExist.Id;
                                }
                            }
                        }

                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        await _context.Sales.AddAsync(sale, cancellationToken);

                        if (request.Sale.AttachmentList is { Count: > 0 })
                        {
                            foreach (var item in request.Sale.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = sale.Id,
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
                            BranchId = sale.BranchId,
                            Code = _code,
                            DocumentNo = sale.RegistrationNo
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        if (request.Sale.IsSaleReturnPost)
                        {
                            //Add Transaction according to concerns accounts
                            await AddTransaction(sale.Id, request.CounterId, request.Sale.Date, request.Sale.DayStart,
                                request.InvoiceAmount, request.FromTouchInvoice, request.Sale, request.Sale.IsFifo);

                            //Sale Invoice Close Sale Return
                            var isClose = await _context.SaleItems.AnyAsync(x => x.SaleId == request.Sale.SaleInvoiceId && x.RemainingQuantity > 0, cancellationToken: cancellationToken);
                            if (!isClose)
                            {
                                var si = await _context.Sales.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Sale.SaleInvoiceId, cancellationToken: cancellationToken);
                                if (si != null)
                                {
                                    si.IsSaleReturn = true;
                                    _context.Sales.Update(si);
                                }
                            }
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = sale.BranchId,
                            Code = _code,
                            DocumentNo = sale.RegistrationNo
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        scope.Complete();

                        // Return Product id after successfully updating data
                        var message = new Message
                        {
                            Id = sale.Id,
                            IsAddUpdate = "Data has been updated successfully"
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

            private async Task AddTransaction(Guid id, string counterId, DateTime date, bool dayStart, decimal invoiceAmount, bool isTouchInvoice, SaleLookupModel request, bool isFifo)
            {
                var period = await _context.CompanySubmissionPeriods
                    .FirstOrDefaultAsync(x => x.PeriodStart <= date.Date.Date && x.PeriodEnd >= date.Date.Date);

                var sale = await _context.Sales
                      .AsNoTracking()
                      .Include(x => x.SalePayments)
                      .Include(x => x.Customer)
                      .Include(x => x.SaleItems)
                      .ThenInclude(x => x.TaxRate)
                      .Include(x => x.SaleItems)
                      .ThenInclude(x => x.Product)
                      .ThenInclude(x => x.Category)
                      .FirstOrDefaultAsync(x => x.Id == id && x.InvoiceType == InvoiceType.SaleReturn);

                //var taxMethod = _context.CompanyAccountSetups.AsNoTracking().FirstOrDefault()?.TaxMethod;
                var saleInvoice = _context.SaleItems
                    .AsNoTracking()
                    .Where(x => x.SaleId == sale.SaleInvoiceId)
                    .AsQueryable();

                var transactions = new List<TransactionLookupModel>();
                var ledgerTransactions = new List<LedgerTransaction>();
                var accounts = await _context.Accounts.Where(x =>
                    x.Name == "VAT on SaleReturn" ||
                    x.Code == "42600001" ||
                    x.Code == "10100001" ||
                    x.Code == "10500001" ||
                    x.Code == "10100101" ||
                    x.Code == "60505001" ||
                    x.Code == "42000001" ||
                    x.Code == "60000101" ||
                    x.Code == "11100001" ||
                    x.Code == "42000001"
                ).ToListAsync();
                var isPos = _context.CompanyPermissions.AsNoTracking().Any(x => x.Value == "PosWithTransactionlevel");

                decimal inventoryAccount = 0;
                decimal cogsAccount = 0;
                decimal saleAccount = 0;
                Guid vatAccountId;
                var vatAccount = accounts.FirstOrDefault(x => x.Name == "VAT on SaleReturn");
                if (vatAccount == null)
                {
                    var vatCostCenter = await _context.CostCenters.AsNoTracking().Include(x => x.Accounts).FirstOrDefaultAsync(x => x.Code == "250000");
                    var code = vatCostCenter.Accounts.OrderBy(x => x.Code).LastOrDefault()?.Code;
                    var account = new Account()
                    {
                        Code = (Convert.ToInt64(code) + 1).ToString(),
                        Name = "VAT on SaleReturn",
                        NameArabic = "ضريبة القيمة المضافة على البيع",
                        Description = "VAT on SaleReturn",
                        IsActive = true,
                        CostCenterId = vatCostCenter.Id
                    };
                    await _context.Accounts.AddAsync(account);
                    vatAccountId = account.Id;
                }
                else
                {
                    vatAccountId = vatAccount.Id;
                }
                var inventory = _context.Inventories;
                // var totalRemainingQuantity = saleInvoice.Sum(x=>x.RemainingQuantity);
                foreach (var item in sale.SaleItems)
                {
                    //update remaining quantity
                    if (item.ProductId != null)
                    {

                        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == item.WareHouseId);

                        if (stock == null)
                        {
                            var newStock = new Stock()
                            {
                                ProductId = item.ProductId.Value,
                                WareHouseId = sale.WareHouseId,
                                CurrentQuantity = item.Quantity,
                                BranchId = sale.BranchId,
                            };
                            await _context.Stocks.AddAsync(newStock);
                            stock = newStock;
                        }
                        else
                        {
                            stock.CurrentQuantity += item.Quantity;
                            stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);
                        }

                        //var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                        //{
                        //    ProductId = item.ProductId.Value,
                        //    StockId = stock.Id,
                        //    WareHouseId = item.WareHouseId
                        //});
                        //var stockValue = currentInventory.CurrentQuantity * currentInventory.AveragePrice;

                        if (!item.ServiceItem)
                        {
                            var saleInvoiceItem = saleInvoice.FirstOrDefault(x => x.ProductId == item.ProductId && x.BatchNo == item.BatchNo && x.SaleId == sale.SaleInvoiceId);
                            if (saleInvoiceItem != null)
                            {
                                //totalRemainingQuantity =
                                //    totalRemainingQuantity -  item.Quantity;
                                //Convert.ToInt32(saleInvoiceItem.RemainingQuantity - item.Quantity);
                                saleInvoiceItem.RemainingQuantity = Convert.ToInt32(saleInvoiceItem.RemainingQuantity - item.Quantity);
                                _context.SaleItems.Update(saleInvoiceItem);
                            }

                            if (request.IsSerial)
                            {
                                var serialArray = item.Serial.Split(',');
                                for (int i = 0; i < item.Quantity; i++)
                                {
                                    //var averagePrice = (stockValue + returnInvoiceValue) / (Math.Abs(currentInventory.CurrentQuantity) + 1);
                                    var inv = new Inventory()
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentId = sale.Id,
                                        DocumentNumber = sale.RegistrationNo,
                                        Quantity = 1,
                                        Price = Math.Round(item.UnitPrice, 2),
                                        ProductId = item.ProductId.Value,
                                        Serial = serialArray[i],
                                        StockId = stock.Id,
                                        WareHouseId = stock.WareHouseId,
                                        TransactionType = TransactionType.SaleReturn,
                                        //AveragePrice = averagePrice,
                                        //ExpiryDate = currentInventory.ExpiryDate,
                                        //CurrentQuantity = currentInventory.CurrentQuantity + 1,
                                        BranchId = sale.BranchId,
                                    };
                                    await _context.Inventories.AddAsync(inv);

                                    inventoryAccount += Math.Abs(Math.Round(stock.AveragePrice * item.Quantity, 4));
                                    cogsAccount += Math.Abs(Math.Round(stock.AveragePrice * item.Quantity, 4));
                                    //inventory Account
                                    if (isPos)
                                    {
                                        ledgerTransactions.Add(new LedgerTransaction
                                        {
                                            Date = DateTime.UtcNow,
                                            ContactId = sale.CustomerId,
                                            AccountId = item.Product.InventoryAccountId,
                                            Credit = 0,
                                            Debit = Math.Abs(Math.Round(stock.AveragePrice * item.Quantity, 4)),
                                            Description = TransactionType.SaleReturn.ToString(),
                                            DocumentId = sale.Id,
                                            TransactionType = TransactionType.SaleReturn,
                                            DocumentNumber = sale.RegistrationNo,
                                            Year = sale.Date.Year.ToString(),
                                            ProductId = item.ProductId,
                                            PeriodId = period.Id,
                                            BranchId = sale.BranchId,
                                        });
                                        //cost of good sale Account
                                        ledgerTransactions.Add(new LedgerTransaction
                                        {
                                            Date = DateTime.UtcNow,
                                            ContactId = sale.CustomerId,
                                            AccountId = item.Product.CogsAccountId,
                                            Debit = 0,
                                            Credit = Math.Abs(Math.Round(stock.AveragePrice * item.Quantity, 4)),
                                            Description = TransactionType.SaleReturn.ToString(),
                                            DocumentId = sale.Id,
                                            TransactionType = TransactionType.SaleReturn,
                                            DocumentNumber = sale.RegistrationNo,
                                            Year = sale.Date.Year.ToString(),
                                            ProductId = item.ProductId,
                                            PeriodId = period.Id,
                                            BranchId = sale.BranchId,
                                        });
                                    }
                                }
                            }
                            else
                            {
                                //var returnInvoiceValue = item.Quantity * item.UnitPrice;
                                //var averagePrice = (stockValue + returnInvoiceValue) / (Math.Abs(currentInventory.CurrentQuantity) + item.Quantity);

                                // Update inventory of Last Stock
                                if (isFifo)
                                {
                                    var batchInventory = inventory.Where(x => (x.DocumentId == sale.SaleInvoiceId || x.DocumentId == request.SaleOrderId) && x.ProductId == item.ProductId && x.BatchNumber == item.BatchNo).ToList();

                                    if (batchInventory.Count > 0)
                                    {
                                        // decimal remainingQuantity = item.Quantity;
                                        for (var i = 0; i < batchInventory.Count(); i++)
                                        {
                                            var batchData = inventory.FirstOrDefault(x => x.BatchNumber == batchInventory[i].BatchNumber && x.AutoNumbering == batchInventory[i].AutoNumbering && (x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.PurchasePost));
                                            batchData.IsOpen = true;
                                            batchData.IsActive = true;
                                            batchData.RemainingQuantity += item.Quantity;
                                        }
                                    }
                                }

                                var inv = new Inventory()
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentId = sale.Id,
                                    DocumentNumber = sale.RegistrationNo,
                                    Quantity = item.Quantity,
                                    Price = Math.Round(item.UnitPrice, 2),
                                    ProductId = item.ProductId.Value,
                                    Serial = item.Serial,
                                    StockId = stock.Id,
                                    WareHouseId = stock.WareHouseId,
                                    TransactionType = TransactionType.SaleReturn,
                                    //AveragePrice = currentInventory.AveragePrice,
                                    //ExpiryDate = currentInventory.ExpiryDate,
                                    //CurrentQuantity = currentInventory.CurrentQuantity + item.Quantity,
                                    BranchId = sale.BranchId,
                                };

                                await _context.Inventories.AddAsync(inv);
                                inventoryAccount += stock.AveragePrice * item.Quantity;
                                cogsAccount += Math.Abs(Math.Round(stock.AveragePrice * item.Quantity, 4));
                                if (isPos)
                                {

                                    //inventory Account
                                    ledgerTransactions.Add(new LedgerTransaction
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = sale.Date,
                                        ApprovalDate = request.ApprovalDate,
                                        ContactId = sale.CustomerId,
                                        AccountId = item.Product.Category.InventoryAccountId,
                                        Credit = 0,
                                        Debit = stock.AveragePrice * item.Quantity,
                                        Description = TransactionType.SaleReturn.ToString(),
                                        DocumentId = sale.Id,
                                        TransactionType = TransactionType.SaleReturn,
                                        DocumentNumber = sale.RegistrationNo,
                                        Year = sale.Date.Year.ToString(),
                                        ProductId = item.ProductId,
                                        PeriodId = period.Id,
                                        BranchId = sale.BranchId,
                                    });

                                    //cost of good sale Account
                                    ledgerTransactions.Add(new LedgerTransaction
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = sale.Date,
                                        ApprovalDate = request.ApprovalDate,
                                        ContactId = sale.CustomerId,
                                        AccountId = item.Product.Category.COGSAccountId,
                                        Debit = 0,
                                        Credit = Math.Abs(Math.Round(stock.AveragePrice * item.Quantity, 4)),
                                        Description = TransactionType.SaleReturn.ToString(),
                                        DocumentId = sale.Id,
                                        TransactionType = TransactionType.SaleReturn,
                                        DocumentNumber = sale.RegistrationNo,
                                        Year = sale.Date.Year.ToString(),
                                        ProductId = item.ProductId,
                                        PeriodId = period.Id,
                                        BranchId = sale.BranchId,
                                    });
                                }


                            }

                        }




                    }
                    if (!(item.ServiceItem && item.IsFree))
                    {
                        saleAccount += Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))
                            : (item.Quantity * item.UnitPrice), 4);

                        if (isPos)
                        {
                            //sale Account
                            ledgerTransactions.Add(new LedgerTransaction
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = sale.Date,
                                ApprovalDate = request.ApprovalDate,
                                ContactId = sale.CustomerId,
                                AccountId = item.ProductId == null ? accounts.FirstOrDefault(x => x.Code == "42000001").Id : item.Product.Category.SaleAccountId,
                                Debit = Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100))
                                   : (item.Quantity * item.UnitPrice), 4),
                                Credit = 0,

                                Description = TransactionType.SaleInvoice.ToString(),
                                DocumentId = sale.Id,
                                TransactionType = TransactionType.SaleInvoice,
                                DocumentNumber = sale.RegistrationNo,
                                Year = sale.Date.Year.ToString(),
                                ProductId = item.ProductId,
                                PeriodId = period.Id,
                                BranchId = sale.BranchId,
                            });
                        }



                        ////vat
                        //transactions.Add(new TransactionLookupModel
                        //{
                        //    Date = DateTime.UtcNow,
                        //    DocumentDate = sale.Date,
                        //    ApprovalDate = request.ApprovalDate,
                        //    ContactId = sale.CustomerId,
                        //    AccountId = vatAccountId,
                        //    Credit = 0,
                        //    Debit = Math.Abs(Math.Round((((item.UnitPrice * (item.Quantity)) - ((item.Discount != 0 ? (item.UnitPrice * (item.Quantity) * item.Discount) / 100 : item.FixDiscount * item.Quantity))) * item.TaxRate.Rate) / ((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (item.TaxRate.Rate + 100) : 100), 2)),
                        //    Description = TransactionType.SaleReturn.ToString(),
                        //    DocumentId = sale.Id,
                        //    TransactionType = TransactionType.SaleReturn,
                        //    DocumentNumber = sale.RegistrationNo,
                        //    Year = sale.Date.Year.ToString(),
                        //    PeriodId = period.Id
                        //});
                        //await _context.SaveChangesAsync(CancellationToken.None);
                    }
                }
                //inventory Account
                transactions.Add(new TransactionLookupModel
                {
                    Date = DateTime.UtcNow,
                    ContactId = sale.CustomerId,
                    AccountId = accounts.FirstOrDefault(x => x.Code == "11100001").Id,
                    Credit = 0,
                    Debit = inventoryAccount,
                    Description = TransactionType.SaleReturn.ToString(),
                    DocumentId = sale.Id,
                    TransactionType = TransactionType.SaleReturn,
                    DocumentNumber = sale.RegistrationNo,
                    Year = sale.Date.Year.ToString(),
                    ProductId = null,
                    PeriodId = period.Id,
                    BranchId = sale.BranchId,
                });
                //cost of good sale Account
                transactions.Add(new TransactionLookupModel
                {
                    Date = DateTime.UtcNow,
                    ContactId = sale.CustomerId,
                    AccountId = accounts.FirstOrDefault(x => x.Code == "60000101").Id,
                    Debit = 0,
                    Credit = cogsAccount,
                    Description = TransactionType.SaleReturn.ToString(),
                    DocumentId = sale.Id,
                    TransactionType = TransactionType.SaleReturn,
                    DocumentNumber = sale.RegistrationNo,
                    Year = sale.Date.Year.ToString(),
                    ProductId = null,
                    PeriodId = period.Id,
                    BranchId = sale.BranchId,
                });
                //sale Account
                transactions.Add(new TransactionLookupModel
                {
                    Date = DateTime.UtcNow,
                    DocumentDate = sale.Date,
                    ApprovalDate = request.ApprovalDate,
                    ContactId = sale.CustomerId,
                    AccountId = accounts.FirstOrDefault(x => x.Code == "42000001").Id,
                    Debit = saleAccount,
                    Credit = 0,

                    Description = TransactionType.SaleInvoice.ToString(),
                    DocumentId = sale.Id,
                    TransactionType = TransactionType.SaleInvoice,
                    DocumentNumber = sale.RegistrationNo,
                    Year = sale.Date.Year.ToString(),
                    ProductId = null,
                    PeriodId = period.Id,
                    BranchId = sale.BranchId,
                });

                var terminalId = Guid.Parse(counterId);

                var terminalAccount = await _context.Terminals.FirstOrDefaultAsync(x => x.Id == terminalId);







                #region Transaction Level Discount

                var sumQuantity = sale.SaleItems.Sum(product => product.IsFree ? 0 : product.Quantity);


                var totalVat = sale.SaleItems.Sum(prod => ((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? ((((prod.Quantity * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (prod.IsFree ? 0 : (sale.IsBeforeTax ? (((prod.Quantity * prod.UnitPrice) * sale.TransactionLevelDiscount) / 100) : 0))) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
                ((((prod.Quantity * prod.UnitPrice) - prod.FixDiscount - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (prod.IsFree ? 0 : (sale.IsBeforeTax && !sale.IsFixed && sale.IsDiscountOnTransaction ? (((prod.Quantity * prod.UnitPrice) * sale.TransactionLevelDiscount) / 100) : (sale.IsBeforeTax && sale.IsFixed && sale.IsDiscountOnTransaction ? (sale.TransactionLevelDiscount / sumQuantity * prod.Quantity) : 0)))) * prod.TaxRate.Rate) / 100));

                //vat
                transactions.Add(new TransactionLookupModel
                {
                    Date = DateTime.UtcNow,
                    DocumentDate = sale.Date,
                    ApprovalDate = request.ApprovalDate,
                    ContactId = sale.CustomerId,
                    AccountId = vatAccountId,
                    Debit = Math.Abs(Math.Round((totalVat), 4)),
                    Credit = 0,
                    Description = TransactionType.SaleInvoice.ToString(),
                    DocumentId = sale.Id,
                    TransactionType = TransactionType.SaleInvoice,
                    DocumentNumber = sale.RegistrationNo,
                    Year = sale.Date.Year.ToString(),
                    PeriodId = period.Id,
                    BranchId = sale.BranchId,
                });

                var totalVatWithoutFree = sale.SaleItems.Sum(prod => prod.IsFree ? 0 : ((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? ((((prod.Quantity * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (sale.IsBeforeTax ? (((prod.Quantity * prod.UnitPrice) * sale.TransactionLevelDiscount) / 100) : 0)) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
                ((((prod.Quantity * prod.UnitPrice) - prod.FixDiscount - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (sale.IsBeforeTax && !sale.IsFixed && sale.IsDiscountOnTransaction ? (((prod.Quantity * prod.UnitPrice) * sale.TransactionLevelDiscount) / 100) : (sale.IsBeforeTax && sale.IsFixed && sale.IsDiscountOnTransaction ? (sale.TransactionLevelDiscount / sumQuantity * prod.Quantity) : 0))) * prod.TaxRate.Rate) / 100));

                // Discount
                decimal transactionLevelTotalDiscount = 0;
                if (sale.IsDiscountOnTransaction)
                {
                    transactionLevelTotalDiscount = (sale.IsBeforeTax && sale.IsDiscountOnTransaction) ? (sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? (sale.TransactionLevelDiscount * (sale.SaleItems.Sum(x => x.IsFree ? 0 : (x.UnitPrice * x.Quantity) - (x.UnitPrice * x.Quantity * x.TaxRate.Rate / (100 + x.TaxRate.Rate)))) / 100) : (sale.IsFixed ? sale.TransactionLevelDiscount : sale.TransactionLevelDiscount * sale.SaleItems.Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) / 100) : (sale.IsFixed ? sale.TransactionLevelDiscount : sale.TransactionLevelDiscount * (sale.SaleItems.Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) + ((sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? 0 : totalVatWithoutFree)) / 100);
                    if (transactionLevelTotalDiscount != 0)
                    {
                        transactions.Add(new TransactionLookupModel
                        {
                            Date = DateTime.UtcNow,
                            DocumentDate = sale.Date,
                            ApprovalDate = request.ApprovalDate,
                            ContactId = sale.CustomerId,
                            AccountId = accounts.FirstOrDefault(x => x.Code == "42600001").Id,
                            Credit = Math.Abs(Math.Round((transactionLevelTotalDiscount), 4)),
                            Debit = 0,
                            Description = TransactionType.SaleReturn.ToString(),
                            DocumentId = sale.Id,
                            TransactionType = TransactionType.SaleInvoice,
                            DocumentNumber = sale.RegistrationNo,
                            Year = sale.Date.Year.ToString(),
                            PeriodId = period.Id,
                            BranchId = sale.BranchId,
                        });
                    }

                }
                else
                {
                    //Discount  
                    if (sale.SaleItems.Sum(item => Math.Abs(Math.Round((item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : (sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100) : item.FixDiscount), 4))) != 0)
                    {
                        transactions.Add(new TransactionLookupModel
                        {
                            Date = DateTime.UtcNow,
                            DocumentDate = sale.Date,
                            ApprovalDate = request.ApprovalDate,
                            ContactId = sale.CustomerId,
                            AccountId = accounts.FirstOrDefault(x => x.Code == "42600001")?.Id ?? Guid.Empty,
                            Credit = sale.SaleItems.Sum(item => Math.Abs(Math.Round((item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : (sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? item.FixDiscount + (item.FixDiscount * item.TaxRate.Rate / 100) : item.FixDiscount), 4))),
                            Debit = 0,
                            Description = TransactionType.SaleReturn.ToString(),
                            DocumentId = sale.Id,
                            TransactionType = TransactionType.SaleReturn,
                            DocumentNumber = sale.RegistrationNo,
                            Year = sale.Date.Year.ToString(),
                            PeriodId = period.Id,
                            BranchId = sale.BranchId,
                        });
                    }

                }
                #endregion


                // Discount on Over all Sale
                if (sale.Discount != 0)
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
                        ContactId = sale.CustomerId,
                        AccountId = accountId,
                        Debit = sale.Discount > 0 ? Math.Abs(Math.Round((sale.Discount), 4)) : 0,
                        Credit = sale.Discount < 0 ? Math.Abs(Math.Round((sale.Discount), 4)) : 0,
                        Description = "Discount/ Adjustment",
                        DocumentId = sale.Id,
                        TransactionType = TransactionType.SaleInvoice,
                        DocumentNumber = sale.RegistrationNo,
                        Year = sale.Date.Year.ToString(),
                        PeriodId = period.Id,
                        BranchId = sale.BranchId,
                    });


                }



                //account Receivable
                if (sale.IsCredit)
                {
                    decimal total = 0;
                    if (sale.IsDiscountOnTransaction)
                    {
                        var saleTotal = sale.SaleItems.Where(x => !x.IsFree).Sum(x => x.UnitPrice * x.Quantity);
                        var disc = (sale.IsBeforeTax && sale.IsDiscountOnTransaction) ? (sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? (sale.TransactionLevelDiscount * (sale.SaleItems.Sum(x => x.IsFree ? 0 : (x.UnitPrice * x.Quantity))) / 100) : (sale.IsFixed ? sale.TransactionLevelDiscount : sale.TransactionLevelDiscount * sale.SaleItems.Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) / 100) : (sale.IsFixed ? sale.TransactionLevelDiscount : sale.TransactionLevelDiscount * (sale.SaleItems.Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) + ((sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? 0 : totalVatWithoutFree)) / 100);

                        total = Math.Round(saleTotal + ((sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? 0 : totalVatWithoutFree) - disc + sale.Discount, 4);
                    }
                    else
                    {
                        total = (sale.SaleItems.Where(x => !x.IsFree).Sum(x => Math.Round((x.OfferQuantity == 0 ? (((((x.UnitPrice * x.Quantity) - (x.Discount != 0 ? (x.UnitPrice * x.Quantity * x.Discount) / 100 : (sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل" ? x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100) : x.FixDiscount))) * ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : (100 + x.TaxRate.Rate))) / ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : 100)))
                                : (((((x.UnitPrice * x.Quantity) - (x.Discount != 0 ? (x.UnitPrice * x.OfferQuantity * x.Discount) / 100 : x.FixDiscount)) * ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : (100 + x.TaxRate.Rate))) / ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : 100)))), 2))) + sale.Discount;

                    }
                    transactions.Add(new TransactionLookupModel
                    {
                        Date = DateTime.UtcNow,
                        DocumentDate = sale.Date,
                        ApprovalDate = request.ApprovalDate,
                        ContactId = sale.CustomerId,
                        AccountId = sale.Customer.AccountId.Value,
                        Credit = Math.Abs(Math.Round(total, 4)),
                        Debit = 0,
                        Description = TransactionType.SaleReturn.ToString(),
                        DocumentId = sale.Id,
                        TransactionType = TransactionType.SaleReturn,
                        DocumentNumber = sale.RegistrationNo,
                        Year = sale.Date.Year.ToString(),
                        PeriodId = period.Id,
                        BranchId = sale.BranchId,
                    });
                }
                else
                {
                    foreach (var payment in sale.SalePayments)
                    {
                        transactions.Add(new TransactionLookupModel
                        {
                            Date = DateTime.UtcNow,
                            DocumentDate = sale.Date,
                            ApprovalDate = request.ApprovalDate,
                            ContactId = sale.CustomerId,
                            AccountId = payment.PaymentType == SalePaymentType.Bank ? terminalAccount.AccountId.Value //Bank "10500001"
                                                                                    : accounts.FirstOrDefault(x => x.Code == "10100001")?.Id ?? Guid.Empty, //Cash
                            Credit = payment.Received,
                            Debit = 0,
                            Description = TransactionType.SaleReturn.ToString(),
                            DocumentId = sale.Id,
                            TransactionType = TransactionType.SaleReturn,
                            DocumentNumber = sale.RegistrationNo,
                            Year = sale.Date.Year.ToString(),
                            PeriodId = period.Id,
                            BranchId = sale.BranchId,
                        });
                    }
                    if (sale.IsService)
                    {
                        decimal total = 0;
                        var freeItem = sale.SaleItems.Where(x => x.IsFree && !x.ServiceItem);
                        foreach (var item in freeItem)
                        {
                            total += Math.Round((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (((item.Quantity * item.UnitPrice) - (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount)) * 100) / (100 + item.TaxRate.Rate) + (item.Discount != 0 ? (item.UnitPrice * item.Quantity * item.Discount) / 100 : item.FixDiscount)
                                : (item.Quantity * item.UnitPrice), 2);

                            total += Math.Abs(Math.Round((((item.UnitPrice * (item.Quantity)) - ((item.Discount != 0 ? (item.UnitPrice * (item.Quantity) * item.Discount) / 100 : item.FixDiscount))) * item.TaxRate.Rate) / ((item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل") ? (item.TaxRate.Rate + 100) : 100), 2));
                        }

                        transactions.Add(new TransactionLookupModel
                        {
                            Date = DateTime.UtcNow,
                            DocumentDate = sale.Date,
                            ApprovalDate = request.ApprovalDate,
                            ContactId = sale.CustomerId,
                            AccountId = accounts.FirstOrDefault(x => x.Code == "60505001").Id,
                            Debit = 0,
                            Credit = total,
                            Description = TransactionType.SaleInvoice.ToString(),
                            DocumentId = sale.Id,
                            TransactionType = TransactionType.SaleInvoice,
                            DocumentNumber = sale.RegistrationNo,
                            Year = sale.Date.Year.ToString(),
                            PeriodId = period.Id,
                            BranchId = sale.BranchId,
                        });
                    }
                    //Cash In Hand
                    Guid cashAccountId = default;
                    if (dayStart)
                    {
                        var terminalId1 = Guid.Parse(counterId);
                        var terminalAccount1 = await _context.Terminals.AsNoTracking().FirstOrDefaultAsync(x => x.Id == terminalId1);
                        if (terminalAccount1.AccountId != null)
                        {
                            cashAccountId = terminalAccount.CashAccountId.Value;
                        }
                    }
                    else
                    {
                        var invoiceSetting = _context.PrintSettings.AsNoTracking().FirstOrDefault();
                        if (invoiceSetting != null)
                        {
                            cashAccountId = invoiceSetting.CashAccountId ?? Guid.Empty;
                        }
                    }

                    if (invoiceAmount < 0 && isTouchInvoice)
                    {

                        transactions.Add(new TransactionLookupModel()
                        {
                            Date = DateTime.UtcNow,
                            DocumentDate = sale.Date,
                            ApprovalDate = request.ApprovalDate,
                            ContactId = sale.CustomerId,
                            AccountId = cashAccountId,
                            Debit = 0,
                            Credit = Math.Abs(invoiceAmount),
                            Description = TransactionType.SaleReturn.ToString(),
                            DocumentId = sale.Id,
                            TransactionType = TransactionType.SaleReturn,
                            DocumentNumber = sale.RegistrationNo,
                            Year = sale.Date.Year.ToString(),
                            PeriodId = period.Id,
                            BranchId = sale.BranchId,
                        });
                    }
                    else
                    {
                        decimal total = 0;
                        if (sale.IsDiscountOnTransaction)
                        {
                            var saleTotal = sale.SaleItems.Where(x => !x.IsFree).Sum(x => x.UnitPrice * x.Quantity);
                            var disc = (sale.IsBeforeTax && sale.IsDiscountOnTransaction) ? (sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? (sale.TransactionLevelDiscount * (sale.SaleItems.Sum(x => x.IsFree ? 0 : (x.UnitPrice * x.Quantity))) / 100) : (sale.IsFixed ? sale.TransactionLevelDiscount : sale.TransactionLevelDiscount * sale.SaleItems.Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) / 100) : (sale.IsFixed ? sale.TransactionLevelDiscount : sale.TransactionLevelDiscount * (sale.SaleItems.Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) + ((sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? 0 : totalVatWithoutFree)) / 100);

                            total = Math.Round(saleTotal + ((sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? 0 : totalVatWithoutFree) - disc + sale.Discount, 4);
                        }
                        else
                        {
                            total = (sale.SaleItems.Where(x => !x.IsFree).Sum(x => Math.Round((x.OfferQuantity == 0 ? (((((x.UnitPrice * x.Quantity) - (x.Discount != 0 ? (x.UnitPrice * x.Quantity * x.Discount) / 100 : (sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل" ? x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100) : x.FixDiscount))) * ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : (100 + x.TaxRate.Rate))) / ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : 100)))
                                    : (((((x.UnitPrice * x.Quantity) - (x.Discount != 0 ? (x.UnitPrice * x.OfferQuantity * x.Discount) / 100 : x.FixDiscount)) * ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : (100 + x.TaxRate.Rate))) / ((x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل") ? 1 : 100)))), 2))) + sale.Discount;

                        }

                        transactions.Add(new TransactionLookupModel
                        {
                            Date = DateTime.UtcNow,
                            DocumentDate = sale.Date,
                            ApprovalDate = request.ApprovalDate,
                            ContactId = sale.CustomerId,
                            AccountId = cashAccountId,
                            Debit = 0,

                            Credit = Math.Abs(Math.Round(total, 4)),
                            Description = TransactionType.SaleReturn.ToString(),
                            DocumentId = sale.Id,
                            TransactionType = TransactionType.SaleReturn,
                            DocumentNumber = sale.RegistrationNo,
                            Year = sale.Date.Year.ToString(),
                            PeriodId = period.Id,
                            BranchId = sale.BranchId,
                        });
                    }
                }




                foreach (var transaction in transactions)
                {
                    await _mediator.Send(new AddTransactionCommand
                    {
                        Transaction = transaction,
                        BranchId = sale.BranchId,
                        Code = _code
                    });
                }
                await _context.LedgerTransactions.AddRangeAsync(ledgerTransactions);

            }

        }
    }
}
