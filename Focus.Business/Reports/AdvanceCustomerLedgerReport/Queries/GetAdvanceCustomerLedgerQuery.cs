using Focus.Business.Reports.AdvanceCustomerLedgerReport.Models;
using MediatR;
using System.Collections.Generic;
using System;
using Focus.Business.Interface;
using Focus.Business.Reports.CustomerLedgerReport;
using Focus.Business.Users;
using Focus.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using Focus.Business.Reports.AdvanceAccountLedgerDetailsReport.Models;

namespace Focus.Business.Reports.AdvanceCustomerLedgerReport.Queries
{
    public class GetAdvanceCustomerLedgerQuery : IRequest<List<AdvanceCustomerLedgerLookupModel>>
    {
        public bool IsCustomer { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool FormName { get; set; }
        public string CompareWith { get; set; }
        public string NumberOfPeriods { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<GetAdvanceCustomerLedgerQuery, List<AdvanceCustomerLedgerLookupModel>>
        {
            public readonly IConfiguration Configuration;
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context,
                ILogger<GetAdvanceCustomerLedgerQuery> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }
            public async Task<List<AdvanceCustomerLedgerLookupModel>> Handle(GetAdvanceCustomerLedgerQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "select Contacts.Code as ContactCode, Contacts.CustomerDisplayName as ContactName,  Contacts.VatNo, Contacts.IsCustomer, " +
                       " Accounts.Code as AccountCode, Accounts.Name as AccountName, Accounts.NameArabic as AccountNameArabic, sum(tr.Debit-tr.Credit) as Amount, tr.Date, tr.Debit,tr.Credit" +
                       " from Contacts" +
                       " left outer join Accounts on Contacts.AccountId = Accounts.Id" +
                       " left outer join Transactions tr on  Accounts.Id = tr.AccountId" +
                       " left outer join CostCenters cc on cc.Id = Accounts.CostCenterId" +
                       " where  (cc.Name = 'Customer Reciveables' OR cc.Name = 'Supplier Payable')" +
                       " AND Contacts.IsCustomer = '" + request.IsCustomer + "'" + " and tr.CompanyId= '" + _contextProvider.GetCompanyId() + "'" +
                       " group by Contacts.Code, Contacts.CustomerDisplayName, Contacts.VatNo, Contacts.IsCustomer, Accounts.Code, Accounts.Name, Accounts.NameArabic,tr.Date,tr.Debit,tr.Credit";


                    var query = conn.Query<AdvanceCustomerLedgerLookupModel>(sb).AsQueryable();

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        query = query.Where(x => branchIdList.Contains(x.BranchId.ToString()));
                    }

                    if (request.CompareWith == "Previous Year(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);

                        var advanceCustomerLedgers = new List<AdvanceCustomerLedgerLookupModel>();

                        for (int i = 0; i < comparePeriod; i++)
                        {
                            int year = DateTime.Now.Year;
                            int years = year - i;
                            DateTime firstDayOfYear = new DateTime(years, 1, 1);
                            DateTime lastDayOfYear = new DateTime(years, 12, 31);

                            var query1 = query.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date);

                            query1 = query1.GroupBy(x => x.ContactCode).Select(x => new AdvanceCustomerLedgerLookupModel()
                            {
                                ContactCode = x.Key,
                                ContactName = x.FirstOrDefault().ContactName,
                                VatNo = x.FirstOrDefault().VatNo,
                                AccountCode = x.FirstOrDefault().AccountCode,
                                AccountName = x.FirstOrDefault().AccountName == null ? x.FirstOrDefault().AccountNameArabic : x.FirstOrDefault().AccountName,
                                AccountNameArabic = x.FirstOrDefault().AccountNameArabic,
                                Amount = x.Sum(y => y.Amount),
                            });

                            advanceCustomerLedgers.Add(new AdvanceCustomerLedgerLookupModel()
                            {
                                CompareWith = firstDayOfYear.ToString("dd/MM/yyyy") + " - " + lastDayOfYear.ToString("dd/MM/yyyy"),
                                CompareWithList = query1.ToList(),
                            });
                        }

                        return advanceCustomerLedgers;
                    }
                    else if (request.CompareWith == "Previous Period(s)")
                    {
                        int extractedYear = int.TryParse(request.NumberOfPeriods.Substring(0, 4), out int year) ? year : -1;
                        int currentYear = DateTime.Now.Year;
                        int yearDiff = currentYear - extractedYear;
                        var advanceCustomerLedgers = new List<AdvanceCustomerLedgerLookupModel>();

                        for (int i = 0; i <= yearDiff; i++)
                        {

                            int years = currentYear - i;
                            DateTime firstDayOfYear = new DateTime(years, 1, 1);
                            DateTime lastDayOfYear = new DateTime(years, 12, 31);

                            var query1 = query.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date);

                            query1 = query1.GroupBy(x => x.ContactCode).Select(x => new AdvanceCustomerLedgerLookupModel()
                            {
                                ContactCode = x.Key,
                                ContactName = x.FirstOrDefault().ContactName,
                                VatNo = x.FirstOrDefault().VatNo,
                                AccountCode = x.FirstOrDefault().AccountCode,
                                AccountName = x.FirstOrDefault().AccountName == null ? x.FirstOrDefault().AccountNameArabic : x.FirstOrDefault().AccountName,
                                AccountNameArabic = x.FirstOrDefault().AccountNameArabic,
                                Amount = x.Sum(y => y.Amount),
                            });

                            advanceCustomerLedgers.Add(new AdvanceCustomerLedgerLookupModel()
                            {
                                CompareWith = firstDayOfYear.ToString("dd/MM/yyyy") + " - " + lastDayOfYear.ToString("dd/MM/yyyy"),
                                CompareWithList = query1.ToList(),
                            });
                        }
                        return advanceCustomerLedgers;
                    }
                    else if (request.CompareWith == "Previous Quarter(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        var advanceCustomerLedgers = new List<AdvanceCustomerLedgerLookupModel>();

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

                            DateTime firstDayOfYear = new DateTime(year, 3 * quarter + 1, 1);
                            DateTime lastDayOfYear = firstDayOfYear.AddMonths(3).AddDays(-1);

                            var query1 = query.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date);

