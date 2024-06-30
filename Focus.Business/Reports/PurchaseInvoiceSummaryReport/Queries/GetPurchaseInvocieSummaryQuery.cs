using Dapper;
using Focus.Business.Interface;
using Focus.Business.Reports.AdvanceAccountLedger.Models;
using Focus.Business.Reports.PurchaseInvoiceSummaryReport.Models;
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

namespace Focus.Business.Reports.PurchaseInvoiceSummaryReport.Queries
{
    public class GetPurchaseInvocieSummaryQuery : IRequest<List<PurchaseInvoiceSummaryLookupModel>>
    {
        public string CompareWith { get; set; }
        public string NumberOfPeriods { get; set; }
        public string InvoiceType { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<GetPurchaseInvocieSummaryQuery, List<PurchaseInvoiceSummaryLookupModel>>
        {
            public readonly IConfiguration Configuration;
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context,
                ILogger<GetPurchaseInvocieSummaryQuery> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }
            public async Task<List<PurchaseInvoiceSummaryLookupModel>> Handle(GetPurchaseInvocieSummaryQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "Select sa.Id as PurchaseId, c.id as SuplierId, sa.IsPurchasePost  as IsPurchasePost,sa.IsPurchaseReturn as IsPurchaseReturn, sa.RegistrationNo as RegistrationNo, " +
                                "sa.Date as Date, c.CustomerDisplayName as SuplierDisplayName, sa.TotalWithOutAmount as GrossAmount, " +
                                "sa.DiscountAmount as DiscountAmount,(sa.TotalWithOutAmount - sa.DiscountAmount) as NetTotal, " +
                                "sa.VatAmount as VatAmount, sa.TotalAmount as TotalAmount from PurchasePosts sa " +
                                "join Contacts c on sa.SupplierId = c.Id " +
                                "where sa.IsPurchasePost = 1 or sa.IsPurchaseReturn = 1 order by sa.RegistrationNo";

                    var query = conn.Query<PurchaseInvoiceSummaryDapperDateLookupModel>(sb).AsQueryable();
                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        query = query.Where(x => branchIdList.Contains(x.BranchId.ToString()));
                    }
                    if (request.InvoiceType == "Purchase Inovice Post")
                    {
                        query = query.Where(x => x.IsPurchasePost);
                    }
                    else if (request.InvoiceType == "Purchase Return")
                    {
                        query = query.Where(x => x.IsPurchaseReturn);
                    }

                    if (request.CompareWith == "Previous Year(s)")
                    {
                        var purchaseInvoiceSummaryList = new List<PurchaseInvoiceSummaryLookupModel>();

                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);

                        for (int i = 0; i < comparePeriod; i++)
                        {
                            int year = DateTime.Now.Year;
                            int years = year - i;
                            DateTime fromDate = new DateTime(years, 1, 1);
                            DateTime toDate = new DateTime(years, 12, 31);

                            var filterDate = query.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();

                            purchaseInvoiceSummaryList.Add(new PurchaseInvoiceSummaryLookupModel()
                            {
                                TotalPurchases = filterDate.Count(),
                                TotalGrossAmount = filterDate.Sum(x => x.GrossAmount),
                                TotalDiscountAmount = filterDate.Sum(x => x.DiscountAmount),
                                TotalNetTotal = filterDate.Sum(x => x.NetTotal),
                                TotalVatAmount = filterDate.Sum(x => x.VatAmount),
                                TotalTotalAmount = filterDate.Sum(x => x.TotalAmount),
                                CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                ApexChartValue = fromDate.ToString("yyyy"),
                            }) ;
                        }

                        return purchaseInvoiceSummaryList;
                    }
                    else if (request.CompareWith == "Previous Period(s)")
                    {
                        var purchaseInvoiceSummaryList = new List<PurchaseInvoiceSummaryLookupModel>();

                        int extractedYear = int.TryParse(request.NumberOfPeriods.Substring(0, 4), out int year) ? year : -1;
                        int currentYear = DateTime.Now.Year;

                        int yearDiff = currentYear - extractedYear;

                        for (int i = 0; i <= yearDiff; i++)
                        {
                            int years = currentYear - i;
                            DateTime fromDate = new DateTime(years, 1, 1);
                            DateTime toDate = new DateTime(years, 12, 31);

                            var filterDate = query.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();

                            purchaseInvoiceSummaryList.Add(new PurchaseInvoiceSummaryLookupModel()
                            {
                                TotalPurchases = filterDate.Count(),
                                TotalGrossAmount = filterDate.Sum(x => x.GrossAmount),
                                TotalDiscountAmount = filterDate.Sum(x => x.DiscountAmount),
                                TotalNetTotal = filterDate.Sum(x => x.NetTotal),
                                TotalVatAmount = filterDate.Sum(x => x.VatAmount),
                                TotalTotalAmount = filterDate.Sum(x => x.TotalAmount),
                                CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                ApexChartValue = fromDate.ToString("yyyy"),
                            });
                        }
                        return purchaseInvoiceSummaryList;
                    }
                    else if (request.CompareWith == "Previous Quarter(s)")
                    {
                        var purchaseInvoiceSummaryList = new List<PurchaseInvoiceSummaryLookupModel>();

                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        var advanceLedger = new List<AdvanceLedgerAccountLookupModel>();


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

                            purchaseInvoiceSummaryList.Add(new PurchaseInvoiceSummaryLookupModel()
                            {
                                TotalPurchases = filterDate.Count(),
                                TotalGrossAmount = filterDate.Sum(x => x.GrossAmount),
                                TotalDiscountAmount = filterDate.Sum(x => x.DiscountAmount),
                                TotalNetTotal = filterDate.Sum(x => x.NetTotal),
                                TotalVatAmount = filterDate.Sum(x => x.VatAmount),
                                TotalTotalAmount = filterDate.Sum(x => x.TotalAmount),
                                CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                ApexChartValue = fromDate.ToString("MMMM, yyyy")+ " - " + toDate.ToString("MMMM, yyyy"),
                            });
                        }
                        return purchaseInvoiceSummaryList;
                    }
                    else if (request.CompareWith == "Previous Month(s)")
                    {
                        var purchaseInvoiceSummaryList = new List<PurchaseInvoiceSummaryLookupModel>();

                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);


