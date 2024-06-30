using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Stocks.Commands.AddStock
{
    public class AddStockCommand : IRequest<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid WareHouseId { get; set; }

        public class Handler : IRequestHandler<AddStockCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddStockCommand> logger , IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddStockCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.ProductId == Guid.Empty)
                    {
                        throw new ApplicationException("Product is not found.");
                    }

                    if (request.WareHouseId == Guid.Empty)
                    {
                        throw new ApplicationException("Warehouse is not found.");
                    }

                    var stock = new Stock()
                    {
                        ProductId = request.ProductId,
                        WareHouseId = request.WareHouseId
                        
                    };

                    await _context.Stocks.AddAsync(stock, cancellationToken);
                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(), 
                        Code = _code,
                      
                    }, cancellationToken);

                    //Save changes to database
                    await _context.SaveChangesAsync(cancellationToken);

                    // Return Product id after successfully updating data
                    return stock.Id;
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
