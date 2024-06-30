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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Transactions.JVTransaction
{
    public class GetDateWiseTransactionList : IRequest<List<DayWiseHeaderLookUp>>
    {
        public DateTime Date { get; set; }

        public class Handler : IRequestHandler<GetDateWiseTransactionList, List<DayWiseHeaderLookUp>>
        {
            public readonly IConfiguration Configuration;
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context,
                ILogger<GetDateWiseTransactionList> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }
            public async Task<List<DayWiseHeaderLookUp>> Handle(GetDateWiseTransactionList request, CancellationToken cancellationToken)
            {
                try
                {
                    var companyId = _contextProvider.GetCompanyId();
                    var date = request.Date.Date.ToString("yyyy-MM-dd hh:mm:ss", new CultureInfo("en-US").DateTimeFormat);
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "select ATYPE.Name AS AccountType,COST.Name as CostCentre,Acc.Name as AccountName,DocumentNumber,TransactionType,TR.Debit AS DEBIT ,TR.Credit AS CREDIT,Date from Transactions AS TR join Accounts AS AC ON AC.Id=TR.AccountId " +
                                "JOIN CostCenters AS COST ON COST.Id=AC.CostCenterId " +
                                "JOIN AccountTypes AS ATYPE ON ATYPE.Id=COST.AccountTypeId " +
                                "Join Accounts as Acc on Acc.Id = TR.AccountId " +
                                "WHERE CAST(TR.Date as date) = '" + date + "' and TR.CompanyId ='" + companyId +"';";
                    string sb1 = "select DocumentNumber from Transactions WHERE CAST(Transactions.Date as date) = '" + date + "' and CompanyId = '" + companyId + "' group by DocumentNumber";
                    var query = conn.Query<DayWiseTransactionLookupModel>(sb).AsQueryable().ToList();
                    var documentList = conn.Query<string>(sb1).AsQueryable().ToList();
                    var dayWise = new List<DayWiseHeaderLookUp>();
                    foreach (var q in documentList)
                    {
                        dayWise.Add(new DayWiseHeaderLookUp()
                        {
                            Header = string.IsNullOrEmpty(q)?"Doc Number":q,
                            DayWiseTransactionLookup = query.Where(x=>x.DocumentNumber == q).ToList(),
                            Total = query.Where(x => x.DocumentNumber == q).Sum(x=>x.Debit - x.Credit)
                        });
                    }
                    
                    return dayWise;
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
