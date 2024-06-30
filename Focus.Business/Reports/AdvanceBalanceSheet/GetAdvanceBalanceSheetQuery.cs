using Dapper;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.Interface;
using Focus.Business.Reports.BalanceSheetReport;
using Focus.Business.Reports.IncomeStatementReport;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Reports.AdvanceBalanceSheet
{
    public class GetAdvanceBalanceSheetQuery : IRequest<BalanceSheetListModel>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CompareWith { get; set; }
        public string NumberOfPeriods { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<GetAdvanceBalanceSheetQuery, BalanceSheetListModel>
        {
            public readonly IConfiguration Configuration;
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context,
                ILogger<GetIncomeStatementQuery> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }
            public async Task<BalanceSheetListModel> Handle(GetAdvanceBalanceSheetQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var fromDate = request.FromDate.Date.ToString("yyyy-MM-dd hh:mm:ss", new CultureInfo("en-US").DateTimeFormat);
                    var toDate = request.ToDate.Date.ToString("yyyy-MM-dd hh:mm:ss", new CultureInfo("en-US").DateTimeFormat);
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "select CostCenters.Code,CostCenters.Name as CostCenter,CostCenters.NameArabic as CostCenterArabic,AccountTypes.Name as AccountType,AccountTypes.NameArabic as AccountTypeArabic, tr.Date," +
                        " case when AccountTypes.Name = 'Assets' then sum(tr.Debit-tr.Credit) when AccountTypes.Name = 'Liabilities' OR AccountTypes.Name = 'Equity' OR AccountTypes.Name = 'Income' OR AccountTypes.Name = 'Expenses' then sum(tr.Credit-tr.Debit) end as Amount" +
                        " from AccountTypes" +
                        " left outer join CostCenters on  CostCenters.AccountTypeId = AccountTypes.Id" +
                        " left outer join Accounts on  CostCenters.Id = Accounts.CostCenterId" +
                        " left outer join Transactions tr on  Accounts.Id = tr.AccountId" +
                        " where (AccountTypes.Name = 'Assets' OR AccountTypes.Name = 'Liabilities' OR AccountTypes.Name = 'Equity' OR AccountTypes.Name = 'Income' OR AccountTypes.Name = 'Expenses') " +
                        " group by CostCenters.Name,AccountTypes.Name,CostCenters.NameArabic, AccountTypes.NameArabic, CostCenters.Code, tr.Date";

                    var query = conn.Query<BalanceSheetModel>(sb).AsQueryable();
                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        query = query.Where(x => branchIdList.Contains(x.BranchId.ToString()));
                    }

                    var comparisonBalanceSheet = new List<BalanceSheetComparisonLookupModel>();

                    if (request.CompareWith == "Previous Year(s)")
                     {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);

                        for (int i = 0; i < comparePeriod; i++)
                        {
                            int year = DateTime.Now.Year;
                            int years = year - i;
                            DateTime firstDayOfYear = new DateTime(years, 1, 1);
                            DateTime lastDayOfYear = new DateTime(years, 12, 31);


                            var query1 = query.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date);

                            var asset1 = new List<BalanceSheetModel>();
                            var liability1 = new List<BalanceSheetModel>();
                            var equity1 = new List<BalanceSheetModel>();
                            var income = new List<BalanceSheetModel>();
                            var expense = new List<BalanceSheetModel>();

                            foreach (var data in query1)
                            {
                                if (data.AccountType == "Assets")
                                {
                                    asset1.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                                if (data.AccountType == "Liabilities")
                                {
                                    liability1.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                                if (data.AccountType == "Equity")
                                {
                                    equity1.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                                if (data.AccountType == "Income")
                                {
                                    income.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                                if (data.AccountType == "Expenses")
                                {
                                    expense.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                            }

                            var expneseTotal = expense.Sum(x => x.Amount);
                            var incomeTotal = income.Sum(x => x.Amount);

                            comparisonBalanceSheet.Add(new BalanceSheetComparisonLookupModel()
                            {
                                TotalAssets = asset1.Count > 0 ? asset1.Sum(x => x.Amount) : 0,
                                TotalLiabilities = liability1.Count > 0 ? liability1.Sum(x => x.Amount) : 0,
                                TotalEquities = equity1.Count > 0 ? equity1.Sum(x => x.Amount) : 0,
                                NetAmount = Math.Abs(incomeTotal) - (-1 * expneseTotal),
                                CompareWith = years.ToString(),
                                CompareWithForReport = years.ToString(),
                                Asset = asset1,
                                Liability = liability1,
                                Equity= equity1,
                            });
                        }
                        return new BalanceSheetListModel
                        {
                            BalanceSheetComparison = comparisonBalanceSheet,
                            Asset = {},
                            Liability = {},
                            Equity = { }
                        };
                    }
                    else if(request.CompareWith == "Previous Period(s)")
                    {
                        

                        int extractedYear = int.TryParse(request.NumberOfPeriods.Substring(0, 4), out int year) ? year : -1;
                        int currentYear = DateTime.Now.Year;

                        int yearDiff = currentYear - extractedYear;

                        for (int i = 0; i <= yearDiff; i++)
                        {
                            int years = currentYear - i;
                            DateTime firstDayOfYear = new DateTime(years, 1, 1);
                            DateTime lastDayOfYear = new DateTime(years, 12, 31);


                            var query1 = query.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date);

                            var asset1 = new List<BalanceSheetModel>();
                            var liability1 = new List<BalanceSheetModel>();
                            var equity1 = new List<BalanceSheetModel>();
                            var income = new List<BalanceSheetModel>();
                            var expense = new List<BalanceSheetModel>();

                            foreach (var data in query1)
                            {
                                if (data.AccountType == "Assets")
                                {
                                    asset1.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                                if (data.AccountType == "Liabilities")
                                {
                                    liability1.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                                if (data.AccountType == "Equity")
                                {
                                    equity1.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                                if (data.AccountType == "Income")
                                {
                                    income.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                                if (data.AccountType == "Expenses")
                                {
                                    expense.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                            }
                            var expneseTotal = expense.Sum(x => x.Amount);
                            var incomeTotal = income.Sum(x => x.Amount);
                            comparisonBalanceSheet.Add(new BalanceSheetComparisonLookupModel()
                            {
                                TotalAssets = asset1.Count > 0 ? asset1.Sum(x => x.Amount) : 0,
                                TotalLiabilities = liability1.Count > 0 ? liability1.Sum(x => x.Amount) : 0,
                                TotalEquities = equity1.Count > 0 ? equity1.Sum(x => x.Amount) : 0,
                                NetAmount = Math.Abs(incomeTotal) - (-1 * expneseTotal),
                                CompareWith = years.ToString(),
                                CompareWithForReport = years.ToString(),
                                Asset = asset1,
                                Liability = liability1,
                                Equity = equity1,
                            });
                        }
                        return new BalanceSheetListModel
                        {
                            BalanceSheetComparison = comparisonBalanceSheet,
                            Asset = { },
                            Liability = { },
                            Equity = { }
                        };
                    }
                    else if(request.CompareWith == "Previous Quarter(s)")
                    {
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

                            DateTime startDate = new DateTime(year, 3 * quarter + 1, 1);
                            DateTime endDate = startDate.AddMonths(3).AddDays(-1);

                            var query1 = query.Where(x => x.Date.Date >= startDate.Date && x.Date.Date <= endDate.Date);

                            var asset1 = new List<BalanceSheetModel>();
                            var liability1 = new List<BalanceSheetModel>();
                            var equity1 = new List<BalanceSheetModel>();
                            var income = new List<BalanceSheetModel>();
                            var expense = new List<BalanceSheetModel>();

                            foreach (var data in query1)
                            {
                                if (data.AccountType == "Assets")
                                {
                                    asset1.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                                if (data.AccountType == "Liabilities")
                                {
                                    liability1.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                                if (data.AccountType == "Equity")
                                {
                                    equity1.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                                if (data.AccountType == "Income")
                                {
                                    income.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                                if (data.AccountType == "Expenses")
                                {
                                    expense.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                            }
                            var expneseTotal = expense.Sum(x => x.Amount);
                            var incomeTotal = income.Sum(x => x.Amount);
                            comparisonBalanceSheet.Add(new BalanceSheetComparisonLookupModel()
                            {
                                TotalAssets = asset1.Count > 0 ? asset1.Sum(x => x.Amount) : 0,
                                TotalLiabilities = liability1.Count > 0 ? liability1.Sum(x => x.Amount) : 0,
                                TotalEquities = equity1.Count > 0 ? equity1.Sum(x => x.Amount) : 0,
                                CompareWith = startDate.ToString("dd/MM/yyyy") + " - " + endDate.ToString("dd/MM/yyyy"),
                                CompareWithForReport = startDate.ToString("dd MMMM-yyyy") + " - " + endDate.ToString("dd MMMM-yyyy"),
                                NetAmount = Math.Abs(incomeTotal) - (-1 * expneseTotal),
                                Asset = asset1,
                                Liability = liability1,
                                Equity = equity1,
                            });
                        }
                        return new BalanceSheetListModel
                        {
                            BalanceSheetComparison = comparisonBalanceSheet,
                            Asset = { },
                            Liability = { },
                            Equity = { }
                        };
                    } 
                    else if(request.CompareWith == "Previous Month(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);

                        for (int i = 1; i <= comparePeriod; i++)
                        {
                            DateTime fromDates = DateTime.Now.AddMonths(-i);
                            DateTime toDates =  DateTime.Now.AddMonths(-(i-1));


                            var query1 = query.Where(x => x.Date.Date >= fromDates.Date && x.Date.Date <= toDates.Date);

                            var asset1 = new List<BalanceSheetModel>();
                            var liability1 = new List<BalanceSheetModel>();
                            var equity1 = new List<BalanceSheetModel>();
                            var income = new List<BalanceSheetModel>();
                            var expense = new List<BalanceSheetModel>();

                            foreach (var data in query1)
                            {
                                if (data.AccountType == "Assets")
                                {
                                    asset1.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                                if (data.AccountType == "Liabilities")
                                {
                                    liability1.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                                if (data.AccountType == "Equity")
                                {
                                    equity1.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                                if (data.AccountType == "Income")
                                {
                                    income.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                                if (data.AccountType == "Expenses")
                                {
                                    expense.Add(new BalanceSheetModel()
                                    {
                                        Code = data.Code,
                                        CostCenter = data.CostCenter,
                                        CostCenterArabic = data.CostCenterArabic,
                                        AccountType = data.AccountType,
                                        AccountTypeArabic = data.AccountTypeArabic,
                                        Amount = data.Amount,
                                        Date = data.Date
                                    });
                                }
                            }
                            var expneseTotal = expense.Sum(x => x.Amount);
                            var incomeTotal = income.Sum(x => x.Amount);
                            comparisonBalanceSheet.Add(new BalanceSheetComparisonLookupModel()
                            {
                                TotalAssets = asset1.Count > 0 ? asset1.Sum(x => x.Amount) : 0,
                                TotalLiabilities = liability1.Count > 0 ? liability1.Sum(x => x.Amount) : 0,
                                TotalEquities = equity1.Count > 0 ? equity1.Sum(x => x.Amount) : 0,
                                CompareWith = fromDates.ToString("dd/MM/yyyy") + " - " + toDates.ToString("dd/MM/yyyy"),
                                CompareWithForReport = fromDates.ToString("dd MMMM-yyyy") + " - " + toDates.ToString("dd MMMM-yyyy"),
                                NetAmount = Math.Abs(incomeTotal) - (-1 * expneseTotal),
                                Asset = asset1,
                                Liability = liability1,
                                Equity = equity1,
                            });
                        }
                        return new BalanceSheetListModel
                        {
                            BalanceSheetComparison = comparisonBalanceSheet,
                            Asset = { },
                            Liability = { },
                            Equity = { }
                        };
                    }
                    else
                    {
                        query = query.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);

                        var asset = new List<BalanceSheetModel>();
                        var liability = new List<BalanceSheetModel>();
                        var equity = new List<BalanceSheetModel>();

                        foreach (var data in query)
                        {
                            if (data.AccountType == "Assets")
                            {
                                asset.Add(new BalanceSheetModel()
                                {
                                    Code = data.Code,
                                    CostCenter = data.CostCenter,
                                    CostCenterArabic = data.CostCenterArabic,
                                    AccountType = data.AccountType,
                                    AccountTypeArabic = data.AccountTypeArabic,
                                    Amount = data.Amount,
                                    Date = data.Date
                                });
                            }
                            if (data.AccountType == "Liabilities")
                            {
                                liability.Add(new BalanceSheetModel()
                                {
                                    Code = data.Code,
                                    CostCenter = data.CostCenter,
                                    CostCenterArabic = data.CostCenterArabic,
                                    AccountType = data.AccountType,
                                    AccountTypeArabic = data.AccountTypeArabic,
                                    Amount = data.Amount,
                                    Date = data.Date
                                });
                            }
                            if (data.AccountType == "Equity")
                            {
                                equity.Add(new BalanceSheetModel()
                                {
                                    Code = data.Code,
                                    CostCenter = data.CostCenter,
                                    CostCenterArabic = data.CostCenterArabic,
                                    AccountType = data.AccountType,
                                    AccountTypeArabic = data.AccountTypeArabic,
                                    Amount = data.Amount,
                                    Date = data.Date
                                });
                            }
                        }
                        return new BalanceSheetListModel
                        {
                            BalanceSheetComparison = {},
                            Asset = asset,
                            Liability = liability,
                            Equity = equity
                        };
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
