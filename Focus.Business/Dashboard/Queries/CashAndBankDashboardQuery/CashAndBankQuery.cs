using Focus.Domain.Interface;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Focus.Business.Reports.IncomeStatementReport;
using Focus.Business.Dashboard.Queries.AccountDashboardQuery;

namespace Focus.Business.Dashboard.Queries.CashAndBankDashboardQuery
{
    public class CashAndBankQuery : IRequest<CashAndBankLookUpModel>
    {
        public string OverViewFilter { get; set; }

        public class Handler : IRequestHandler<CashAndBankQuery, CashAndBankLookUpModel>
        {
            private readonly ILogger _logger;
            public readonly Microsoft.Extensions.Configuration.IConfiguration Configuration;
            private readonly IUserHttpContextProvider _contextProvider;

            public Handler(ILogger<CashAndBankQuery> logger,
                Microsoft.Extensions.Configuration.IConfiguration configuration, IUserHttpContextProvider contextProvider)
            {
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
            }


            public async Task<CashAndBankLookUpModel> Handle(CashAndBankQuery request, CancellationToken cancellationToken)

            {
                try
                {
                    var transactions = new List<TransactionDashboardLookUpModel>();
                    decimal expeneAmount = 0;
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    #region Filter
                    if (request.OverViewFilter == "Monthly")
                    {

                        string sb = "select AccountTypes.Name as AccountType,AccountTypes.NameArabic as AccountTypeArabic, CostCenters.Code,CostCenters.Name as CostCenterName,CostCenters.NameArabic as CostCenterArabic,tr.Debit,tr.Credit,tr.Date from AccountTypes" +
                                " left outer join CostCenters on  CostCenters.AccountTypeId = AccountTypes.Id" +
                                " left outer join Accounts on  CostCenters.Id = Accounts.CostCenterId" +
                                " left outer join Transactions tr on  Accounts.Id = tr.AccountId" +
                        " where Month(tr.Date) = Month(GETDATE()) " +
                        " and CostCenters.Name='Cash in Hand' or  CostCenters.Name='Banks'and  tr.CompanyId= '" + _contextProvider.GetCompanyId() + "'";


                        transactions = conn.Query<TransactionDashboardLookUpModel>(sb).ToList();


                    }
                    else if (request.OverViewFilter == "3 Month")
                    {
                        var lastDay = DateTime.UtcNow.Date;
                        var firstDay = lastDay.AddMonths(-3);
                        string sb = "select AccountTypes.Name as AccountType,AccountTypes.NameArabic as AccountTypeArabic, CostCenters.Code,CostCenters.Name as CostCenterName,CostCenters.NameArabic as CostCenterArabic,tr.Debit,tr.Credit,tr.Date from AccountTypes" +
                                " left outer join CostCenters on  CostCenters.AccountTypeId = AccountTypes.Id" +
                                " left outer join Accounts on  CostCenters.Id = Accounts.CostCenterId" +
                                " left outer join Transactions tr on  Accounts.Id = tr.AccountId" +
                         " Where Cast(tr.Date as date )  between  '" + firstDay + "'" +
                        " and '" + lastDay + "'" + " and CostCenters.Name='Cash in Hand' or  CostCenters.Name='Banks'and  tr.CompanyId= '" + _contextProvider.GetCompanyId() + "'";


                        transactions = conn.Query<TransactionDashboardLookUpModel>(sb).ToList();


                    }


                    else if (request.OverViewFilter == "6 Month")
                    {
                        var lastDay = DateTime.UtcNow.Date;
                        var firstDay = lastDay.AddMonths(-6);
                        string sb = "select AccountTypes.Name as AccountType,AccountTypes.NameArabic as AccountTypeArabic, CostCenters.Code,CostCenters.Name as CostCenterName,CostCenters.NameArabic as CostCenterArabic,tr.Debit,tr.Credit,tr.Date from AccountTypes" +
                                 " left outer join CostCenters on  CostCenters.AccountTypeId = AccountTypes.Id" +
                                 " left outer join Accounts on  CostCenters.Id = Accounts.CostCenterId" +
                                 " left outer join Transactions tr on  Accounts.Id = tr.AccountId" +
                          " Where Cast(tr.Date as date )  between  '" + firstDay + "'" +
                        " and '" + lastDay + "'" + "  and CostCenters.Name='Cash in Hand' or  CostCenters.Name='Banks'and  tr.CompanyId= '" + _contextProvider.GetCompanyId() + "'";


                        transactions = conn.Query<TransactionDashboardLookUpModel>(sb).ToList();


                    }
                    else if (request.OverViewFilter == "Year")
                    {
                        string sb = "select AccountTypes.Name as AccountType,AccountTypes.NameArabic as AccountTypeArabic, CostCenters.Code,CostCenters.Name as CostCenterName,CostCenters.NameArabic as CostCenterArabic,tr.Debit,tr.Credit,tr.Date from AccountTypes" +
                                " left outer join CostCenters on  CostCenters.AccountTypeId = AccountTypes.Id" +
                                " left outer join Accounts on  CostCenters.Id = Accounts.CostCenterId" +
                                " left outer join Transactions tr on  Accounts.Id = tr.AccountId" +
                         " where Year(tr.Date) = Year(GETDATE()) " +
                         " and CostCenters.Name='Cash in Hand' or  CostCenters.Name='Banks'and  tr.CompanyId= '" + _contextProvider.GetCompanyId() + "'";


                        transactions = conn.Query<TransactionDashboardLookUpModel>(sb).ToList();


                    }
                    #endregion


                    #region Calculation
                    var bank = Math.Abs(transactions.Where(x => x.Code == "105000")
                        .Sum(x => x.Debit - x.Credit));
                    var cash = Math.Abs(transactions.Where(x => x.Code == "101000")
                         .Sum(x => x.Debit - x.Credit));

                    var cashAndBankList = new List<CashAndBankModel>();



                    if (request.OverViewFilter == "Monthly")
                    {
                        var record = transactions.GroupBy(x => x.Date.Day);
                        cashAndBankList = record.Select(x => new CashAndBankModel
                        {
                            DateInType = x.Key.ToString(),
                            Date = x.FirstOrDefault().Date,
                            CashAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "101000").Sum(x => x.Debit - x.Credit))),
                            BankAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "105000").Sum(x => x.Debit - x.Credit))),
                        }).ToList();

                    }
                    else if (request.OverViewFilter == "3 Month")
                    {
                        var record = transactions.GroupBy(x => x.Date.Month);
                        cashAndBankList = record.Select(x => new CashAndBankModel
                        {
                            DateInType = x.Key.ToString(),
                            Date = x.FirstOrDefault().Date,
                            CashAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "101000").Sum(x => x.Debit - x.Credit))),
                            BankAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "105000").Sum(x => x.Debit - x.Credit))),

                        }).ToList();
                    }
                    else if (request.OverViewFilter == "6 Month")
                    {
                        var record = transactions.GroupBy(x => x.Date.Month);
                        cashAndBankList = record.Select(x => new CashAndBankModel
                        {
                            DateInType = x.Key.ToString(),
                            Date = x.FirstOrDefault().Date,
                            CashAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "101000").Sum(x => x.Debit - x.Credit))),
                            BankAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "105000").Sum(x => x.Debit - x.Credit))),

                        }).ToList();
                    }
                    else if (request.OverViewFilter == "Year")
                    {
                        var record = transactions.GroupBy(x => x.Date.Month);
                        cashAndBankList = record.Select(x => new CashAndBankModel
                        {
                            DateInType = x.Key.ToString(),
                            Date = x.FirstOrDefault().Date,
                            CashAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "101000").Sum(x => x.Debit - x.Credit))),
                            BankAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "105000").Sum(x => x.Debit - x.Credit))),

                        }).ToList();
                        cashAndBankList = record.Select(x => new CashAndBankModel
                        {
                            DateInType = x.Key.ToString(),
                            Date = x.FirstOrDefault().Date,
                            CashAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "101000").Sum(x => x.Debit - x.Credit))),
                            BankAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "105000").Sum(x => x.Debit - x.Credit))),

                        }).ToList();


                    }

                    #endregion


                    return new CashAndBankLookUpModel
                    {




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