                        for (int i = 1; i <= comparePeriod; i++)
                        {
                            DateTime fromDate = DateTime.Now.AddMonths(-i);
                            DateTime toDate = DateTime.Now.AddMonths(-(i - 1));

                            var filterDate = query.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();

                            purchaseInvoiceSummaryList.Add(new PurchaseInvoiceSummaryLookupModel()
                            {
                                TotalPurchases = filterDate.Count(),
                                TotalGrossAmount = filterDate.Sum(x => x.GrossAmount),
                                TotalDiscountAmount = filterDate.Sum(x => x.DiscountAmount),
                                TotalNetTotal = filterDate.Sum(x => x.NetTotal),
                                TotalVatAmount = filterDate.Sum(x => x.VatAmount),
                                TotalTotalAmount = filterDate.Sum(x => x.TotalAmount),
                                CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                ApexChartValue = fromDate.ToString("MMM, yyyy") + " - " + toDate.ToString("MMM, yyyy"),
                            });
                        }
                        return purchaseInvoiceSummaryList;
                    }
                    else
                    {
                        var purchaseInvoiceSummaryList = new List<PurchaseInvoiceSummaryLookupModel>();
                        if (request.NumberOfPeriods == "7 Days")
                        {
                            for (int i = 0; i < 7; i++)
                            {
                                DateTime fromDate = DateTime.Now.AddDays(-i);
                                DateTime toDate = DateTime.Now.AddDays(-i);

                                var filterDate = query.Where(x => x.Date.Date >= fromDate.Date && x.Date.Date <= toDate.Date).ToList();

                                purchaseInvoiceSummaryList.Add(new PurchaseInvoiceSummaryLookupModel()
                                {
                                    TotalPurchases = filterDate.Count(),
                                    TotalGrossAmount = filterDate.Sum(x => x.GrossAmount),
                                    TotalDiscountAmount = filterDate.Sum(x => x.DiscountAmount),
                                    TotalNetTotal = filterDate.Sum(x => x.NetTotal),
                                    TotalVatAmount = filterDate.Sum(x => x.VatAmount),
                                    TotalTotalAmount = filterDate.Sum(x => x.TotalAmount),
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

                                purchaseInvoiceSummaryList.Add(new PurchaseInvoiceSummaryLookupModel()
                                {
                                    TotalPurchases = filterDate.Count(),
                                    TotalGrossAmount = filterDate.Sum(x => x.GrossAmount),
                                    TotalDiscountAmount = filterDate.Sum(x => x.DiscountAmount),
                                    TotalNetTotal = filterDate.Sum(x => x.NetTotal),
                                    TotalVatAmount = filterDate.Sum(x => x.VatAmount),
                                    TotalTotalAmount = filterDate.Sum(x => x.TotalAmount),
                                    ApexChartValue = fromDate.ToString("MMMM dd, yyyy"),
                                    CompareWith = fromDate.ToString("MMM dd,yyyy") + " - " + toDate.ToString("MMM dd,yyyy"),
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

                                purchaseInvoiceSummaryList.Add(new PurchaseInvoiceSummaryLookupModel()
                                {
                                    TotalPurchases = filterDate.Count(),
                                    TotalGrossAmount = filterDate.Sum(x => x.GrossAmount),
                                    TotalDiscountAmount = filterDate.Sum(x => x.DiscountAmount),
                                    TotalNetTotal = filterDate.Sum(x => x.NetTotal),
                                    TotalVatAmount = filterDate.Sum(x => x.VatAmount),
                                    TotalTotalAmount = filterDate.Sum(x => x.TotalAmount),
                                    CompareWith = fromDate.ToString("MMMM dd,yyyy") + " - " + toDate.ToString("MMMM dd,yyyy"),
                                    ApexChartValue = fromDate.ToString("MMM dd, yyyy"),
                                });
                            }
                        }
                        return purchaseInvoiceSummaryList;
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
