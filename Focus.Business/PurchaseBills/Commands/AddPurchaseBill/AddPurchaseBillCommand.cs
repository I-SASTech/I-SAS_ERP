using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Exceptions;
using Focus.Business.Transactions.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.PurchaseBills.Commands.AddPurchaseBill
{
    public class AddPurchaseBillCommand : IRequest<Guid>
    {

        public PurchaseBillLookupModel PurchaseBill { get; set; }
        public class Handler : IRequestHandler<AddPurchaseBillCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;
            private readonly ILogger _logger;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddPurchaseBillCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddPurchaseBillCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.PurchaseBill.Id == Guid.Empty)
                    {

                        var purchaseBill = new PurchaseBill()
                        {
                            Date = request.PurchaseBill.Date,
                            DueDate = request.PurchaseBill.DueDate,
                            RegistrationNo = request.PurchaseBill.RegistrationNo,
                            Narration = request.PurchaseBill.Narration,
                            Reference = request.PurchaseBill.Reference,
                            TaxMethod = request.PurchaseBill.TaxMethod,
                            ApprovalStatus = request.PurchaseBill.ApprovalStatus,
                            BillerId = request.PurchaseBill.BillerId,
                            TotalAmount = request.PurchaseBill.PurchaseBillItems.Sum(x => x.Amount),
                            RemainingAmount = request.PurchaseBill.RemainingAmount,
                            PartiallyInvoices = PartiallyInvoices.UnPaid,
                            BranchId = request.PurchaseBill.BranchId,
                            BillDate = request.PurchaseBill.BillDate,
                            PurchaseBillItems = request.PurchaseBill.PurchaseBillItems.Select(x =>
                                new PurchaseBillItem()
                                {
                                    Description = x.Description,
                                    Amount = x.Amount,
                                    AccountId = x.AccountId,
                                    PurchaseBillId = x.PurchaseBillId
                                }).ToList()
                        };
                        await _context.PurchaseBills.AddAsync(purchaseBill, cancellationToken);


                        if (request.PurchaseBill.AttachmentList is { Count: > 0 })
                        {
                            foreach (var item in request.PurchaseBill.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = purchaseBill.Id,
                                    Description = item.Description,
                                    FileName = item.FileName,
                                    Path = item.Path
                                };
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);
                            }
                        }

                        foreach (var x in request.PurchaseBill.BillAttachments)
                        {
                            if (!string.IsNullOrEmpty(x.Path))
                            {
                                var billAttachment = new BillAttachment()
                                {
                                    PurchaseBillId = purchaseBill.Id,
                                    Path = x.Path,
                                    Date = x.Date,
                                    Description = x.Description,
                                    Name = x.Name
                                };
                                await _context.BillAttachments.AddAsync(billAttachment, cancellationToken);
                            }
                        }

                        if (request.PurchaseBill.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            request.PurchaseBill.ApprovalDate = DateTime.UtcNow;
                            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                            var transactions = new Collection<TransactionLookupModel>();
                            foreach (var item in purchaseBill.PurchaseBillItems)
                            {
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = request.PurchaseBill.Date,
                                    DocumentDate = request.PurchaseBill.BillDate,
                                    ApprovalDate = request.PurchaseBill.ApprovalDate,
                                    AccountId = item.AccountId,
                                    Debit = item.Amount,
                                    Credit = 0,
                                    Description = "Expense Line Item Account",
                                    DocumentId = purchaseBill.Id,
                                    TransactionType = TransactionType.Expense,
                                    DocumentNumber = request.PurchaseBill.RegistrationNo,
                                    Year = request.PurchaseBill.Date.Year.ToString(),
                                    BranchId = purchaseBill.BranchId,
                                });
                            }
                            transactions.Add(new TransactionLookupModel
                            {
                                Date = request.PurchaseBill.Date,
                                DocumentDate = request.PurchaseBill.BillDate,
                                ApprovalDate = request.PurchaseBill.ApprovalDate,
                                AccountId = request.PurchaseBill.BillerId,
                                Debit = 0,
                                Credit = request.PurchaseBill.PurchaseBillItems.Sum(x => x.Amount),
                                Description = "Expense Head Account ",
                                DocumentId = purchaseBill.Id,
                                TransactionType = TransactionType.Expense,
                                DocumentNumber = request.PurchaseBill.RegistrationNo,
                                Year = request.PurchaseBill.Date.Year.ToString(),
                                BranchId = purchaseBill.BranchId,
                            });

                            foreach (var transaction in transactions)
                            {
                                await _mediator.Send(new AddTransactionCommand
                                {
                                    Transaction = transaction,
                                    BranchId = purchaseBill.BranchId,
                                    Code = _code
                                }, cancellationToken);
                            }

                            scope.Complete();
                        }


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            BranchId = request.PurchaseBill.BranchId,
                            DocumentNo = purchaseBill.RegistrationNo
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);
                        // Return Product id after successfully updating data
                        return purchaseBill.Id;
                    }
                    else
                    {
                        var purchase = await _context.PurchaseBills.FindAsync(request.PurchaseBill.Id);
                        if (purchase == null)
                        {
                            throw new NotFoundException("Purchase Bills ", "");
                        }

                        purchase.Date = request.PurchaseBill.Date;
                        purchase.DueDate = request.PurchaseBill.DueDate;
                        purchase.RegistrationNo = request.PurchaseBill.RegistrationNo;
                        purchase.Narration = request.PurchaseBill.Narration;
                        purchase.Reference = request.PurchaseBill.Reference;
                        purchase.TaxMethod = request.PurchaseBill.TaxMethod;
                        purchase.ApprovalStatus = request.PurchaseBill.ApprovalStatus;
                        purchase.BillerId = request.PurchaseBill.BillerId;
                        purchase.TotalAmount = request.PurchaseBill.PurchaseBillItems.Sum(x => x.Amount);
                        purchase.RemainingAmount = request.PurchaseBill.RemainingAmount;
                        purchase.PartiallyInvoices = PartiallyInvoices.UnPaid;
                        purchase.BillDate = request.PurchaseBill.BillDate;

                        _context.PurchaseBillItems.RemoveRange(purchase.PurchaseBillItems);
                        purchase.PurchaseBillItems = request.PurchaseBill.PurchaseBillItems.Select(x =>
                                                       new PurchaseBillItem()
                                                       {
                                                           Description = x.Description,
                                                           Amount = x.Amount,
                                                           AccountId = x.AccountId,
                                                           PurchaseBillId = x.PurchaseBillId

                                                       }).ToList();
                        _context.BillAttachments.RemoveRange(purchase.BillAttachments);

                        if (request.PurchaseBill.AttachmentList != null)
                        {
                            var attachments = _context.Attachments
                                .AsNoTracking()
                                .Where(x => x.DocumentId == purchase.Id)
                                .AsQueryable();

                            if (attachments.Any())
                            {
                                _context.Attachments.RemoveRange(attachments);
                            }
                            foreach (var item in request.PurchaseBill.AttachmentList)
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


                        foreach (var x in request.PurchaseBill.BillAttachments)
                        {
                            if (!string.IsNullOrEmpty(x.Path))
                            {
                                var billAttachment = new BillAttachment()
                                {
                                    PurchaseBillId = purchase.Id,
                                    Path = x.Path,
                                    Date = x.Date,
                                    Description = x.Description,
                                    Name = x.Name
                                };
                                await _context.BillAttachments.AddAsync(billAttachment, cancellationToken);
                            }

                        }
                        if (request.PurchaseBill.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            var transactionsList = await _context.Transactions.Where(x => x.DocumentNumber == request.PurchaseBill.RegistrationNo).ToListAsync();
                            if(transactionsList.Count > 0)
                            {
                                _context.Transactions.RemoveRange(transactionsList);
                            }

                            request.PurchaseBill.ApprovalDate = DateTime.UtcNow;
                            using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                            var transactions = new Collection<TransactionLookupModel>();
                            foreach (var item in request.PurchaseBill.PurchaseBillItems)
                            {
                                transactions.Add(new TransactionLookupModel
                                {
                                    Date = request.PurchaseBill.Date,
                                    DocumentDate = request.PurchaseBill.BillDate,
                                    ApprovalDate = request.PurchaseBill.ApprovalDate,
                                    AccountId = item.AccountId,
                                    Debit = item.Amount,
                                    Credit = 0,
                                    Description = "Expense Line Item Account",
                                    DocumentId = request.PurchaseBill.Id,
                                    TransactionType = TransactionType.Expense,
                                    DocumentNumber = request.PurchaseBill.RegistrationNo,
                                    Year = request.PurchaseBill.Date.Year.ToString(),
                                    BranchId = purchase.BranchId,
                                });

                            }
                            transactions.Add(new TransactionLookupModel
                            {
                                Date = request.PurchaseBill.Date,
                                DocumentDate = request.PurchaseBill.BillDate,
                                ApprovalDate = request.PurchaseBill.ApprovalDate,
                                AccountId = request.PurchaseBill.BillerId,
                                Debit = 0,
                                Credit = request.PurchaseBill.PurchaseBillItems.Sum(x => x.Amount),
                                Description = "Expense Head Account ",
                                DocumentId = request.PurchaseBill.Id,
                                TransactionType = TransactionType.Expense,
                                DocumentNumber = request.PurchaseBill.RegistrationNo,
                                Year = request.PurchaseBill.Date.Year.ToString(),
                                BranchId = purchase.BranchId,
                            });

                            foreach (var transaction in transactions)
                            {
                                await _mediator.Send(new AddTransactionCommand
                                {
                                    Transaction = transaction,
                                    BranchId = purchase.BranchId,
                                    Code = _code
                                }, cancellationToken);
                            }

                            scope.Complete();


                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            BranchId = purchase.BranchId,
                            DocumentNo = purchase.RegistrationNo
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return purchase.Id;
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
