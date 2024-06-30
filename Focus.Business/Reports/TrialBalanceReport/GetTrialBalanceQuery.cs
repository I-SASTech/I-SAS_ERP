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

namespace Focus.Business.Reports.TrialBalanceReport
{
    public class GetTrialBalanceQuery : IRequest<List<TrialLookUp>>
    {
        
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<GetTrialBalanceQuery, List<TrialLookUp>>
        {
            public readonly IConfiguration Configuration;
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context,
                ILogger<GetTrialBalanceQuery> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }
            public async Task<List<TrialLookUp>> Handle(GetTrialBalanceQuery request, CancellationToken cancellationToken)
            {
                try
                    {
                    var fromDate = request.FromDate.Date.ToString("yyyy-MM-dd ", new CultureInfo("en-US").DateTimeFormat);
                    var toDate = request.ToDate.Date.ToString("yyyy-MM-dd ", new CultureInfo("en-US").DateTimeFormat);
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "select  CostCenters.Code, CostCenters.Name, CostCenters.NameArabic,Accounts.Name as AccountName,Accounts.NameArabic as AccountNameArabic," +
                                "  sum(tr.Debit) as Debit ," +
                                "  sum(tr.Credit) as Credit " +
                        " from CostCenters " +
                        " left outer join Accounts on  CostCenters.Id = Accounts.CostCenterId" +
                        " left outer join Transactions tr on  Accounts.Id = tr.AccountId" +
                        " Where Cast(tr.Date as date )  between  '" + fromDate + "'" +
                        " and '" + toDate + "'" + " and tr.CompanyId= '" + _contextProvider.GetCompanyId() + "'" +
                        " group by  CostCenters.Code,CostCenters.Name,CostCenters.NameArabic,Accounts.Name,Accounts.NameArabic";


                    var query = conn.Query<TrialLookUp>(sb).AsQueryable();
                    var   trialLooks = query.GroupBy(x => x.Name).ToList();


                    var inventoryList = trialLooks.Select(x => new TrialLookUp
                    {
                        
                        Name = x.Key,
                        NameArabic = x.FirstOrDefault()?.NameArabic,
                        Code = x.FirstOrDefault()?.Code,
                        Debit = x.Sum(y=>y.Debit),
                        Credit = x.Sum(y => y.Credit),
                        Total=Convert.ToDecimal((string.Format("{0:0.##}", x.Sum(y=>y.Debit)-x.Sum(y=>y.Credit)))),
                        TrialBalanceModel = x.Select(y => new TrialBalanceModel
                        {
                            Code=y.Code,
                            Debit = y.Debit,
                            Credit = y.Credit,
                            AccountName = y.AccountName,
                            AccountNameArabic = y.AccountNameArabic,
                            Total = Convert.ToDecimal((string.Format("{0:0.##}", (y.Debit-y.Credit)))),
                        }).ToList()

                    }).ToList();

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        inventoryList = inventoryList.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                    }

                    return inventoryList;
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
