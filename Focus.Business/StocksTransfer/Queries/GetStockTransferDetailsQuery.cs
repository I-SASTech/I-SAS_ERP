using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Business.StocksTransfer.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.StocksTransfer.Queries
{
    public class GetStockTransferDetailsQuery : IRequest<StockTransferLookupModel>
    {
        public Guid Id { get; set; }
        public Guid? BranchId { get; set; }
        public bool IsStockReceived { get; set; }

        public class Handler : IRequestHandler<GetStockTransferDetailsQuery, StockTransferLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetStockTransferDetailsQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<StockTransferLookupModel> Handle(GetStockTransferDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var stockRequest = await _context.StockTransfers.AsNoTracking()
                                            .Include(x => x.StockTransferItems)
                                            .ThenInclude(x => x.Product)
                                            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                    var stockRequestItems = await _context.StockRequestItems.AsNoTracking()
                        .Where(x => x.StockRequestId==stockRequest.StockRequestId)
                        .ToListAsync(cancellationToken: cancellationToken);

                    var stocks = await _context.Stocks.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                    var branches = await _context.Branches.AsNoTracking().ToListAsync();

                    if (stockRequest == null)
                        throw new NotFoundException("No Stock Request Found", "");

                    var detail = new StockTransferLookupModel
                    {
                        Id = stockRequest.Id,
                        Code = stockRequest.Code,
                        Date = stockRequest.Date,
                        ApprovalStatus = stockRequest.ApprovalStatus,
                        BranchId = stockRequest.BranchId,
                        StockRequesBranchtId = stockRequest.StockRequesBranchtId,
                        DriverName = stockRequest.DriverName,
                        DriverNationalId = stockRequest.DriverNationalId,
                        DriverNumber = stockRequest.DriverNumber,
                        StockRequestedBranch = branches.FirstOrDefault(x => x.Id == stockRequest.BranchId)?.Code + " - " + branches.FirstOrDefault(x => x.Id == stockRequest.BranchId)?.BranchName,
                        StockTransferBranch = branches.FirstOrDefault(x => x.Id == stockRequest.StockRequest.BranchId)?.Code + " - " + branches.FirstOrDefault(x => x.Id == stockRequest.StockRequest.BranchId)?.BranchName,
                        StockRequestId = stockRequest.StockRequestId,
                        VehicalNo = stockRequest.VehicalNo,
                        WareHouseId = stockRequest.WarehouseId,
                        StockStatus = stockRequest.StockTransferStatus.ToString(),
                        StockTransferStatus= stockRequest.StockTransferStatus,
                        TotalAmount = !request.IsStockReceived ? 0 : stockRequest.TotalAmount,
                        wareHouseTransferItems = stockRequest.StockTransferItems.Select(x => new StockTransferItemsLookupModel
                        {
                            Id = x.Id,
                            BranchId = stockRequest.BranchId,
                            ProductId = x.ProductId,
                            Quantity = x.Quantity,
                            StockTransferId = x.StockTransferId,
                            Amount = stocks.FirstOrDefault(y => y.ProductId == x.ProductId)?.AveragePrice ?? 0,
                            TransferAmount = x.TransferAmount,
                            TransferQuantity = x.TransferQuantity,
                            //RemainingQuantity= x.Quantity - x.RemainingQuantity,
                            RemainingQuantity= stockRequestItems.FirstOrDefault(y=>y.ProductId==x.ProductId)?.RemainingQuantity??0,
                            ReceivedQuantity = 0,
                            LineTotal= !request.IsStockReceived ? 0 : x.LineTotal,
                            Product = new ProductDropdownLookUpModel
                            {
                                Id = x.Product.Id,
                                BarCode = x.Product.BarCode,
                                Code = x.Product.Code,
                                EnglishName = x.Product.EnglishName,
                                ArabicName = x.Product.ArabicName,
                                IsExpire = x.Product.IsExpire,
                                UnitPerPack = x.Product.UnitPerPack,
                                Inventory = new Domain.Entities.Inventory()
                                {
                                    CurrentQuantity = Convert.ToInt32(stocks.FirstOrDefault(y => y.ProductId == x.ProductId && y.WareHouseId == (request.BranchId == null ? stockRequest.WarehouseId : request.BranchId))?.CurrentQuantity ?? 0),
                                    AveragePrice = stocks.FirstOrDefault(y => y.ProductId == x.ProductId)?.AveragePrice ?? 0,
                                }
                            }
                        }).ToList(),
                    };


                    return detail;
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
