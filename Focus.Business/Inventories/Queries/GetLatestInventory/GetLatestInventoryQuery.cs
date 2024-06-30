using Focus.Business.Interface;
using Focus.Business.Inventories.Commands.AddInventory;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Inventories.Queries.GetLatestInventory
{
    public class GetLatestInventoryQuery : IRequest<InventoryLookupModel>
    {
        public Guid ProductId { get; set; }
        public Guid StockId { get; set; }
        public Guid WareHouseId { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetLatestInventoryQuery, InventoryLookupModel>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetLatestInventoryQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<InventoryLookupModel> Handle(GetLatestInventoryQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.BranchId != null)
                    {
                        var inventory = await _context.Inventories
                                         .OrderBy(x => x.ProductId == request.ProductId && x.WareHouseId == request.WareHouseId && x.BranchId == request.BranchId).LastOrDefaultAsync(cancellationToken);

                        if (inventory == null)
                        {
                            return new InventoryLookupModel() { Price = 0 };
                        }

                        return new InventoryLookupModel
                        {
                            AveragePrice = inventory.AveragePrice,
                            DocumentId = inventory.DocumentId,
                            Date = inventory.Date,
                            Price = inventory.Price,
                            Quantity = inventory.Quantity,
                            StockId = inventory.StockId,
                            ProductId = inventory.ProductId,
                            WareHouseId = inventory.WareHouseId,
                            TransactionType = inventory.TransactionType,
                            CurrentQuantity = inventory.CurrentQuantity,
                            ExpiryDate = inventory.ExpiryDate,
                            BranchId = inventory?.BranchId,
                        };
                    }
                    else
                    {

                        var inventory = await _context.Inventories
                                         .OrderBy(x => x.ProductId == request.ProductId && x.WareHouseId == request.WareHouseId).LastOrDefaultAsync(cancellationToken);

                        if (inventory == null)
                        {
                            return new InventoryLookupModel() { Price = 0 };
                        }

                        return new InventoryLookupModel
                        {
                            AveragePrice = inventory.AveragePrice,
                            DocumentId = inventory.DocumentId,
                            Date = inventory.Date,
                            Price = inventory.Price,
                            Quantity = inventory.Quantity,
                            StockId = inventory.StockId,
                            ProductId = inventory.ProductId,
                            WareHouseId = inventory.WareHouseId,
                            TransactionType = inventory.TransactionType,
                            CurrentQuantity = inventory.CurrentQuantity,
                            ExpiryDate = inventory.ExpiryDate
                        };

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
