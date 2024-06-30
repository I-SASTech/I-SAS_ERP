using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Focus.Business.Interface;
using Focus.Business.Users;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Focus.Business.Reports.IncomeStatementReport
{
    public class GetIncomeStatementQuery : IRequest<IncomeStatementListModel>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string BranchId { get; set; }


        public class Handler : IRequestHandler<GetIncomeStatementQuery, IncomeStatementListModel>
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
            public async Task<IncomeStatementListModel> Handle(GetIncomeStatementQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var fromDate = request.FromDate.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US").DateTimeFormat);
                    var toDate = request.ToDate.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US").DateTimeFormat);
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "select CostCenters.Code,CostCenters.Name as CostCenter,CostCenters.NameArabic as CostCenterArabic,AccountTypes.Name as AccountType,AccountTypes.NameArabic as AccountTypeArabic, sum(tr.Debit-tr.Credit) as Amount" +
                                " from AccountTypes" +
                                " left outer join CostCenters on  CostCenters.AccountTypeId = AccountTypes.Id" +
                                " left outer join Accounts on  CostCenters.Id = Accounts.CostCenterId" +
                                " left outer join Transactions tr on  Accounts.Id = tr.AccountId" +
                                " where (AccountTypes.Name = 'Income' OR AccountTypes.Name = 'Expenses') AND (cast (tr.Date as date )  between  '" + fromDate + "'" +
                                "  and '" + toDate + "')" +
                                " group by CostCenters.Name,AccountTypes.Name,CostCenters.NameArabic, AccountTypes.NameArabic, CostCenters.Code";

                    var query = conn.Query<IncomeStatementModel>(sb).AsQueryable();
                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        query = query.Where(x => branchIdList.Contains(x.BranchId.ToString()));
                    }
                    var income = new List<IncomeStatementModel>();
                    var expenses = new List<IncomeStatementModel>();
                    foreach (var data in query)
                    {
                        if (data.AccountType == "Income")
                        {
                            income.Add(new IncomeStatementModel()
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
                            expenses.Add(new IncomeStatementModel()
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
                    return new IncomeStatementListModel
                    {
                        Income = income, Expenses = expenses
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
