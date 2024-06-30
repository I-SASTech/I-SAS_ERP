using Dapper;
using Focus.Business.Interface;
using Focus.Business.Reports.AdvanceTrialBalanceReport.Models;
using Focus.Business.Users;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Reports.AdvanceTrialBalanceReport
{
    public class GetAdvanceTrialBalanceReportQuery : IRequest<List<AdvanceTrialBalanceLookupModel>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CompareWith { get; set; }
        public string NumberOfPeriods { get; set; }
        public string BranchId { get; set; }
        public class Handler : IRequestHandler<GetAdvanceTrialBalanceReportQuery, List<AdvanceTrialBalanceLookupModel>>
        {
            public readonly IConfiguration Configuration;
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context,
                ILogger<GetAdvanceTrialBalanceReportQuery> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }
            public async Task<List<AdvanceTrialBalanceLookupModel>> Handle(GetAdvanceTrialBalanceReportQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var fromDate = request.FromDate.Date.ToString("yyyy-MM-dd ", new CultureInfo("en-US").DateTimeFormat);
                    var toDate = request.ToDate.Date.ToString("yyyy-MM-dd ", new CultureInfo("en-US").DateTimeFormat);
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "select  CostCenters.Code, CostCenters.Name, CostCenters.NameArabic,Accounts.Name as AccountName,Accounts.NameArabic as AccountNameArabic,tr.Date," +
                                "  sum(tr.Debit) as Debit ," +
                                "  sum(tr.Credit) as Credit " +
                        " from CostCenters " +
                        " left outer join Accounts on  CostCenters.Id = Accounts.CostCenterId" +
                        " left outer join Transactions tr on  Accounts.Id = tr.AccountId" +
                        " Where tr.CompanyId= '" + _contextProvider.GetCompanyId() + "'" +
                        " group by  CostCenters.Code,CostCenters.Name,CostCenters.NameArabic,Accounts.Name,Accounts.NameArabic,tr.Date";

                    var query = conn.Query<AdvanceTrialBalanceLookupModel>(sb).AsQueryable();

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        query = query.Where(x => branchIdList.Contains(x.BranchId.ToString()));
                    }

                    if (request.CompareWith == "Previous Year(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        var trialBalance = new List<AdvanceTrialBalanceLookupModel>();
                        

                        for (int i = 0; i < comparePeriod; i++)
                        {
                            int year = DateTime.Now.Year;
                            int years = year - i;
                            DateTime firstDayOfYear = new DateTime(years, 1, 1);
                            DateTime lastDayOfYear = new DateTime(years, 12, 31);

                            var query1 = query.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date);
                            int aa = query.Count();
                            var trialLooks = query1.GroupBy(x => x.Name).ToList();

                            var inventoryList = trialLooks.Select(x => new AdvanceTrialBalanceLookupModel
                            {

                                Name = x.Key,
                                NameArabic = x.FirstOrDefault()?.NameArabic,
                                Code = x.FirstOrDefault()?.Code,
                                Debit = x.Sum(y => y.Debit),
                                Credit = x.Sum(y => y.Credit),
                                Total = Convert.ToDecimal((string.Format("{0:0.##}", x.Sum(y => y.Debit) - x.Sum(y => y.Credit)))),
                                TrialBalance = x.GroupBy(x => x.AccountName).Select(y => new TrialBalanceLookupModel
                                {
                                    Code = y.FirstOrDefault()?.Code,
                                    Debit = y.Sum(y => y.Debit),
                                    Credit = y.Sum(y => y.Credit),
                                    Name = y.FirstOrDefault().Name,
                                    AccountName = y.FirstOrDefault().AccountName,
                                    AccountNameArabic = y.FirstOrDefault().AccountNameArabic,
                                    Total = Convert.ToDecimal((string.Format("{0:0.##}", (y.Sum(y => y.Debit) - y.Sum(y => y.Credit))))),
                                }).ToList()

                            }).ToList();

                            trialBalance.Add(new AdvanceTrialBalanceLookupModel
                            {
                                ListOfAdvanceTrialBalances = inventoryList,
                                CompareWithValue = firstDayOfYear.ToString("dd/MM/yyyy") + " - " + lastDayOfYear.ToString("dd/MM/yyyy"),
                                CompareWithReport = firstDayOfYear.ToString("dd MMMM yyyy") + " - " + lastDayOfYear.ToString("dd MMMM yyyy"),
                            });
                        }
                        return trialBalance;
                    }
                    else if (request.CompareWith == "Previous Period(s)")
                    {
                        int extractedYear = int.TryParse(request.NumberOfPeriods.Substring(0, 4), out int year) ? year : -1;
                        int currentYear = DateTime.Now.Year;
                        var trialBalance = new List<AdvanceTrialBalanceLookupModel>();

                        int yearDiff = currentYear - extractedYear;

                        for (int i = 0; i <= yearDiff; i++)
                        {
                            int years = currentYear - i;
                            DateTime firstDayOfYear = new DateTime(years, 1, 1);
                            DateTime lastDayOfYear = new DateTime(years, 12, 31);

                            var query1 = query.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date);
                            var trialLooks = query1.GroupBy(x => x.Name).ToList();

                            var inventoryList = trialLooks.Select(x => new AdvanceTrialBalanceLookupModel
                            {

                                Name = x.Key,
                                CompareWith = firstDayOfYear.ToString("dd/MM/yyyy") + " - " + lastDayOfYear.ToString("dd/MM/yyyy"),
                                CompareWithReport = firstDayOfYear.ToString("dd MMMM yyyy") + " - " + lastDayOfYear.ToString("dd MMMM yyyy"),
                                NameArabic = x.FirstOrDefault()?.NameArabic,
                                Code = x.FirstOrDefault()?.Code,
                                Debit = x.Sum(y => y.Debit),
                                Credit = x.Sum(y => y.Credit),
                                Total = Convert.ToDecimal((string.Format("{0:0.##}", x.Sum(y => y.Debit) - x.Sum(y => y.Credit)))),
                                TrialBalance = x.GroupBy(x => x.AccountName).Select(y => new TrialBalanceLookupModel
                                {
                                    Code = y.FirstOrDefault()?.Code,
                                    Debit = y.Sum(y => y.Debit),
                                    Credit = y.Sum(y => y.Credit),
                                    Name = y.FirstOrDefault().Name,
                                    AccountName = y.FirstOrDefault().AccountName,
                                    AccountNameArabic = y.FirstOrDefault().AccountNameArabic,
                                    Total = Convert.ToDecimal((string.Format("{0:0.##}", (y.Sum(y => y.Debit) - y.Sum(y => y.Credit))))),
                                }).ToList()

                            }).ToList();

                            trialBalance.Add(new AdvanceTrialBalanceLookupModel
                            {
                                ListOfAdvanceTrialBalances = inventoryList,
                                CompareWithValue = firstDayOfYear.ToString("dd/MM/yyyy") + " - " + lastDayOfYear.ToString("dd/MM/yyyy"),
                                CompareWithReport = firstDayOfYear.ToString("dd MMMM yyyy") + " - " + lastDayOfYear.ToString("dd MMMM yyyy"),
                            });
                        }
                        return trialBalance;
                    }
                    else if (request.CompareWith == "Previous Quarter(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        var trialBalance = new List<AdvanceTrialBalanceLookupModel>();


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

                            DateTime startDate = new DateTime(year, 3 * quarter + 1, 1);
                            DateTime endDate = startDate.AddMonths(3).AddDays(-1);

                            var query1 = query.Where(x => x.Date.Date >= startDate.Date && x.Date.Date <= endDate.Date);
                            var trialLooks = query1.GroupBy(x => x.Name).ToList();

                            var inventoryList = trialLooks.Select(x => new AdvanceTrialBalanceLookupModel
                            {

                                Name = x.Key,
                                CompareWith = startDate.ToString("dd/MM/yyyy") + " - " + endDate.ToString("dd/MM/yyyy"),
                                CompareWithReport = startDate.ToString("dd MMMM yyyy") + " - " + endDate.ToString("dd MMMM yyyy"),
                                NameArabic = x.FirstOrDefault()?.NameArabic,
                                Code = x.FirstOrDefault()?.Code,
                                Debit = x.Sum(y => y.Debit),
                                Credit = x.Sum(y => y.Credit),
                                Total = Convert.ToDecimal((string.Format("{0:0.##}", x.Sum(y => y.Debit) - x.Sum(y => y.Credit)))),
                                TrialBalance = x.GroupBy(x => x.AccountName).Select(y => new TrialBalanceLookupModel
                                {
                                    Code = y.FirstOrDefault()?.Code,
                                    Debit = y.Sum(y => y.Debit),
                                    Credit = y.Sum(y => y.Credit),
                                    Name = y.FirstOrDefault().Name,
                                    AccountName = y.FirstOrDefault().AccountName,
                                    AccountNameArabic = y.FirstOrDefault().AccountNameArabic,
                                    Total = Convert.ToDecimal((string.Format("{0:0.##}", (y.Sum(y => y.Debit) - y.Sum(y => y.Credit))))),
                                }).ToList()

                            }).ToList();

                            trialBalance.Add(new AdvanceTrialBalanceLookupModel
                            {
                                ListOfAdvanceTrialBalances = inventoryList,
                                CompareWithValue = startDate.ToString("dd/MM/yyyy") + " - " + endDate.ToString("dd/MM/yyyy"),
                                CompareWithReport = startDate.ToString("dd MMMM yyyy") + " - " + endDate.ToString("dd MMMM yyyy"),
                            });
                        }

                        return trialBalance;
                    }
                    else if (request.CompareWith == "Previous Month(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        var trialBalance = new List<AdvanceTrialBalanceLookupModel>();


                        for (int i = 1; i <= comparePeriod; i++)
                        {
                            DateTime fromDates = DateTime.Now.AddMonths(-i);
                            DateTime toDates = DateTime.Now.AddMonths(-(i - 1));

                            var query1 = query.Where(x => x.Date.Date >= fromDates.Date && x.Date.Date <= toDates.Date);
                            var trialLooks = query1.GroupBy(x => x.Name).ToList();

                            var inventoryList = trialLooks.Select(x => new AdvanceTrialBalanceLookupModel
                            {

                                Name = x.Key,
                                CompareWith = fromDates.ToString("dd/MM/yyyy") + " - " + toDates.ToString("dd/MM/yyyy"),
                                CompareWithReport = fromDates.ToString("dd MMMM yyyy") + " - " + toDates.ToString("dd MMMM yyyy"),
                                NameArabic = x.FirstOrDefault()?.NameArabic,
                                Code = x.FirstOrDefault()?.Code,
                                Debit = x.Sum(y => y.Debit),
                                Credit = x.Sum(y => y.Credit),
                                Total = Convert.ToDecimal((string.Format("{0:0.##}", x.Sum(y => y.Debit) - x.Sum(y => y.Credit)))),
                                TrialBalance = x.GroupBy(x => x.AccountName).Select(y => new TrialBalanceLookupModel
                                {
                                    Code = y.FirstOrDefault()?.Code,
                                    Debit = y.Sum(y => y.Debit),
                                    Credit = y.Sum(y => y.Credit),
                                    Name = y.FirstOrDefault().Name,
                                    AccountName = y.FirstOrDefault().AccountName,
                                    AccountNameArabic = y.FirstOrDefault().AccountNameArabic,
                                    Total = Convert.ToDecimal((string.Format("{0:0.##}", (y.Sum(y => y.Debit) - y.Sum(y => y.Credit))))),
                                }).ToList()

                            }).ToList();

                            trialBalance.Add(new AdvanceTrialBalanceLookupModel
                            {
                                ListOfAdvanceTrialBalances = inventoryList,
                                CompareWithValue = fromDates.ToString("dd/MM/yyyy") + " - " + toDates.ToString("dd/MM/yyyy"),
                                CompareWithReport = fromDates.ToString("dd MMMM yyyy") + " - " + toDates.ToString("dd MMMM yyyy"),
                            });
                        }

                        return trialBalance;
                    }
                    else
                    {
                        var trialBalance = new List<AdvanceTrialBalanceLookupModel>();

                        var query1 = query.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);
                        var trialLooks = query1.GroupBy(x => x.Name).ToList(); 
                        
                        var inventoryList = trialLooks.Select(x => new AdvanceTrialBalanceLookupModel
                        {

                            Name = x.Key,
                            NameArabic = x.FirstOrDefault()?.NameArabic,
                            Code = x.FirstOrDefault()?.Code,
                            Debit = x.Sum(y => y.Debit),
                            Credit = x.Sum(y => y.Credit),
                            Total = Convert.ToDecimal((string.Format("{0:0.##}", x.Sum(y => y.Debit) - x.Sum(y => y.Credit)))),
                            TrialBalance = x.GroupBy(x => x.AccountName).Select(y => new TrialBalanceLookupModel
                            {
                                Code = y.FirstOrDefault()?.Code,
                                Debit = y.Sum(y => y.Debit),
                                Credit = y.Sum(y => y.Credit),
                                Name = y.FirstOrDefault().Name,
                                AccountName = y.FirstOrDefault().AccountName,
                                AccountNameArabic = y.FirstOrDefault().AccountNameArabic,
                                Total = Convert.ToDecimal((string.Format("{0:0.##}", (y.Sum(y => y.Debit) - y.Sum(y => y.Credit))))),
                            }).ToList()

                        }).ToList();
                        return inventoryList;
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
