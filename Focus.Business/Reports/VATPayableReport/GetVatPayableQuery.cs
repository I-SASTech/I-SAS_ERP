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
using Focus.Domain.Entities;

namespace Focus.Business.Reports.VATPayableReport
{
    public class GetVatPayableQuery : IRequest<VatPayableListModel>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string BranchId { get; set; }

        public class Handler : IRequestHandler<GetVatPayableQuery, VatPayableListModel>
        {
            public readonly IConfiguration Configuration;
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context,
                ILogger<GetVatPayableQuery> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }
            public async Task<VatPayableListModel> Handle(GetVatPayableQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var fromDate = request.FromDate.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US").DateTimeFormat);
                    var toDate = request.ToDate.Date.ToString("yyyy-MM-dd ", new CultureInfo("en-US").DateTimeFormat);
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    string sb = "select CostCenters.Name as CostCenter,Accounts.Code, Accounts.Name, Accounts.NameArabic , sum(tr.Debit-tr.Credit) as Amount" +
                    " from Accounts" +
                    " left outer join CostCenters on  CostCenters.Id = Accounts.CostCenterId" +
                    " left outer join Transactions tr on  Accounts.Id = tr.AccountId" +
                    " where (CostCenters.Name = 'Vat Payable' OR CostCenters.Name = 'VAT PAID') AND  tr.TransactionType!=7 AND (CAST(tr.Date as date ) >= '" + fromDate + "'" + " and CAST(tr.Date as date ) <= '" + toDate + "')" +
                    " and tr.CompanyId = '" + _contextProvider.GetCompanyId() + "'" +
                    " group by CostCenters.Name,Accounts.Code, Accounts.Name,Accounts.NameArabic";

                    var query = conn.Query<VatPayableModel>(sb).AsQueryable();

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        query = query.Where(x => branchIdList.Contains(x.BranchId.ToString()));
                    }


                    var vatpaid = new List<VatPayableModel>();
                    var vatpayable = new List<VatPayableModel>();
                    var OtherVocher = new List<VatPayableModel>();

                    foreach (var data in query)
                    {
                        if (data.CostCenter == "VAT Paid")
                        {
                            vatpaid.Add(new VatPayableModel()
                            {
                                Code = data.Code,
                                CostCenter = data.CostCenter,
                                Name = data.Name,
                                NameArabic = data.NameArabic,
                                Amount = data.Amount
                            });
                        }
                        if (data.CostCenter == "VAT Payable")
                        {
                            vatpayable.Add(new VatPayableModel()
                            {
                                Code = data.Code,
                                CostCenter = data.CostCenter,
                                Name = data.Name,
                                NameArabic = data.NameArabic,
                                Amount = data.Amount
                            });
                        }
                    }
                   

                    return new VatPayableListModel
                    {
                        VatPaid = vatpaid,
                        VatPayable = vatpayable
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