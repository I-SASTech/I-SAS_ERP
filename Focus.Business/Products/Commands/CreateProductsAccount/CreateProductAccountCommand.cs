using DocumentFormat.OpenXml.Office2010.Word;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.Products.Commands.AddUpdateProduct;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Products.Commands.CreateProductsAccount
{
    public class CreateProductAccountCommand : IRequest<Message>
    { 
        public bool ProductAccount { get; set; }
        public string Sum { get; set; }
        public class Handler : IRequestHandler<CreateProductAccountCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            public readonly IMediator _Mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<CreateProductAccountCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _Mediator = mediator;
            }
            public async Task<Message> Handle(CreateProductAccountCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.ProductAccount)
                    {
                        var products = await Context.Products.ToListAsync();

                        var accountCount = await Context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "11100001", cancellationToken: cancellationToken);
                        if (accountCount == null)
                            throw new NotFoundException("Account  Code 11100001 ", "");

                        var accountCogs = await Context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "60000101", cancellationToken: cancellationToken);
                        if (accountCogs == null)
                            throw new NotFoundException("Account  Code 60000101 ", "");

                        var accountSale = await Context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "42000001", cancellationToken: cancellationToken);
                        if (accountSale == null)
                            throw new NotFoundException("Account  Code 42000001 ", "");

                        //var accounts = new List<LedgerAccount>();
                        foreach (var item in products)
                        {

                            if (item.InventoryAccountId == null || item.InventoryAccountId == Guid.Empty)
                            {
                                var inv = new LedgerAccount
                                {
                                    Name = item.EnglishName,
                                    NameArabic = item.ArabicName,
                                    Description = item.EnglishName + " " + item.Code,
                                    Code = item.Code,
                                    AccountId = accountCount.Id,
                                    AccountLedgerId = item.Id,
                                    IsActive = item.IsActive
                                };
                                Context.LedgerAccounts.Add(inv);
                                item.InventoryAccountId = inv.Id;
                            }

                            if (item.CogsAccountId == null || item.CogsAccountId == Guid.Empty)
                            {
                                var cogs = new LedgerAccount
                                {
                                    Name = item.EnglishName,
                                    NameArabic = item.ArabicName,
                                    Description = item.EnglishName + " " + item.Code,
                                    Code = item.Code,
                                    AccountId = accountCogs.Id,
                                    AccountLedgerId = item.Id,
                                    IsActive = item.IsActive
                                };
                                Context.LedgerAccounts.Add(cogs);
                                item.CogsAccountId = cogs.Id;
                            }

                            if (item.SaleAccountId == null || item.SaleAccountId == Guid.Empty)
                            {
                                var sale = new LedgerAccount
                                {
                                    Name = item.EnglishName,
                                    NameArabic = item.ArabicName,
                                    Description = item.EnglishName + " " + item.Code,
                                    Code = item.Code,
                                    AccountId = accountSale.Id,
                                    AccountLedgerId = item.Id,
                                    IsActive = item.IsActive
                                };
                                Context.LedgerAccounts.Add(sale);
                                item.SaleAccountId = sale.Id;
                            }
                        }
                        Context.Products.UpdateRange(products);

                        //var log = Context.SyncLog();
                        //var branches = await Context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        //var list = new List<SyncRecordModel>();
                        //foreach (var branch in branches)
                        //{
                        //    var syncRecords = log.Select(x => new SyncRecordModel
                        //    {
                        //        Table = x.Table,
                        //        ColumnId = x.ColumnId,
                        //        CompanyId = x.CompanyId,
                        //        ColumnType = x.ColumnType,
                        //        Push = x.Push,
                        //        Pull = x.Pull,
                        //        Action = x.Action,
                        //        ChangeDate = DateTime.Now,
                        //        PullDate = x.PullDate,
                        //        PushDate = x.PushDate,
                        //        ColumnName = x.ColumnName,
                        //        BranchId = branch.Id,
                        //        Code = _code,
                        //    }).ToList();

                        //    list.AddRange(syncRecords);
                        //}

                        //await _Mediator.Send(new AddUpdateSyncRecordCommand()
                        //{
                        //    SyncRecordModels = list,
                        //    IsServer=true
                        //}, cancellationToken);


                        await Context.SaveChangesAsync(cancellationToken);
                        return new Message
                        {
                            IsAddUpdate = "Data has been added successfully"
                        };
                    }
                    else if (request.Sum == "Sum")
                    {
                        var sales = await Context.Sales
                           .Include(x => x.SaleItems)
                           .ThenInclude(x => x.Product)
                           .ThenInclude(x => x.Category)
                           .OrderBy(x => x.RegistrationNo)
                           .ToListAsync();

                        var accountCount = await Context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "11100001", cancellationToken: cancellationToken);
                        if (accountCount == null)
                            throw new NotFoundException("Account  Code 11100001 ", "");

                        var accountCogs = await Context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "60000101", cancellationToken: cancellationToken);
                        if (accountCogs == null)
                            throw new NotFoundException("Account  Code 60000101 ", "");

                        var accountSale = await Context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "42000001", cancellationToken: cancellationToken);
                        if (accountSale == null)
                            throw new NotFoundException("Account  Code 42000001 ", "");

                        var transactions = await Context.Transactions.Where(x => x.ProductId != null).ToListAsync();

                        int batchSize = 100; 
                        int batchSaleSize = 100; 
                        int batchCount = (int)Math.Ceiling(transactions.Count / (double)batchSize); 

                        int batchCountSale = (int)Math.Ceiling(sales.Count / (double)batchSaleSize);

                        for (int i = 0; i < batchCount; i++)
                        {
                            var batchTransactions = transactions.Skip(i * batchSize).Take(batchSize);


                            for (int j = 0; j < batchCountSale; j++)
                            {
                                var batchSale = sales.Skip(j * batchSaleSize).Take(batchSaleSize);
                                foreach (var sale in batchSale)
                                {
                                    var removeTranscation = new List<Transaction>();
                                    var saveTranscation = new List<Transaction>();

                                    foreach (var item in sale.SaleItems)
                                    {

                                        var inventoryTransaction = batchTransactions.Where(x => x.DocumentId == sale.Id && x.ProductId == item.ProductId && x.AccountId == item.Product.Category.InventoryAccountId).ToList();

                                        foreach (var a in inventoryTransaction)
                                        {
                                            removeTranscation.Add(a);
                                            saveTranscation.Add(new Transaction
                                            {
                                                Id = a.Id,
                                                DocumentId = a.DocumentId,
                                                Date = a.Date,
                                                Description = a.Description,
                                                DocumentDate = a.DocumentDate,
                                                Debit = a.Debit,
                                                DocumentNumber = a.DocumentNumber,
                                                ApprovalDate = a.ApprovalDate,
                                                TransactionType = a.TransactionType,
                                                AccountId = accountCount.Id,
                                                IsArchived = a.IsArchived,
                                                ContactId = a.ContactId,
                                                Credit = a.Credit,
                                                PeriodId = a.PeriodId,
                                                ProductId = a.ProductId,
                                                Year = a.Year,
                                            });
                                        }


                                        var cogsTransaction = batchTransactions.Where(x => x.DocumentId == sale.Id && x.ProductId == item.ProductId && x.AccountId == item.Product.Category.COGSAccountId).ToList();
                                        {
                                            foreach (var b in cogsTransaction)
                                            {
                                                removeTranscation.Add(b);
                                                saveTranscation.Add(new Transaction
                                                {
                                                    Id = b.Id,
                                                    DocumentId = b.DocumentId,
                                                    Date = b.Date,
                                                    Description = b.Description,
                                                    DocumentDate = b.DocumentDate,
                                                    Debit = b.Debit,
                                                    DocumentNumber = b.DocumentNumber,
                                                    ApprovalDate = b.ApprovalDate,
                                                    TransactionType = b.TransactionType,
                                                    AccountId = accountCogs.Id,
                                                    IsArchived = b.IsArchived,
                                                    ContactId = b.ContactId,
                                                    Credit = b.Credit,
                                                    PeriodId = b.PeriodId,
                                                    ProductId = b.ProductId,
                                                    Year = b.Year,
                                                });
                                            }

                                        }

                                        var saleTransaction = batchTransactions.Where(x => x.DocumentId == sale.Id && x.ProductId == item.ProductId && x.AccountId == item.Product.Category.SaleAccountId).ToList();
                                        {
                                            foreach (var c in saleTransaction)
                                            {
                                                removeTranscation.Add(c);
                                                saveTranscation.Add(new Transaction
                                                {
                                                    Id = c.Id,
                                                    DocumentId = c.DocumentId,
                                                    Date = c.Date,
                                                    Description = c.Description,
                                                    DocumentDate = c.DocumentDate,
                                                    Debit = c.Debit,
                                                    DocumentNumber = c.DocumentNumber,
                                                    ApprovalDate = c.ApprovalDate,
                                                    TransactionType = c.TransactionType,
                                                    AccountId = accountSale.Id,
                                                    IsArchived = c.IsArchived,
                                                    ContactId = c.ContactId,
                                                    Credit = c.Credit,
                                                    PeriodId = c.PeriodId,
                                                    ProductId = c.ProductId,
                                                    Year = c.Year,
                                                });
                                            }

                                        }
                                    }



                                    var invAcc = saveTranscation.FirstOrDefault(x => x.AccountId == accountCount.Id);
                                    if (invAcc != null)
                                    {
                                        await Context.Transactions.AddAsync(new Transaction
                                        {
                                            AccountId = invAcc.AccountId,
                                            Debit = saveTranscation.Where(x => x.AccountId == accountCount.Id).Sum(x => x.Debit),
                                            Credit = saveTranscation.Where(x => x.AccountId == accountCount.Id).Sum(x => x.Credit),
                                            DocumentId = invAcc.DocumentId,
                                            Date = invAcc.Date,
                                            Description = invAcc.Description,
                                            DocumentDate = invAcc.DocumentDate,
                                            DocumentNumber = invAcc.DocumentNumber,
                                            ApprovalDate = invAcc.ApprovalDate,
                                            TransactionType = invAcc.TransactionType,
                                            IsArchived = invAcc.IsArchived,
                                            ContactId = invAcc.ContactId,
                                            PeriodId = invAcc.PeriodId,
                                            ProductId = invAcc.ProductId,
                                            Year = invAcc.Year,
                                        });
                                    }
                                    var cogsAcc = saveTranscation.FirstOrDefault(x => x.AccountId == accountCogs.Id);
                                    if (cogsAcc != null)
                                    {
                                        await Context.Transactions.AddAsync(new Transaction
                                        {
                                            AccountId = cogsAcc.AccountId,
                                            Debit = saveTranscation.Where(x => x.AccountId == accountCogs.Id).Sum(x => x.Debit),
                                            Credit = saveTranscation.Where(x => x.AccountId == accountCogs.Id).Sum(x => x.Credit),
                                            DocumentId = cogsAcc.DocumentId,
                                            Date = cogsAcc.Date,
                                            Description = cogsAcc.Description,
                                            DocumentDate = cogsAcc.DocumentDate,
                                            DocumentNumber = cogsAcc.DocumentNumber,
                                            ApprovalDate = cogsAcc.ApprovalDate,
                                            TransactionType = cogsAcc.TransactionType,
                                            IsArchived = cogsAcc.IsArchived,
                                            ContactId = cogsAcc.ContactId,
                                            PeriodId = cogsAcc.PeriodId,
                                            ProductId = cogsAcc.ProductId,
                                            Year = cogsAcc.Year,
                                        });
                                    }
                                    var saleAcc = saveTranscation.FirstOrDefault(x => x.AccountId == accountSale.Id);
                                    if (saleAcc != null)
                                    {
                                        await Context.Transactions.AddAsync(new Transaction
                                        {
                                            AccountId = saleAcc.AccountId,
                                            Debit = saveTranscation.Where(x => x.AccountId == accountSale.Id).Sum(x => x.Debit),
                                            Credit = saveTranscation.Where(x => x.AccountId == accountSale.Id).Sum(x => x.Credit),
                                            DocumentId = saleAcc.DocumentId,
                                            Date = saleAcc.Date,
                                            Description = saleAcc.Description,
                                            DocumentDate = saleAcc.DocumentDate,
                                            DocumentNumber = saleAcc.DocumentNumber,
                                            ApprovalDate = saleAcc.ApprovalDate,
                                            TransactionType = saleAcc.TransactionType,
                                            IsArchived = saleAcc.IsArchived,
                                            ContactId = saleAcc.ContactId,
                                            PeriodId = saleAcc.PeriodId,
                                            ProductId = saleAcc.ProductId,
                                            Year = saleAcc.Year,
                                        });
                                    }

                                    Context.Transactions.RemoveRange(removeTranscation);


                                    var log = Context.SyncLog();
                                    var branches = await Context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                                    var list = new List<SyncRecordModel>();
                                    foreach (var branch in branches)
                                    {
                                        var syncRecords = log.Select(x => new SyncRecordModel
                                        {
                                            Table = x.Table,
                                            ColumnId = x.ColumnId,
                                            CompanyId = x.CompanyId,
                                            ColumnType = x.ColumnType,
                                            Push = x.Push,
                                            Pull = x.Pull,
                                            Action = x.Action,
                                            ChangeDate = DateTime.Now,
                                            PullDate = x.PullDate,
                                            PushDate = x.PushDate,
                                            ColumnName = x.ColumnName,
                                            BranchId = branch.Id,
                                            Code = _code,
                                        }).ToList();

                                        list.AddRange(syncRecords);
                                    }

                                    //await _Mediator.Send(new AddUpdateSyncRecordCommand()
                                    //{
                                    //    SyncRecordModels = list,
                                    //    IsServer=true
                                    //}, cancellationToken);

                                    await Context.SaveChangesAsync(cancellationToken);
                                }

                            }


                        }

                       
                        return new Message
                        {
                            IsAddUpdate = "Data has been added successfully"
                        };
                    }
                    else if(request.Sum == "PurchasePost")
                    {
                        var ledgerAccounts = await Context.LedgerAccounts.AsNoTracking().ToListAsync();
                        if(ledgerAccounts == null)
                        {
                            return new Message
                            {
                                IsSuccess = false,
                                IsAddUpdate = "First You need to create Ledger Accounts"
                            };
                        }

                        var purchasePosts = await Context.PurchasePosts.AsNoTracking().Include(x => x.PurchasePostItems).ToListAsync();

                        var ledgerTransactions = await Context.LedgerTransactions.AsNoTracking().Where(x => x.TransactionType == TransactionType.PurchasePost).ToListAsync();

                       

                        var ledgerTransactionList = new List<LedgerTransaction>();

                        foreach (var purchase in purchasePosts)
                        {
                            var ledgerTrans = ledgerTransactions.FirstOrDefault(x => x.DocumentNumber == purchase.RegistrationNo);

                            if(ledgerTrans == null)
                            {
                                foreach (var purchasePostItem in purchase.PurchasePostItems)
                                {
                                    var ledgerAccount = ledgerAccounts.FirstOrDefault(x => x.AccountLedgerId == purchasePostItem.ProductId);

                                    ledgerTransactionList.Add(new LedgerTransaction()
                                    {
                                        DocumentId = purchase.Id,
                                        DocumentNumber = purchase.RegistrationNo,
                                        TransactionType = TransactionType.PurchasePost,
                                        Date = purchase.Date,
                                        DocumentDate = purchase.Date,
                                        Description = TransactionType.PurchasePost.ToString(),
                                        Year = purchase.Date.Year.ToString(),
                                        Debit = purchasePostItem.TotalWithOutAmount,
                                        Credit = 0,
                                        AccountId = ledgerAccount.Id,
                                        ApprovalDate = purchase.Date,
                                        ContactId = purchase.SupplierId,
                                        PeriodId = purchase.PeriodId,
                                        ProductId = purchasePostItem.ProductId,
                                        IsArchived = false,
                                    });
                                }
                            }
                            
                        }

                        await Context.LedgerTransactions.AddRangeAsync(ledgerTransactionList);


                        var log = Context.SyncLog();
                        var branches = await Context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        var list = new List<SyncRecordModel>();
                        foreach (var branch in branches)
                        {
                            var syncRecords = log.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }

                        await _Mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer=true
                        }, cancellationToken);


                        await Context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            IsSuccess = true,
                            IsAddUpdate = "Data has been added successfully"
                        };
                    }
                    else
                    {
                        var products = await Context.Products.Include(x => x.Category).ToListAsync();


                        var transactions = await Context.Transactions.Where(x => x.TransactionType == TransactionType.SaleInvoice).ToListAsync();
                        var ledgerTransactionList = new List<LedgerTransaction>();

                        foreach (var item in products)
                        {
                            var inventoryTransactions = transactions.Where(x => x.ProductId == item.Id && x.AccountId == item.Category.InventoryAccountId);
                            foreach (var inventoryTransaction in inventoryTransactions)
                            {
                                ledgerTransactionList.Add(new LedgerTransaction
                                {
                                    DocumentId = inventoryTransaction.DocumentId,
                                    DocumentNumber = inventoryTransaction.DocumentNumber,
                                    TransactionType = inventoryTransaction.TransactionType,
                                    Date = inventoryTransaction.Date,
                                    Description = inventoryTransaction.Description,
                                    Year = inventoryTransaction.Year,
                                    Debit = inventoryTransaction.Debit,
                                    Credit = inventoryTransaction.Credit,
                                    AccountId = item.InventoryAccountId,
                                    ContactId = inventoryTransaction.ContactId,
                                    ProductId = inventoryTransaction.ProductId,
                                    IsArchived = inventoryTransaction.IsArchived,
                                    ApprovalDate = inventoryTransaction.ApprovalDate,
                                    DocumentDate = inventoryTransaction.DocumentDate,
                                    PeriodId = inventoryTransaction.PeriodId,

                                });
                            }

                            var cogsTransactions = transactions.Where(x => x.ProductId == item.Id && x.AccountId == item.Category.COGSAccountId);
                            foreach (var cogsTransaction in cogsTransactions)
                            {
                                ledgerTransactionList.Add(new LedgerTransaction
                                {
                                    DocumentId = cogsTransaction.DocumentId,
                                    DocumentNumber = cogsTransaction.DocumentNumber,
                                    TransactionType = cogsTransaction.TransactionType,
                                    Date = cogsTransaction.Date,
                                    Description = cogsTransaction.Description,
                                    Year = cogsTransaction.Year,
                                    Debit = cogsTransaction.Debit,
                                    Credit = cogsTransaction.Credit,
                                    AccountId = item.CogsAccountId,
                                    ContactId = cogsTransaction.ContactId,
                                    ProductId = cogsTransaction.ProductId,
                                    IsArchived = cogsTransaction.IsArchived,
                                    ApprovalDate = cogsTransaction.ApprovalDate,
                                    DocumentDate = cogsTransaction.DocumentDate,
                                    PeriodId = cogsTransaction.PeriodId,

                                });
                            }

                            var saleTransactions = transactions.Where(x => x.ProductId == item.Id && x.AccountId == item.Category.SaleAccountId);
                            foreach (var saleTransaction in saleTransactions)
                            {
                                ledgerTransactionList.Add(new LedgerTransaction
                                {
                                    DocumentId = saleTransaction.DocumentId,
                                    DocumentNumber = saleTransaction.DocumentNumber,
                                    TransactionType = saleTransaction.TransactionType,
                                    Date = saleTransaction.Date,
                                    Description = saleTransaction.Description,
                                    Year = saleTransaction.Year,
                                    Debit = saleTransaction.Debit,
                                    Credit = saleTransaction.Credit,
                                    AccountId = item.SaleAccountId,
                                    ContactId = saleTransaction.ContactId,
                                    ProductId = saleTransaction.ProductId,
                                    IsArchived = saleTransaction.IsArchived,
                                    ApprovalDate = saleTransaction.ApprovalDate,
                                    DocumentDate = saleTransaction.DocumentDate,
                                    PeriodId = saleTransaction.PeriodId,

                                });
                            }

                        }

                        await Context.LedgerTransactions.AddRangeAsync(ledgerTransactionList);


                        var log = Context.SyncLog();
                        var branches = await Context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        var list = new List<SyncRecordModel>();
                        foreach (var branch in branches)
                        {
                            var syncRecords = log.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }

                        await _Mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer=true
                        }, cancellationToken);


                        await Context.SaveChangesAsync(cancellationToken);
                        return new Message
                        {
                            IsSuccess = true,
                            IsAddUpdate = "Data has been added successfully"
                        };
                    }
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
