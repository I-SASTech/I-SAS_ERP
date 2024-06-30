using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Transaction = Focus.Domain.Entities.Transaction;

namespace Focus.Business.TemporaryCashIssues.Commands
{
    public class AddUpdateTemporaryCashIssue : IRequest<Guid>
    {
        public TemporaryCashIssueLookUp TemporaryCash { get; set; }

        public class Handler : IRequestHandler<AddUpdateTemporaryCashIssue, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateTemporaryCashIssue> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddUpdateTemporaryCashIssue request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    if (request.TemporaryCash.Id == Guid.Empty)
                    {

                        if (request.TemporaryCash.UserId == Guid.Empty)
                        {
                            var cashRequestUser = new CashRequestUser()
                            {
                                Name = request.TemporaryCash.NewUser,
                                IsActive = true,
                            };
                            await _context.CashRequestUsers.AddAsync(cashRequestUser, cancellationToken);

                            request.TemporaryCash.UserId = cashRequestUser.Id;
                            request.TemporaryCash.IsCashRequesterUser = true;
                        }


                        var temporaryCash = new TemporaryCashIssue()
                        {
                            Date = request.TemporaryCash.Date,
                            UserId = request.TemporaryCash.UserId,
                            ApprovalStatus = request.TemporaryCash.ApprovalStatus,
                            RegistrationNo = request.TemporaryCash.RegistrationNo,
                            Note = request.TemporaryCash.Note,
                            Reason = request.TemporaryCash.Reason,
                            NewUser = request.TemporaryCash.NewUser,
                            IsCashRequesterUser = request.TemporaryCash.IsCashRequesterUser,
                            TemporaryCashRequestId = request.TemporaryCash.TemporaryCashRequestId,
                            CashIssuerId = request.TemporaryCash.CashIssuerId,
                            BranchId = request.TemporaryCash.BranchId,
                            TemporaryCashIssueItems = request.TemporaryCash.TemporaryCashIssueItems.Select(x =>
                                new TemporaryCashIssueItem()
                                {
                                    Description = x.Description,
                                    Amount = x.Amount
                                }).ToList()
                        };

                        await _context.TemporaryCashIssues.AddAsync(temporaryCash, cancellationToken);



                        if (request.TemporaryCash.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            if (request.TemporaryCash.TemporaryCashRequestId!=null)
                            {
                                var temporaryCashRequest = await _context.TemporaryCashRequests
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == request.TemporaryCash.TemporaryCashRequestId, cancellationToken: cancellationToken);
                                if (temporaryCashRequest!=null && !temporaryCashRequest.IsClose)
                                {
                                    temporaryCashRequest.IsClose = true;
                                    _context.TemporaryCashRequests.Update(temporaryCashRequest);
                                }
                                else
                                {
                                    throw new NotFoundException("Temporary Cash Request Not Found", "");
                                }
                            }
                            
                        }

                        if (request.TemporaryCash.AttachmentList != null && request.TemporaryCash.AttachmentList.Count > 0)
                        {
                            foreach (var item in request.TemporaryCash.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = temporaryCash.Id,
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
                        }, cancellationToken);
                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        scope.Complete();
                        return temporaryCash.Id;
                    }

                    else
                    {
                        if (request.TemporaryCash.UserId == Guid.Empty)
                        {
                            var cashRequestUser = new CashRequestUser()
                            {
                                Name = request.TemporaryCash.NewUser,
                                IsActive = true
                            };
                            await _context.CashRequestUsers.AddAsync(cashRequestUser, cancellationToken);

                            request.TemporaryCash.UserId = cashRequestUser.Id;
                            request.TemporaryCash.IsCashRequesterUser = true;
                        }

                        var temporaryCash = await _context.TemporaryCashIssues.FindAsync(request.TemporaryCash.Id);

                        temporaryCash.Date = request.TemporaryCash.Date;
                        temporaryCash.UserId = request.TemporaryCash.UserId;
                        temporaryCash.ApprovalStatus = request.TemporaryCash.ApprovalStatus;
                        temporaryCash.RegistrationNo = request.TemporaryCash.RegistrationNo;
                        temporaryCash.Note = request.TemporaryCash.Note;
                        temporaryCash.Reason = request.TemporaryCash.Reason;
                        temporaryCash.NewUser = request.TemporaryCash.NewUser;
                        temporaryCash.TemporaryCashRequestId = request.TemporaryCash.TemporaryCashRequestId;
                        temporaryCash.IsCashRequesterUser = request.TemporaryCash.IsCashRequesterUser;
                        temporaryCash.CashIssuerId = request.TemporaryCash.CashIssuerId;
                        temporaryCash.BranchId = request.TemporaryCash.BranchId;

                        if (request.TemporaryCash.AttachmentList != null)
                        {
                            var attachments = _context.Attachments
                                .AsNoTracking()
                                .Where(x => x.DocumentId == temporaryCash.Id)
                                .AsQueryable();

                            if (attachments.Any())
                            {
                                _context.Attachments.RemoveRange(attachments);
                            }

                            foreach (var item in request.TemporaryCash.AttachmentList)
                            {
                                var attachment = new Attachment()
                                {
                                    Date = item.Date,
                                    DocumentId = temporaryCash.Id,
                                    Description = item.Description,
                                    FileName = item.FileName,
                                    Path = item.Path
                                };
                                //Add Attachments to database
                                await _context.Attachments.AddAsync(attachment, cancellationToken);
                            }
                        }


                        _context.TemporaryCashIssueItems.RemoveRange(temporaryCash.TemporaryCashIssueItems);

                        temporaryCash.TemporaryCashIssueItems = request.TemporaryCash.TemporaryCashIssueItems.Select(x =>
                                                       new TemporaryCashIssueItem()
                                                       {
                                                           Description = x.Description,
                                                           Amount = x.Amount
                                                       }).ToList();


                        if (request.TemporaryCash.ApprovalStatus == ApprovalStatus.Approved)
                        {
                            if (request.TemporaryCash.TemporaryCashRequestId != null)
                            {
                                var temporaryCashRequest = await _context.TemporaryCashRequests
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == request.TemporaryCash.TemporaryCashRequestId, cancellationToken: cancellationToken);
                                if (temporaryCashRequest != null && !temporaryCashRequest.IsClose)
                                {
                                    temporaryCashRequest.IsClose = true;
                                    _context.TemporaryCashRequests.Update(temporaryCashRequest);
                                }
                                else
                                {
                                    throw new NotFoundException("Temporary Cash Request Not Found", "");
                                }
                            }
                            
                        }

                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                          
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        scope.Complete();
                        return temporaryCash.Id;
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
