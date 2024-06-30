using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Purchases.Commands.DeletePurchase
{
    public class DeleteMultiplePurchaseCommand : IRequest<Message>
    {
        public List<Guid> Id { get; set; }

        public class Handler : IRequestHandler<DeleteMultiplePurchaseCommand, Message>
        {

            private readonly IMediator _mediator;
            private string _code;
            public IApplicationDbContext Context { get; set; }
            public ILogger Logger { get; set; }
            public Handler(IApplicationDbContext context, ILogger<DeleteMultiplePurchaseCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;

            }
            public async Task<Message> Handle(DeleteMultiplePurchaseCommand request, CancellationToken cancellationToken)
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
                        var purchaseOrder = await Context.Purchases.FindAsync(id);

                        Context.Purchases.Remove(purchaseOrder);
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
                        IsAddUpdate = "Data has been deleted successfully"
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
