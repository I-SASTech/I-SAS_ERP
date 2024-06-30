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
    public class GetStockRequestDetailsQuery : IRequest<StockRequestLookupModel>
    {
        public Guid Id { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetStockRequestDetailsQuery, StockRequestLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<GetStockRequestDetailsQuery> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<StockRequestLookupModel> Handle(GetStockRequestDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var stockRequest = await _context.StockRequests.AsNoTracking()
                                            .Include(x => x.StockRequestItems)
                                            .ThenInclude(x => x.Product)
                                            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
                    
                    var stocks = await _context.Stocks.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                    var branches = await _context.Branches.AsNoTracking().ToListAsync();

                    if (stockRequest == null)
                        throw new NotFoundException("No Stock Request Found", "");

                    return new StockRequestLookupModel
                    {
                        Id = stockRequest.Id,
                        Code = stockRequest.Code,
                        Date = stockRequest.Date,
                        ApprovalStatus = stockRequest.ApprovalStatus,
                        BranchId = stockRequest.BranchId,
                        ToBranchId = stockRequest.ToBranchId,
                        ToBranchName = branches.FirstOrDefault(x => x.MainBranch)?.Code + " " + branches.FirstOrDefault(x => x.MainBranch)?.BranchName,
                        FromBranchName = branches.FirstOrDefault(x => x.Id == stockRequest.FromBranchId)?.Code + " " + branches.FirstOrDefault(x => x.Id == stockRequest.FromBranchId)?.BranchName,
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
                            RemainingQuantity = 0,
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
                                    CurrentQuantity = stocks.FirstOrDefault(y => y.ProductId == x.ProductId && y.WareHouseId == (request.BranchId == null ? stockRequest.FromWareHouse: request.BranchId))?.CurrentQuantity ?? 0,
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
