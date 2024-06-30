using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Dashboard.Queries.GetInventoryList;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Dashboard.Queries
{
    public class GetStockListQuery : IRequest<StockListLookUpModel>
    {
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public Guid WarehouseId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SupplierId { get; set; }
        public string Search { get; set; }
        public class Handler : IRequestHandler<GetStockListQuery, StockListLookUpModel>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetStockListQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<StockListLookUpModel> Handle(GetStockListQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var products = Context.Products
                        .AsNoTracking()
                        .Include(x => x.Inventories)
                        .AsQueryable();
                    if (!string.IsNullOrEmpty(request.Search))
                    {
                        var searchTerm = request.Search;
                        products = products.Where(x =>
                                                     (x.EnglishName == null || x.EnglishName == "" ? x.ArabicName.ToLower().Contains(searchTerm) : x.EnglishName.ToLower().Contains(searchTerm)) ||
                                                     (x.ArabicName == null || x.ArabicName == "" ? x.EnglishName.ToLower().Contains(searchTerm) : x.ArabicName.ToLower().Contains(searchTerm)) ||
                                                     x.Code.ToLower().Contains(searchTerm.ToLower()) ||
                                                     x.DisplayName.ToLower().Contains(searchTerm.ToLower()) ||
                                                     x.DisplayNameForPrint.ToLower().Contains(searchTerm.ToLower())

                                                 );
                    }

                    products = products.OrderBy(x => x.Code);
                    products = products.Where(x => x.Inventories.Count > 0 && x.Inventories != null);

                    Warehouse wareHouse = null;
                    if (request.WarehouseId != Guid.Empty)
                    {
                        products = products.Where(x => x.Inventories.Any(y => y.WareHouseId == request.WarehouseId));
                        wareHouse = await Context.Warehouses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.WarehouseId, cancellationToken: cancellationToken);
                    }


                    var stockList = new List<InventoryLookUpModel>();
                    foreach (var item in products)
                    {
                        //Opening Stock
                        var openingBuy = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date < request.FromDate.Date) && (x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.SaleReturn)).Sum(x => x.Quantity);
                        var averagePrice = item.Inventories.OrderBy(x => x.ProductId == item.Id && x.Date.Date < request.FromDate.Date).LastOrDefault()?.AveragePrice;
                        var openingSale = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date < request.FromDate.Date) && (x.TransactionType == TransactionType.SaleInvoice || x.TransactionType == TransactionType.StockOut || x.TransactionType == TransactionType.PurchaseReturn)).Sum(x => x.Quantity);

                        //Current Stock
                        var currentBuy = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date) && (x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.SaleReturn)).Sum(x => x.Quantity);
                        var currentSale = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date) && (x.TransactionType == TransactionType.SaleInvoice || x.TransactionType == TransactionType.StockOut || x.TransactionType == TransactionType.PurchaseReturn)).Sum(x => x.Quantity);
                        stockList.Add(new InventoryLookUpModel
                        {
                            Id = item.Id,
                            ProductName = string.IsNullOrEmpty(item.DisplayNameForPrint) ?  item.EnglishName : item.DisplayNameForPrint,
                            ProductNameArabic = string.IsNullOrEmpty(item.DisplayNameForPrint) ? item.EnglishName : item.DisplayNameForPrint,
                            Opening = openingBuy - openingSale,
                            AvgPrice = (decimal)(averagePrice ==null?0: averagePrice),
                            QuantityIn = currentBuy,
                            QuantityOut = currentSale,
                            UnitPerPack = item.UnitPerPack,
                            Balance = (openingBuy - openingSale) + (currentBuy - currentSale),
                            ProductId = item.Id,
                        });


                    }

                    return new StockListLookUpModel
                    {
                        InventoryLook = stockList,
                        FromDate = request.FromDate,
                        ToDate = request.ToDate,
                        WareHouseName = wareHouse?.Name,
                        WareHouseNameArabic = wareHouse?.NameArabic,
                        WarehouseId = request.WarehouseId
                    };
                    
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }

    }
}