                            query1 = query1.GroupBy(x => x.ContactCode).Select(x => new AdvanceCustomerLedgerLookupModel()
                            {
                                ContactCode = x.Key,
                                ContactName = x.FirstOrDefault().ContactName,
                                VatNo = x.FirstOrDefault().VatNo,
                                AccountCode = x.FirstOrDefault().AccountCode,
                                AccountName = x.FirstOrDefault().AccountName == null ? x.FirstOrDefault().AccountNameArabic : x.FirstOrDefault().AccountName,
                                AccountNameArabic = x.FirstOrDefault().AccountNameArabic,
                                Amount = x.Sum(y => y.Amount),
                            });

                            advanceCustomerLedgers.Add(new AdvanceCustomerLedgerLookupModel()
                            {
                                CompareWith = firstDayOfYear.ToString("dd/MM/yyyy") + " - " + lastDayOfYear.ToString("dd/MM/yyyy"),
                                CompareWithList = query1.ToList(),
                            });
                        }
                        return advanceCustomerLedgers;
                    }
                    else if (request.CompareWith == "Previous Month(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        var advanceCustomerLedgers = new List<AdvanceCustomerLedgerLookupModel>();

                        for (int i = 1; i <= comparePeriod; i++)
                        {
                            DateTime firstDayOfYear = DateTime.Now.AddMonths(-i);
                            DateTime lastDayOfYear = DateTime.Now.AddMonths(-(i - 1));

                            var query1 = query.Where(x => x.Date.Date >= firstDayOfYear.Date && x.Date.Date <= lastDayOfYear.Date);

                            query1 = query1.GroupBy(x => x.ContactCode).Select(x => new AdvanceCustomerLedgerLookupModel()
                            {
                                ContactCode = x.Key,
                                ContactName = x.FirstOrDefault().ContactName,
                                VatNo = x.FirstOrDefault().VatNo,
                                AccountCode = x.FirstOrDefault().AccountCode,
                                AccountName = x.FirstOrDefault().AccountName == null ? x.FirstOrDefault().AccountNameArabic : x.FirstOrDefault().AccountName,
                                AccountNameArabic = x.FirstOrDefault().AccountNameArabic,
                                Amount = x.Sum(y => y.Amount),
                            });

                            advanceCustomerLedgers.Add(new AdvanceCustomerLedgerLookupModel()
                            {
                                CompareWith = firstDayOfYear.ToString("dd/MM/yyyy") + " - " + lastDayOfYear.ToString("dd/MM/yyyy"),
                                CompareWithList = query1.ToList(),
                            });
                        }
                        return advanceCustomerLedgers;
                    }
                    else
                    {
                        var query1 = query.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date);

                        query1 = query1.GroupBy(x => x.ContactCode).Select(x => new AdvanceCustomerLedgerLookupModel()
                        {
                            ContactCode = x.Key,
                            ContactName = x.FirstOrDefault().ContactName,
                            VatNo = x.FirstOrDefault().VatNo,
                            AccountCode = x.FirstOrDefault().AccountCode,
                            AccountName = x.FirstOrDefault().AccountName == null ? x.FirstOrDefault().AccountNameArabic : x.FirstOrDefault().AccountName,
                            AccountNameArabic = x.FirstOrDefault().AccountNameArabic,
                            Amount = x.Sum(y => y.Amount),
                        });

                        return query1.ToList();
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
