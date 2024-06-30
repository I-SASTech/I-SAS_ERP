using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.CreditNotes.Model;
using Focus.Business.Exceptions;
using Focus.Business.Inventories.Queries.GetLatestInventory;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Transaction = Focus.Domain.Entities.Transaction;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.EmailConfigurationSetup.Model;
using Focus.Business.Models;
using System.Text;

namespace Focus.Business.CreditNotes.Commands
{
    public class AddUpdateCreditNoteCommand : IRequest<Message>
    {
        public CreditNotesModel CreditNotes { get; set; }

        public class Handler : IRequestHandler<AddUpdateCreditNoteCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private readonly IUserHttpContextProvider _contextProvider;
            private string _code;
            private readonly ISendEmail sendEmail;
            public Handler(IApplicationDbContext context, ILogger<AddUpdateCreditNoteCommand> logger, IMediator mediator, IUserHttpContextProvider contextProvider, ISendEmail _sendEmail)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
                _contextProvider = contextProvider;
                sendEmail = _sendEmail;
            }

            public async Task<Message> Handle(AddUpdateCreditNoteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    // Check Financial Year
                    var period = await _context.CompanySubmissionPeriods.AsNoTracking().FirstOrDefaultAsync(x => x.PeriodStart.Year == request.CreditNotes.Date.Year && x.PeriodStart.Month == request.CreditNotes.Date.Month, cancellationToken: cancellationToken);

                    if (period == null)
                        throw new NotFoundException("Financial Year Not Found", "");

                    if (period.IsPeriodClosed)
                        throw new ApplicationException("Financial year period closed");


                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.CreditNotes.Id == Guid.Empty)
                    {
                        var saleOrder = new CreditNote()
                        {
                            Code = request.CreditNotes.Code,
                            Date = request.CreditNotes.Date,
                            ContactId = request.CreditNotes.ContactId,
                            SaleId = request.CreditNotes.SaleId,
                            PurchasePostId = request.CreditNotes.PurchasePostId,
                            IsInventoryTransaction = request.CreditNotes.IsInventoryTransaction,
                            IsItemDescription = request.CreditNotes.IsItemDescription,
                            Narration = request.CreditNotes.Narration,
                            GrossAmount = request.CreditNotes.GrossAmount,
                            VatAmount = request.CreditNotes.VatAmount,
                            Amount = request.CreditNotes.Amount,
                            TaxMethod = request.CreditNotes.TaxMethod,
                            TaxRateId = request.CreditNotes.TaxRateId,
                            TerminalId = request.CreditNotes.TerminalId,
                            IsCreditNote = request.CreditNotes.IsCreditNote,
                            ApprovalStatus = request.CreditNotes.ApprovalStatus,
                            WareHouseId = request.CreditNotes.WareHouseId,
                            IsService = request.CreditNotes.IsService,
                            BranchId = request.CreditNotes.BranchId,

                            CreatedBy = _contextProvider.GetUserId(),
                            CreditNoteItems = request.CreditNotes.SaleOrderItems.Select(x => new CreditNoteItem()
                            {
                                ProductId = x.ProductId,
                                TaxRateId = x.TaxRateId,
                                TaxMethod = x.TaxMethod,
                                WareHouseId = request.CreditNotes.WareHouseId,
                                Discount = x.Discount,
                                FixDiscount = x.FixDiscount,
                                Quantity = x.Quantity,
                                UnitPrice = x.UnitPrice,
                                Description = x.Description,
                                ServiceItem = x.ServiceItem,
                                IsFree = x.IsFree,
                                TotalAmount = x.TotalAmount,
                                VatAmount = x.VatAmount,
                                DiscountAmount = x.DiscountAmount,
                                TotalWithOutAmount = x.GrossAmount,
                            }).ToList()
                        };

                        await _context.CreditNotes.AddAsync(saleOrder, cancellationToken);


                        //Add Attachments
                        if (request.CreditNotes.AttachmentList is { Count: > 0 })
                        {
                            foreach (var item in request.CreditNotes.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = saleOrder.Id,
                                    Description = item.Description,
                                    FileName = item.FileName,
                                    Path = item.Path
                                };
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);
                            }
                        }


                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                        decimal inventoryAccount = 0;
                        decimal cogsAccount = 0;

                        if (request.CreditNotes.ApprovalStatus == ApprovalStatus.Approved && request.CreditNotes.IsCreditNote)
                        {

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                BranchId = saleOrder.BranchId,
                                Code = _code,
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);


                            var transactionList = new List<Transaction>();
                            

                            var creditNote = await (from goods in _context.CreditNotes
                                                    select new
                                                    {
                                                        ContactAccoutId = goods.Contact.AccountId,
                                                        goods.Id,
                                                        goods.Code,
                                                        goods.GrossAmount,
                                                        goods.VatAmount,
                                                        goods.Amount,
                                                        goods.BranchId,
                                                        SaleItems = goods.CreditNoteItems.Select(x => new
                                                        {
                                                            x.ProductId,
                                                            x.Description,
                                                            x.ServiceItem,
                                                            x.IsFree,
                                                            x.TaxRateId,
                                                            x.TaxMethod,
                                                            x.WareHouseId,
                                                            x.Discount,
                                                            x.FixDiscount,
                                                            x.Quantity,
                                                            x.UnitPrice,
                                                            VatRate = x.TaxRate.Rate,
                                                            x.Product.InventoryAccountId,
                                                            x.Product.CogsAccountId,
                                                            x.Product.SaleAccountId,
                                                        }).ToList(),
                                                    }).FirstOrDefaultAsync(x => x.Id == saleOrder.Id, cancellationToken: cancellationToken);

                            var accounts = await (from account in _context.Accounts
                                                  where account.Code == "42000001" || account.Code == "1300001" || account.Code == "25000001" || account.Code == "11100001" || account.Code == "60000101"
                                                  select new
                                                  {
                                                      account.Id,
                                                      account.Code,
                                                  }).ToListAsync(cancellationToken: cancellationToken);

                            if (request.CreditNotes.IsInventoryTransaction)
                            {
                                foreach (var item in creditNote.SaleItems)
                                {
                                    var stock = saleOrder.BranchId==null ? await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == request.CreditNotes.WareHouseId, cancellationToken: cancellationToken) : await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == request.CreditNotes.WareHouseId && x.BranchId==saleOrder.BranchId, cancellationToken: cancellationToken);

                                    if (stock == null)
                                    {
                                        var newStock = new Stock()
                                        {
                                            ProductId = item.ProductId.Value,
                                            WareHouseId = request.CreditNotes.WareHouseId ?? Guid.Empty,
                                            CurrentQuantity = item.Quantity,
                                            BranchId = creditNote.BranchId
                                        };
                                        await _context.Stocks.AddAsync(newStock, cancellationToken);
                                        stock = newStock;
                                    }
                                    else
                                    {
                                        stock.CurrentQuantity += item.Quantity;
                                    }

                                    var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                                    {
                                        ProductId = item.ProductId.Value,
                                        StockId = stock.Id,
                                        WareHouseId = request.CreditNotes.WareHouseId ?? Guid.Empty
                                    }, cancellationToken);


                                    var inv = new Inventory()
                                    {
                                        Date = request.CreditNotes.Date,
                                        DocumentId = creditNote.Id,
                                        DocumentNumber = creditNote.Code,
                                        Quantity = item.Quantity,
                                        Price = Math.Round(item.UnitPrice, 2),
                                        ProductId = item.ProductId.Value,
                                        StockId = stock.Id,
                                        WareHouseId = stock.WareHouseId,
                                        TransactionType = TransactionType.SaleReturn,
                                        AveragePrice = currentInventory.AveragePrice,
                                        ExpiryDate = currentInventory.ExpiryDate,
                                        CurrentQuantity = currentInventory.CurrentQuantity + item.Quantity,
                                        CurrentStockValue = ((currentInventory.CurrentQuantity + item.Quantity) * currentInventory.AveragePrice)
                                        ,
                                        BranchId = creditNote.BranchId
                                    };
                                    await _context.Inventories.AddAsync(inv, cancellationToken);

                                    inventoryAccount += currentInventory.AveragePrice * item.Quantity;
                                    cogsAccount += Math.Abs(Math.Round(currentInventory.AveragePrice * item.Quantity, 4));
                                }


                                //inventory Account
                                transactionList.Add(new Transaction()
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.CreditNotes.Date,
                                    ContactId = request.CreditNotes.ContactId,
                                    AccountId = accounts.FirstOrDefault(x => x.Code == "11100001")?.Id,
                                    Credit = 0,
                                    Debit = inventoryAccount,
                                    Description = TransactionType.SaleReturn.ToString(),
                                    DocumentId = creditNote.Id,
                                    TransactionType = TransactionType.SaleReturn,
                                    DocumentNumber = creditNote.Code,
                                    Year = request.CreditNotes.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = creditNote.BranchId
                                });

                                //cost of good sale Account
                                transactionList.Add(new Transaction()
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.CreditNotes.Date,
                                    ContactId = request.CreditNotes.ContactId,
                                    AccountId = accounts.FirstOrDefault(x => x.Code == "60000101")?.Id,
                                    Debit = 0,
                                    Credit = cogsAccount,
                                    Description = TransactionType.SaleReturn.ToString(),
                                    DocumentId = creditNote.Id,
                                    TransactionType = TransactionType.SaleReturn,
                                    DocumentNumber = creditNote.Code,
                                    Year = request.CreditNotes.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = creditNote.BranchId
                                });
                            }

                            var taxRate = await _context.TaxRates.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.CreditNotes.TaxRateId, cancellationToken: cancellationToken);
                            //sale Account
                            transactionList.Add(new Transaction
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = request.CreditNotes.Date,
                                ContactId = request.CreditNotes.ContactId,
                                AccountId = accounts.FirstOrDefault(x => x.Code == "42000001")?.Id,
                                Debit = Math.Round(request.CreditNotes.TaxMethod == "Exclusive" || request.CreditNotes.TaxMethod == "غير شامل" ? creditNote.GrossAmount : creditNote.GrossAmount * 100 / (100 + taxRate?.Rate ?? 0), 4),
                                Credit = 0,
                                Description = TransactionType.SaleInvoice.ToString(),
                                DocumentId = creditNote.Id,
                                TransactionType = TransactionType.SaleInvoice,
                                DocumentNumber = creditNote.Code,
                                Year = request.CreditNotes.Date.Year.ToString(),
                                PeriodId = period.Id,
                                BranchId = creditNote.BranchId
                            });

                            //vat
                            transactionList.Add(new Transaction()
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = request.CreditNotes.Date,
                                AccountId = accounts.FirstOrDefault(x => x.Code == "25000001").Id,
                                Credit = 0,
                                Debit = creditNote.VatAmount,
                                Description = TransactionType.SaleInvoice.ToString(),
                                DocumentId = creditNote.Id,
                                TransactionType = TransactionType.SaleInvoice,
                                DocumentNumber = creditNote.Code,
                                Year = period.Year,
                                PeriodId = period.Id,
                                BranchId = creditNote.BranchId
                            });

                            //account Payable
                            transactionList.Add(new Transaction
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = request.CreditNotes.Date,
                                AccountId = creditNote.ContactAccoutId,
                                Debit = 0,
                                Credit = creditNote.Amount,
                                Description = TransactionType.SaleInvoice.ToString(),
                                DocumentId = creditNote.Id,
                                TransactionType = TransactionType.SaleInvoice,
                                DocumentNumber = creditNote.Code,
                                Year = period.Year,
                                PeriodId = period.Id,
                                BranchId = creditNote.BranchId
                            });



                            if (transactionList.Count > 0)
                            {
                                await _context.Transactions.AddRangeAsync(transactionList, cancellationToken);
                            }

                        }

                        if (request.CreditNotes.ApprovalStatus == ApprovalStatus.Approved && !request.CreditNotes.IsCreditNote)
                        {
                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                BranchId = saleOrder.BranchId,
                                Code = _code,
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);

                            var transactionList = new List<Transaction>();
                            

                            var creditNote = await (from goods in _context.CreditNotes
                                                    select new
                                                    {
                                                        ContactAccoutId = goods.Contact.AccountId,
                                                        goods.Id,
                                                        goods.Code,
                                                        goods.GrossAmount,
                                                        goods.VatAmount,
                                                        goods.Amount,
                                                        goods.BranchId,
                                                        SaleItems = goods.CreditNoteItems.Select(x => new
                                                        {
                                                            x.ProductId,
                                                            x.TaxRateId,
                                                            x.ServiceItem,
                                                            x.Description,
                                                            x.IsFree,
                                                            x.TaxMethod,
                                                            x.WareHouseId,
                                                            x.Discount,
                                                            x.FixDiscount,
                                                            x.Quantity,
                                                            x.UnitPrice,
                                                            VatRate = x.TaxRate.Rate,
                                                            x.Product.InventoryAccountId,
                                                            x.Product.CogsAccountId,
                                                            x.Product.SaleAccountId,
                                                        }).ToList(),
                                                    }).FirstOrDefaultAsync(x => x.Id == saleOrder.Id, cancellationToken: cancellationToken);

                            var accounts = await (from account in _context.Accounts
                                                  where account.Code == "42000001" || account.Code == "1300001" || account.Code == "25000001" || account.Code == "11100001" || account.Code == "60000101"
                                                  select new
                                                  {
                                                      account.Id,
                                                      account.Code,
                                                  }).ToListAsync(cancellationToken: cancellationToken);

                            if (request.CreditNotes.IsInventoryTransaction)
                            {
                                foreach (var item in creditNote.SaleItems)
                                {
                                    var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.WareHouseId == request.CreditNotes.WareHouseId, cancellationToken: cancellationToken);

                                    if (stock == null)
                                    {
                                        var newStock = new Stock()
                                        {
                                            ProductId = item.ProductId.Value,
                                            WareHouseId = request.CreditNotes.WareHouseId ?? Guid.Empty,
                                            CurrentQuantity = -item.Quantity,
                                            BranchId = creditNote.BranchId,
                                        };
                                        await _context.Stocks.AddAsync(newStock, cancellationToken);
                                        stock = newStock;
                                    }
                                    else
                                    {
                                        stock.CurrentQuantity -= item.Quantity;
                                    }

                                    var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                                    {
                                        ProductId = item.ProductId.Value,
                                        StockId = stock.Id,
                                        WareHouseId = request.CreditNotes.WareHouseId ?? Guid.Empty
                                    }, cancellationToken);


                                    var inv = new Inventory()
                                    {
                                        Date = request.CreditNotes.Date,
                                        DocumentId = creditNote.Id,
                                        DocumentNumber = creditNote.Code,
                                        Quantity = item.Quantity,
                                        Price = Math.Round(item.UnitPrice, 2),
                                        ProductId = item.ProductId.Value,
                                        StockId = stock.Id,
                                        WareHouseId = stock.WareHouseId,
                                        TransactionType = TransactionType.SaleReturn,
                                        AveragePrice = currentInventory.AveragePrice,
                                        ExpiryDate = currentInventory.ExpiryDate,
                                        CurrentQuantity = currentInventory.CurrentQuantity - item.Quantity,
                                        CurrentStockValue = ((currentInventory.CurrentQuantity - item.Quantity) * currentInventory.AveragePrice),
                                        BranchId = creditNote.BranchId
                                    };

                                    await _context.Inventories.AddAsync(inv, cancellationToken);

                                    ////inventory Account
                                    //transactionList.Add(new Transaction()
                                    //{
                                    //    Date = DateTime.UtcNow,
                                    //    DocumentDate = request.CreditNotes.Date,
                                    //    ContactId = request.CreditNotes.ContactId,
                                    //    AccountId = item.InventoryAccountId,
                                    //    Credit = currentInventory.AveragePrice * item.Quantity,
                                    //    Debit = 0,
                                    //    Description = TransactionType.SaleReturn.ToString(),
                                    //    DocumentId = creditNote.Id,
                                    //    TransactionType = TransactionType.SaleReturn,
                                    //    DocumentNumber = creditNote.Code,
                                    //    Year = request.CreditNotes.Date.Year.ToString(),
                                    //    ProductId = item.ProductId,
                                    //    PeriodId = period.Id
                                    //});

                                    ////cost of good sale Account
                                    //transactionList.Add(new Transaction()
                                    //{
                                    //    Date = DateTime.UtcNow,
                                    //    DocumentDate = request.CreditNotes.Date,
                                    //    ContactId = request.CreditNotes.ContactId,
                                    //    AccountId = item.CogsAccountId,
                                    //    Debit = Math.Abs(Math.Round(currentInventory.AveragePrice * item.Quantity, 4)),
                                    //    Credit = 0,
                                    //    Description = TransactionType.SaleReturn.ToString(),
                                    //    DocumentId = creditNote.Id,
                                    //    TransactionType = TransactionType.SaleReturn,
                                    //    DocumentNumber = creditNote.Code,
                                    //    Year = request.CreditNotes.Date.Year.ToString(),
                                    //    ProductId = item.ProductId,
                                    //    PeriodId = period.Id
                                    //});
                                    inventoryAccount += currentInventory.AveragePrice * item.Quantity;
                                    cogsAccount += Math.Abs(Math.Round(currentInventory.AveragePrice * item.Quantity, 4));
                                }

                                //inventory Account
                                transactionList.Add(new Transaction()
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.CreditNotes.Date,
                                    ContactId = request.CreditNotes.ContactId,
                                    AccountId = accounts.FirstOrDefault(x => x.Code == "11100001")?.Id,
                                    Debit = 0,
                                    Credit = inventoryAccount,
                                    Description = TransactionType.SaleReturn.ToString(),
                                    DocumentId = creditNote.Id,
                                    TransactionType = TransactionType.SaleReturn,
                                    DocumentNumber = creditNote.Code,
                                    Year = request.CreditNotes.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = creditNote.BranchId
                                });

                                //cost of good sale Account
                                transactionList.Add(new Transaction()
                                {
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.CreditNotes.Date,
                                    ContactId = request.CreditNotes.ContactId,
                                    AccountId = accounts.FirstOrDefault(x => x.Code == "60000101")?.Id,
                                    Credit = 0,
                                    Debit = cogsAccount,
                                    Description = TransactionType.SaleReturn.ToString(),
                                    DocumentId = creditNote.Id,
                                    TransactionType = TransactionType.SaleReturn,
                                    DocumentNumber = creditNote.Code,
                                    Year = request.CreditNotes.Date.Year.ToString(),
                                    PeriodId = period.Id,
                                    BranchId = creditNote.BranchId
                                });
                            }

                            var taxRate = await _context.TaxRates.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.CreditNotes.TaxRateId, cancellationToken: cancellationToken);
                            //sale Account
                            transactionList.Add(new Transaction
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = request.CreditNotes.Date,
                                ContactId = request.CreditNotes.ContactId,
                                AccountId = accounts.FirstOrDefault(x => x.Code == "42000001")?.Id,
                                Credit = Math.Round(request.CreditNotes.TaxMethod == "Exclusive" || request.CreditNotes.TaxMethod == "غير شامل" ? creditNote.GrossAmount : creditNote.GrossAmount * 100 / (100 + taxRate?.Rate ?? 0), 4),
                                Debit = 0,
                                Description = TransactionType.SaleInvoice.ToString(),
                                DocumentId = creditNote.Id,
                                TransactionType = TransactionType.SaleInvoice,
                                DocumentNumber = creditNote.Code,
                                Year = request.CreditNotes.Date.Year.ToString(),
                                PeriodId = period.Id,
                                BranchId = creditNote.BranchId
                            });

                            //vat
                            transactionList.Add(new Transaction()
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = request.CreditNotes.Date,
                                AccountId = accounts.FirstOrDefault(x => x.Code == "1300001").Id,
                                Debit = 0,
                                Credit = creditNote.VatAmount,
                                Description = TransactionType.SaleInvoice.ToString(),
                                DocumentId = creditNote.Id,
                                TransactionType = TransactionType.SaleInvoice,
                                DocumentNumber = creditNote.Code,
                                Year = period.Year,
                                PeriodId = period.Id,
                                BranchId = creditNote.BranchId
                            });

                            //account Payable
                            transactionList.Add(new Transaction
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = request.CreditNotes.Date,
                                AccountId = creditNote.ContactAccoutId,
                                Credit = 0,
                                Debit = creditNote.Amount,
                                Description = TransactionType.SaleInvoice.ToString(),
                                DocumentId = creditNote.Id,
                                TransactionType = TransactionType.SaleInvoice,
                                DocumentNumber = creditNote.Code,
                                Year = period.Year,
                                PeriodId = period.Id,
                                BranchId = creditNote.BranchId
                            });


                            if (transactionList.Count > 0)
                            {
                                await _context.Transactions.AddRangeAsync(transactionList, cancellationToken);
                            }
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            BranchId = saleOrder.BranchId,
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        
                        scope.Complete();
                        return new Message()
                        {
                            Id = saleOrder.Id
                        };
                    }

                    return null;
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException(e.Message);
                }
                finally
                {
                    Requests.IsDuplicateSale = false;
                }
            }

        }
    }
}
