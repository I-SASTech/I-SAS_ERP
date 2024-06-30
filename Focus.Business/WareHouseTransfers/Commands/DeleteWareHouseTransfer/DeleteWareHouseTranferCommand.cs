using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.WareHouseTransfers.Commands.DeleteWareHouseTransfer
{
    public class DeleteWareHouseTransferCommand : IRequest<Message>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeleteWareHouseTransferCommand, Message>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<DeleteWareHouseTransferCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(DeleteWareHouseTransferCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    var wareHouseTransferOrder = await _context.WareHouseTransfers.FindAsync(request.Id);

                    _context.WareHouseTransfers.Remove(wareHouseTransferOrder);
                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                       
                        Code = _code,
                    }, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);
                    var message = new Message
                    {
                        Id = request.Id,
                        IsAddUpdate = "Data has been deleted successfully"
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
                    _logger.LogError(exception.Message);
                    return message;
                }
            }
        }
    }
}
