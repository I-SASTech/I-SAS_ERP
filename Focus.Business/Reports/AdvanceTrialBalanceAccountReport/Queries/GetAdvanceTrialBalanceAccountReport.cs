using Focus.Business.Interface;
using Focus.Business.Reports.AdvanceTrialBalanceAccountReport.Models;
using Focus.Business.Reports.TrialBalanceAccountReport;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using Focus.Business.Reports.AdvanceTrialBalanceReport.Models;
using Focus.Domain.Entities;
using System.Diagnostics;

namespace Focus.Business.Reports.AdvanceTrialBalanceAccountReport.Queries
{
    public class GetAdvanceTrialBalanceAccountReport : IRequest<List<AdvanceTrialBalanceAccountLookupModel>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CompareWith { get; set; }
        public string NumberOfPeriods { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<GetAdvanceTrialBalanceAccountReport, List<AdvanceTrialBalanceAccountLookupModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            public readonly IConfiguration Configuration;
            private readonly IUserHttpContextProvider _contextProvider;
            public Handler(IApplicationDbContext context, ILogger<GetAdvanceTrialBalanceAccountReport> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
            }

            public async Task<List<AdvanceTrialBalanceAccountLookupModel>> Handle(GetAdvanceTrialBalanceAccountReport request, CancellationToken cancellationToken)
            {
                try
                {
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "select ac.Code as AccountCode, ac.Name as AccountName, ac.NameArabic as AccountNameArabic,cc.Name as CostCenter," +
                                " act.Name as AccountType,t.Date, SUM(t.Credit) as Credit, SUM(t.Debit) as Debit, SUM(t.Debit - t.Credit) as Total from Transactions t" +
                                " inner join  Accounts ac on t.AccountId = ac.Id inner join CostCenters cc on ac.CostCenterId = cc.Id " +
                        " inner join AccountTypes act on cc.AccountTypeId = act.Id " +
                        " Where t.CompanyId= '" + _contextProvider.GetCompanyId() + "'" +
                        " Group by ac.Code, ac.Name, ac.NameArabic,cc.Name,act.Name,t.Date";


                    var query = conn.Query<AdvanceTrialBalanceAccountLookupModel>(sb).AsQueryable();

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        query = query.Where(x => branchIdList.Contains(x.BranchId.ToString()));
                    }
                    if (request.CompareWith == "Previous Year(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        var trialBalanceAccount = new List<AdvanceTrialBalanceAccountLookupModel>();
                        for (int i = 0; i < comparePeriod; i++)
                        {
                            int year = DateTime.Now.Year;
                            int years = year - i;
                            DateTime firstDayOfYear = new DateTime(years, 1, 1);
                            DateTime lastDayOfYear = new DateTime(years, 12, 31);

                            var query1 = query.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date).ToList();

                            var query2 = query1.GroupBy(x => x.CostCenter).ToList();

                            var trialBalanceAccountList = query2.Select(x => new AdvanceTrialBalanceAccountLookupModel()
                            {
                                AccountCode = x.FirstOrDefault()?.AccountCode,
                                AccountName = x.FirstOrDefault()?.AccountName,
                                AccountNameArabic = x.FirstOrDefault()?.AccountNameArabic,
                                CostCenter = x.Key,
                                AccountType = x.FirstOrDefault()?.AccountType,
                                Debit = x.Sum(x => x.Debit),
                                Credit = x.Sum(x => x.Credit),
                                Total = Convert.ToDecimal((string.Format("{0:0.##}", (x.Sum(y => y.Debit) - x.Sum(y => y.Credit))))),
                            }).ToList();

                            trialBalanceAccount.Add(new AdvanceTrialBalanceAccountLookupModel()
                            {
                                CompareWith = firstDayOfYear.ToString("dd/MM/yyyy") + " - " + lastDayOfYear.ToString("dd/MM/yyyy"),
                                CompairWithReport = firstDayOfYear.ToString("dd MMMM-yyyy") + " - " + lastDayOfYear.ToString("dd MMMM-yyyy"),
                                CompareWithList = trialBalanceAccountList,
                            });
                        }
                        return trialBalanceAccount;
                    }
                    else if (request.CompareWith == "Previous Period(s)")
                    {
                        int extractedYear = int.TryParse(request.NumberOfPeriods.Substring(0, 4), out int year) ? year : -1;
                        int currentYear = DateTime.Now.Year;
                        var trialBalanceAccount = new List<AdvanceTrialBalanceAccountLookupModel>();

                        int yearDiff = currentYear - extractedYear;

                        for (int i = 0; i <= yearDiff; i++)
                        {
                            int years = currentYear - i;
                            DateTime firstDayOfYear = new DateTime(years, 1, 1);
                            DateTime lastDayOfYear = new DateTime(years, 12, 31);

                            var query1 = query.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date).ToList();
                            var query2 = query1.GroupBy(x => x.CostCenter).ToList();

                            var trialBalanceAccountList = query2.Select(x => new AdvanceTrialBalanceAccountLookupModel()
                            {
                                AccountCode = x.FirstOrDefault()?.AccountCode,
                                AccountName = x.FirstOrDefault()?.AccountName,
                                AccountNameArabic = x.FirstOrDefault()?.AccountNameArabic,
                                CostCenter = x.Key,
                                AccountType = x.FirstOrDefault()?.AccountType,
                                Debit = x.Sum(x => x.Debit),
                                Credit = x.Sum(x => x.Credit),
                                Total = Convert.ToDecimal((string.Format("{0:0.##}", (x.Sum(y => y.Debit) - x.Sum(y => y.Credit))))),
                            }).ToList();

                            trialBalanceAccount.Add(new AdvanceTrialBalanceAccountLookupModel()
                            {
                                CompareWith = firstDayOfYear.ToString("dd/MM/yyyy") + " - " + lastDayOfYear.ToString("dd/MM/yyyy"),
                                CompairWithReport = firstDayOfYear.ToString("dd MMMM-yyyy") + " - " + lastDayOfYear.ToString("dd MMMM-yyyy"),
                                CompareWithList = trialBalanceAccountList,
                            });
                        }
                        return trialBalanceAccount;
                    }
                    else if (request.CompareWith == "Previous Quarter(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        var trialBalanceAccount = new List<AdvanceTrialBalanceAccountLookupModel>();


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

                            var query1 = query.Where(x => x.Date.Date >= startDate.Date && x.Date.Date <= endDate.Date).ToList();

                            var query2 = query1.GroupBy(x => x.CostCenter).ToList();

                            var trialBalanceAccountList = query2.Select(x => new AdvanceTrialBalanceAccountLookupModel()
                            {
                                AccountCode = x.FirstOrDefault()?.AccountCode,
                                AccountName = x.FirstOrDefault()?.AccountName,
                                AccountNameArabic = x.FirstOrDefault()?.AccountNameArabic,
                                CostCenter = x.Key,
                                AccountType = x.FirstOrDefault()?.AccountType,
                                Debit = x.Sum(x => x.Debit),
                                Credit = x.Sum(x => x.Credit),
                                Total = Convert.ToDecimal((string.Format("{0:0.##}", (x.Sum(y => y.Debit) - x.Sum(y => y.Credit))))),
                            }).ToList();

                            trialBalanceAccount.Add(new AdvanceTrialBalanceAccountLookupModel()
                            {
                                CompareWith = startDate.ToString("dd/MM/yyyy") + " - " + endDate.ToString("dd/MM/yyyy"),
                                CompairWithReport = startDate.ToString("dd MMMM-yyyy") + " - " + endDate.ToString("dd MMMM-yyyy"),
                                CompareWithList = trialBalanceAccountList,
                            });
                        }
                        return trialBalanceAccount;
                    }
                    else if (request.CompareWith == "Previous Month(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        var trialBalanceAccount = new List<AdvanceTrialBalanceAccountLookupModel>();

                        for (int i = 1; i <= comparePeriod; i++)
                        {
                            DateTime fromDates = DateTime.Now.AddMonths(-i);
                            DateTime toDates = DateTime.Now.AddMonths(-(i - 1));

                            var query1 = query.Where(x => x.Date.Date >= fromDates.Date && x.Date.Date <= toDates.Date).ToList();
                            var query2 = query1.GroupBy(x => x.CostCenter).ToList();

                            var trialBalanceAccountList = query2.Select(x => new AdvanceTrialBalanceAccountLookupModel()
                            {
                                AccountCode = x.FirstOrDefault()?.AccountCode,
                                AccountName = x.FirstOrDefault()?.AccountName,
                                AccountNameArabic = x.FirstOrDefault()?.AccountNameArabic,
                                CostCenter = x.Key,
                                AccountType = x.FirstOrDefault()?.AccountType,
                                Debit = x.Sum(x => x.Debit),
                                Credit = x.Sum(x => x.Credit),
                                Total = Convert.ToDecimal((string.Format("{0:0.##}", (x.Sum(y => y.Debit) - x.Sum(y => y.Credit))))),
                            }).ToList();

                            trialBalanceAccount.Add(new AdvanceTrialBalanceAccountLookupModel()
                            {
                                CompareWith = fromDates.ToString("dd/MM/yyyy") + " - " + toDates.ToString("dd/MM/yyyy"),
                                CompairWithReport = fromDates.ToString("dd MMMM-yyyy") + " - " + toDates.ToString("dd MMMM-yyyy"),
                                CompareWithList = trialBalanceAccountList,
                            });
                        }
                        return trialBalanceAccount;
                    }
                    else
                    {
                        var query1 = query.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date).ToList();

                        var query2 = query1.GroupBy(x => x.CostCenter).ToList();

                        var trialBalanceAccountList = query2.Select(x => new AdvanceTrialBalanceAccountLookupModel()
                        {
                            AccountCode = x.FirstOrDefault()?.AccountCode,
                            AccountName = x.FirstOrDefault()?.AccountName,
                            AccountNameArabic = x.FirstOrDefault()?.AccountNameArabic,
                            CostCenter = x.Key,
                            AccountType = x.FirstOrDefault()?.AccountType,
                            Debit = x.Sum(x => x.Debit),
                            Credit = x.Sum(x => x.Credit),
                            Total = Convert.ToDecimal((string.Format("{0:0.##}", (x.Sum(y => y.Debit) - x.Sum(y => y.Credit))))),
                        }).ToList();

                        
                        return trialBalanceAccountList;

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
