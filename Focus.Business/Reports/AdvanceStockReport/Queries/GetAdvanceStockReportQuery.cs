using Focus.Business.Interface;
using Focus.Business.Reports.AdvanceStockReport.Models;
using Focus.Business.Users;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Focus.Domain.Enum;
using Dapper;
using Microsoft.EntityFrameworkCore;
using NPOI.POIFS.Storage;
using DocumentFormat.OpenXml.InkML;
using Focus.Domain.Entities;

namespace Focus.Business.Reports.AdvanceStockReport.Queries
{
    public class GetAdvanceStockReportQuery : IRequest<List<AdvanceStockLookupModel>>
    {
        public Guid ProductId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CompareWith { get; set; }
        public string NumberOfPeriods { get; set; }
        public string BranchId { get; set; }
        public Guid WarehouseId { get; set; }

        public class Handler : IRequestHandler<GetAdvanceStockReportQuery, List<AdvanceStockLookupModel>>
        {
            public readonly IConfiguration Configuration;
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context,
                ILogger<GetAdvanceStockReportQuery> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }
            public async Task<List<AdvanceStockLookupModel>> Handle(GetAdvanceStockReportQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var products = _context.Products
                        .AsNoTracking()
                        .Include(x => x.Inventories)
                        .Where(y => y.Id == request.ProductId)
                        .AsQueryable();

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        products = products.Where(x => branchIdList.Contains(request.BranchId));
                    }

                    products = products.OrderBy(x => x.Code);
                    products = products.Where(x => x.Inventories.Count > 0 && x.Inventories != null);

                    

                    Warehouse wareHouse = null;
                    if (request.WarehouseId != Guid.Empty)
                    {
                        products = products.Where(x => x.Inventories.Any(y => y.WareHouseId == request.WarehouseId));
                        wareHouse = await _context.Warehouses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.WarehouseId, cancellationToken: cancellationToken);
                    }

