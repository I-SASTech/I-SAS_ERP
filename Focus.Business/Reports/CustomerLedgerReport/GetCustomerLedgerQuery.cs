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

namespace Focus.Business.Reports.CustomerLedgerReport
{
    public class GetCustomerLedgerQuery : IRequest<List<CustomerLedgerModel>>
    {
        public bool IsCustomer { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DateType { get; set; }
        public string BranchId { get; set; }


        public class Handler : IRequestHandler<GetCustomerLedgerQuery, List<CustomerLedgerModel>>
        {
            public readonly IConfiguration Configuration;
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context,
                ILogger<GetCustomerLedgerQuery> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }
            public async Task<List<CustomerLedgerModel>> Handle(GetCustomerLedgerQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    //if (request.DateType == "Document Date")
                    //{
                    //    var fromDate = request.FromDate.Date.ToString("yyyy-MM-dd hh:mm:ss", new CultureInfo("en-US").DateTimeFormat);
                    //    var toDate = request.ToDate.Date.ToString("yyyy-MM-dd hh:mm:ss", new CultureInfo("en-US").DateTimeFormat);
                    //    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    //    string sb = "select Contacts.Code as ContactCode, Contacts.EnglishName as ContactName, Contacts.ArabicName as ContactNameArabic, Contacts.VatNo, Contacts.IsCustomer," +
                    //    " Accounts.Code as AccountCode, Accounts.Name as AccountName , Accounts.NameArabic as AccountNameArabic	, sum(tr.Debit-tr.Credit) as Amount" +
                    //    " from Contacts" +
                    //    " left outer join Accounts on Contacts.AccountId = Accounts.Id" +
                    //    " left outer join Transactions tr on  Accounts.Id = tr.AccountId" +
                    //    " left outer join CostCenters cc on cc.Id = Accounts.CostCenterId" +
                    //    " where  ((cc.Name = 'Customer Reciveables' OR cc.Name = 'Supplier Payable') AND (CAST(tr.documentDate as date )  >=  '" + fromDate + "'" +
                    //       "  and CAST(tr.documentDate as datetime )   <= '" + toDate + "'" + "))" +
                    //    " AND Contacts.IsCustomer = '" + request.IsCustomer + "'" + " and tr.CompanyId= '" + _contextProvider.GetCompanyId() + "'" +
                    //    " group by Contacts.EnglishName,Contacts.ArabicName,Contacts.VatNo, Contacts.Code, Accounts.Code, Accounts.Name , Accounts.NameArabic, Contacts.IsCustomer";


                    //    var query = conn.Query<CustomerLedgerModel>(sb).AsQueryable();

                    //    return query.ToList();
                    //}
                    //else if (request.DateType == "Approval Date")
                    //{
                    //    var fromDate = request.FromDate.Date.ToString("yyyy-MM-dd hh:mm:ss", new CultureInfo("en-US").DateTimeFormat);
                    //    var toDate = request.ToDate.Date.ToString("yyyy-MM-dd hh:mm:ss", new CultureInfo("en-US").DateTimeFormat);
                    //    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    //    string sb = "select Contacts.Code as ContactCode, Contacts.EnglishName as ContactName, Contacts.ArabicName as ContactNameArabic, Contacts.VatNo, Contacts.IsCustomer," +
                    //    " Accounts.Code as AccountCode, Accounts.Name as AccountName , Accounts.NameArabic as AccountNameArabic	, sum(tr.Debit-tr.Credit) as Amount" +
                    //    " from Contacts" +
                    //    " left outer join Accounts on Contacts.AccountId = Accounts.Id" +
                    //    " left outer join Transactions tr on  Accounts.Id = tr.AccountId" +
                    //    " left outer join CostCenters cc on cc.Id = Accounts.CostCenterId" +
                    //    " where  ((cc.Name = 'Customer Reciveables' OR cc.Name = 'Supplier Payable') AND (CAST(tr.approvalDate as date )  >=  '" + fromDate + "'" +
                    //       "  and CAST(tr.approvalDate as datetime )   <= '" + toDate + "'" + "))" +
                    //    " AND Contacts.IsCustomer = '" + request.IsCustomer + "'" + " and tr.CompanyId= '" + _contextProvider.GetCompanyId() + "'" +
                    //    " group by Contacts.EnglishName,Contacts.ArabicName,Contacts.VatNo, Contacts.Code, Accounts.Code, Accounts.Name , Accounts.NameArabic, Contacts.IsCustomer";


                    //    var query = conn.Query<CustomerLedgerModel>(sb).AsQueryable();

                    //    return query.ToList();
                    //}



                    var fromDate = request.FromDate.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US").DateTimeFormat);
                    var toDate = request.ToDate.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US").DateTimeFormat);
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "select Contacts.Code as ContactCode, Contacts.CustomerDisplayName as ContactName,  Contacts.VatNo, Contacts.IsCustomer, " +
                    " Accounts.Code as AccountCode, Accounts.Name as AccountName , Accounts.NameArabic as AccountNameArabic	, sum(tr.Debit-tr.Credit) as Amount" +
                    " from Contacts" +
                    " left outer join Accounts on Contacts.AccountId = Accounts.Id" +
                    " left outer join Transactions tr on  Accounts.Id = tr.AccountId" +
                    " left outer join CostCenters cc on cc.Id = Accounts.CostCenterId" +
                    " where  ((cc.Name = 'Customer Reciveables' OR cc.Name = 'Supplier Payable') AND (CAST(tr.date as date )  >=  '" + fromDate + "'" +
                       "  and CAST(tr.date as date )   <= '" + toDate + "'" + "))" +
                    " AND Contacts.IsCustomer = '" + request.IsCustomer + "'" + " and tr.CompanyId= '" + _contextProvider.GetCompanyId() + "'" +
                    " group by Contacts.Code, Contacts.CustomerDisplayName, Contacts.VatNo, Contacts.IsCustomer, Accounts.Code, Accounts.Name, Accounts.NameArabic";


                    var query = conn.Query<CustomerLedgerModel>(sb).AsQueryable();
                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        query = query.Where(x => branchIdList.Contains(x.BranchId.ToString()));
                    }
                    return query.ToList();


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
