using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Transactions.JVTransaction;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Transaction = Focus.Domain.Entities.Transaction;

namespace Focus.Business.JournalVouchers.Commands.UpdateApprovalStatusJv
{
    public class UpdateApprovalStatusJvCommand:IRequest<Message>
    {
        public ICollection<Guid> Id { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public class Handler : IRequestHandler<UpdateApprovalStatusJvCommand, Message>
        {
            private IApplicationDbContext Context { get; set; }
            private ILogger Logger { get; set; }
            private IMediator Mediator { get; set; }
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<UpdateApprovalStatusJvCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                Mediator = mediator;
            }
            public async Task<Message> Handle(UpdateApprovalStatusJvCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                    foreach (var id in request.Id)
                    {
                        var jv = await Context.JournalVouchers.
                            FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
                        if (jv == null)
                            throw new NotFoundException("Not Found", request.Id);

                        jv.ApprovalStatus = request.ApprovalStatus;


                        var transactionList = new List<Transaction>();
                        foreach (var item in jv.JournalVoucherItems)
                        {
                            if (request.ApprovalStatus == ApprovalStatus.Approved)
                            {
                                var transaction = new Transaction
                                {
                                    TransactionType = TransactionType.JournalVoucher,
                                    DocumentId = jv.Id,
                                    DocumentNumber = jv.VoucherNumber,
                                    Date = DateTime.UtcNow,
                                    AccountId = item.AccountId,
                                    Description = item.Description,
                                    Debit = item.Debit,
                                    Credit = item.Credit,
                                };
                                transactionList.Add(transaction);
                            }
                        }

                        if (transactionList.Count > 0)
                        {
                            await Mediator.Send(new PostTransactionCommand
                            {
                                Transactions = transactionList,
                                Code = _code
                            }, cancellationToken);
                        }

                        await Mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            BranchId = jv.BranchId,
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        transactionScope.Complete();
                    }

                    
                    var message = new Message
                    {
                        Id = Guid.NewGuid(),
                        IsAddUpdate = "Status has been changed successfully"
                    };
                    return message;

                }
                catch (NotFoundException notFoundException)
                {
                    Logger.LogError(notFoundException.Message);
                    return new Message
                    {
                        IsAddUpdate = "This Object not in Database " + notFoundException.Message
                    };
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    return new Message
                    {
                        IsAddUpdate = "Some Error Occurred /n " + exception.Message
                    };
                }
            }
        }
    }
}