                    if (request.CompareWith == "Previous Year(s)")
                    {
                        var stockList = new List<AdvanceStockLookupModel>();
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        for (int i = 0; i < comparePeriod; i++)
                        {

                            int year = DateTime.Now.Year;
                            int years = year - i;
                            DateTime fromDate = new DateTime(years, 1, 1);
                            DateTime toDate = new DateTime(years, 12, 31);

                            foreach (var item in products)
                            {
                                //Opening Stock
                                var openingBuy = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date < fromDate.Date) && (x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.SaleReturn)).Sum(x => x.Quantity);
                                var averagePrice = item.Inventories.OrderBy(x => x.ProductId == item.Id && x.Date.Date < fromDate.Date).LastOrDefault()?.AveragePrice;
                                var openingSale = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date < fromDate.Date) && (x.TransactionType == TransactionType.SaleInvoice || x.TransactionType == TransactionType.StockOut || x.TransactionType == TransactionType.PurchaseReturn)).Sum(x => x.Quantity);

                                //Current Stock
                                var currentBuy = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date) && (x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.SaleReturn)).Sum(x => x.Quantity);
                                var currentSale = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date) && (x.TransactionType == TransactionType.SaleInvoice || x.TransactionType == TransactionType.StockOut || x.TransactionType == TransactionType.PurchaseReturn)).Sum(x => x.Quantity);
                                stockList.Add(new AdvanceStockLookupModel
                                {
                                    CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                    ProductCode = item.Code,
                                    ProductName = string.IsNullOrEmpty(item.DisplayNameForPrint) ? item.EnglishName : item.DisplayNameForPrint,
                                    ProductNameArabic = string.IsNullOrEmpty(item.DisplayNameForPrint) ? item.ArabicName : item.DisplayNameForPrint,
                                    Opening = openingBuy - openingSale,
                                    AvgPrice = (decimal)(averagePrice == null ? 0 : averagePrice),
                                    QuantityIn = currentBuy,
                                    QuantityOut = currentSale,
                                    UnitPerPack = item.UnitPerPack,
                                    Balance = (openingBuy - openingSale) + (currentBuy - currentSale),
                                    ProductId = item.Id,
                                });
                            }
                        }

                        return stockList;
                    }
                    else if (request.CompareWith == "Previous Period(s)")
                    {
                        var stockList = new List<AdvanceStockLookupModel>();

                        int extractedYear = int.TryParse(request.NumberOfPeriods.Substring(0, 4), out int year) ? year : -1;
                        int currentYear = DateTime.Now.Year;

                        int yearDiff = currentYear - extractedYear;

                        for (int i = 0; i <= yearDiff; i++)
                        {
                            int years = currentYear - i;
                            DateTime fromDate = new DateTime(years, 1, 1);
                            DateTime toDate = new DateTime(years, 12, 31);


                            foreach (var item in products)
                            {
                                //Opening Stock
                                var openingBuy = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date < fromDate.Date) && (x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.SaleReturn)).Sum(x => x.Quantity);
                                var averagePrice = item.Inventories.OrderBy(x => x.ProductId == item.Id && x.Date.Date < fromDate.Date).LastOrDefault()?.AveragePrice;
                                var openingSale = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date < fromDate.Date) && (x.TransactionType == TransactionType.SaleInvoice || x.TransactionType == TransactionType.StockOut || x.TransactionType == TransactionType.PurchaseReturn)).Sum(x => x.Quantity);

                                //Current Stock
                                var currentBuy = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date) && (x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.SaleReturn)).Sum(x => x.Quantity);
                                var currentSale = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date) && (x.TransactionType == TransactionType.SaleInvoice || x.TransactionType == TransactionType.StockOut || x.TransactionType == TransactionType.PurchaseReturn)).Sum(x => x.Quantity);
                                stockList.Add(new AdvanceStockLookupModel
                                {
                                    CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                    ProductCode = item.Code,
                                    ProductName = string.IsNullOrEmpty(item.DisplayNameForPrint) ? item.EnglishName : item.DisplayNameForPrint,
                                    ProductNameArabic = string.IsNullOrEmpty(item.DisplayNameForPrint) ? item.ArabicName : item.DisplayNameForPrint,
                                    Opening = openingBuy - openingSale,
                                    AvgPrice = (decimal)(averagePrice == null ? 0 : averagePrice),
                                    QuantityIn = currentBuy,
                                    QuantityOut = currentSale,
                                    UnitPerPack = item.UnitPerPack,
                                    Balance = (openingBuy - openingSale) + (currentBuy - currentSale),
                                    ProductId = item.Id,
                                });
                            }
                            
                        }
                        
                        return stockList;
                    }
                    else if (request.CompareWith == "Previous Quarter(s)")
                    {
                        var stockList = new List<AdvanceStockLookupModel>();

                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        DateTime currentDate = DateTime.Now;

                        for (int q = 1; q <= comparePeriod; q++)
                        {
                            int year = currentDate.Year;
                            int quarter = (currentDate.Month - 1) / 3 + 1;

                            if (quarter - q < 0)
                            {
                                year--;
                                quarter = quarter + 4 - q;
                            }
                            else
                            {
                                quarter = quarter - q;
                            }

                            DateTime fromDate = new DateTime(year, 3 * quarter + 1, 1);
                            DateTime toDate = fromDate.AddMonths(3).AddDays(-1);


                            foreach (var item in products)
                            {
                                //Opening Stock
                                var openingBuy = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date < fromDate.Date) && (x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.SaleReturn)).Sum(x => x.Quantity);
                                var averagePrice = item.Inventories.OrderBy(x => x.ProductId == item.Id && x.Date.Date < fromDate.Date).LastOrDefault()?.AveragePrice;
                                var openingSale = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date < fromDate.Date) && (x.TransactionType == TransactionType.SaleInvoice || x.TransactionType == TransactionType.StockOut || x.TransactionType == TransactionType.PurchaseReturn)).Sum(x => x.Quantity);

                                //Current Stock
                                var currentBuy = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date) && (x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.SaleReturn)).Sum(x => x.Quantity);
                                var currentSale = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date) && (x.TransactionType == TransactionType.SaleInvoice || x.TransactionType == TransactionType.StockOut || x.TransactionType == TransactionType.PurchaseReturn)).Sum(x => x.Quantity);
                                stockList.Add(new AdvanceStockLookupModel
                                {
                                    CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                    ProductCode = item.Code,
                                    ProductName = string.IsNullOrEmpty(item.DisplayNameForPrint) ? item.EnglishName : item.DisplayNameForPrint,
                                    ProductNameArabic = string.IsNullOrEmpty(item.DisplayNameForPrint) ? item.ArabicName : item.DisplayNameForPrint,
                                    Opening = openingBuy - openingSale,
                                    AvgPrice = (decimal)(averagePrice == null ? 0 : averagePrice),
                                    QuantityIn = currentBuy,
                                    QuantityOut = currentSale,
                                    UnitPerPack = item.UnitPerPack,
                                    Balance = (openingBuy - openingSale) + (currentBuy - currentSale),
                                    ProductId = item.Id,
                                });
                            }
                            
                        }
                        return stockList;
                    }
                    else if (request.CompareWith == "Previous Month(s)")
                    {
                        var stockList = new List<AdvanceStockLookupModel>();

                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);

                        for (int i = 1; i <= comparePeriod; i++)
                        {
                            DateTime fromDate = DateTime.Now.AddMonths(-i);
                            DateTime toDate = DateTime.Now.AddMonths(-(i - 1));


                            foreach (var item in products)
                            {
                                //Opening Stock
                                var openingBuy = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date < fromDate.Date) && (x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.SaleReturn)).Sum(x => x.Quantity);
                                var averagePrice = item.Inventories.OrderBy(x => x.ProductId == item.Id && x.Date.Date < fromDate.Date).LastOrDefault()?.AveragePrice;
                                var openingSale = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date < fromDate.Date) && (x.TransactionType == TransactionType.SaleInvoice || x.TransactionType == TransactionType.StockOut || x.TransactionType == TransactionType.PurchaseReturn)).Sum(x => x.Quantity);

                                //Current Stock
                                var currentBuy = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date) && (x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.SaleReturn)).Sum(x => x.Quantity);
                                var currentSale = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date) && (x.TransactionType == TransactionType.SaleInvoice || x.TransactionType == TransactionType.StockOut || x.TransactionType == TransactionType.PurchaseReturn)).Sum(x => x.Quantity);
                                stockList.Add(new AdvanceStockLookupModel
                                {
                                    CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                    ProductCode = item.Code,
                                    ProductName = string.IsNullOrEmpty(item.DisplayNameForPrint) ? item.EnglishName : item.DisplayNameForPrint,
                                    ProductNameArabic = string.IsNullOrEmpty(item.DisplayNameForPrint) ? item.ArabicName : item.DisplayNameForPrint,
                                    Opening = openingBuy - openingSale,
                                    AvgPrice = (decimal)(averagePrice == null ? 0 : averagePrice),
                                    QuantityIn = currentBuy,
                                    QuantityOut = currentSale,
                                    UnitPerPack = item.UnitPerPack,
                                    Balance = (openingBuy - openingSale) + (currentBuy - currentSale),
                                    ProductId = item.Id,
                                });
                            }
                        }
                        
                        return stockList;
                    }
                    else
                    {
                        var stockList = new List<AdvanceStockLookupModel>();

                        foreach (var item in products)
                        {
                            //Opening Stock
                            var openingBuy = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date < request.FromDate.Date) && (x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.SaleReturn)).Sum(x => x.Quantity);
                            var averagePrice = item.Inventories.OrderBy(x => x.ProductId == item.Id && x.Date.Date < request.FromDate.Date).LastOrDefault()?.AveragePrice;
                            var openingSale = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date < request.FromDate.Date) && (x.TransactionType == TransactionType.SaleInvoice || x.TransactionType == TransactionType.StockOut || x.TransactionType == TransactionType.PurchaseReturn)).Sum(x => x.Quantity);

                            //Current Stock
                            var currentBuy = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date) && (x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.SaleReturn)).Sum(x => x.Quantity);
                            var currentSale = item.Inventories.Where(x => (x.ProductId == item.Id && x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date) && (x.TransactionType == TransactionType.SaleInvoice || x.TransactionType == TransactionType.StockOut || x.TransactionType == TransactionType.PurchaseReturn)).Sum(x => x.Quantity);
                            stockList.Add(new AdvanceStockLookupModel
                            {
                                ProductCode = item.Code,
                                ProductName = string.IsNullOrEmpty(item.DisplayNameForPrint) ? item.EnglishName : item.DisplayNameForPrint,
                                ProductNameArabic = string.IsNullOrEmpty(item.DisplayNameForPrint) ? item.ArabicName : item.DisplayNameForPrint,
                                Opening = openingBuy - openingSale,
                                AvgPrice = (decimal)(averagePrice == null ? 0 : averagePrice),
                                QuantityIn = currentBuy,
                                QuantityOut = currentSale,
                                UnitPerPack = item.UnitPerPack,
                                Balance = (openingBuy - openingSale) + (currentBuy - currentSale),
                                ProductId = item.Id,
                            });
                        }
                        return stockList;
                    }

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
