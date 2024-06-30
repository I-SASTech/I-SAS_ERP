using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Purchases.Commands.UpdateApprovalStatus
{
    public class UpdateApprovalStatusCommand : IRequest<Message>
    {

        public ICollection<Guid> Id { get; set; }
        public class Handler : IRequestHandler<UpdateApprovalStatusCommand, Message>
        {
            public IApplicationDbContext Context { get; set; }
            public ILogger Logger { get; set; }
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<UpdateApprovalStatusCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }
             public async Task<Message> Handle(UpdateApprovalStatusCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    foreach (var id in request.Id)
                    {
                        var saleOrder = await Context.Purchases.
                            FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
                        if (saleOrder == null)
                            throw new NotFoundException("Not Found", request.Id);

                    }
                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                      
                        Code = _code,
                    }, cancellationToken);

                    await Context.SaveChangesAsync(cancellationToken);
                    var message = new Message
                    {
                        Id = Guid.NewGuid(),
                        IsAddUpdate = "Status has been changed successfully"
                    };
                    return message;
                }
                catch (NotFoundException notFoundException)
                {
                    var message = new Message
                    {
                        Id = Guid.Empty,
                        IsAddUpdate = "This Object not in Database " + notFoundException.Message
                    };
                    Logger.LogError(notFoundException.Message);
                    return message;
                }
                catch (Exception exception)
                {
                    var message = new Message
                    {
                        Id = Guid.Empty,
                        IsAddUpdate = "Some Error Occurred /n " + exception.Message
                    };
                    Logger.LogError(exception.Message);

                    return message;
                }

            }
        }
    }
}
