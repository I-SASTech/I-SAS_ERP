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
using Focus.Business.Reports.CustomerLedgerReport;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace Focus.Business.Reports.AdvanceContactLedger
{
    public class AdvanceContactLedger : IRequest<List<CustomerLedgerModel>>
    {
        public bool IsCustomer { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DateType { get; set; }
        public string BranchId { get; set; }
        public string DocumentName { get; set; }


        public class Handler : IRequestHandler<AdvanceContactLedger, List<CustomerLedgerModel>>
        {
            public readonly IConfiguration Configuration;
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context,
                ILogger<AdvanceContactLedger> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }
            public async Task<List<CustomerLedgerModel>> Handle(AdvanceContactLedger request, CancellationToken cancellationToken)
            {
                try
                {
                    var branchIdList = new List<string>();
                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        branchIdList = new List<string>(request.BranchId.Split(','));
                       
                    }

                    var fromDate = request.FromDate.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US").DateTimeFormat);
                    var toDate = request.ToDate.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US").DateTimeFormat);
                    var advanceAccount = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Name == (request.DocumentName == "CustomerSummary" ? "Customers Advance" : "Supplier Advance"));
                    var customerInfo =  _context.Contacts.AsNoTracking().ToList();
                    var contacts = customerInfo.Where(x => x.IsCustomer == (request.DocumentName == "CustomerSummary" ? true:false)).Select(x=>x.AccountId).ToList();
                    var ladgerReport =await _context.Transactions.AsNoTracking().Where(x=>x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date && x.AccountId == advanceAccount.Id && contacts.Contains(x.ContactId)).ToListAsync();
                  var modelForAdvanceLedger = ladgerReport.Select(x => new CustomerLedgerModel
                    {
                      ContactCode = customerInfo.FirstOrDefault(x =>x.AccountId == x.Id)?.Code,
                      ContactName = customerInfo.FirstOrDefault(x => x.AccountId == x.Id)?.EnglishName,
                      Date = x.Date,
                      DocumentDate = (DateTime)x.DocumentDate,
                      VatNo = customerInfo.FirstOrDefault(x => x.AccountId == x.Id)?.VatNo,
                      IsCustomer = customerInfo.FirstOrDefault(x => x.AccountId == x.Id)?.IsCustomer ?? false,
                      AccountCode = advanceAccount?.Code, 
                      AccountName = advanceAccount?.Name,
                      AccountNameArabic = advanceAccount?.NameArabic,
                      Amount = x.Debit - x.Credit

                  }).ToList();

                   
                   
                    return modelForAdvanceLedger;


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
