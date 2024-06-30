using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Attachments.Commands;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Inventories.Queries.GetLatestInventory;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Transaction = Focus.Domain.Entities.Transaction;

namespace Focus.Business.StockAdjustments.Commands.AddUpdateStockAdjustment
{
    public class AddUpdateStockAdjustmentCommand : IRequest<Message>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public string Narration { get; set; }
        public bool IsDraft { get; set; }
        public Guid WarehouseId { get; set; }
        public StockAdjustmentType StockAdjustmentType { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<StockAdjustmentDetail> StockAdjustmentDetails { get; set; }
        public string TaxMethod { get; set; }
        public Guid? TaxRateId { get; set; }
        public string Reason { get; set; }
        public bool IsSerial { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public Guid? BankCashAccountId { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<AddUpdateStockAdjustmentCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateStockAdjustmentCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AddUpdateStockAdjustmentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    // Check Financial Year
                    var period = await _context.CompanySubmissionPeriods.AsNoTracking().FirstOrDefaultAsync(x => x.PeriodStart.Year == request.Date.Year && x.PeriodStart.Month == request.Date.Month, cancellationToken: cancellationToken);

                    if (period == null)
                        throw new NotFoundException("Financial Year Not Found", "");

                    if (period.IsPeriodClosed)
                        throw new ApplicationException("Financial year period closed");

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    //FOR UPDATE EXISTING
                    if (request.Id != Guid.Empty)
                    {
                        //FOR DRAFT
                        if (request.IsDraft)
                        {
                            var sa = await _context.StockAdjustments.FindAsync(request.Id);

                            sa.Date = request.Date;
                            sa.Code = request.Code;
                            sa.Reason = request.Reason;
                            sa.Narration = request.Narration;
                            sa.WarehouseId = request.WarehouseId;
                            sa.StockAdjustmentType = request.StockAdjustmentType;
                            sa.isDraft = request.IsDraft;
                            sa.TaxMethod = request.TaxMethod;
                            sa.TaxRateId = request.TaxRateId;
                            sa.IsSerial = request.IsSerial;
                            sa.BankCashAccountId = request.BankCashAccountId;

                            if (request.AttachmentList != null)
                            {
                                var attachments = _context.Attachments
                                    .AsNoTracking()
                                    .Where(x => x.DocumentId == sa.Id)
                                    .AsQueryable();

                                if (attachments.Any())
                                {
                                    _context.Attachments.RemoveRange(attachments);
                                }
                                foreach (var item in request.AttachmentList)
                                {
                                    var attachment = new Attachment()
                                    {
                                        Date = item.Date,
                                        DocumentId = sa.Id,
                                        Description = item.Description,
                                        FileName = item.FileName,
                                        Path = item.Path
                                    };
                                    //Add Attachments to database
                                    await _context.Attachments.AddAsync(attachment, cancellationToken);

                                }
                            }

                            _context.StockAdjustmentDetails.RemoveRange(sa.StockAdjustmentDetails);

                            foreach (var item in request.StockAdjustmentDetails)
                            {
                                var stockAdjustmentDetail = new StockAdjustmentDetail
                                {
                                    StockAdjustmentId = sa.Id,
                                    WarehouseId = item.WarehouseId,
                                    Price = item.Price,
                                    Quantity = item.Quantity,
                                    ProductId = item.ProductId,
                                    Serial = item.Serial,
                                    WarrantyTypeId = item.WarrantyTypeId,
                                    GuaranteeDate = item.GuaranteeDate,
                                    SerialNo = item.SerialNo,
                                    BatchExpiry = item.BatchExpiry,
                                    BatchNo = item.BatchNo
                                };
                                await _context.StockAdjustmentDetails.AddAsync(stockAdjustmentDetail, cancellationToken);
                                //await _context.StockAdjustmentDetails.AddAsync(item, cancellationToken);
                            }
                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                BranchId = sa.BranchId,
                                Code = _code,
                            }, cancellationToken);

                            //Save changes to database
                            await _context.SaveChangesAsync(cancellationToken);

                            var message = new Message
                            {
                                Id = request.Id,
                                IsAddUpdate = "Data Updated Successfully"
                            };
                            return message;
                        }
                        //FOR POST
                        else
                        {
                            var Accounts = _context.Accounts.AsNoTracking().ToList();
                            var products = _context.Products.AsNoTracking()
                                .AsQueryable();

                            var cashAccount = Accounts.FirstOrDefault(x => x.Code == "10100001");
                            if (cashAccount == null)
                            {
                                throw new ApplicationException("Cash Account Account Not Found");
                            }

                            var generalExpense = Accounts.FirstOrDefault(x => x.Code == "60505001");
                            if (generalExpense == null)
                            {
                                throw new ApplicationException("General Expense Account Not Found");
                            }

                            var accountInventory = Accounts.FirstOrDefault(x => x.Code == "11100001");
                            if (accountInventory == null)
                            {
                                throw new ApplicationException("Inventory Account Not Found");
                            }

                            var accounts = Accounts.Where(x => x.Code == "25000001" || x.Code == "42600001").ToList();
                            var sa = await _context.StockAdjustments
                                .FindAsync(request.Id);

                            sa.Date = request.Date;
                            sa.Code = request.Code;
                            sa.Reason = request.Reason;
                            sa.Narration = request.Narration;
                            sa.WarehouseId = request.WarehouseId;
                            sa.StockAdjustmentType = request.StockAdjustmentType;
                            sa.isDraft = request.IsDraft;
                            sa.TaxMethod = request.TaxMethod;
                            sa.TaxRateId = request.TaxRateId;
                            sa.IsSerial = request.IsSerial;
                            sa.BankCashAccountId = request.BankCashAccountId;

                            if (request.AttachmentList != null)
                            {
                                var attachments = _context.Attachments
                                    .AsNoTracking()
                                    .Where(x => x.DocumentId == sa.Id)
                                    .AsQueryable();

                                if (attachments.Any())
                                {
                                    _context.Attachments.RemoveRange(attachments);
                                }
                                foreach (var item in request.AttachmentList)
                                {
                                    var attachment = new Attachment()
                                    {
                                        Date = item.Date,
                                        DocumentId = sa.Id,
                                        Description = item.Description,
                                        FileName = item.FileName,
                                        Path = item.Path
                                    };
                                    //Add Attachments to database
                                    await _context.Attachments.AddAsync(attachment, cancellationToken);

                                }
                            }


                            _context.StockAdjustmentDetails.RemoveRange(sa.StockAdjustmentDetails);

                            foreach (var item in request.StockAdjustmentDetails)
                            {
                                var stockAdjustmentDetail = new StockAdjustmentDetail
                                {
                                    StockAdjustmentId = sa.Id,
                                    WarehouseId = item.WarehouseId,
                                    Price = item.Price,
                                    Quantity = item.Quantity,
                                    ProductId = item.ProductId,
                                    Serial = item.Serial,
                                    WarrantyTypeId = item.WarrantyTypeId,
                                    GuaranteeDate = item.GuaranteeDate,
                                    SerialNo = item.SerialNo,
                                    BatchExpiry = item.BatchExpiry,
                                    BatchNo = item.BatchNo
                                };
                                await _context.StockAdjustmentDetails.AddAsync(stockAdjustmentDetail, cancellationToken);
                            }

                            decimal rate;
                            if (request.TaxRateId == null && string.IsNullOrEmpty(request.TaxMethod))
                            {
                                rate = 0;
                            }
                            else
                            {
                                var rateValue = _context.TaxRates.FirstOrDefault(x => x.Id == request.TaxRateId);
                                rate = rateValue?.Rate ?? 0;
                            }
                            var transactions = new List<Transaction>();
                            var ledgerTransactions = new List<LedgerTransaction>();
                            decimal inventoryAccount = 0;
                            decimal cogsAccount = 0;
                            decimal purchaseAccount = 0;
                            var isPos = _context.CompanyPermissions.AsNoTracking().Any(x => x.Value == "PosWithTransactionlevel");

                            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);


                            if (TransactionType.StockIn == (TransactionType)request.StockAdjustmentType)
                            {
                                var autoNumber = _context.Inventories.OrderByDescending(x => x.AutoNumbering).FirstOrDefault(x => x.AutoNumbering != 0)?.AutoNumbering;

                                foreach (var item in request.StockAdjustmentDetails)
                                {
                                    transactions.Add(new Transaction
                                    {
                                        DocumentId = request.Id,
                                        DocumentNumber = request.Code,
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.Date,
                                        Description = request.Narration,
                                        Year = request.Date.Year.ToString(),
                                        AccountId = request.BankCashAccountId ?? cashAccount.Id,
                                        TransactionType = (TransactionType)request.StockAdjustmentType,
                                        Credit = (item.Quantity * item.Price),
                                        Debit = 0M,
                                        ProductId = item.ProductId,
                                        BranchId = sa.BranchId,
                                    });
                                    inventoryAccount += (item.Quantity * item.Price);
                                    if (isPos)
                                    {
                                        ledgerTransactions.Add(new LedgerTransaction
                                        {
                                            DocumentId = request.Id,
                                            DocumentNumber = request.Code,
                                            Date = DateTime.UtcNow,
                                            DocumentDate = request.Date,
                                            Description = request.Narration,
                                            Year = request.Date.Year.ToString(),
                                            AccountId = products.FirstOrDefault(x => x.Id == item.ProductId)?.InventoryAccountId ?? Guid.Empty,
                                            //AccountId = products.FirstOrDefault(x => x.Id == item.ProductId)?.Category.InventoryAccountId ?? Guid.Empty,
                                            TransactionType = (TransactionType)request.StockAdjustmentType,
                                            Debit = (item.Quantity * item.Price),
                                            Credit = 0M,
                                            ProductId = item.ProductId,
                                            BranchId = sa.BranchId,
                                        });
                                    }




                                    var stockId = _context.Stocks.FirstOrDefault(x => x.ProductId == item.ProductId && x.WareHouseId == item.WarehouseId);
                                    if (stockId == null)
                                    {
                                        var stock = new Stock()
                                        {
                                            ProductId = item.ProductId,
                                            WareHouseId = request.WarehouseId,
                                            CurrentQuantity = item.Quantity,
                                            AveragePrice = Math.Round(request.TaxMethod == "Inclusive" || request.TaxMethod == "شامل" ? item.Price * 100 / (100 + sa.TaxRate.Rate) : item.Price, 2)
                                        };
                                        _context.Stocks.Add(stock);

                                        stockId = stock;
                                    }
                                    else
                                    {
                                        stockId.AveragePrice = Math.Round(((stockId.AveragePrice * stockId.CurrentQuantity) + ((request.TaxMethod == "Inclusive" || request.TaxMethod == "شامل" ? item.Price * 100 / (100 + sa.TaxRate.Rate) : item.Price) * item.Quantity)) / (stockId.CurrentQuantity + item.Quantity), 2);
                                        stockId.CurrentQuantity += item.Quantity;
                                        stockId.CurrentStockValue = Math.Round(stockId.CurrentQuantity * stockId.AveragePrice, 2);
                                    }
                                    //var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                                    //{
                                    //    ProductId = item.ProductId,
                                    //    StockId = stockId.Id,
                                    //    WareHouseId = request.WarehouseId
                                    //}, cancellationToken);

                                    var inv = new Inventory()
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentId = request.Id,
                                        DocumentNumber = sa.Code,
                                        Quantity = Convert.ToDecimal(item.Quantity),
                                        Price = Math.Round(item.Price, 2),
                                        ProductId = item.ProductId,
                                        StockId = stockId.Id,
                                        Serial = item.SerialNo,
                                        WareHouseId = request.WarehouseId,
                                        TransactionType = (TransactionType)request.StockAdjustmentType,
                                        //AveragePrice = currentInventory.Price == 0 ? item.Price : Math.Round((currentInventory.AveragePrice + item.Price) / 2, 2),
                                        //CurrentQuantity = currentInventory.CurrentQuantity + Convert.ToDecimal(item.Quantity),
                                        //CurrentStockValue = ((currentInventory.CurrentQuantity + item.Quantity) * currentInventory.AveragePrice),
                                        BatchNumber = item.BatchNo,
                                        IsActive = true,
                                        IsOpen = true,
                                        AutoNumbering = (int)(autoNumber == null ? 1 : ++autoNumber),
                                        RemainingQuantity = Convert.ToDecimal(item.Quantity),
                                        ExpiryDate = item.BatchExpiry,
                                        BranchId = sa.BranchId,
                                    };
                                    await _context.Inventories.AddAsync(inv, cancellationToken);
                                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                                    {
                                        SyncRecordModels = _context.SyncLog(),
                                        BranchId = sa.BranchId,
                                        Code = _code,
                                    }, cancellationToken);

                                    //Save changes to database
                                    await _context.SaveChangesAsync(cancellationToken);
                                }
                                transactions.Add(new Transaction
                                {
                                    DocumentId = request.Id,
                                    DocumentNumber = request.Code,
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.Date,
                                    Description = request.Narration,
                                    Year = request.Date.Year.ToString(),
                                    AccountId = accountInventory.Id,
                                    //AccountId = products.FirstOrDefault(x => x.Id == item.ProductId)?.Category.InventoryAccountId ?? Guid.Empty,
                                    TransactionType = (TransactionType)request.StockAdjustmentType,
                                    Debit = inventoryAccount,
                                    Credit = 0M,
                                    ProductId = null,
                                    BranchId = sa.BranchId,
                                });
                            }
                            else if (TransactionType.StockProduction == (TransactionType)request.StockAdjustmentType)
                            {
                                foreach (var item in request.StockAdjustmentDetails)
                                {
                                    transactions.Add(new Transaction()
                                    {
                                        DocumentId = request.Id,
                                        DocumentNumber = request.Code,
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.Date,
                                        Description = request.Narration,
                                        Year = request.Date.Year.ToString(),
                                        AccountId = cashAccount.Id,
                                        TransactionType = (TransactionType)request.StockAdjustmentType,
                                        Credit = (item.Quantity * item.Price),
                                        Debit = 0M,
                                        ProductId = item.ProductId,
                                        BranchId = sa.BranchId,
                                    });
                                    inventoryAccount += (item.Quantity * item.Price);
                                    if (isPos)
                                    {
                                        ledgerTransactions.Add(new LedgerTransaction
                                        {
                                            DocumentId = request.Id,
                                            DocumentNumber = request.Code,
                                            Date = DateTime.UtcNow,
                                            DocumentDate = request.Date,
                                            Description = request.Narration,
                                            Year = request.Date.Year.ToString(),
                                            AccountId = products.FirstOrDefault(x => x.Id == item.ProductId)?.InventoryAccountId ?? Guid.Empty,
                                            TransactionType = (TransactionType)request.StockAdjustmentType,
                                            Debit = (item.Quantity * item.Price),
                                            Credit = 0M,
                                            ProductId = item.ProductId,
                                            BranchId = sa.BranchId,
                                        });
                                    }


                                    var stockId = _context.Stocks.FirstOrDefault(x => x.ProductId == item.ProductId && x.WareHouseId == item.WarehouseId);
                                    if (stockId == null)
                                    {
                                        var stock = new Stock()
                                        {
                                            ProductId = item.ProductId,
                                            WareHouseId = request.WarehouseId,
                                            BranchId = sa.BranchId,
                                        };

                                        _context.Stocks.Add(stock);

                                        stockId = stock;
                                    }

                                    var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                                    {
                                        ProductId = item.ProductId,
                                        StockId = stockId.Id,
                                        WareHouseId = request.WarehouseId
                                    }, cancellationToken);

                                    var batchInventory =
                                        _context.Inventories.FirstOrDefault(
                                            x => x.BatchNumber == item.BatchNo && x.IsActive && x.IsOpen && x.ProductId == item.ProductId);
                                    if (batchInventory != null)
                                    {
                                        if (batchInventory.RemainingQuantity > item.Quantity)
                                        {
                                            batchInventory.RemainingQuantity = batchInventory.RemainingQuantity - item.Quantity;
                                        }
                                        else if (batchInventory.RemainingQuantity == item.Quantity)
                                        {
                                            batchInventory.RemainingQuantity = 0;
                                            batchInventory.IsActive = false;
                                            batchInventory.IsOpen = false;
                                        }
                                        else
                                        {
                                            var batchInventoryForRemaining =
                                                _context.Inventories.FirstOrDefault(
                                                    x => x.IsActive && x.IsOpen && x.ProductId == item.ProductId && x.AutoNumbering > batchInventory.AutoNumbering);
                                            batchInventoryForRemaining.RemainingQuantity = batchInventoryForRemaining.RemainingQuantity - (item.Quantity - batchInventory.RemainingQuantity);
                                            batchInventory.RemainingQuantity = 0;
                                            batchInventory.IsActive = false;
                                            batchInventory.IsOpen = false;
                                        }
                                    }
                                    var inv = new Inventory()
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentNumber = sa.Code,
                                        DocumentId = request.Id,
                                        Quantity = Convert.ToDecimal(item.Quantity),
                                        Price = Math.Round(item.Price, 2),
                                        ProductId = item.ProductId,
                                        StockId = stockId.Id,
                                        WareHouseId = request.WarehouseId,
                                        TransactionType = (TransactionType)request.StockAdjustmentType,
                                        AveragePrice = currentInventory.Price == 0 ? item.Price : Math.Round((currentInventory.AveragePrice + item.Price) / 2, 2),
                                        CurrentQuantity = currentInventory.CurrentQuantity + Convert.ToDecimal(item.Quantity),
                                        CurrentStockValue = ((currentInventory.CurrentQuantity + item.Quantity) * currentInventory.AveragePrice),
                                        BranchId = sa.BranchId,

                                    };
                                    await _context.Inventories.AddAsync(inv, cancellationToken);
                                }
                                transactions.Add(new Transaction
                                {
                                    DocumentId = request.Id,
                                    DocumentNumber = request.Code,
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.Date,
                                    Description = request.Narration,
                                    Year = request.Date.Year.ToString(),
                                    AccountId = accountInventory.Id,
                                    //AccountId = products.FirstOrDefault(x => x.Id == item.ProductId)?.Category.InventoryAccountId ?? Guid.Empty,
                                    TransactionType = (TransactionType)request.StockAdjustmentType,
                                    Debit = inventoryAccount,
                                    Credit = 0M,
                                    ProductId = null,
                                    BranchId = sa.BranchId,
                                });
                            }
                            else if (TransactionType.StockOut == (TransactionType)request.StockAdjustmentType)
                            {
                                foreach (var item in request.StockAdjustmentDetails)
                                {

                                    inventoryAccount += Math.Round((request.TaxMethod == "Exclusive" || request.TaxMethod == "غير شامل") ? (item.Quantity * item.Price) : (item.Quantity * item.Price * 100 / (100 + rate)), 2);
                                    // Inventory Credit in Stock OUT
                                    if (isPos)
                                    {
                                        ledgerTransactions.Add(new LedgerTransaction
                                        {
                                            DocumentId = request.Id,
                                            DocumentNumber = request.Code,
                                            Date = DateTime.UtcNow,
                                            DocumentDate = request.Date,
                                            Description = request.Narration,
                                            Year = request.Date.Year.ToString(),
                                            AccountId = products.FirstOrDefault(x => x.Id == item.ProductId)?.InventoryAccountId ?? Guid.Empty,
                                            TransactionType = (TransactionType)request.StockAdjustmentType,
                                            Credit = Math.Round((request.TaxMethod == "Exclusive" || request.TaxMethod == "غير شامل") ? (item.Quantity * item.Price) : (item.Quantity * item.Price * 100 / (100 + rate)), 2),
                                            Debit = 0M,
                                            ProductId = item.ProductId,
                                            BranchId = sa.BranchId,
                                        });
                                    }


                                    // General Expense Debit
                                    transactions.Add(new Transaction
                                    {
                                        DocumentId = request.Id,
                                        DocumentNumber = request.Code,
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.Date,
                                        Description = request.Narration,
                                        Year = request.Date.Year.ToString(),
                                        AccountId = request.BankCashAccountId ?? generalExpense.Id,
                                        TransactionType = (TransactionType)request.StockAdjustmentType,
                                        Debit = Math.Round((request.TaxMethod == "Exclusive" || request.TaxMethod == "غير شامل") ? ((item.Quantity * item.Price) * (rate + 100) / 100) : (item.Quantity * item.Price), 2),
                                        Credit = 0M,
                                        ProductId = item.ProductId,
                                        BranchId = sa.BranchId,

                                    });
                                    //vat
                                    transactions.Add(new Transaction
                                    {
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.Date,
                                        AccountId = accounts.FirstOrDefault(x => x.Code == "25000001")?.Id ?? Guid.Empty,
                                        Credit = Math.Round((request.TaxMethod == "Inclusive" || request.TaxMethod == "شامل") ? (item.Quantity * item.Price * rate) / (rate + 100) : item.Quantity * item.Price * rate / 100, 2),

                                        Debit = 0,
                                        Description = TransactionType.StockOut.ToString(),
                                        DocumentId = request.Id,
                                        TransactionType = TransactionType.StockOut,
                                        DocumentNumber = request.Code,
                                        Year = request.Date.Year.ToString(),
                                        ProductId = item.ProductId,
                                        BranchId = sa.BranchId,

                                    });



                                    var stockId = _context.Stocks.FirstOrDefault(x => x.ProductId == item.ProductId && x.WareHouseId == item.WarehouseId);
                                    if (stockId == null)
                                    {
                                        var stock = new Stock()
                                        {
                                            ProductId = item.ProductId,
                                            WareHouseId = request.WarehouseId,
                                            CurrentQuantity = -item.Quantity,
                                            BranchId = sa.BranchId,
                                        };
                                        _context.Stocks.Add(stock);

                                        stockId = stock;
                                    }
                                    else
                                    {
                                        stockId.CurrentQuantity -= item.Quantity;
                                        stockId.CurrentStockValue = Math.Round(stockId.CurrentQuantity * stockId.AveragePrice, 2);
                                        stockId.AveragePrice = Math.Round(stockId.CurrentStockValue / stockId.CurrentQuantity, 2);
                                    }

                                    //var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                                    //{
                                    //    ProductId = item.ProductId,
                                    //    StockId = stockId.Id,
                                    //    WareHouseId = request.WarehouseId
                                    //}, cancellationToken);

                                    if (request.IsSerial && item.IsSerial)
                                    {
                                        var serialArray = item.Serial.Split(',');
                                        for (int i = 0; i < item.Quantity; i++)
                                        {
                                            var inv = new Inventory()
                                            {
                                                Date = DateTime.UtcNow,
                                                DocumentNumber = sa.Code,
                                                DocumentId = request.Id,
                                                Quantity = Convert.ToDecimal(item.Quantity),
                                                Price = Math.Round(item.Price, 2),
                                                ProductId = item.ProductId,
                                                StockId = stockId.Id,
                                                Serial = serialArray[i],
                                                WareHouseId = request.WarehouseId,
                                                TransactionType = (TransactionType)request.StockAdjustmentType,
                                                //AveragePrice = currentInventory.Price == 0 ? item.Price : Math.Round((currentInventory.AveragePrice + item.Price) / 2, 2),
                                                //CurrentQuantity = currentInventory.CurrentQuantity - item.Quantity,
                                                //CurrentStockValue = ((currentInventory.CurrentQuantity - item.Quantity) * currentInventory.AveragePrice),
                                                SalePrice = item.Price,
                                                BranchId = sa.BranchId,
                                            };
                                            await _context.Inventories.AddAsync(inv, cancellationToken);
                                        }

                                    }
                                    else
                                    {

                                        var batchInventory =
                                            _context.Inventories.FirstOrDefault(
                                                x => x.BatchNumber == item.BatchNo && x.IsActive && x.IsOpen && x.ProductId == item.ProductId);
                                        if (batchInventory != null)
                                        {
                                            if (batchInventory.RemainingQuantity > item.Quantity)
                                            {
                                                batchInventory.RemainingQuantity = batchInventory.RemainingQuantity - item.Quantity;
                                            }
                                            else if (batchInventory.RemainingQuantity == item.Quantity)
                                            {
                                                batchInventory.RemainingQuantity = 0;
                                                batchInventory.IsActive = false;
                                                batchInventory.IsOpen = false;
                                            }
                                            else
                                            {
                                                var batchInventoryForRemaining =
                                                    _context.Inventories.FirstOrDefault(
                                                        x => x.IsActive && x.IsOpen && x.ProductId == item.ProductId && x.AutoNumbering > batchInventory.AutoNumbering);
                                                batchInventoryForRemaining.RemainingQuantity = batchInventoryForRemaining.RemainingQuantity - (item.Quantity - batchInventory.RemainingQuantity);
                                                batchInventory.RemainingQuantity = 0;
                                                batchInventory.IsActive = false;
                                                batchInventory.IsOpen = false;
                                            }
                                        }

                                        var inv = new Inventory()
                                        {
                                            Date = DateTime.UtcNow,
                                            DocumentNumber = sa.Code,
                                            DocumentId = request.Id,
                                            Quantity = Convert.ToDecimal(item.Quantity),
                                            Price = Math.Round(item.Price, 2),
                                            ProductId = item.ProductId,
                                            StockId = stockId.Id,
                                            WareHouseId = request.WarehouseId,
                                            TransactionType = (TransactionType)request.StockAdjustmentType,
                                            //AveragePrice = currentInventory.Price == 0 ? item.Price : Math.Round((currentInventory.AveragePrice + item.Price) / 2, 2),
                                            //CurrentQuantity = currentInventory.CurrentQuantity - item.Quantity,
                                            //CurrentStockValue = ((currentInventory.CurrentQuantity - item.Quantity) * currentInventory.AveragePrice),
                                            SalePrice = item.Price,
                                            BranchId = sa.BranchId,
                                        };
                                        await _context.Inventories.AddAsync(inv, cancellationToken);
                                    }
                                }
                                ledgerTransactions.Add(new LedgerTransaction
                                {
                                    DocumentId = request.Id,
                                    DocumentNumber = request.Code,
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.Date,
                                    Description = request.Narration,
                                    Year = request.Date.Year.ToString(),
                                    AccountId = Accounts.FirstOrDefault(x => x.Code == "11100001").Id,
                                    TransactionType = (TransactionType)request.StockAdjustmentType,
                                    Credit = inventoryAccount,
                                    Debit = 0M,
                                    BranchId = sa.BranchId,
                                });
                            }


                            await _context.Transactions.AddRangeAsync(transactions, cancellationToken);
                            await _context.LedgerTransactions.AddRangeAsync(ledgerTransactions, cancellationToken);
                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                BranchId = sa.BranchId,
                                Code = _code,
                            }, cancellationToken);

                            //Save changes to database
                            await _context.SaveChangesAsync(cancellationToken);

                            scope.Complete();

                            if (request.Id != Guid.Empty)
                            {
                                var message = new Message
                                {
                                    Id = sa.Id,
                                    IsAddUpdate = "Data Added Successfully"
                                };
                                return message;
                            }
                            else
                            {
                                var message = new Message
                                {
                                    Id = Guid.Empty,
                                    IsAddUpdate = "Data Not Added Successfully"
                                };
                                return message;
                            }
                        }
                    }
                    //FOR INSERT NEW
                    else
                    {
                        var isSaExist = await _context.StockAdjustments
                                .Where(x => x.StockAdjustmentType == request.StockAdjustmentType)
                                .AnyAsync(x => x.Code == request.Code, cancellationToken: cancellationToken);

                        if (isSaExist)
                        {
                            var message = new Message
                            {
                                Id = request.Id,
                                IsAddUpdate = "Code Already Exist"
                            };
                            return message;
                        }
                        else
                        {
                            //FOR NEW DRAFT
                            if (request.IsDraft)
                            {

                                var stockAdjustment = new StockAdjustment
                                {
                                    Date = request.Date,
                                    Code = request.Code,
                                    Reason = request.Reason,
                                    Narration = request.Narration,
                                    WarehouseId = request.WarehouseId,
                                    StockAdjustmentType = request.StockAdjustmentType,
                                    isDraft = request.IsDraft,
                                    TaxMethod = request.TaxMethod,
                                    TaxRateId = request.TaxRateId,
                                    IsSerial = true,
                                    BankCashAccountId = request.BankCashAccountId,
                                    BranchId = request.BranchId,
                                };

                                await _context.StockAdjustments.AddAsync(stockAdjustment, cancellationToken);

                                if (request.AttachmentList != null && request.AttachmentList.Count > 0)
                                {

                                    foreach (var item in request.AttachmentList)
                                    {
                                        var attachment = new Attachment()
                                        {
                                            Date = item.Date,
                                            DocumentId = stockAdjustment.Id,
                                            Description = item.Description,
                                            FileName = item.FileName,
                                            Path = item.Path
                                        };
                                        //Add Attachments to database
                                        await _context.Attachments.AddAsync(attachment, cancellationToken);

                                    }
                                }

                                foreach (var item in request.StockAdjustmentDetails)
                                {
                                    var stockAdjustmentDetail = new StockAdjustmentDetail
                                    {
                                        StockAdjustmentId = stockAdjustment.Id,
                                        WarehouseId = item.WarehouseId,
                                        Price = item.Price,
                                        Quantity = item.Quantity,
                                        ProductId = item.ProductId,
                                        Serial = item.Serial,
                                        WarrantyTypeId = item.WarrantyTypeId,
                                        GuaranteeDate = item.GuaranteeDate,
                                        SerialNo = item.SerialNo,
                                        BatchExpiry = item.BatchExpiry,
                                        BatchNo = item.BatchNo
                                    };
                                    await _context.StockAdjustmentDetails.AddAsync(stockAdjustmentDetail, cancellationToken);
                                }
                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = _context.SyncLog(),
                                    BranchId = stockAdjustment.BranchId,
                                    Code = _code,
                                }, cancellationToken);
                                var success = await _context.SaveChangesAsync(cancellationToken);

                                //Save changes to database
                                //await _context.SaveChangesAsync(cancellationToken);

                                if (success > 0)
                                {
                                    var message = new Message
                                    {
                                        Id = stockAdjustment.Id,
                                        IsAddUpdate = "Data Added Successfully"
                                    };
                                    return message;
                                }
                                else
                                {
                                    var message = new Message
                                    {
                                        Id = Guid.Empty,
                                        IsAddUpdate = "Data Not Added Successfully"
                                    };
                                    return message;
                                }
                            }
                            //FOR NEW POST
                            else
                            {
                                var Accounts = _context.Accounts.AsNoTracking().ToList();
                                var products = _context.Products.AsNoTracking()
                                    .AsQueryable();


                                var cashAccount = Accounts.FirstOrDefault(x => x.Code == "10100001");
                                if (cashAccount == null)
                                {
                                    throw new ApplicationException("Cash Account Account Not Found");
                                }
                                var generalExpense = Accounts.FirstOrDefault(x => x.Code == "60505001");
                                if (generalExpense == null)
                                {
                                    throw new ApplicationException("General Expense Account Not Found");
                                }
                                var accountInventory = Accounts.FirstOrDefault(x => x.Code == "11100001");
                                if (accountInventory == null)
                                {
                                    throw new ApplicationException("Inventory Account Not Found");
                                }
                                var accounts = Accounts.Where(x => x.Code == "25000001" || x.Code == "42600001").ToList();


                                var stockAdjustment = new StockAdjustment
                                {
                                    Date = request.Date,
                                    Code = request.Code,
                                    Reason = request.Reason,
                                    Narration = request.Narration,
                                    WarehouseId = request.WarehouseId,
                                    StockAdjustmentType = request.StockAdjustmentType,
                                    isDraft = request.IsDraft,
                                    TaxMethod = request.TaxMethod,
                                    TaxRateId = request.TaxRateId,
                                    BankCashAccountId = request.BankCashAccountId,
                                    BranchId = request.BranchId,
                                    //IsSerial = request.IsSerial
                                };
                                await _context.StockAdjustments.AddAsync(stockAdjustment, cancellationToken);

                                if (request.AttachmentList != null && request.AttachmentList.Count > 0)
                                {

                                    foreach (var item in request.AttachmentList)
                                    {
                                        var attachment = new Attachment()
                                        {
                                            Date = item.Date,
                                            DocumentId = stockAdjustment.Id,
                                            Description = item.Description,
                                            FileName = item.FileName,
                                            Path = item.Path
                                        };
                                        //Add Attachments to database
                                        await _context.Attachments.AddAsync(attachment, cancellationToken);

                                    }
                                }

                                foreach (var item in request.StockAdjustmentDetails)
                                {
                                    var stockAdjustmentDetail = new StockAdjustmentDetail
                                    {
                                        StockAdjustmentId = stockAdjustment.Id,
                                        WarehouseId = item.WarehouseId,
                                        Price = item.Price,
                                        Quantity = item.Quantity,
                                        ProductId = item.ProductId,
                                        Serial = item.Serial,
                                        WarrantyTypeId = item.WarrantyTypeId,
                                        GuaranteeDate = item.GuaranteeDate,
                                        SerialNo = item.SerialNo,
                                        BatchExpiry = item.BatchExpiry,
                                        BatchNo = item.BatchNo
                                    };
                                    await _context.StockAdjustmentDetails.AddAsync(stockAdjustmentDetail, cancellationToken);
                                }


                                decimal rate;
                                if (request.TaxRateId == null && string.IsNullOrEmpty(request.TaxMethod))
                                {
                                    rate = 0;
                                }
                                else
                                {
                                    rate = _context.TaxRates.FirstOrDefault(x => x.Id == request.TaxRateId)?.Rate ?? 0;
                                }

                                using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                                var transactions = new List<Transaction>();
                                var ledgerTransactions = new Collection<LedgerTransaction>();
                                decimal inventoryAccountVal = 0;
                                decimal cogsAccountVal = 0;
                                decimal purchaseAccountVal = 0;
                                var isPos = _context.CompanyPermissions.AsNoTracking().Any(x => x.Value == "PosWithTransactionlevel");

                                if (TransactionType.StockIn == (TransactionType)request.StockAdjustmentType)
                                {
                                    var autoNumber = _context.Inventories.OrderByDescending(x => x.AutoNumbering).FirstOrDefault(x => x.AutoNumbering != 0)?.AutoNumbering;
                                    foreach (var item in request.StockAdjustmentDetails)
                                    {
                                        transactions.Add(new Transaction
                                        {
                                            DocumentId = stockAdjustment.Id,
                                            DocumentNumber = request.Code,
                                            Date = DateTime.UtcNow,
                                            DocumentDate = request.Date,
                                            Description = request.Narration,
                                            Year = request.Date.Year.ToString(),
                                            AccountId = request.BankCashAccountId ?? cashAccount.Id,
                                            TransactionType = (TransactionType)request.StockAdjustmentType,
                                            Credit = (item.Quantity * item.Price),
                                            Debit = 0M,
                                            ProductId = item.ProductId,
                                            BranchId = stockAdjustment.BranchId,
                                        });
                                        inventoryAccountVal += (item.Quantity * item.Price);
                                        if (isPos)
                                        {
                                            ledgerTransactions.Add(new LedgerTransaction
                                            {
                                                DocumentId = stockAdjustment.Id,
                                                DocumentNumber = request.Code,
                                                Date = DateTime.UtcNow,
                                                DocumentDate = request.Date,
                                                Description = request.Narration,
                                                Year = request.Date.Year.ToString(),
                                                AccountId = products.FirstOrDefault(x => x.Id == item.ProductId)?.InventoryAccountId ?? Guid.Empty,
                                                TransactionType = (TransactionType)request.StockAdjustmentType,
                                                Debit = (item.Quantity * item.Price),
                                                Credit = 0M,
                                                ProductId = item.ProductId,
                                                BranchId = stockAdjustment.BranchId,
                                            });
                                        }




                                        var stockId = _context.Stocks.FirstOrDefault(x => x.ProductId == item.ProductId && x.WareHouseId == item.WarehouseId);
                                        if (stockId == null)
                                        {
                                            var stock = new Stock()
                                            {
                                                ProductId = item.ProductId,
                                                WareHouseId = request.WarehouseId,
                                                CurrentQuantity = item.Quantity,
                                                AveragePrice = Math.Round(request.TaxMethod == "Inclusive" || request.TaxMethod == "شامل" ? item.Price * 100 / (100 + stockAdjustment.TaxRate.Rate) : item.Price, 2),
                                                BranchId = stockAdjustment.BranchId,
                                            };
                                            stock.CurrentStockValue = Math.Round(stock.CurrentQuantity * stock.AveragePrice, 2);
                                            _context.Stocks.Add(stock);
                                            stockId = stock;
                                        }
                                        else
                                        {
                                            stockId.AveragePrice = Math.Round(((stockId.AveragePrice * stockId.CurrentQuantity) + ((request.TaxMethod == "Inclusive" || request.TaxMethod == "شامل" ? item.Price * 100 / (100 + stockAdjustment.TaxRate.Rate) : item.Price) * item.Quantity)) / (stockId.CurrentQuantity + item.Quantity), 2);
                                            stockId.CurrentQuantity += item.Quantity;
                                            stockId.CurrentStockValue = Math.Round(stockId.CurrentQuantity * stockId.AveragePrice, 2);
                                        }
                                        //var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                                        //{
                                        //    ProductId = item.ProductId,
                                        //    StockId = stockId.Id,
                                        //    WareHouseId = request.WarehouseId
                                        //}, cancellationToken);

                                        var inv = new Inventory()
                                        {
                                            Date = DateTime.UtcNow,
                                            DocumentNumber = stockAdjustment.Code,
                                            DocumentId = stockAdjustment.Id,
                                            Quantity = Convert.ToDecimal(item.Quantity),
                                            Price = Math.Round(item.Price, 2),
                                            ProductId = item.ProductId,
                                            Serial = item.SerialNo,
                                            StockId = stockId.Id,
                                            WareHouseId = request.WarehouseId,
                                            TransactionType = (TransactionType)request.StockAdjustmentType,
                                            //AveragePrice = currentInventory.Price == 0 ? item.Price : Math.Round((currentInventory.AveragePrice + item.Price) / 2, 2),
                                            //CurrentQuantity = currentInventory.CurrentQuantity + item.Quantity,
                                            //CurrentStockValue = ((currentInventory.CurrentQuantity + item.Quantity) * currentInventory.AveragePrice),
                                            BatchNumber = item.BatchNo,
                                            IsActive = true,
                                            IsOpen = true,
                                            AutoNumbering = (int)(autoNumber == null ? 1 : ++autoNumber),
                                            RemainingQuantity = Convert.ToDecimal(item.Quantity),
                                            ExpiryDate = item.BatchExpiry,
                                            BranchId = stockAdjustment.BranchId,
                                        };
                                        await _context.Inventories.AddAsync(inv, cancellationToken);
                                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                                        {
                                            SyncRecordModels = _context.SyncLog(),
                                            BranchId = stockAdjustment.BranchId,
                                            Code = _code,
                                        }, cancellationToken);

                                        //Save changes to database
                                        await _context.SaveChangesAsync(cancellationToken);

                                    }

                                    transactions.Add(new Transaction
                                    {
                                        DocumentId = stockAdjustment.Id,
                                        DocumentNumber = request.Code,
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.Date,
                                        Description = request.Narration,
                                        Year = request.Date.Year.ToString(),
                                        AccountId = accountInventory.Id,
                                        TransactionType = (TransactionType)request.StockAdjustmentType,
                                        Debit = inventoryAccountVal,
                                        Credit = 0M,
                                        ProductId = null,
                                        BranchId = stockAdjustment.BranchId,
                                    });
                                }
                                else if (TransactionType.StockProduction == (TransactionType)request.StockAdjustmentType)
                                {
                                    foreach (var item in request.StockAdjustmentDetails)
                                    {
                                        transactions.Add(new Transaction
                                        {
                                            DocumentId = stockAdjustment.Id,
                                            DocumentNumber = request.Code,
                                            Date = DateTime.UtcNow,
                                            DocumentDate = request.Date,
                                            Description = request.Narration,
                                            Year = request.Date.Year.ToString(),
                                            AccountId = cashAccount.Id,
                                            TransactionType = (TransactionType)request.StockAdjustmentType,
                                            Credit = (item.Quantity * item.Price),
                                            Debit = 0M,
                                            ProductId = item.ProductId,
                                            BranchId = stockAdjustment.BranchId,
                                        });
                                        inventoryAccountVal = inventoryAccountVal + (item.Quantity * item.Price);

                                        if (isPos)
                                        {
                                            ledgerTransactions.Add(new LedgerTransaction
                                            {
                                                DocumentId = stockAdjustment.Id,
                                                DocumentNumber = request.Code,
                                                Date = DateTime.UtcNow,
                                                DocumentDate = request.Date,
                                                Description = request.Narration,
                                                Year = request.Date.Year.ToString(),
                                                AccountId = products.FirstOrDefault(x => x.Id == item.ProductId)?.InventoryAccountId ?? Guid.Empty,
                                                TransactionType = (TransactionType)request.StockAdjustmentType,
                                                Debit = (item.Quantity * item.Price),
                                                Credit = 0M,
                                                ProductId = item.ProductId,
                                                BranchId = stockAdjustment.BranchId,
                                            });
                                        }


                                        var stockId = _context.Stocks.FirstOrDefault(x => x.ProductId == item.ProductId && x.WareHouseId == item.WarehouseId);
                                        if (stockId == null)
                                        {
                                            var stock = new Stock()
                                            {
                                                ProductId = item.ProductId,
                                                WareHouseId = request.WarehouseId,
                                                BranchId = stockAdjustment.BranchId,
                                            };

                                            _context.Stocks.Add(stock);

                                            stockId = stock;
                                        }

                                        var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                                        {
                                            ProductId = item.ProductId,
                                            StockId = stockId.Id,
                                            WareHouseId = request.WarehouseId
                                        }, cancellationToken);

                                        var inv = new Inventory()
                                        {
                                            Date = DateTime.UtcNow,
                                            DocumentNumber = stockAdjustment.Code,
                                            DocumentId = stockAdjustment.Id,
                                            Quantity = Convert.ToDecimal(item.Quantity),
                                            Price = Math.Round(item.Price, 2),
                                            ProductId = item.ProductId,
                                            StockId = stockId.Id,
                                            WareHouseId = request.WarehouseId,
                                            TransactionType = (TransactionType)request.StockAdjustmentType,
                                            AveragePrice = currentInventory.Price == 0 ? item.Price : Math.Round((currentInventory.AveragePrice + item.Price) / 2, 2),
                                            CurrentQuantity = currentInventory.CurrentQuantity + item.Quantity,
                                            CurrentStockValue = ((currentInventory.CurrentQuantity + item.Quantity) * currentInventory.AveragePrice),
                                            BranchId = stockAdjustment.BranchId,

                                        };
                                        await _context.Inventories.AddAsync(inv, cancellationToken);
                                    }
                                    transactions.Add(new Transaction
                                    {
                                        DocumentId = stockAdjustment.Id,
                                        DocumentNumber = request.Code,
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.Date,
                                        Description = request.Narration,
                                        Year = request.Date.Year.ToString(),
                                        AccountId = accountInventory.Id,
                                        TransactionType = (TransactionType)request.StockAdjustmentType,
                                        Debit = inventoryAccountVal,
                                        Credit = 0M,
                                        ProductId = null,
                                        BranchId = stockAdjustment.BranchId,
                                    });
                                }
                                else if (TransactionType.StockOut == (TransactionType)request.StockAdjustmentType)
                                {
                                    foreach (var item in request.StockAdjustmentDetails)
                                    {
                                        inventoryAccountVal += Math.Round((request.TaxMethod == "Exclusive" || request.TaxMethod == "غير شامل") ? (item.Quantity * item.Price) : (item.Quantity * item.Price * 100 / (100 + rate)), 2);
                                        if (isPos)
                                        {
                                            ledgerTransactions.Add(new LedgerTransaction
                                            {
                                                DocumentId = stockAdjustment.Id,
                                                DocumentNumber = request.Code,
                                                Date = DateTime.UtcNow,
                                                DocumentDate = request.Date,
                                                Description = request.Narration,
                                                Year = request.Date.Year.ToString(),
                                                AccountId = products.FirstOrDefault(x => x.Id == item.ProductId)?.InventoryAccountId ?? Guid.Empty,
                                                TransactionType = (TransactionType)request.StockAdjustmentType,
                                                Credit = Math.Round((request.TaxMethod == "Exclusive" || request.TaxMethod == "غير شامل") ? (item.Quantity * item.Price) : (item.Quantity * item.Price * 100 / (100 + rate)), 2),
                                                Debit = 0M,
                                                ProductId = item.ProductId,
                                                BranchId = stockAdjustment.BranchId,

                                            });

                                        }
                                        // Inventory Credit in Stock OUT

                                        // General Expense Debit
                                        transactions.Add(new Transaction
                                        {
                                            DocumentId = stockAdjustment.Id,
                                            DocumentNumber = request.Code,
                                            Date = DateTime.UtcNow,
                                            DocumentDate = request.Date,
                                            Description = request.Narration,
                                            Year = request.Date.Year.ToString(),
                                            AccountId = request.BankCashAccountId ?? generalExpense.Id,
                                            TransactionType = (TransactionType)request.StockAdjustmentType,
                                            Debit = Math.Round((request.TaxMethod == "Exclusive" || request.TaxMethod == "غير شامل") ? ((item.Quantity * item.Price) * (rate + 100) / 100) : (item.Quantity * item.Price), 2),
                                            Credit = 0M,
                                            ProductId = item.ProductId,
                                            BranchId = stockAdjustment.BranchId,

                                        });
                                        //vat
                                        transactions.Add(new Transaction
                                        {
                                            Date = DateTime.UtcNow,
                                            DocumentDate = request.Date,
                                            AccountId = accounts.FirstOrDefault(x => x.Code == "25000001")?.Id ?? Guid.Empty,
                                            Credit = Math.Round((request.TaxMethod == "Inclusive" || request.TaxMethod == "شامل") ? (item.Quantity * item.Price * rate) / (rate + 100) : item.Quantity * item.Price * rate / 100, 2),

                                            Debit = 0,
                                            Description = TransactionType.StockOut.ToString(),
                                            DocumentId = stockAdjustment.Id,
                                            TransactionType = TransactionType.StockOut,
                                            DocumentNumber = stockAdjustment.Code,
                                            Year = stockAdjustment.Date.Year.ToString(),
                                            ProductId = item.ProductId,
                                            BranchId = stockAdjustment.BranchId,

                                        });


                                        var stockId = _context.Stocks.FirstOrDefault(x => x.ProductId == item.ProductId && x.WareHouseId == item.WarehouseId);
                                        if (stockId == null)
                                        {
                                            var stock = new Stock()
                                            {
                                                ProductId = item.ProductId,
                                                WareHouseId = request.WarehouseId,
                                                CurrentQuantity = -item.Quantity,
                                                BranchId = stockAdjustment.BranchId,
                                            };
                                            _context.Stocks.Add(stock);
                                            stockId = stock;
                                        }
                                        else
                                        {
                                            stockId.AveragePrice = Math.Round(((stockId.AveragePrice * stockId.CurrentQuantity) + ((request.TaxMethod == "Inclusive" || request.TaxMethod == "شامل" ? item.Price * 100 / (100 + stockAdjustment.TaxRate.Rate) : item.Price) * item.Quantity)) / (stockId.CurrentQuantity + item.Quantity), 2);
                                            stockId.CurrentQuantity -= item.Quantity;
                                            stockId.CurrentStockValue = Math.Round(stockId.CurrentQuantity * stockId.AveragePrice, 2);

                                        }
                                        //var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                                        //{
                                        //    ProductId = item.ProductId,
                                        //    StockId = stockId.Id,
                                        //    WareHouseId = request.WarehouseId
                                        //}, cancellationToken);

                                        if (request.IsSerial && item.IsSerial)
                                        {
                                            var serialArray = item.Serial.Split(',');
                                            for (int i = 0; i < item.Quantity; i++)
                                            {
                                                var inv = new Inventory()
                                                {
                                                    Date = DateTime.UtcNow,
                                                    DocumentNumber = stockAdjustment.Code,
                                                    DocumentId = stockAdjustment.Id,
                                                    Quantity = Convert.ToDecimal(item.Quantity),
                                                    Price = Math.Round(item.Price, 2),
                                                    ProductId = item.ProductId,
                                                    Serial = serialArray[i],
                                                    StockId = stockId.Id,
                                                    WareHouseId = request.WarehouseId,
                                                    TransactionType = (TransactionType)request.StockAdjustmentType,
                                                    //AveragePrice = currentInventory.Price == 0 ? item.Price : Math.Round((currentInventory.AveragePrice + item.Price) / 2, 2),
                                                    //CurrentQuantity = currentInventory.CurrentQuantity - item.Quantity,
                                                    //CurrentStockValue = ((currentInventory.CurrentQuantity - item.Quantity) * currentInventory.AveragePrice),
                                                    SalePrice = item.Price,
                                                    BranchId = stockAdjustment.BranchId,
                                                };
                                                await _context.Inventories.AddAsync(inv, cancellationToken);
                                            }
                                        }
                                        else
                                        {
                                            var inv = new Inventory()
                                            {
                                                Date = DateTime.UtcNow,
                                                DocumentNumber = stockAdjustment.Code,
                                                DocumentId = stockAdjustment.Id,
                                                Quantity = Convert.ToDecimal(item.Quantity),
                                                Price = Math.Round(item.Price, 2),
                                                ProductId = item.ProductId,
                                                StockId = stockId.Id,
                                                WareHouseId = request.WarehouseId,
                                                TransactionType = (TransactionType)request.StockAdjustmentType,
                                                //AveragePrice = currentInventory.Price == 0 ? item.Price : Math.Round((currentInventory.AveragePrice + item.Price) / 2, 2),
                                                //CurrentQuantity = currentInventory.CurrentQuantity - item.Quantity,
                                                //CurrentStockValue = ((currentInventory.CurrentQuantity - item.Quantity) * currentInventory.AveragePrice),
                                                SalePrice = item.Price,
                                                BranchId = stockAdjustment.BranchId,
                                            };
                                            await _context.Inventories.AddAsync(inv, cancellationToken);
                                        }
                                    }
                                    transactions.Add(new Transaction
                                    {
                                        DocumentId = stockAdjustment.Id,
                                        DocumentNumber = request.Code,
                                        Date = DateTime.UtcNow,
                                        DocumentDate = request.Date,
                                        Description = request.Narration,
                                        Year = request.Date.Year.ToString(),
                                        AccountId = accountInventory.Id,
                                        TransactionType = (TransactionType)request.StockAdjustmentType,
                                        Credit = inventoryAccountVal,
                                        Debit = 0M,
                                        BranchId = stockAdjustment.BranchId,
                                    });
                                }

                                await _context.Transactions.AddRangeAsync(transactions, cancellationToken);
                                await _context.LedgerTransactions.AddRangeAsync(ledgerTransactions, cancellationToken);

                                await _mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = _context.SyncLog(),
                                    BranchId = stockAdjustment.BranchId,
                                    Code = _code,
                                }, cancellationToken);

                                //Save changes to database
                                await _context.SaveChangesAsync(cancellationToken);

                                scope.Complete();

                                if (stockAdjustment.Id != Guid.Empty)
                                {
                                    var message = new Message
                                    {
                                        Id = stockAdjustment.Id,
                                        IsAddUpdate = "Data Added Successfully"
                                    };
                                    return message;
                                }
                                else
                                {
                                    var message = new Message
                                    {
                                        Id = Guid.Empty,
                                        IsAddUpdate = "Data Not Added Successfully"
                                    };
                                    return message;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    var message = new Message
                    {
                        Id = Guid.Empty,
                        IsAddUpdate = e.Message
                    };
                    return message;
                }
            }
        }
    }
}