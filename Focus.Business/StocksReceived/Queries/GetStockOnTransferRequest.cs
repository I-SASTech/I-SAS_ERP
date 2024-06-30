using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Business.StocksReceived.Models;
using Focus.Business.StocksTransfer.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.StocksReceived.Queries
{
    public class GetStockOnTransferRequest : IRequest<StockReceivedLookupModel>
    {
        public Guid? StockRequestedId { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetStockOnTransferRequest, StockReceivedLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<GetStockOnTransferRequest> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<StockReceivedLookupModel> Handle(GetStockOnTransferRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var stockRequest = await _context.StockRequests.AsNoTracking()
                                            .Include(x => x.StockRequestItems)
                                            .Include(x => x.StockTransfer)
                                            .ThenInclude(x => x.StockTransferItems)
                                            .FirstOrDefaultAsync(x => x.Id == request.StockRequestedId);

                    var stockTransfer = await _context.StockTransfers.AsNoTracking()
                                            .Include(x => x.StockTransferItems)
                                            .FirstOrDefaultAsync(x => x.StockRequestId == stockRequest.Id);

                    if (stockRequest == null)
                        throw new Exception("No Data Found");

                    return new StockReceivedLookupModel();

                    //return new StockReceivedLookupModel
                    //{
                    //    Id = stockRequest.Id,
                    //    Code = stockRequest.Code,
                    //    Date = stockRequest.Date,
                    //    StockRequestId = stockRequest?.Id,
                    //    StockTransferId = stockTransfer != null ? stockTransfer.Id,
                    //    StockReceived = stockRequest.StockRequestItems.Select(x => new StockTransferItemsLookupModel
                    //    {
                    //        Id = x.Id,
                    //        BranchId = stockRequest.BranchId,
                    //        ProductId = x.ProductId,
                    //        Quantity = x.Quantity,
                    //        StockTransferId = x.StockTransferId,
                    //        Amount = stocks.FirstOrDefault(y => y.ProductId == x.ProductId)?.AveragePrice ?? 0,
                    //        TransferAmount = x.TransferAmount,
                    //        TransferQuantity = x.TransferQuantity,
                    //        Product = new ProductDropdownLookUpModel
                    //        {
                    //            Id = x.Product.Id,
                    //            BarCode = x.Product.BarCode,
                    //            Code = x.Product.Code,
                    //            EnglishName = x.Product.EnglishName,
                    //            ArabicName = x.Product.ArabicName,
                    //            IsExpire = x.Product.IsExpire,
                    //            UnitPerPack = x.Product.UnitPerPack,
                    //            Inventory = new Domain.Entities.Inventory()
                    //            {
                    //                CurrentQuantity = Convert.ToInt32(stocks.FirstOrDefault(y => y.ProductId == x.ProductId && y.WareHouseId == (request.BranchId == null ? stockRequest.WarehouseId : request.BranchId))?.CurrentQuantity ?? 0),
                    //                AveragePrice = stocks.FirstOrDefault(y => y.ProductId == x.ProductId)?.AveragePrice ?? 0,
                    //            }
                    //        }
                    //    }).ToList(),

                 }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Something went wrong " + ex.Message);
                }
            }
        }
    }
}
