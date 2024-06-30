using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Dapper;
using Focus.Domain.Interface;
using Microsoft.Extensions.Configuration;
using Focus.Business.Dashboard.Queries.GetWareHouseInventory;
using System;
using System.Collections.Generic;

namespace Focus.Business.Dashboard.Queries.InventoryDashBoadReport
{
    public class InventoryDashboardQuery : IRequest<InventoryDashboardLookUpModel>
    {
        public string OverViewFilter { get; set; }

        public class Handler : IRequestHandler<InventoryDashboardQuery, InventoryDashboardLookUpModel>
        {
            private readonly ILogger _logger;
            public readonly IConfiguration Configuration;
            private readonly IUserHttpContextProvider _contextProvider;

            public Handler(ILogger<InventoryDashboardQuery> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider)
            {
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
            }

            public async Task<InventoryDashboardLookUpModel> Handle(InventoryDashboardQuery request, CancellationToken cancellationToken)

            {
                try
                {
                    var list = new List<TrendingProductLookUpModel>();
                    var listofTopCategory = new List<TrendingProductLookUpModel>();
                    var wrostInventoires = new List<ProductInventoryLookUpModel>();
                    var wrostCategoryInventoires = new List<ProductInventoryLookUpModel>();

                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));

                    #region Filter
                    if (request.OverViewFilter == "Monthly")
                    {

                        string sb = "select   p.DisplayName as DisplayName, p.ArabicName as ProductArabicName,c.Name as CategoryName,c.NameArabic as CategoryNameArabic ,c.Id AS CategoryId,inv.Date,inv.Price,inv.AveragePrice,inv.Quantity,inv.CurrentQuantity,inv.CurrentStockValue,inv.SalePrice,inv.ProductId from Inventories " +
                                 " inv inner join Products p on p.Id=inv.ProductId" +
                                 " inner join Categories c on c.Id=p.CategoryId" +
                         " where Month(inv.Date) = Month(GETDATE()) " +
                         " and  inv.CompanyId= '" + _contextProvider.GetCompanyId() + "'";


                        var query = conn.Query<ProductInventoryLookUpModel>(sb).ToList();

                        var topProduct = query.GroupBy(x => x.ProductId);

                        var topProducts = topProduct.Select(x => new ProductInventoryLookUpModel
                        {
                            Date = x.FirstOrDefault().Date,
                            DisplayName = x.FirstOrDefault().DisplayName,
                            ProductId = x.FirstOrDefault().ProductId,
                            ProductArabicName = x.FirstOrDefault().ProductArabicName,
                            Quantity = x.Sum(x => x.Quantity)
                        }).ToList();
                        var trendingProductList = topProducts.OrderBy(x => x.Quantity).TakeLast(5);
                        var wrostProductList = topProducts.OrderByDescending(x => x.Quantity).TakeLast(5);
                        list = trendingProductList.Select(item =>
                       new TrendingProductLookUpModel()
                       {
                           Name = item.ProductName,
                           Date = item.Date.ToString("d"),
                           JanSale = query.Where(x => x.Date.Month == 1 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           FebSale = query.Where(x => x.Date.Month == 2 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           MarSale = query.Where(x => x.Date.Month == 3 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           AprSale = query.Where(x => x.Date.Month == 4 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           MaySale = query.Where(x => x.Date.Month == 5 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           JunSale = query.Where(x => x.Date.Month == 6 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           JulSale = query.Where(x => x.Date.Month == 7 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           AugSale = query.Where(x => x.Date.Month == 8 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           SepSale = query.Where(x => x.Date.Month == 9 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           OctSale = query.Where(x => x.Date.Month == 10 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           NovSale = query.Where(x => x.Date.Month == 11 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           DecSale = query.Where(x => x.Date.Month == 12 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                       }).ToList();
                        wrostInventoires = wrostProductList.Select(item => new ProductInventoryLookUpModel()
                        {
                            ProductName = item.ProductName,
                            ProductArabicName = item.ProductArabicName
                        }
                            ).ToList();
                        // Category Wise Trending Product
                        var groupbyCategoryProduct = query.GroupBy(x => x.CategoryId);
                        var topCategoryProducts = groupbyCategoryProduct.Select(x => new ProductInventoryLookUpModel
                        {
                            Date = x.FirstOrDefault().Date,
                            ProductName = x.FirstOrDefault().ProductName,
                            ProductId = x.FirstOrDefault().ProductId,
                            CategoryName = x.FirstOrDefault().CategoryName,
                            CategoryId = x.FirstOrDefault().CategoryId,
                            CategoryNameArabic = x.FirstOrDefault().CategoryNameArabic,
                            ProductArabicName = x.FirstOrDefault().ProductArabicName,
                            Quantity = x.Sum(x => x.Quantity)
                        }).ToList();
                        var trendingProductCategoryList = topCategoryProducts.Where(x => x.CategoryId != null).OrderBy(x => x.Quantity).TakeLast(5);
                        var wrostProductCategoryList = topCategoryProducts.Where(x => x.CategoryId != null).OrderByDescending(x => x.Quantity).TakeLast(5);
                        listofTopCategory = trendingProductCategoryList.Select(item =>
                        new TrendingProductLookUpModel()
                        {
                            Name = item.ProductName,
                            CategoryName = item.CategoryName,
                            CategoryNameAr = item.CategoryNameArabic,
                            Date = item.Date.ToString("d"),
                            JanSale = query.Where(x => x.Date.Month == 1 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            FebSale = query.Where(x => x.Date.Month == 2 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            MarSale = query.Where(x => x.Date.Month == 3 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            AprSale = query.Where(x => x.Date.Month == 4 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            MaySale = query.Where(x => x.Date.Month == 5 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            JunSale = query.Where(x => x.Date.Month == 6 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            JulSale = query.Where(x => x.Date.Month == 7 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            AugSale = query.Where(x => x.Date.Month == 8 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            SepSale = query.Where(x => x.Date.Month == 9 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            OctSale = query.Where(x => x.Date.Month == 10 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            NovSale = query.Where(x => x.Date.Month == 11 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            DecSale = query.Where(x => x.Date.Month == 12 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                        }).ToList();
                        wrostCategoryInventoires = wrostProductCategoryList.Select(item => new ProductInventoryLookUpModel()
                        {
                            ProductName = item.ProductName,
                            CategoryName = item.CategoryName,
                            CategoryNameArabic = item.CategoryNameArabic,
                            ProductArabicName = item.ProductArabicName
                        }
                            ).ToList();



                    }

                    else if (request.OverViewFilter == "3 Month")
                    {
                        var lastDay = DateTime.UtcNow.Date;
                        var firstDay = lastDay.AddMonths(-3);


                        string sb = "select   p.EnglishName as ProductName, p.ArabicName as ProductArabicName,c.Name as CategoryName,c.NameArabic as CategoryNameArabic ,c.Id AS CategoryId,inv.Date,inv.Price,inv.AveragePrice,inv.Quantity,inv.CurrentQuantity,inv.CurrentStockValue,inv.SalePrice,inv.ProductId from Inventories " +
                               " inv inner join Products p on p.Id=inv.ProductId" +
                               " inner join Categories c on c.Id=p.CategoryId" +
                       " Where Cast(inv.Date as date )  between  '" + firstDay + "'" +
                        " and '" + lastDay + "'" + " and  inv.CompanyId= '" + _contextProvider.GetCompanyId() + "'";


                        var query = conn.Query<ProductInventoryLookUpModel>(sb).ToList();

                        var topProduct = query.GroupBy(x => x.ProductId);

                        var topProducts = topProduct.Select(x => new ProductInventoryLookUpModel
                        {
                            Date = x.FirstOrDefault().Date,
                            ProductName = x.FirstOrDefault().ProductName,
                            ProductId = x.FirstOrDefault().ProductId,
                            ProductArabicName = x.FirstOrDefault().ProductArabicName,
                            Quantity = x.Sum(x => x.Quantity)
                        }).ToList();
                        var trendingProductList = topProducts.OrderBy(x => x.Quantity).TakeLast(5);
                        var wrostProductList = topProducts.OrderByDescending(x => x.Quantity).TakeLast(5);
                        list = trendingProductList.Select(item =>
                       new TrendingProductLookUpModel()
                       {
                           Name = item.ProductName,
                           Date = item.Date.ToString("d"),
                           JanSale = query.Where(x => x.Date.Month == 1 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           FebSale = query.Where(x => x.Date.Month == 2 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           MarSale = query.Where(x => x.Date.Month == 3 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           AprSale = query.Where(x => x.Date.Month == 4 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           MaySale = query.Where(x => x.Date.Month == 5 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           JunSale = query.Where(x => x.Date.Month == 6 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           JulSale = query.Where(x => x.Date.Month == 7 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           AugSale = query.Where(x => x.Date.Month == 8 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           SepSale = query.Where(x => x.Date.Month == 9 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           OctSale = query.Where(x => x.Date.Month == 10 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           NovSale = query.Where(x => x.Date.Month == 11 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           DecSale = query.Where(x => x.Date.Month == 12 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                       }).ToList();
                        wrostInventoires = wrostProductList.Select(item => new ProductInventoryLookUpModel()
                        {
                            ProductName = item.ProductName,
                            ProductArabicName = item.ProductArabicName
                        }
                            ).ToList();
                        // Category Wise Trending Product
                        var groupbyCategoryProduct = query.GroupBy(x => x.CategoryId);
                        var topCategoryProducts = groupbyCategoryProduct.Select(x => new ProductInventoryLookUpModel
                        {
                            Date = x.FirstOrDefault().Date,
                            ProductName = x.FirstOrDefault().ProductName,
                            ProductId = x.FirstOrDefault().ProductId,
                            CategoryName = x.FirstOrDefault().CategoryName,
                            CategoryId = x.FirstOrDefault().CategoryId,
                            CategoryNameArabic = x.FirstOrDefault().CategoryNameArabic,
                            ProductArabicName = x.FirstOrDefault().ProductArabicName,
                            Quantity = x.Sum(x => x.Quantity)
                        }).ToList();
                        var trendingProductCategoryList = topCategoryProducts.Where(x => x.CategoryId != null).OrderBy(x => x.Quantity).TakeLast(5);
                        var wrostProductCategoryList = topCategoryProducts.Where(x => x.CategoryId != null).OrderByDescending(x => x.Quantity).TakeLast(5);
                        listofTopCategory = trendingProductCategoryList.Select(item =>
                        new TrendingProductLookUpModel()
                        {
                            Name = item.ProductName,
                            CategoryName = item.CategoryName,
                            CategoryNameAr = item.CategoryNameArabic,
                            Date = item.Date.ToString("d"),
                            JanSale = query.Where(x => x.Date.Month == 1 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            FebSale = query.Where(x => x.Date.Month == 2 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            MarSale = query.Where(x => x.Date.Month == 3 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            AprSale = query.Where(x => x.Date.Month == 4 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            MaySale = query.Where(x => x.Date.Month == 5 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            JunSale = query.Where(x => x.Date.Month == 6 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            JulSale = query.Where(x => x.Date.Month == 7 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            AugSale = query.Where(x => x.Date.Month == 8 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            SepSale = query.Where(x => x.Date.Month == 9 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            OctSale = query.Where(x => x.Date.Month == 10 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            NovSale = query.Where(x => x.Date.Month == 11 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            DecSale = query.Where(x => x.Date.Month == 12 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                        }).ToList();
                        wrostCategoryInventoires = wrostProductCategoryList.Select(item => new ProductInventoryLookUpModel()
                        {
                            ProductName = item.ProductName,
                            CategoryName = item.CategoryName,
                            CategoryNameArabic = item.CategoryNameArabic,
                            ProductArabicName = item.ProductArabicName
                        }
                            ).ToList();




                    }

                    else if (request.OverViewFilter == "6 Month")
                    {
                        var lastDay = DateTime.UtcNow.Date;
                        var firstDay = lastDay.AddMonths(-6);


                        string sb = "select   p.EnglishName as ProductName, p.ArabicName as ProductArabicName,c.Name as CategoryName,c.NameArabic as CategoryNameArabic ,c.Id AS CategoryId,inv.Date,inv.Price,inv.AveragePrice,inv.Quantity,inv.CurrentQuantity,inv.CurrentStockValue,inv.SalePrice,inv.ProductId from Inventories " +
                                " inv inner join Products p on p.Id=inv.ProductId" +
                                " inner join Categories c on c.Id=p.CategoryId" +
                        " Where Cast(inv.Date as date )  between  '" + firstDay + "'" +
                        " and '" + lastDay + "'" + " and  inv.CompanyId= '" + _contextProvider.GetCompanyId() + "'";


                        var query = conn.Query<ProductInventoryLookUpModel>(sb).ToList();

                        var topProduct = query.GroupBy(x => x.ProductId);

                        var topProducts = topProduct.Select(x => new ProductInventoryLookUpModel
                        {
                            Date = x.FirstOrDefault().Date,
                            ProductName = x.FirstOrDefault().ProductName,
                            ProductId = x.FirstOrDefault().ProductId,
                            ProductArabicName = x.FirstOrDefault().ProductArabicName,
                            Quantity = x.Sum(x => x.Quantity)
                        }).ToList();
                        var trendingProductList = topProducts.OrderBy(x => x.Quantity).TakeLast(5);
                        var wrostProductList = topProducts.OrderByDescending(x => x.Quantity).TakeLast(5);
                        list = trendingProductList.Select(item =>
                       new TrendingProductLookUpModel()
                       {
                           Name = item.ProductName,
                           Date = item.Date.ToString("d"),
                           JanSale = query.Where(x => x.Date.Month == 1 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           FebSale = query.Where(x => x.Date.Month == 2 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           MarSale = query.Where(x => x.Date.Month == 3 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           AprSale = query.Where(x => x.Date.Month == 4 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           MaySale = query.Where(x => x.Date.Month == 5 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           JunSale = query.Where(x => x.Date.Month == 6 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           JulSale = query.Where(x => x.Date.Month == 7 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           AugSale = query.Where(x => x.Date.Month == 8 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           SepSale = query.Where(x => x.Date.Month == 9 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           OctSale = query.Where(x => x.Date.Month == 10 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           NovSale = query.Where(x => x.Date.Month == 11 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           DecSale = query.Where(x => x.Date.Month == 12 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                       }).ToList();
                        wrostInventoires = wrostProductList.Select(item => new ProductInventoryLookUpModel()
                        {
                            ProductName = item.ProductName,
                            ProductArabicName = item.ProductArabicName
                        }
                            ).ToList();
                        // Category Wise Trending Product
                        var groupbyCategoryProduct = query.GroupBy(x => x.CategoryId);
                        var topCategoryProducts = groupbyCategoryProduct.Select(x => new ProductInventoryLookUpModel
                        {
                            Date = x.FirstOrDefault().Date,
                            ProductName = x.FirstOrDefault().ProductName,
                            ProductId = x.FirstOrDefault().ProductId,
                            CategoryName = x.FirstOrDefault().CategoryName,
                            CategoryId = x.FirstOrDefault().CategoryId,
                            CategoryNameArabic = x.FirstOrDefault().CategoryNameArabic,
                            ProductArabicName = x.FirstOrDefault().ProductArabicName,
                            Quantity = x.Sum(x => x.Quantity)
                        }).ToList();
                        var trendingProductCategoryList = topCategoryProducts.Where(x => x.CategoryId != null).OrderBy(x => x.Quantity).TakeLast(5);
                        var wrostProductCategoryList = topCategoryProducts.Where(x => x.CategoryId != null).OrderByDescending(x => x.Quantity).TakeLast(5);
                        listofTopCategory = trendingProductCategoryList.Select(item =>
                        new TrendingProductLookUpModel()
                        {
                            Name = item.ProductName,
                            CategoryName = item.CategoryName,
                            CategoryNameAr = item.CategoryNameArabic,
                            Date = item.Date.ToString("d"),
                            JanSale = query.Where(x => x.Date.Month == 1 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            FebSale = query.Where(x => x.Date.Month == 2 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            MarSale = query.Where(x => x.Date.Month == 3 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            AprSale = query.Where(x => x.Date.Month == 4 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            MaySale = query.Where(x => x.Date.Month == 5 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            JunSale = query.Where(x => x.Date.Month == 6 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            JulSale = query.Where(x => x.Date.Month == 7 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            AugSale = query.Where(x => x.Date.Month == 8 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            SepSale = query.Where(x => x.Date.Month == 9 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            OctSale = query.Where(x => x.Date.Month == 10 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            NovSale = query.Where(x => x.Date.Month == 11 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            DecSale = query.Where(x => x.Date.Month == 12 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                        }).ToList();
                        wrostCategoryInventoires = wrostProductCategoryList.Select(item => new ProductInventoryLookUpModel()
                        {
                            ProductName = item.ProductName,
                            CategoryName = item.CategoryName,
                            CategoryNameArabic = item.CategoryNameArabic,
                            ProductArabicName = item.ProductArabicName
                        }
                            ).ToList();





                    }

                    else if (request.OverViewFilter == "Year")
                    {
                        string sb = "select   p.EnglishName as ProductName, p.ArabicName as ProductArabicName,c.Name as CategoryName,c.NameArabic as CategoryNameArabic ,c.Id AS CategoryId,inv.Date,inv.Price,inv.AveragePrice,inv.Quantity,inv.CurrentQuantity,inv.CurrentStockValue,inv.SalePrice,inv.ProductId from Inventories " +
                               " inv inner join Products p on p.Id=inv.ProductId" +
                               " inner join Categories c on c.Id=p.CategoryId" +
                        " where Year(inv.Date) = Year(GETDATE()) " +
                          " and  inv.CompanyId= '" + _contextProvider.GetCompanyId() + "'";

                        var query = conn.Query<ProductInventoryLookUpModel>(sb).ToList();

                        var topProduct = query.GroupBy(x => x.ProductId);

                        var topProducts = topProduct.Select(x => new ProductInventoryLookUpModel
                        {
                            Date = x.FirstOrDefault().Date,
                            ProductName = x.FirstOrDefault().ProductName,
                            ProductId = x.FirstOrDefault().ProductId,
                            ProductArabicName = x.FirstOrDefault().ProductArabicName,
                            Quantity = x.Sum(x => x.Quantity)
                        }).ToList();
                        var trendingProductList = topProducts.OrderBy(x => x.Quantity).TakeLast(5);
                        var wrostProductList = topProducts.OrderByDescending(x => x.Quantity).TakeLast(5);
                        list = trendingProductList.Select(item =>
                       new TrendingProductLookUpModel()
                       {
                           Name = item.ProductName,
                           Date = item.Date.ToString("d"),
                           JanSale = query.Where(x => x.Date.Month == 1 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           FebSale = query.Where(x => x.Date.Month == 2 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           MarSale = query.Where(x => x.Date.Month == 3 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           AprSale = query.Where(x => x.Date.Month == 4 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           MaySale = query.Where(x => x.Date.Month == 5 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           JunSale = query.Where(x => x.Date.Month == 6 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           JulSale = query.Where(x => x.Date.Month == 7 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           AugSale = query.Where(x => x.Date.Month == 8 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           SepSale = query.Where(x => x.Date.Month == 9 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           OctSale = query.Where(x => x.Date.Month == 10 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           NovSale = query.Where(x => x.Date.Month == 11 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                           DecSale = query.Where(x => x.Date.Month == 12 && x.ProductId == item.ProductId).Sum(x => x.Quantity),
                       }).ToList();
                        wrostInventoires = wrostProductList.Select(item => new ProductInventoryLookUpModel()
                        {
                            ProductName = item.ProductName,
                            ProductArabicName = item.ProductArabicName
                        }
                            ).ToList();
                        // Category Wise Trending Product
                        var groupbyCategoryProduct = query.GroupBy(x => x.CategoryId);
                        var topCategoryProducts = groupbyCategoryProduct.Select(x => new ProductInventoryLookUpModel
                        {
                            Date = x.FirstOrDefault().Date,
                            ProductName = x.FirstOrDefault().ProductName,
                            ProductId = x.FirstOrDefault().ProductId,
                            CategoryName = x.FirstOrDefault().CategoryName,
                            CategoryId = x.FirstOrDefault().CategoryId,
                            CategoryNameArabic = x.FirstOrDefault().CategoryNameArabic,
                            ProductArabicName = x.FirstOrDefault().ProductArabicName,
                            Quantity = x.Sum(x => x.Quantity)
                        }).ToList();
                        var trendingProductCategoryList = topCategoryProducts.Where(x => x.CategoryId != null).OrderBy(x => x.Quantity).TakeLast(5);
                        var wrostProductCategoryList = topCategoryProducts.Where(x => x.CategoryId != null).OrderByDescending(x => x.Quantity).TakeLast(5);
                        listofTopCategory = trendingProductCategoryList.Select(item =>
                        new TrendingProductLookUpModel()
                        {
                            Name = item.ProductName,
                            CategoryName = item.CategoryName,
                            CategoryNameAr = item.CategoryNameArabic,
                            Date = item.Date.ToString("d"),
                            JanSale = query.Where(x => x.Date.Month == 1 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            FebSale = query.Where(x => x.Date.Month == 2 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            MarSale = query.Where(x => x.Date.Month == 3 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            AprSale = query.Where(x => x.Date.Month == 4 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            MaySale = query.Where(x => x.Date.Month == 5 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            JunSale = query.Where(x => x.Date.Month == 6 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            JulSale = query.Where(x => x.Date.Month == 7 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            AugSale = query.Where(x => x.Date.Month == 8 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            SepSale = query.Where(x => x.Date.Month == 9 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            OctSale = query.Where(x => x.Date.Month == 10 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            NovSale = query.Where(x => x.Date.Month == 11 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                            DecSale = query.Where(x => x.Date.Month == 12 && x.CategoryId == item.CategoryId).Sum(x => x.Quantity),
                        }).ToList();
                        wrostCategoryInventoires = wrostProductCategoryList.Select(item => new ProductInventoryLookUpModel()
                        {
                            ProductName = item.ProductName,
                            CategoryName = item.CategoryName,
                            CategoryNameArabic = item.CategoryNameArabic,
                            ProductArabicName = item.ProductArabicName
                        }
                            ).ToList();



                    }
                    #endregion

                    #region Calculation




                    #endregion


                    return new InventoryDashboardLookUpModel
                    {


                        TrendingProducts = list,
                        TrendingCategoryProducts = listofTopCategory,
                        WrostInventoires = wrostInventoires,
                        WrostCategoryInventoires = wrostCategoryInventoires,


                    };
                }


                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }

            }
        }
    }

}

