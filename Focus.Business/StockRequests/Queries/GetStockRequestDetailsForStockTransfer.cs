using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Business.StockRequests.Models;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.StockRequests.Queries
{
    public class GetStockRequestDetailsForStockTransfer : IRequest<StockRequestLookupModel>
    {
        public Guid Id { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetStockRequestDetailsForStockTransfer, StockRequestLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetStockRequestDetailsForStockTransfer> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<StockRequestLookupModel> Handle(GetStockRequestDetailsForStockTransfer request, CancellationToken cancellationToken)
            {
                try
                {
                    var stockRequest = await _context.StockRequests.AsNoTracking()
                                            .Include(x => x.StockRequestItems)
                                            .ThenInclude(x => x.Product)
                                            .ThenInclude(x => x.TaxRate)
                                            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                    var stocks = await _context.Stocks.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                    

                    if (stockRequest == null)
                        throw new NotFoundException("No Stock Request Found", "");

                    var detail= new StockRequestLookupModel
                    {
                        Id = stockRequest.Id,
                        Code = stockRequest.Code,
                        Date = stockRequest.Date,
                        ApprovalStatus = stockRequest.ApprovalStatus,
                        BranchId = stockRequest.BranchId,
                        ToBranchId = stockRequest.ToBranchId,
                        FromBranchId = stockRequest.FromBranchId,
                        WareHouseId = stockRequest.FromWareHouse,
                        WareHouseTransferItems = stockRequest.StockRequestItems.Select(x => new StockRequestItemsLookupModel
                        {
                            Id = x.Id,
                            BranchId = x.BranchId,
                            ProductId = x.ProductId,
                            Quantity = x.Quantity,
                            StockRequestId = x.StockRequestId,
                            Amount = stocks.FirstOrDefault(y => y.ProductId == x.ProductId)?.AveragePrice ?? 0,
                            TransferAmount = 0,
                            TransferQuantity = 0,
                            RemainingQuantity = x.RemainingQuantity,
                            LineTotal  = 0,
                            highQty = 0,
                            TotalPiece = 0,
                            Product = new ProductDropdownLookUpModel
                            {
                                Id = x.Product.Id,
                                BarCode = x.Product.BarCode,
                                Code = x.Product.Code,
                                EnglishName = x.Product.EnglishName,
                                ArabicName = x.Product.ArabicName,
                                IsExpire = x.Product.IsExpire,
                                UnitPerPack = x.Product.UnitPerPack,
                                Inventory = new Inventory()
                                {
                                    CurrentQuantity = stocks.FirstOrDefault(y => y.ProductId == x.ProductId && y.WareHouseId == (request.BranchId == null ? stockRequest.FromWareHouse : request.BranchId))?.CurrentQuantity ?? 0,
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
