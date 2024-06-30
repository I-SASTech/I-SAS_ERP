using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Attachments.Commands;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Transactions.JVTransaction;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Transaction = Focus.Domain.Entities.Transaction;

namespace Focus.Business.JournalVouchers.Commands.AddUpdateJournalVoucher
{
    public class AddUpdateJournalVoucherCommand : IRequest<Message>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string VoucherNumber { get; set; }
        public string Comments { get; set; }
        public bool OpeningCash { get; set; }
        public bool View { get; set; }
        public bool OneTimeEntry { get; set; }
        public string Narration { get; set; }
        public virtual ICollection<JournalVoucherItem> JournalVoucherItems { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<AddUpdateJournalVoucherCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<AddUpdateJournalVoucherCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AddUpdateJournalVoucherCommand request, CancellationToken cancellationToken)
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

                    if (request.Id != Guid.Empty)
                    {
                        var jv = await _context.JournalVouchers.FindAsync(request.Id);

                        if (jv == null)
                            throw new NotFoundException("Journal Voucher Not Found", "");

                        if (request.View)
                        {
                            if (request.AttachmentList != null)
                            {
                                var attachments = _context.Attachments
                                    .AsNoTracking()
                                    .Where(x => x.DocumentId == jv.Id)
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
                                        DocumentId = jv.Id,
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
                                DocumentNo = request.VoucherNumber,
                                BranchId = jv.BranchId,
                            }, cancellationToken);
                            await _context.SaveChangesAsync(cancellationToken);
                            return new Message
                            {
                                Id = jv.Id,
                                IsAddUpdate = "Data has been updated successfully"
                            };
                        }
                        else
                        {
                            if (jv.ApprovalStatus == ApprovalStatus.Approved)
                            {
                                var transactions = await _context.Transactions.AsNoTracking().Where(x => x.DocumentId == jv.Id).ToListAsync(cancellationToken: cancellationToken);
                                _context.Transactions.RemoveRange(transactions);
                            }

                            jv.Narration = request.Narration;
                            jv.OpeningCash = request.OpeningCash;
                            jv.VoucherNumber = request.VoucherNumber;
                            jv.Date = request.Date;
                            jv.ApprovalStatus = request.ApprovalStatus;
                            jv.Comments = request.Comments;

                            if (request.AttachmentList != null)
                            {
                                var attachments = _context.Attachments
                                    .AsNoTracking()
                                    .Where(x => x.DocumentId == jv.Id)
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
                                        DocumentId = jv.Id,
                                        Description = item.Description,
                                        FileName = item.FileName,
                                        Path = item.Path
                                    };
                                    //Add Attachments to database
                                    await _context.Attachments.AddAsync(attachment, cancellationToken);
                                }
                            }

                            
                            var transactionList = new List<Transaction>();
                            if (request.ApprovalStatus == ApprovalStatus.Approved)
                            {

                                jv.PeriodId = period.Id;

                                //New Transaction

                                foreach (var item in request.JournalVoucherItems)
                                {
                                    item.JournalVoucherId = jv.Id;
                                    if (request.ApprovalStatus == ApprovalStatus.Approved)
                                    {
                                        var transaction = new Transaction
                                        {
                                            TransactionType = TransactionType.JournalVoucher,
                                            DocumentId = jv.Id,
                                            DocumentNumber = jv.VoucherNumber,
                                            Date = DateTime.UtcNow,
                                            DocumentDate = request.Date,
                                            ApprovalDate = DateTime.UtcNow,
                                            Year = period.Year,
                                            AccountId = item.AccountId,
                                            Description = item.Description is null or "" ? TransactionType.JournalVoucher.ToString() : item.Description,
                                            Debit = item.Debit,
                                            Credit = item.Credit,
                                            PeriodId = period.Id,
                                            BranchId = jv.BranchId
                                        };
                                        transactionList.Add(transaction);
                                    }
                                }
                            }

                            _context.JournalVoucherItems.RemoveRange(jv.JournalVoucherItems);

                            foreach (var item in request.JournalVoucherItems)
                            {
                                var jvItem = new JournalVoucherItem()
                                {
                                    Debit = item.Debit,
                                    PaymentMode = item.PaymentMode,
                                    PaymentMethod = item.PaymentMethod,
                                    ChequeNo = item.ChequeNo,
                                    Credit = item.Credit,
                                    AccountId = item.AccountId,
                                    Description = item.Description,
                                    JournalVoucherId = jv.Id
                                };

                                await _context.JournalVoucherItems.AddAsync(jvItem, cancellationToken);
                            }


                            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);


                            if (transactionList.Count > 0)
                            {
                                await _mediator.Send(new PostTransactionCommand
                                {
                                    Transactions = transactionList,
                                    DocumentNo = request.VoucherNumber,
                                    Code = _code,

                                }, cancellationToken);
                            }

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                                DocumentNo = request.VoucherNumber,
                                BranchId = jv.BranchId,
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);
                            transactionScope.Complete();
                            var message = new Message
                            {
                                Id = jv.Id,
                                IsSuccess = true,
                                IsAddUpdate = "Data has been updated successfully"
                            };
                            return message;
                        }
                    }
                    else
                    {

                        var isJvExist = _context.JournalVouchers.Any(x => x.VoucherNumber == request.VoucherNumber);
                        if (isJvExist)
                            throw new ObjectAlreadyExistsException("Journal Voucher Already Exist");

                        var journalVoucher = new JournalVoucher
                        {
                            Date = request.Date,
                            OpeningCash = request.OpeningCash,
                            VoucherNumber = request.VoucherNumber,
                            Narration = request.Narration,
                            Comments = request.Comments,
                            ApprovalStatus = request.ApprovalStatus,
                            BranchId = request.BranchId,
                        };
                        if (request.OpeningCash)
                        {
                            journalVoucher.OneTimeEntry = true;
                        }

                        if (request.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            journalVoucher.PeriodId = period.Id;
                        }
                        await _context.JournalVouchers.AddAsync(journalVoucher, cancellationToken);

                        if (request.AttachmentList is { Count: > 0 })
                        {
                            foreach (var item in request.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = journalVoucher.Id,
                                    Description = item.Description,
                                    FileName = item.FileName,
                                    Path = item.Path
                                };
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);
                            }
                        }


                        var transactionList = new List<Transaction>();
                        foreach (var item in request.JournalVoucherItems)
                        {
                            item.JournalVoucherId = journalVoucher.Id;
                            await _context.JournalVoucherItems.AddAsync(item, cancellationToken);

                            if (request.ApprovalStatus == ApprovalStatus.Approved)
                            {
                                var transaction = new Transaction
                                {
                                    TransactionType = TransactionType.JournalVoucher,
                                    DocumentId = journalVoucher.Id,
                                    DocumentNumber = journalVoucher.VoucherNumber,
                                    Date = DateTime.UtcNow,
                                    DocumentDate = request.Date,
                                    ApprovalDate = DateTime.UtcNow,
                                    AccountId = item.AccountId,
                                    Description = string.IsNullOrEmpty(item.Description) ? TransactionType.JournalVoucher.ToString() : item.Description,
                                    Year = period.Year,
                                    Debit = item.Debit,
                                    Credit = item.Credit,
                                    PeriodId = period.Id,
                                    BranchId = journalVoucher.BranchId,
                                };
                                transactionList.Add(transaction);
                            }
                        }

                        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        if (transactionList.Count > 0)
                        {
                            await _mediator.Send(new PostTransactionCommand
                            {
                                Transactions = transactionList,
                                DocumentNo = request.VoucherNumber,
                                Code = _code,
                            }, cancellationToken);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo = request.VoucherNumber,
                            BranchId = journalVoucher.BranchId,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        transactionScope.Complete();
                        var message = new Message
                        {
                            Id = journalVoucher.Id,
                            IsSuccess = true,
                            IsAddUpdate = "Data has been added successfully"
                        };
                        return message;
                    }
                }
                
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return new Message
                    {
                        IsAddUpdate = e.Message
                    };
                }
            }
        }
    }
}
