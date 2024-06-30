using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;

namespace Focus.Business.StockAdjustments.Commands.DeleteStockAdjustment
{
    public class DeleteStockAdjustmentCommand : IRequest<Message>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeleteStockAdjustmentCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<DeleteStockAdjustmentCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(DeleteStockAdjustmentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    var stockAdjustments = await _context.StockAdjustments.FindAsync(request.Id);
                    if (stockAdjustments==null)
                    {
                        throw new NotFoundException("Stock Adjustments", "");
                    }

                    _context.StockAdjustments.Remove(stockAdjustments);

                    var stockAdjustmentsDetails = _context.StockAdjustmentDetails.FirstOrDefault(x=> x.StockAdjustmentId == request.Id);
                    if (stockAdjustmentsDetails != null)
                    {
                        _context.StockAdjustmentDetails.Remove(stockAdjustmentsDetails);
                    }

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        BranchId = stockAdjustments.BranchId,
                        Code = _code,
                    }, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);
                    var message = new Message
                    {
                        Id = request.Id,
                        IsDeleted = "Data has been deleted successfully"
                    };
                    return message;
                }
               
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    var message = new Message
                    {
                        IsDeleted = "Some Error Occurred /n " + exception.Message
                    };
                    return message;
                }
            }
        }
    }
}
