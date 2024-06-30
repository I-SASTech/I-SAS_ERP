using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Inventories.Commands.AddInventory
{
    public class AddInventoryCommand : IRequest<Guid>
    {
        public InventoryLookupModel Inventory { get; set; }
        public class Handler : IRequestHandler<AddInventoryCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor
            public Handler(IApplicationDbContext context, ILogger<AddInventoryCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddInventoryCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.Inventory == null)
                    {
                        throw new ApplicationException("Inventory is not found");
                    }
                    var purchasePost = new Inventory()
                    {
                        Price = request.Inventory.Price,
                        Date = DateTime.UtcNow,
                        DocumentId = request.Inventory.DocumentId,
                        AveragePrice = request.Inventory.AveragePrice,
                        ProductId = request.Inventory.ProductId,
                        Quantity = request.Inventory.Quantity,
                        StockId = request.Inventory.StockId,
                        TransactionType = request.Inventory.TransactionType,
                        WareHouseId = request.Inventory.WareHouseId
                    };

                    await _context.Inventories.AddAsync(purchasePost, cancellationToken);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        Code = _code,
                        DocumentNo=purchasePost.DocumentNumber
                    }, cancellationToken);

                    //Save changes to database
                    await _context.SaveChangesAsync(cancellationToken);

                    // Return Product id after successfully updating data
                    return purchasePost.Id;
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
