using Focus.Business.Interface;
using Focus.Business.Users;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;
using System.Threading;
using System;
using Dapper;
using System.Linq;

namespace Focus.Business.Reports.BalanceSheetReport
{
    public class GetBalanceSheetComparisonReport : IRequest<BalanceSheetListModel>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public class Handler : IRequestHandler<GetBalanceSheetComparisonReport, BalanceSheetListModel>
        {
            public readonly IConfiguration Configuration;
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context,
                ILogger<GetBalanceSheetQuery> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }
            public async Task<BalanceSheetListModel> Handle(GetBalanceSheetComparisonReport request, CancellationToken cancellationToken)
            {
                try
                {
                    var fromDate = request.FromDate.Date.ToString("yyyy-MM-dd hh:mm:ss", new CultureInfo("en-US").DateTimeFormat);
                    var toDate = request.ToDate.Date.ToString("yyyy-MM-dd hh:mm:ss", new CultureInfo("en-US").DateTimeFormat);
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "select CostCenters.Code,CostCenters.Name as CostCenter,CostCenters.NameArabic as CostCenterArabic,tr.Date as Date, AccountTypes.Name as AccountType,AccountTypes.NameArabic as AccountTypeArabic," +
                        " case when AccountTypes.Name = 'Assets' then sum(tr.Debit-tr.Credit) when AccountTypes.Name = 'Liabilities' OR AccountTypes.Name = 'Equity' then sum(tr.Credit-tr.Debit) end as Amount" +
                        " from AccountTypes" +
                        " left outer join CostCenters on  CostCenters.AccountTypeId = AccountTypes.Id" +
                        " left outer join Accounts on  CostCenters.Id = Accounts.CostCenterId" +
                        " left outer join Transactions tr on  Accounts.Id = tr.AccountId" +
                        " where (AccountTypes.Name = 'Assets' OR AccountTypes.Name = 'Liabilities' OR AccountTypes.Name = 'Equity') AND (CAST(tr.date as date) between  '" + fromDate + "'" +
                        "  and '" + toDate + "')" +
                        " group by CostCenters.Name,AccountTypes.Name,CostCenters.NameArabic, AccountTypes.NameArabic, CostCenters.Code, tr.Date";

                    var query = conn.Query<BalanceSheetModel>(sb).AsQueryable();
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
                        Asset = asset,
                        Liability = liability,
                        Equity = equity
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
