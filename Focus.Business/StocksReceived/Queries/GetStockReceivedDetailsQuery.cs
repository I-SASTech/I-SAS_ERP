using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Business.StocksReceived.Models;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.StocksReceived.Queries
{
    public class GetStockReceivedDetailsQuery : IRequest<StockReceivedLookupModel>
    {
        public Guid Id { get; set; }
        public Guid? BranchId { get; set; }
        public class Handler : IRequestHandler<GetStockReceivedDetailsQuery, StockReceivedLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<GetStockReceivedDetailsQuery> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<StockReceivedLookupModel> Handle(GetStockReceivedDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var stockRequest = await _context.StockReceived.AsNoTracking()
                                            .Include(x => x.StockReceivedItems)
                                            .ThenInclude(x => x.Product)
                                            .Include(x => x.StockTransfer)
                                            .FirstOrDefaultAsync(x => x.Id == request.Id);
                    var stocks = await _context.Stocks.AsNoTracking().ToListAsync();
                    var branches = await _context.Branches.AsNoTracking().ToListAsync();
                    

                    if (stockRequest == null)
                        throw new NotFoundException("No Stock Request Found", "");

                    return new StockReceivedLookupModel
                    {
                        Id = stockRequest.Id,
                        Code = stockRequest.Code,
                        Date = stockRequest.Date,
                        ApprovalStatus = stockRequest.ApprovalStatus,
                        BranchId = stockRequest.BranchId,
                        StockTransferId = stockRequest.StockTransferId,
                        WarehouseId = stockRequest.WarehouseId,
                        StockRequestId= stockRequest.StockRequestId,
                        StockTransferBranch = branches.Count > 0 ? branches.FirstOrDefault(x => x.Id == stockRequest.StockTransfer.BranchId)?.Code + " - " + branches.FirstOrDefault(x => x.Id == stockRequest.StockTransfer.BranchId)?.BranchName : "",
                        StockRecievedBranch = branches.FirstOrDefault(x => x.Id == stockRequest.BranchId)?.Code + " " + branches.FirstOrDefault(x => x.Id == stockRequest.BranchId)?.BranchName,
                        IsStockReceived = true,
                        TotalAmount = stockRequest.TotalAmount,
                        WareHouseTransferItems = stockRequest.StockReceivedItems.Select(x => new StockReceivedItemsLookupModel
                        {
                            Id = x.Id,
                            BranchId = x.BranchId,
                            ProductId = x.ProductId,
                            Quantity = x.Quantity,
                            Amount = stocks.FirstOrDefault(y => y.ProductId == x.ProductId)?.AveragePrice ?? 0,
                            TransferAmount = x.TransferAmount,
                            ReceivedQuantity = x.ReceivedQuantity,
                            RemainingQuantity = x.RemainingQuantity,
                            StockReceivedId = x.StockReceivedId,
                            TransferQuantity = x.TransferQuantity,
                            lineTotal = x.lineTotal,
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
                                    CurrentQuantity = stocks.FirstOrDefault(y => y.ProductId == x.ProductId && y.WareHouseId == (request.BranchId == null ? stockRequest.WarehouseId : request.BranchId))?.CurrentQuantity ?? 0,
                                    AveragePrice = stocks.FirstOrDefault(y => y.ProductId == x.ProductId)?.AveragePrice ?? 0,
                                }
                            }
                        }).ToList(),
                    };
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
