using Dapper;
using Focus.Business.Interface;
using Focus.Business.Reports.ProductSummaryReport.Models;
using Focus.Business.Users;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Reports.ProductSummaryReport.Queries
{
    public class GetProductSummaryQuery : IRequest<List<ProductSummaryLookupModel>>
    {
        public string CompareWith { get; set; }
        public string NumberOfPeriods { get; set; }
        public string ProductId { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<GetProductSummaryQuery, List<ProductSummaryLookupModel>>
        {
            public readonly IConfiguration Configuration;
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context,
                ILogger<GetProductSummaryQuery> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }
            public async Task<List<ProductSummaryLookupModel>> Handle(GetProductSummaryQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "select sa.Date as Date, pr.Id as Productid, pr.Code as Code, pr.EnglishName as EnglishName, pr.ArabicName as ArabicName, " +
                                "si.TotalWithOutAmount as GrossAmount, si.DiscountAmount as DiscountAmount,(si.TotalWithOutAmount - si.DiscountAmount) as NetTotal, " +
                                "si.VatAmount as VatAmount, si.TotalAmount as TotalAmount from Sales sa " +
                                "left outer join SaleItems si on sa.Id = si.SaleId " +
                                "left outer join Products pr on si.ProductId = pr.Id " +
                                "where pr.Id = '"+request.ProductId+"' and (sa.InvoiceType = 1 or sa.InvoiceType = 2 or sa.InvoiceType = 3)";

                    string sbForPurchase = "select sa.Date as Date, pr.Id as Productid, pr.Code as Code, pr.EnglishName as EnglishName, " +
                                           "pr.ArabicName as ArabicName, si.TotalWithOutAmount as GrossAmount, si.DiscountAmount as DiscountAmount," +
                                           "(si.TotalWithOutAmount - si.DiscountAmount) as NetTotal, " +
                                           "si.VatAmount as VatAmount, si.TotalAmount as TotalAmount from PurchasePosts sa " +
                                           "left outer join PurchasePostItems si on sa.Id = si.PurchasePostId " +
                                           "left outer join Products pr on si.ProductId = pr.Id " +
                                           "where pr.Id = '" + request.ProductId + "' and (sa.IsPurchasePost = 1 or sa.IsPurchaseReturn = 1)";

                    var query = conn.Query<ProductSummaryDapperDateLookupModel>(sb).AsQueryable();
                    var purchaseItemQuery = conn.Query<ProductSummaryDapperDateLookupModel>(sbForPurchase).AsQueryable();
                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        query = query.Where(x => branchIdList.Contains(x.BranchId.ToString()));
                    }

                    if (request.CompareWith == "Previous Year(s)")
                    {
                        var productSummaryList = new List<ProductSummaryLookupModel>();

                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);

                        for (int i = 0; i < comparePeriod; i++)
                        {
                            int year = DateTime.Now.Year;
                            int years = year - i;
                            DateTime fromDate = new DateTime(years, 1, 1);
                            DateTime toDate = new DateTime(years, 12, 31);

                            var filterDate = query.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();
                            var filterPurchaseDate = purchaseItemQuery.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();

                            productSummaryList.Add(new ProductSummaryLookupModel()
                            {
                                Code = filterDate.FirstOrDefault()?.Code,
                                EnglishName = filterDate.FirstOrDefault()?.EnglishName,
                                ArabicName = filterDate.FirstOrDefault()?.ArabicName,
                                TotalPurchases = filterDate.Count(),
                                TotalGrossAmount = filterDate.Sum(x => x.GrossAmount),
                                TotalDiscountAmount = filterDate.Sum(x => x.DiscountAmount),
                                TotalNetTotal = filterDate.Sum(x => x.NetTotal),
                                TotalVatAmount = filterDate.Sum(x => x.VatAmount),
                                TotalTotalAmount = filterDate.Sum(x => x.TotalAmount),
                                TotalPurchaseGrossAmount = filterPurchaseDate.Sum(x => x.GrossAmount),
                                TotalPurchaseDiscountAmount = filterPurchaseDate.Sum(x => x.DiscountAmount),
                                TotalPurchaseNetTotal = filterPurchaseDate.Sum(x => x.NetTotal),
                                TotalPurchaseVatAmount = filterPurchaseDate.Sum(x => x.VatAmount),
                                TotalPurchaseAmount = filterPurchaseDate.Sum(x => x.TotalAmount),
                                CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                ApexChartValue = fromDate.ToString("yyyy"),
                            });
                        }

                        return productSummaryList;
                    }
                    else if (request.CompareWith == "Previous Period(s)")
                    {
                        var productSummaryList = new List<ProductSummaryLookupModel>();

                        int extractedYear = int.TryParse(request.NumberOfPeriods.Substring(0, 4), out int year) ? year : -1;
                        int currentYear = DateTime.Now.Year;

                        int yearDiff = currentYear - extractedYear;

                        for (int i = 0; i <= yearDiff; i++)
                        {
                            int years = currentYear - i;
                            DateTime fromDate = new DateTime(years, 1, 1);
                            DateTime toDate = new DateTime(years, 12, 31);

                            var filterDate = query.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();
                            var filterPurchaseDate = purchaseItemQuery.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();

                            productSummaryList.Add(new ProductSummaryLookupModel()
                            {
                                Code = filterDate.FirstOrDefault()?.Code,
                                EnglishName = filterDate.FirstOrDefault()?.EnglishName,
                                ArabicName = filterDate.FirstOrDefault()?.ArabicName,
                                TotalPurchases = filterDate.Count(),
                                TotalGrossAmount = filterDate.Sum(x => x.GrossAmount),
                                TotalDiscountAmount = filterDate.Sum(x => x.DiscountAmount),
                                TotalNetTotal = filterDate.Sum(x => x.NetTotal),
                                TotalVatAmount = filterDate.Sum(x => x.VatAmount),
                                TotalTotalAmount = filterDate.Sum(x => x.TotalAmount),
                                TotalPurchaseGrossAmount = filterPurchaseDate.Sum(x => x.GrossAmount),
                                TotalPurchaseDiscountAmount = filterPurchaseDate.Sum(x => x.DiscountAmount),
                                TotalPurchaseNetTotal = filterPurchaseDate.Sum(x => x.NetTotal),
                                TotalPurchaseVatAmount = filterPurchaseDate.Sum(x => x.VatAmount),
                                TotalPurchaseAmount = filterPurchaseDate.Sum(x => x.TotalAmount),
                                CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                ApexChartValue = fromDate.ToString("yyyy"),
                            });
                        }
                        return productSummaryList;
                    }
                    else if (request.CompareWith == "Previous Quarter(s)")
                    {
                        var productSummaryList = new List<ProductSummaryLookupModel>();

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

                            var filterDate = query.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();
                            var filterPurchaseDate = purchaseItemQuery.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();

                            productSummaryList.Add(new ProductSummaryLookupModel()
                            {
                                Code = filterDate.FirstOrDefault()?.Code,
                                EnglishName = filterDate.FirstOrDefault()?.EnglishName,
                                ArabicName = filterDate.FirstOrDefault()?.ArabicName,
                                TotalPurchases = filterDate.Count(),
                                TotalGrossAmount = filterDate.Sum(x => x.GrossAmount),
                                TotalDiscountAmount = filterDate.Sum(x => x.DiscountAmount),
                                TotalNetTotal = filterDate.Sum(x => x.NetTotal),
                                TotalVatAmount = filterDate.Sum(x => x.VatAmount),
                                TotalTotalAmount = filterDate.Sum(x => x.TotalAmount),
                                TotalPurchaseGrossAmount = filterPurchaseDate.Sum(x => x.GrossAmount),
                                TotalPurchaseDiscountAmount = filterPurchaseDate.Sum(x => x.DiscountAmount),
                                TotalPurchaseNetTotal = filterPurchaseDate.Sum(x => x.NetTotal),
                                TotalPurchaseVatAmount = filterPurchaseDate.Sum(x => x.VatAmount),
                                TotalPurchaseAmount = filterPurchaseDate.Sum(x => x.TotalAmount),
                                CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                ApexChartValue = fromDate.ToString("MMMM, yyyy") + " - " + toDate.ToString("MMMM, yyyy"),
                            });
                        }
                        return productSummaryList;
                    }
                    else if (request.CompareWith == "Previous Month(s)")
                    {
                        var productSummaryList = new List<ProductSummaryLookupModel>();

                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);


                        for (int i = 1; i <= comparePeriod; i++)
                        {
                            DateTime fromDate = DateTime.Now.AddMonths(-i);
                            DateTime toDate = DateTime.Now.AddMonths(-(i - 1));

                            var filterDate = query.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();
                            var filterPurchaseDate = purchaseItemQuery.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();

                            productSummaryList.Add(new ProductSummaryLookupModel()
                            {
                                Code = filterDate.FirstOrDefault()?.Code,
                                EnglishName = filterDate.FirstOrDefault()?.EnglishName,
                                ArabicName = filterDate.FirstOrDefault()?.ArabicName,
                                TotalPurchases = filterDate.Count(),
                                TotalGrossAmount = filterDate.Sum(x => x.GrossAmount),
                                TotalDiscountAmount = filterDate.Sum(x => x.DiscountAmount),
                                TotalNetTotal = filterDate.Sum(x => x.NetTotal),
                                TotalVatAmount = filterDate.Sum(x => x.VatAmount),
                                TotalTotalAmount = filterDate.Sum(x => x.TotalAmount),
                                TotalPurchaseGrossAmount = filterPurchaseDate.Sum(x => x.GrossAmount),
                                TotalPurchaseDiscountAmount = filterPurchaseDate.Sum(x => x.DiscountAmount),
                                TotalPurchaseNetTotal = filterPurchaseDate.Sum(x => x.NetTotal),
                                TotalPurchaseVatAmount = filterPurchaseDate.Sum(x => x.VatAmount),
                                TotalPurchaseAmount = filterPurchaseDate.Sum(x => x.TotalAmount),
                                CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                ApexChartValue = fromDate.ToString("MMM, yyyy") + " - " + toDate.ToString("MMM, yyyy"),
                            });
                        }
                        return productSummaryList;
                    }
                    else
                    {
                        var productSummaryList = new List<ProductSummaryLookupModel>();
                        if (request.NumberOfPeriods == "7 Days")
                        {
                            for (int i = 0; i < 7; i++)
                            {
                                DateTime fromDate = DateTime.Now.AddDays(-i);
                                DateTime toDate = DateTime.Now.AddDays(-i);

                                var filterDate = query.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();
                                var filterPurchaseDate = purchaseItemQuery.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();

                                productSummaryList.Add(new ProductSummaryLookupModel()
                                {
                                    Code = filterDate.FirstOrDefault()?.Code,
                                    EnglishName = filterDate.FirstOrDefault()?.EnglishName,
                                    ArabicName = filterDate.FirstOrDefault()?.ArabicName,
                                    TotalPurchases = filterDate.Count(),
                                    TotalGrossAmount = filterDate.Sum(x => x.GrossAmount),
                                    TotalDiscountAmount = filterDate.Sum(x => x.DiscountAmount),
                                    TotalNetTotal = filterDate.Sum(x => x.NetTotal),
                                    TotalVatAmount = filterDate.Sum(x => x.VatAmount),
                                    TotalTotalAmount = filterDate.Sum(x => x.TotalAmount),
                                    TotalPurchaseGrossAmount = filterPurchaseDate.Sum(x => x.GrossAmount),
                                    TotalPurchaseDiscountAmount = filterPurchaseDate.Sum(x => x.DiscountAmount),
                                    TotalPurchaseNetTotal = filterPurchaseDate.Sum(x => x.NetTotal),
                                    TotalPurchaseVatAmount = filterPurchaseDate.Sum(x => x.VatAmount),
                                    TotalPurchaseAmount = filterPurchaseDate.Sum(x => x.TotalAmount),
                                    CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                    ApexChartValue = fromDate.ToString("MMM dd, yyyy"),
                                });
                            }
                        }
                        else if (request.NumberOfPeriods == "15 Days")
                        {
                            for (int i = 0; i < 14; i++)
                            {
                                DateTime fromDate = DateTime.Now.AddDays(-i);
                                DateTime toDate = DateTime.Now.AddDays(-i);

                                var filterDate = query.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();
                                var filterPurchaseDate = purchaseItemQuery.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();

                                productSummaryList.Add(new ProductSummaryLookupModel()
                                {
                                    Code = filterDate.FirstOrDefault()?.Code,
                                    EnglishName = filterDate.FirstOrDefault()?.EnglishName,
                                    ArabicName = filterDate.FirstOrDefault()?.ArabicName,
                                    TotalPurchases = filterDate.Count(),
                                    TotalGrossAmount = filterDate.Sum(x => x.GrossAmount),
                                    TotalDiscountAmount = filterDate.Sum(x => x.DiscountAmount),
                                    TotalNetTotal = filterDate.Sum(x => x.NetTotal),
                                    TotalVatAmount = filterDate.Sum(x => x.VatAmount),
                                    TotalTotalAmount = filterDate.Sum(x => x.TotalAmount),
                                    TotalPurchaseGrossAmount = filterPurchaseDate.Sum(x => x.GrossAmount),
                                    TotalPurchaseDiscountAmount = filterPurchaseDate.Sum(x => x.DiscountAmount),
                                    TotalPurchaseNetTotal = filterPurchaseDate.Sum(x => x.NetTotal),
                                    TotalPurchaseVatAmount = filterPurchaseDate.Sum(x => x.VatAmount),
                                    TotalPurchaseAmount = filterPurchaseDate.Sum(x => x.TotalAmount),
                                    CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                    ApexChartValue = fromDate.ToString("MMM dd, yyyy"),
                                });
                            }
                        }
                        else if (request.NumberOfPeriods == "30 Days")
                        {
                            for (int i = 0; i < 31; i++)
                            {
                                DateTime fromDate = DateTime.Now.AddDays(-i);
                                DateTime toDate = DateTime.Now.AddDays(-i);

                                var filterDate = query.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();
                                var filterPurchaseDate = purchaseItemQuery.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();

                                productSummaryList.Add(new ProductSummaryLookupModel()
                                {
                                    Code = filterDate.FirstOrDefault()?.Code,
                                    EnglishName = filterDate.FirstOrDefault()?.EnglishName,
                                    ArabicName = filterDate.FirstOrDefault()?.ArabicName,
                                    TotalPurchases = filterDate.Count(),
                                    TotalGrossAmount = filterDate.Sum(x => x.GrossAmount),
                                    TotalDiscountAmount = filterDate.Sum(x => x.DiscountAmount),
                                    TotalNetTotal = filterDate.Sum(x => x.NetTotal),
                                    TotalVatAmount = filterDate.Sum(x => x.VatAmount),
                                    TotalTotalAmount = filterDate.Sum(x => x.TotalAmount),
                                    TotalPurchaseGrossAmount = filterPurchaseDate.Sum(x => x.GrossAmount),
                                    TotalPurchaseDiscountAmount = filterPurchaseDate.Sum(x => x.DiscountAmount),
                                    TotalPurchaseNetTotal = filterPurchaseDate.Sum(x => x.NetTotal),
                                    TotalPurchaseVatAmount = filterPurchaseDate.Sum(x => x.VatAmount),
                                    TotalPurchaseAmount = filterPurchaseDate.Sum(x => x.TotalAmount),
                                    CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                    ApexChartValue = fromDate.ToString("MMM dd, yyyy"),
                                });
                            }
                        }
                        return productSummaryList;
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
