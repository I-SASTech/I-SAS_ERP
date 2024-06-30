using Dapper;
using Focus.Business.Interface;
using Focus.Business.Reports.AdvanceAccountLedger.Models;
using Focus.Business.Reports.GetAdvanceSalesInvoice.Models;
using Focus.Business.Users;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Reports.GetAdvanceSalesInvoice.Queries
{
    public class GetSalesInvoiceQuery : IRequest<List<AdvanceSalesInvoiceLookupModel>>
    {
        public string CustomerId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CompareWith { get; set; }
        public string NumberOfPeriods { get; set; }
        public string InvoiceType { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<GetSalesInvoiceQuery, List<AdvanceSalesInvoiceLookupModel>>
        {
            public readonly IConfiguration Configuration;
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context,
                ILogger<GetSalesInvoiceQuery> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }
            public async Task<List<AdvanceSalesInvoiceLookupModel>> Handle(GetSalesInvoiceQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "Select sa.Id as SaleId, c.Id as CustomerId, sa.InvoiceType as InvoiceType, sa.IsSaleReturnPost as SaleReturnPost, " +
                                "sa.RegistrationNo as RegistrationNo, sa.Date as Date, c.CustomerDisplayName as CustomerDisplayName, " +
                                "sa.TotalWithOutAmount as GrossAmount, sa.DiscountAmount as DiscountAmount,(sa.TotalWithOutAmount - sa.DiscountAmount) as NetTotal, " +
                                "sa.VatAmount as VatAmount, sa.TotalAmount as TotalAmount from Sales sa " +
                                "join Contacts c on sa.CustomerId = c.Id " +
                                "order by sa.RegistrationNo"; 

                    var query = conn.Query<AdvanceSalesInvoiceLookupModel>(sb).AsQueryable();
                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        query = query.Where(x => branchIdList.Contains(x.BranchId.ToString()));
                    }
                    if (!String.IsNullOrEmpty(request.CustomerId))
                    {
                        query = query.Where(x => x.CustomerId == Guid.Parse(request.CustomerId));
                    }
                    if(request.InvoiceType == "Sale Inovice Hold")
                    {
                        query = query.Where(x => x.InvoiceType == Domain.Entities.InvoiceType.Hold);
                    }
                    else if(request.InvoiceType == "Sale Inovice Post")
                    {
                        query = query.Where(x => x.InvoiceType == Domain.Entities.InvoiceType.Paid);
                    }
                    else if(request.InvoiceType == "Sale Return")
                    {
                        query = query.Where(x => x.InvoiceType == Domain.Entities.InvoiceType.SaleReturn);
                    }

                    if (request.CompareWith == "Previous Year(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);

                        for (int i = 0; i < comparePeriod; i++)
                        {
                            int year = DateTime.Now.Year;
                            int years = year - i;
                            DateTime fromDate = new DateTime(years, 1, 1);
                            DateTime toDate = new DateTime(years, 12, 31);
                        }

                        return null;
                    }
                    else if (request.CompareWith == "Previous Period(s)")
                    {
                        int extractedYear = int.TryParse(request.NumberOfPeriods.Substring(0, 4), out int year) ? year : -1;
                        int currentYear = DateTime.Now.Year;

                        int yearDiff = currentYear - extractedYear;

                        for (int i = 0; i <= yearDiff; i++)
                        {
                            int years = currentYear - i;
                            DateTime fromDate = new DateTime(years, 1, 1);
                            DateTime toDate = new DateTime(years, 12, 31);
                        }
                        return null;
                    }
                    else if (request.CompareWith == "Previous Quarter(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);
                        var advanceLedger = new List<AdvanceLedgerAccountLookupModel>();


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

                            DateTime fromDate = new DateTime(year, 3 * quarter + 1, 1);
                            DateTime toDate = fromDate.AddMonths(3).AddDays(-1);
                        }
                        return null;
                    }
                    else if (request.CompareWith == "Previous Month(s)")
                    {
                        int comparePeriod = Convert.ToInt32(request.NumberOfPeriods);


                        for (int i = 1; i <= comparePeriod; i++)
                        {
                            DateTime fromDate = DateTime.Now.AddMonths(-i);
                            DateTime toDate = DateTime.Now.AddMonths(-(i - 1));
                        }
                        return null;
                    }
                    else
                    {
                        var query1 = query.Where(x => x.Date.Date >= request.FromDate.Date && x.Date.Date <= request.ToDate.Date).ToList();
                        var salesList = query1.Select(x => new AdvanceSalesInvoiceLookupModel()
                        {
                            Date = x.Date,
                            RegistrationNo = x.RegistrationNo,
                            CustomerDisplayName = x.CustomerDisplayName,
                            TotalAmount= x.TotalAmount,
                            DiscountAmount = x.DiscountAmount,
                            NetTotal = x.NetTotal,
                            GrossAmount = x.GrossAmount,
                            VatAmount= x.VatAmount,
                        }).ToList();
                        return salesList;
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
