using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.DailyExpenses.Queries.GetDailyExpense;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DailyExpenses.Commands.AddUpdateDailyExpense
{
    public class AddUpdateDailyExpenseCommand : IRequest<Message>
    {
        public DailyExpenseVm ExpenseVm { get; set; }

        public class Handler : IRequestHandler<AddUpdateDailyExpenseCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateDailyExpenseCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AddUpdateDailyExpenseCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var period = await _context.CompanySubmissionPeriods.FirstOrDefaultAsync(x => x.PeriodStart <= request.ExpenseVm.Date.Date && x.PeriodEnd >= request.ExpenseVm.Date.Date, cancellationToken: cancellationToken);
                    if (period == null)
                        throw new NotFoundException("Financial Year Not Found", "");


                    var newAccount = new Account();
                    var vatAccount = new Account();

                    if (request.ExpenseVm.Id == Guid.Empty)
                    {
                        var voucherNumber = String.Empty;
                        if (request.ExpenseVm.ApprovalStatus == ApprovalStatus.Draft)
                        {
                            var invoiceNo = await _mediator.Send(new GetDailyExpenseNumberQuery()
                            {
                                BranchId = request.ExpenseVm.BranchId,
                                ApprovalStatus = ApprovalStatus.Draft
                            }, cancellationToken);
                            voucherNumber = invoiceNo;
                        }
                        if (request.ExpenseVm.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            var invoiceNo = await _mediator.Send(new GetDailyExpenseNumberQuery()
                            {
                                BranchId = request.ExpenseVm.BranchId,
                                ApprovalStatus = ApprovalStatus.Approved
                            }, cancellationToken);
                            voucherNumber = invoiceNo;
                        }


                        var expense = new DailyExpense()
                        {
                            ExpenseDate = request.ExpenseVm.ExpenseDate,
                            VoucherNo = voucherNumber,
                            Date = request.ExpenseVm.Date,
                            GrossAmount = request.ExpenseVm.GrossAmount,
                            TotalAmount = request.ExpenseVm.TotalAmount,
                            TotalVat = request.ExpenseVm.TotalVat,
                            IsExpenseAccount = request.ExpenseVm.IsExpenseAccount,
                            Description = request.ExpenseVm.Description,
                            ApprovalStatus = request.ExpenseVm.ApprovalStatus,
                            Reason = request.ExpenseVm.Reason,
                            IsDraft = request.ExpenseVm.IsDraft,
                            Name = request.ExpenseVm.Name,
                            TaxId = request.ExpenseVm.TaxId,
                            ReferenceNo = request.ExpenseVm.ReferenceNo,
                            AccountId = request.ExpenseVm.AccountId,
                            PaymentMode = request.ExpenseVm.PaymentMode,
                            BranchId = request.ExpenseVm.BranchId,
                            BillerAccountId = request.ExpenseVm.BillerAccountId == Guid.Empty ? null : request.ExpenseVm.BillerAccountId,
                            DailyExpenseDetails = request.ExpenseVm.DailyExpenseDetails.Select(x =>
                                new DailyExpenseDetail()
                                {
                                    Id = x.Id,
                                    TaxMethod = x.TaxMethod,
                                    VatId = x.VatId,
                                    Description = x.Description,
                                    Amount = x.Amount,
                                    ExpenseAccountId = x.ExpenseAccountId
                                }).ToList()
                        };

                        await _context.DailyExpenses.AddAsync(expense, cancellationToken);

                        if (request.ExpenseVm.TemporaryCashIssueId != null)
                        {
                            var temporaryCashIssueExpense = new TemporaryCashIssueExpense()
                            {
                                TemporaryCashIssueId = request.ExpenseVm.TemporaryCashIssueId.Value,
                                DocumentId = expense.Id,
                                DocumentType = "Expenses"
                            };
                            await _context.TemporaryCashIssueExpenses.AddAsync(temporaryCashIssueExpense, cancellationToken);
                        }

                        if (request.ExpenseVm.AttachmentList is { Count: > 0 })
                        {
                            foreach (var item in request.ExpenseVm.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = expense.Id,
                                    Description = item.Description,
                                    FileName = item.FileName,
                                    Path = item.Path
                                };
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);
                            }
                        }


                        if (request.ExpenseVm.ExpenseAttachment != null)
                        {
                            foreach (var x in request.ExpenseVm.ExpenseAttachment)
                            {
                                if (!string.IsNullOrEmpty(x.Path))
                                {
                                    var expenseAttachment = new DailyExpenseAttachment()
                                    {
                                        DailyExpenseId = expense.Id,
                                        Path = x.Path,
                                        Date = x.Date,
                                        Description = x.Description,
                                        Name = x.Name
                                    };
                                    await _context.DailyExpenseAttachments.AddAsync(expenseAttachment, cancellationToken);
                                }
                            }
                        }

                        if (request.ExpenseVm.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            request.ExpenseVm.ApprovalDate = DateTime.UtcNow;
                            if (request.ExpenseVm.BillerAccountId != Guid.Empty && request.ExpenseVm.BillerAccountId != null)
                            {
                                var biller = await _context.PurchaseBills.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.ExpenseVm.BillerAccountId, cancellationToken: cancellationToken);
                                if (biller != null)
                                {
                                    var expenseAmount = request.ExpenseVm.DailyExpenseDetails.Sum(x => x.Amount);
                                    if (expenseAmount >= biller.TotalAmount)
                                    {
                                        biller.PartiallyInvoices = PartiallyInvoices.Fully;
                                        biller.RemainingAmount = expenseAmount;
                                        _context.PurchaseBills.Update(biller);
                                    }
                                    else if (expenseAmount < biller.TotalAmount)
                                    {
                                        if (biller.RemainingAmount == 0)
                                        {
                                            biller.PartiallyInvoices = PartiallyInvoices.Partially;
                                            biller.RemainingAmount = expenseAmount;
                                            _context.PurchaseBills.Update(biller);
                                        }
                                        else
                                        {
                                            var total = expenseAmount + biller.RemainingAmount;
                                            if (total >= biller.TotalAmount)
                                            {
                                                biller.PartiallyInvoices = PartiallyInvoices.Fully;
                                                biller.RemainingAmount = total;
                                                _context.PurchaseBills.Update(biller);
                                            }
                                            else
                                            {
                                                biller.PartiallyInvoices = PartiallyInvoices.Partially;
                                                biller.RemainingAmount = expenseAmount;
                                                _context.PurchaseBills.Update(biller);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        biller.PartiallyInvoices = PartiallyInvoices.UnPaid;
                                    }

                                }
                            }
                            var accounts = await _context.Accounts.AsNoTracking().Include(x => x.CostCenter).ToListAsync(cancellationToken: cancellationToken);
                            var vats = await _context.TaxRates.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                            Guid? cashAccountId = default;
                            Guid? expenseAccount = default;
                            if (request.ExpenseVm.AccountId == null)
                            {
                                if (request.ExpenseVm.IsDayStart)
                                {
                                    var terminalAccount = await _context.Terminals.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.ExpenseVm.CounterId, cancellationToken);
                                    if (terminalAccount.CashAccountId != null)
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
                                expenseAccount = accounts.FirstOrDefault(x => x.Code == "60505001")?.Id;
                                if (expenseAccount == null)
                                    throw new NotFoundException("Expense Account Not Found", null);
                            }
                            else
                            {
                                cashAccountId = request.ExpenseVm.AccountId;
                            }
                            //var cashAccount = accounts.FirstOrDefault(x => x.Code == "10100001");
                            //if (cashAccount == null)
                            //    throw new NotFoundException("Cash Account Account Not Found", null);



                            {
                                var accountType = await _context.AccountTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Expenses" || x.NameArabic == "المصاريف", cancellationToken: cancellationToken);
                                var costCenter = accounts.FirstOrDefault(x => x.CostCenter.Code == "610000");
                                if (costCenter == null)
                                {
                                    var newCostCenter = new CostCenter()
                                    {
                                        Code = "610000",
                                        Name = "Expense Adjustment",
                                        NameArabic = "تعديل المصاريف",
                                        Description = "Expense Adjustment",
                                        IsActive = true,
                                        AccountTypeId = accountType.Id
                                    };
                                    await _context.CostCenters.AddAsync(newCostCenter, cancellationToken);
                                    newAccount = new Account
                                    {
                                        Code = "61000001",
                                        Name = "Expense Adjustment",
                                        NameArabic = "تعديل المصاريف",
                                        Description = "Expense Adjustment",
                                        IsActive = true,
                                        CostCenterId = newCostCenter.Id
                                    };
                                    await _context.Accounts.AddAsync(newAccount, cancellationToken);
                                }
                                var vatExpense = accounts.FirstOrDefault(x => x.Code == "1300002");
                                if (vatExpense == null)
                                {
                                    var expenseCostCenter = accounts.FirstOrDefault(x => x.CostCenter.Code == "130000");
                                    vatAccount = new Account
                                    {
                                        Code = "1300002",
                                        Name = "VAT Paid On Expense",
                                        NameArabic = "دفع ضريبة القيمة المضافة على المصاريف",
                                        Description = "VAT Paid On Expense",
                                        IsActive = true,
                                        CostCenterId = expenseCostCenter.CostCenterId
                                    };
                                    await _context.Accounts.AddAsync(vatAccount, cancellationToken);
                                }
                            }


                            var list = new List<Transaction>();
                            foreach (var item in request.ExpenseVm.DailyExpenseDetails)
                            {
                                var vatIds = vats.FirstOrDefault(x => x.Id == item.VatId);
                                if (item.TaxMethod == "Exclusive" || item.TaxMethod == "غير شامل")
                                {
                                    if (vatIds == null)
                                    {
                                        throw new NotFoundException("You Select Tax Method but not Select VAT", null);
                                    }
                                    if (cashAccountId == Guid.Empty)
                                    {
                                        throw new NotFoundException("You not Select  Cash/Bank Account", null);
                                    }

                                    list.Add(new Transaction
                                    {
                                        AccountId = (Guid)cashAccountId,
                                        DocumentId = expense.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = expense.VoucherNo,
                                        Description = item.Description,
                                        Debit = 0m,
                                        Credit = item.Amount + Math.Round((item.Amount * vatIds.Rate) / 100, 2),
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = expense.BranchId,
                                    });
                                    list.Add(new Transaction
                                    {
                                        AccountId = (Guid)(expenseAccount ?? item.ExpenseAccountId),
                                        DocumentId = expense.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = expense.VoucherNo,
                                        Description = item.Description,
                                        Debit = item.Amount + Math.Round((item.Amount * vatIds.Rate) / 100, 2),
                                        Credit = 0m,
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = expense.BranchId,
                                    });
                                    list.Add(new Transaction
                                    {
                                        AccountId = accounts.FirstOrDefault(x => x.Code == "1300002")?.Id ?? vatAccount.Id,
                                        DocumentId = expense.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = expense.VoucherNo,
                                        Description = "VAT Account Debit in Daily Expense",
                                        Debit = Math.Round((item.Amount * vatIds.Rate) / 100, 2),
                                        Credit = 0m,
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = expense.BranchId,
                                    });
                                    list.Add(new Transaction
                                    {
                                        AccountId = accounts.FirstOrDefault(x => x.Code == "61000001")?.Id ?? newAccount.Id,
                                        DocumentId = expense.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = expense.VoucherNo,
                                        Description = item.Description,
                                        Debit = 0m,
                                        Credit = Math.Round((item.Amount * vatIds.Rate) / 100, 2),
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = expense.BranchId,
                                    });
                                }
                                else if (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                {
                                    if (vatIds == null)
                                    {
                                        throw new NotFoundException("You Select Tax Method but not Select VAT", null);
                                    }
                                    if (cashAccountId == Guid.Empty)
                                    {
                                        throw new NotFoundException("You not Select  Cash/Bank Account", null);
                                    }
                                    list.Add(new Transaction
                                    {
                                        AccountId = (Guid)cashAccountId,
                                        DocumentId = expense.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = expense.VoucherNo,
                                        Description = item.Description,
                                        Debit = 0m,
                                        Credit = item.Amount,
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = expense.BranchId,
                                    });
                                    list.Add(new Transaction
                                    {
                                        AccountId = (Guid)(expenseAccount ?? item.ExpenseAccountId),
                                        DocumentId = expense.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = expense.VoucherNo,
                                        Description = item.Description,
                                        Debit = item.Amount,
                                        Credit = 0m,
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = expense.BranchId,
                                    });
                                    list.Add(new Transaction
                                    {
                                        AccountId = accounts.FirstOrDefault(x => x.Code == "1300002")?.Id ?? vatAccount.Id,
                                        DocumentId = expense.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = expense.VoucherNo,
                                        Description = "VAT Account Debit in Daily Expense",
                                        Debit = Math.Round((item.Amount * vatIds.Rate) / (100 + vatIds.Rate), 2),
                                        Credit = 0m,
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = expense.BranchId,
                                    });
                                    list.Add(new Transaction
                                    {
                                        AccountId = accounts.FirstOrDefault(x => x.Code == "61000001")?.Id ?? newAccount.Id,
                                        DocumentId = expense.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = expense.VoucherNo,
                                        Description = item.Description,
                                        Debit = 0m,
                                        Credit = Math.Round((item.Amount * vatIds.Rate) / (100 + vatIds.Rate), 2),
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = expense.BranchId,
                                    });
                                }
                                else
                                {
                                    if (cashAccountId == Guid.Empty)
                                    {
                                        throw new NotFoundException("You not Select  Cash/Bank Account", null);
                                    }

                                    list.Add(new Transaction
                                    {
                                        AccountId = (Guid)(expenseAccount ?? item.ExpenseAccountId),
                                        DocumentId = expense.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = expense.VoucherNo,
                                        Description = item.Description,
                                        Debit = item.Amount,
                                        Credit = 0m,
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = expense.BranchId,
                                    });
                                    list.Add(new Transaction
                                    {
                                        AccountId = (Guid)cashAccountId,
                                        DocumentId = expense.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = expense.VoucherNo,
                                        Description = item.Description,
                                        Debit = 0m,
                                        Credit = item.Amount,
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = expense.BranchId,
                                    });
                                }
                            }
                            await _context.Transactions.AddRangeAsync(list, cancellationToken);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            DocumentNo = request.ExpenseVm.VoucherNo,
                            BranchId = expense.BranchId,
                            Code = _code,
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Product id after successfully updating data
                        var message = new Message
                        {
                            Id = expense.Id,
                            IsAddUpdate = "Data Added Successfully"
                        };
                        return message;
                    }
                    else
                    {
                        var de = await _context.DailyExpenses.FindAsync(request.ExpenseVm.Id);
                        if (de == null)
                            throw new NotFoundException("Daily Expense Not Found", "");

                        if(!de.VoucherNo.StartsWith("DE"))
                        {
                            if (request.ExpenseVm.ApprovalStatus == ApprovalStatus.Approved)
                            {
                                request.ExpenseVm.ApprovalDate = DateTime.UtcNow;
                                de.VoucherNo = await _mediator.Send(new GetDailyExpenseNumberQuery()
                                {
                                    BranchId = request.ExpenseVm.BranchId,
                                    ApprovalStatus = ApprovalStatus.Approved
                                }, cancellationToken);
                            }
                        }

                        de.Date = request.ExpenseVm.Date;
                        de.GrossAmount = request.ExpenseVm.GrossAmount;
                        de.TotalAmount = request.ExpenseVm.TotalAmount;
                        de.TotalVat = request.ExpenseVm.TotalVat;
                        de.IsExpenseAccount = request.ExpenseVm.IsExpenseAccount;
                        de.Description = request.ExpenseVm.Description;
                        de.IsDraft = request.ExpenseVm.IsDraft;
                        de.ApprovalStatus = request.ExpenseVm.ApprovalStatus;
                        de.BillerAccountId = request.ExpenseVm.BillerAccountId == Guid.Empty ? null : request.ExpenseVm.BillerAccountId;
                        de.Reason = request.ExpenseVm.Reason;
                        de.PaymentMode = request.ExpenseVm.PaymentMode;
                        de.ReferenceNo = request.ExpenseVm.ReferenceNo;
                        de.TaxId = request.ExpenseVm.TaxId;
                        de.Name = request.ExpenseVm.Name;
                        de.ExpenseDate = request.ExpenseVm.ExpenseDate;

                        _context.DailyExpenseAttachments.RemoveRange(de.DailyExpenseAttachments);

                        if (request.ExpenseVm.AttachmentList is { Count: > 0 })
                        {
                            var attachments = await _context.Attachments
                                .AsNoTracking()
                                .Where(x => x.DocumentId == de.Id)
                                .ToListAsync(cancellationToken: cancellationToken);

                            if (attachments.Any())
                            {
                                _context.Attachments.RemoveRange(attachments);
                            }
                            foreach (var item in request.ExpenseVm.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = de.Id,
                                    Description = item.Description,
                                    FileName = item.FileName,
                                    Path = item.Path
                                };
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);
                            }
                        }


                        foreach (var x in request.ExpenseVm.ExpenseAttachment)
                        {
                            if (!string.IsNullOrEmpty(x.Path))
                            {
                                var expenseAttachment = new DailyExpenseAttachment()
                                {
                                    DailyExpenseId = de.Id,
                                    Path = x.Path,
                                    Date = x.Date,
                                    Description = x.Description,
                                    Name = x.Name
                                };
                                await _context.DailyExpenseAttachments.AddAsync(expenseAttachment, cancellationToken);
                            }
                        }
                        _context.DailyExpenseDetails.RemoveRange(de.DailyExpenseDetails);


                        foreach (var item in request.ExpenseVm.DailyExpenseDetails)
                        {
                            var deitem = new DailyExpenseDetail()
                            {
                                Amount = item.Amount,
                                VatId = item.VatId,
                                TaxMethod = item.TaxMethod,
                                Description = item.Description,
                                ExpenseAccountId = item.ExpenseAccountId,
                                DailyExpenseId = de.Id
                            };
                            await _context.DailyExpenseDetails.AddAsync(deitem, cancellationToken);
                        }

                        if (request.ExpenseVm.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            if (request.ExpenseVm.BillerAccountId != Guid.Empty && request.ExpenseVm.BillerAccountId != null)
                            {
                                var biller = await _context.PurchaseBills.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.ExpenseVm.BillerAccountId, cancellationToken: cancellationToken);
                                if (biller != null)
                                {
                                    var expenseAmount = request.ExpenseVm.DailyExpenseDetails.Sum(x => x.Amount);
                                    if (expenseAmount >= biller.TotalAmount)
                                    {
                                        biller.PartiallyInvoices = PartiallyInvoices.Fully;
                                        biller.RemainingAmount = expenseAmount;
                                        _context.PurchaseBills.Update(biller);
                                    }
                                    else if (expenseAmount < biller.TotalAmount)
                                    {
                                        if (biller.RemainingAmount == 0)
                                        {
                                            biller.PartiallyInvoices = PartiallyInvoices.Partially;
                                            biller.RemainingAmount = expenseAmount;
                                            _context.PurchaseBills.Update(biller);
                                        }
                                        else
                                        {
                                            var total = expenseAmount + biller.RemainingAmount;
                                            if (total >= biller.TotalAmount)
                                            {
                                                biller.PartiallyInvoices = PartiallyInvoices.Fully;
                                                biller.RemainingAmount = total;
                                                _context.PurchaseBills.Update(biller);
                                            }
                                            else
                                            {
                                                biller.PartiallyInvoices = PartiallyInvoices.Partially;
                                                biller.RemainingAmount = expenseAmount;
                                                _context.PurchaseBills.Update(biller);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        biller.PartiallyInvoices = PartiallyInvoices.UnPaid;
                                    }

                                }
                            }

                            var transactions = await _context.Transactions.Where(x => x.DocumentNumber == request.ExpenseVm.VoucherNo).ToListAsync();
                            if(transactions.Count > 0) { }
                            {
                                _context.Transactions.RemoveRange(transactions);
                            }

                            var accounts = _context.Accounts.AsQueryable();
                            var vats = _context.TaxRates.AsNoTracking().AsQueryable();

                            {
                                var accountType = await _context.AccountTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Name == "Expenses" || x.NameArabic == "المصاريف", cancellationToken: cancellationToken);
                                var costCenter = accounts.FirstOrDefault(x => x.CostCenter.Code == "610000");
                                if (costCenter == null)
                                {
                                    var newCostCenter = new CostCenter()
                                    {
                                        Code = "610000",
                                        Name = "Expense Adjustment",
                                        NameArabic = "تعديل المصاريف",
                                        Description = "Expense Adjustment",
                                        IsActive = true,
                                        AccountTypeId = accountType.Id
                                    };
                                    await _context.CostCenters.AddAsync(newCostCenter, cancellationToken);
                                    newAccount = new Account
                                    {
                                        Code = "61000001",
                                        Name = "Expense Adjustment",
                                        NameArabic = "تعديل المصاريف",
                                        Description = "Expense Adjustment",
                                        IsActive = true,
                                        CostCenterId = newCostCenter.Id
                                    };
                                    await _context.Accounts.AddAsync(newAccount, cancellationToken);
                                }
                                var vatExpense = accounts.FirstOrDefault(x => x.Code == "1300002");
                                if (vatExpense == null)
                                {
                                    var expenseCostCenter = accounts.FirstOrDefault(x => x.CostCenter.Code == "130000");
                                    vatAccount = new Account
                                    {
                                        Code = "1300002",
                                        Name = "VAT Paid On Expense",
                                        NameArabic = "دفع ضريبة القيمة المضافة على المصاريف",
                                        Description = "VAT Paid On Expense",
                                        IsActive = true,
                                        CostCenterId = expenseCostCenter.CostCenterId
                                    };
                                    await _context.Accounts.AddAsync(vatAccount, cancellationToken);
                                }



                            }
                            Guid? cashAccountId = default;
                            Guid? expenseAccount = default;
                            if (request.ExpenseVm.AccountId == null)
                            {
                                if (request.ExpenseVm.IsDayStart)
                                {
                                    var terminalAccount = await _context.Terminals.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.ExpenseVm.CounterId, cancellationToken);
                                    if (terminalAccount.CashAccountId != null)
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

                                expenseAccount = accounts.FirstOrDefault(x => x.Code == "60505001").Id;
                            }
                            else
                            {
                                cashAccountId = request.ExpenseVm.AccountId;
                            }
                            var list = new List<Transaction>();
                            foreach (var item in request.ExpenseVm.DailyExpenseDetails)
                            {
                                var vatIds = vats.FirstOrDefault(x => x.Id == item.VatId);
                                if (item.TaxMethod == "Exclusive" || item.TaxMethod == "غير شامل")
                                {
                                    list.Add(new Transaction
                                    {
                                        AccountId = (Guid)cashAccountId,
                                        DocumentId = de.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = de.VoucherNo,
                                        Description = item.Description,
                                        Debit = 0m,
                                        Credit = item.Amount,
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = de.BranchId,
                                    });
                                    list.Add(new Transaction
                                    {
                                        AccountId = (Guid)(expenseAccount == null ? item.ExpenseAccountId : expenseAccount),
                                        DocumentId = de.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = de.VoucherNo,
                                        Description = item.Description,
                                        Debit = item.Amount,
                                        Credit = 0m,
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = de.BranchId,
                                    });
                                    list.Add(new Transaction
                                    {
                                        AccountId = accounts.FirstOrDefault(x => x.Code == "1300002")?.Id ?? vatAccount.Id,
                                        DocumentId = de.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = de.VoucherNo,
                                        Description = "VAT Account Debit in Daily Expense",
                                        Debit = Math.Round((item.Amount * vatIds.Rate) / 100, 2),
                                        Credit = 0m,
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = de.BranchId,
                                    });
                                    list.Add(new Transaction
                                    {
                                        AccountId = accounts.FirstOrDefault(x => x.Code == "61000001")?.Id ?? newAccount.Id,
                                        DocumentId = de.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = de.VoucherNo,
                                        Description = item.Description,
                                        Debit = 0m,
                                        Credit = Math.Round((item.Amount * vatIds.Rate) / 100, 2),
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = de.BranchId,
                                    });
                                }
                                else if (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                {
                                    list.Add(new Transaction
                                    {
                                        AccountId = (Guid)cashAccountId,
                                        DocumentId = de.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = de.VoucherNo,
                                        Description = item.Description,
                                        Debit = 0m,
                                        Credit = item.Amount,
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = de.BranchId,
                                    });
                                    list.Add(new Transaction
                                    {
                                        AccountId = (Guid)(expenseAccount == null ? item.ExpenseAccountId : expenseAccount),
                                        DocumentId = de.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = de.VoucherNo,
                                        Description = item.Description,
                                        Debit = item.Amount,
                                        Credit = 0m,
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = de.BranchId,
                                    });
                                    list.Add(new Transaction
                                    {
                                        AccountId = accounts.FirstOrDefault(x => x.Code == "1300002")?.Id ?? vatAccount.Id,
                                        DocumentId = de.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = de.VoucherNo,
                                        Description = "VAT Account Debit in Daily Expense",
                                        Debit = Math.Round((item.Amount * vatIds.Rate) / (100 + vatIds.Rate), 2),
                                        Credit = 0m,
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = de.BranchId,
                                    });
                                    list.Add(new Transaction
                                    {
                                        AccountId = accounts.FirstOrDefault(x => x.Code == "61000001")?.Id ?? newAccount.Id,
                                        DocumentId = de.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = de.VoucherNo,
                                        Description = item.Description,
                                        Debit = 0m,
                                        Credit = Math.Round((item.Amount * vatIds.Rate) / (100 + vatIds.Rate), 2),
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = de.BranchId,
                                    });
                                }
                                else
                                {
                                    list.Add(new Transaction
                                    {
                                        AccountId = (Guid)(expenseAccount == null ? item.ExpenseAccountId : expenseAccount),
                                        DocumentId = de.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = de.VoucherNo,
                                        Description = item.Description,
                                        Debit = item.Amount,
                                        Credit = 0m,
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = de.BranchId,
                                    });
                                    list.Add(new Transaction
                                    {
                                        AccountId = (Guid)cashAccountId,
                                        DocumentId = de.Id,
                                        Date = request.ExpenseVm.ExpenseDate == null ? DateTime.UtcNow : Convert.ToDateTime(request.ExpenseVm.ExpenseDate),
                                        DocumentDate = request.ExpenseVm.Date,
                                        ApprovalDate = request.ExpenseVm.ApprovalDate,
                                        DocumentNumber = de.VoucherNo,
                                        Description = item.Description,
                                        Debit = 0m,
                                        Credit = item.Amount,
                                        TransactionType = TransactionType.ExpenseVoucher,
                                        PeriodId = period.Id,
                                        Year = period.Year,
                                        BranchId = de.BranchId
                                    });
                                }
                            }
                            await _context.Transactions.AddRangeAsync(list, cancellationToken);
                        }
                        //if (request.ExpenseVm.ApprovalStatus == ApprovalStatus.Rejected && request.ExpenseVm.PaymentType== "Credit")
                        //{
                        //    var userId = await _context.DailyExpenses.Where(x => x.Id == request.ExpenseVm.Id).Select(x => new
                        //    {
                        //        Userid = EF.Property<Guid>(x, "UserId")
                        //    }).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                        //    var userInfoGetEmployee = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId.Userid.ToString(), cancellationToken: cancellationToken);
                        //    if (userInfoGetEmployee == null)
                        //        throw new NotFoundException("employee Not found", userId);

                        //    var employeeInfo =await 
                        //        _context.EmployeeRegistrations.FirstOrDefaultAsync(x =>
                        //            x.Id == userInfoGetEmployee.EmployeeId, cancellationToken: cancellationToken);
                        //    if (employeeInfo==null)
                        //        throw new NotFoundException("employee Not found", userId);


                        //    //var cashAccount = accounts.FirstOrDefault(x => x.Code == "10100001");
                        //    if(cashAccountId == null)
                        //        throw new NotFoundException("Cash Account Not Found", null);
                        //    //if (employeeInfo.EmployeeAccountId != null)
                        //    //{
                        //    //    var list = new List<Transaction>();
                        //    //    foreach (var item in request.ExpenseVm.DailyExpenseDetails)
                        //    //    {
                        //    //        //Employee Account Debit In Rejection
                        //    //       if(request.ExpenseVm.PaymentType == "Credit")
                        //    //            list.Add(new Transaction
                        //    //            {
                        //    //                AccountId = employeeInfo.PayableAccountId.Value,
                        //    //                DocumentId = de.Id,
                        //    //                Date = de.Date,
                        //    //                DocumentNumber = de.VoucherNo,
                        //    //                Description = item.Description,
                        //    //                Debit = item.Amount,
                        //    //                Credit = 0m,
                        //    //                TransactionType = Domain.Enum.TransactionType.ExpenseVoucher,
                        //    //                Year = DateTime.UtcNow.Year.ToString()
                        //    //            });
                        //    //        //Cash Account Credit In Rejection
                        //    //        list.Add(new Transaction
                        //    //        {
                        //    //            AccountId = cashAccountId,
                        //    //            DocumentId = de.Id,
                        //    //            Date = de.Date,
                        //    //            DocumentNumber = de.VoucherNo,
                        //    //            Description = item.Description,
                        //    //            Debit = 0m,
                        //    //            Credit = item.Amount,
                        //    //            TransactionType = Domain.Enum.TransactionType.ExpenseVoucher,
                        //    //            Year = DateTime.UtcNow.Year.ToString()
                        //    //        });
                        //    //    }
                        //    //    await _context.Transactions.AddRangeAsync(list, cancellationToken);
                        //    //}
                        //}

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            DocumentNo = request.ExpenseVm.VoucherNo,
                            BranchId = de.BranchId,
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        var message = new Message
                        {
                            Id = request.ExpenseVm.Id,
                            IsAddUpdate = "Data has been updated successfully"
                        };
                        return message;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }
        }
    }
}