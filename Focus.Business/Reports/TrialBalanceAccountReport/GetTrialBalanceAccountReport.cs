using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Globalization;
using Dapper;
using Focus.Domain.Interface;
using Microsoft.Extensions.Configuration;
using Focus.Business.Reports.TrialBalanceReport;

namespace Focus.Business.Reports.TrialBalanceAccountReport
{
    public class GetTrialBalanceAccountReport :  IRequest<List<TrialBalanceAccountReportLookup>>
    {
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<GetTrialBalanceAccountReport, List<TrialBalanceAccountReportLookup>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            public readonly IConfiguration Configuration;
            private readonly IUserHttpContextProvider _contextProvider;
            public Handler(IApplicationDbContext context, ILogger<GetTrialBalanceAccountReport> logger, 
                IConfiguration configuration, IUserHttpContextProvider contextProvider)
            {
                _context = context;
                _logger = logger; 
                Configuration = configuration;
                _contextProvider = contextProvider;
            }

            public async Task<List<TrialBalanceAccountReportLookup>> Handle(GetTrialBalanceAccountReport request, CancellationToken cancellationToken)
            {
                try
                {
                     var fromDate = request.FromDate.Date.ToString("yyyy-MM-dd ", new CultureInfo("en-US").DateTimeFormat);
                    var toDate = request.ToDate.Date.ToString("yyyy-MM-dd ", new CultureInfo("en-US").DateTimeFormat);
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "select ac.Code as AccountCode, ac.Name as AccountName, ac.NameArabic as AccountNameArabic,cc.Name as CostCenter," +
                                " act.Name as AccountType, SUM(t.Credit) as Credit, SUM(t.Debit) as Debit, SUM(t.Debit - t.Credit) as Total from Transactions t" +
                                " inner join  Accounts ac on t.AccountId = ac.Id inner join CostCenters cc on ac.CostCenterId = cc.Id " +
                        " inner join AccountTypes act on cc.AccountTypeId = act.Id " +
                        " Where Cast(t.Date as date )  between  '" + fromDate + "'" +
                        " and '" + toDate + "'" + " and t.CompanyId= '" + _contextProvider.GetCompanyId() + "'" +
                        " Group by ac.Code, ac.Name, ac.NameArabic,cc.Name,act.Name";


                    var query = conn.Query<TrialBalanceAccountReportLookup>(sb).ToList();
                    //var listTrialBalance = new List<TrialBalanceAccountReportLookup>();
                    //    var query = _context.Transactions
                    //        .Include(x => x.Account)
                    //        .ThenInclude(x => x.CostCenter)
                    //        .ThenInclude(x => x.AccountTypes)
                    //        .AsNoTracking()
                    //        .GroupBy(x=>x.AccountId)
                    //        .ToListAsync(cancellationToken: cancellationToken);
                    //    var abc = query;
                    //    //var opening = query.Where(x => x.Date.Date < request.FromDate.Date)
                    //    //    .Sum(x => x.Debit - x.Credit);


                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        query = query.Where(x => branchIdList.Contains(x.BranchId.ToString())).ToList();
                    }
                    return query;

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
