using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using DocumentFormat.OpenXml.Bibliography;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.TemporaryCashRequests.Commands
{
    public class AddUpdateTemporaryCashRequest : IRequest<Guid>
    {

        public TemporaryCashRequestModel TemporaryCash { get; set; }

        public class Handler : IRequestHandler<AddUpdateTemporaryCashRequest, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateTemporaryCashRequest> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddUpdateTemporaryCashRequest request, CancellationToken cancellationToken)
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


                        var temporaryCash = new TemporaryCashRequest()
                        {
                            Date = request.TemporaryCash.Date,
                            UserId = request.TemporaryCash.UserId,
                            ApprovalStatus = request.TemporaryCash.ApprovalStatus,
                            RegistrationNo = request.TemporaryCash.RegistrationNo,
                            Note = request.TemporaryCash.Note,
                            NewUser = request.TemporaryCash.NewUser,
                            IsCashRequesterUser = request.TemporaryCash.IsCashRequesterUser,
                            BranchId = request.TemporaryCash.BranchId,
                            TemporaryCashRequestItems = request.TemporaryCash.TemporaryCashItems.Select(x =>
                                new TemporaryCashRequestItem()
                                {
                                    Description = x.Description,
                                    Amount = x.Amount
                                }).ToList()
                        };
                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                        await _context.TemporaryCashRequests.AddAsync(temporaryCash, cancellationToken);

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

                        var temporaryCash = await _context.TemporaryCashRequests.FindAsync(request.TemporaryCash.Id);

                        temporaryCash.Date = request.TemporaryCash.Date;
                        temporaryCash.UserId = request.TemporaryCash.UserId;
                        temporaryCash.ApprovalStatus = request.TemporaryCash.ApprovalStatus;
                        temporaryCash.RegistrationNo = request.TemporaryCash.RegistrationNo;
                        temporaryCash.Note = request.TemporaryCash.Note;
                        temporaryCash.NewUser = request.TemporaryCash.NewUser;
                        temporaryCash.BranchId = request.TemporaryCash.BranchId;
                        temporaryCash.IsCashRequesterUser = request.TemporaryCash.IsCashRequesterUser;

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


                        _context.TemporaryCashRequestItems.RemoveRange(temporaryCash.TemporaryCashRequestItems);

                        temporaryCash.TemporaryCashRequestItems = request.TemporaryCash.TemporaryCashItems.Select(x =>
                                                       new TemporaryCashRequestItem()
                                                       {
                                                           Description = x.Description,
                                                           Amount = x.Amount
                                                       }).ToList();

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
