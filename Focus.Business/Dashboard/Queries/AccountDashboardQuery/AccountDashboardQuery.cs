using AutoMapper.Configuration;
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

namespace Focus.Business.Dashboard.Queries.AccountDashboardQuery
{
    public class AccountDashboardQuery : IRequest<AccountDashboardLookUpModel>
    {
        public string OverViewFilter { get; set; }

        public class Handler : IRequestHandler<AccountDashboardQuery, AccountDashboardLookUpModel>
        {
            private readonly ILogger _logger;
            public readonly Microsoft.Extensions.Configuration.IConfiguration Configuration;
            private readonly IUserHttpContextProvider _contextProvider;

            public Handler(ILogger<AccountDashboardQuery> logger,
                Microsoft.Extensions.Configuration.IConfiguration configuration, IUserHttpContextProvider contextProvider)
            {
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
            }


            public async Task<AccountDashboardLookUpModel> Handle(AccountDashboardQuery request, CancellationToken cancellationToken)

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
                        " and  tr.CompanyId= '" + _contextProvider.GetCompanyId() + "'";


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
                        " and '" + lastDay + "'" + " and  tr.CompanyId= '" + _contextProvider.GetCompanyId() + "'";


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
                        " and '" + lastDay + "'" + " and  tr.CompanyId= '" + _contextProvider.GetCompanyId() + "'";


                        transactions = conn.Query<TransactionDashboardLookUpModel>(sb).ToList();


                    }
                    else if (request.OverViewFilter == "Year")
                    {
                        string sb = "select AccountTypes.Name as AccountType,AccountTypes.NameArabic as AccountTypeArabic, CostCenters.Code,CostCenters.Name as CostCenterName,CostCenters.NameArabic as CostCenterArabic,tr.Debit,tr.Credit,tr.Date from AccountTypes" +
                                " left outer join CostCenters on  CostCenters.AccountTypeId = AccountTypes.Id" +
                                " left outer join Accounts on  CostCenters.Id = Accounts.CostCenterId" +
                                " left outer join Transactions tr on  Accounts.Id = tr.AccountId" +
                         " where Year(tr.Date) = Year(GETDATE()) " +
                         " and  tr.CompanyId= '" + _contextProvider.GetCompanyId() + "'";


                        transactions = conn.Query<TransactionDashboardLookUpModel>(sb).ToList();


                    }
                    #endregion


                    #region Calculation
                    var groupByCostCEnterIncome = transactions.Where(x => x.AccountType == "Income").GroupBy(x => x.CostCenterName);
                    var groupByCostCEnterExpense = transactions.Where(x => x.AccountType == "Expenses").GroupBy(x => x.CostCenterName);

                    var income = new List<IncomeStatementModel>();
                    var expenses = new List<IncomeStatementModel>();
                    income = groupByCostCEnterIncome.Select(x => new IncomeStatementModel
                    {
                        CostCenter = x.Key,
                        AccountType=x.FirstOrDefault().AccountType,
                        AccountTypeArabic= x.FirstOrDefault().AccountTypeArabic,
                        Amount = Math.Round(Math.Abs(x.Sum(x=>x.Debit-x.Credit)))
                    }).ToList();
                    expenses = groupByCostCEnterExpense.Select(x => new IncomeStatementModel
                    {
                        CostCenter = x.Key,
                        AccountType=x.FirstOrDefault().AccountType,
                        AccountTypeArabic= x.FirstOrDefault().AccountTypeArabic,
                        Amount = Math.Round(Math.Abs(x.Sum(x=>x.Debit-x.Credit)))
                    }).ToList();

                    var bank = Math.Abs( transactions.Where(x => x.Code == "105000")
                         .Sum(x => x.Debit - x.Credit));
                    var cash = Math.Abs( transactions.Where(x => x.Code == "101000")
                         .Sum(x => x.Debit - x.Credit));
                   
                    var accountReceivable = Math.Abs( transactions.Where(x => x.Code == "120000")
                      .Sum(x => x.Debit - x.Credit));
                    var accountPayable = Math.Abs(transactions.Where(x => x.Code == "200000")
                      .Sum(x => x.Debit - x.Credit));
                    var advancePayable = Math.Abs(transactions.Where(x => x.Code == "160000")
                      .Sum(x => x.Debit - x.Credit));
                    var advanceReceivable = Math.Abs(transactions.Where(x => x.Code == "210000")
                      .Sum(x => x.Debit - x.Credit));
                    var vatPayable = Math.Abs(transactions.Where(x => x.Code == "130000")
                      .Sum(x => x.Debit - x.Credit));
                    var vatReceivable = Math.Abs(transactions.Where(x => x.Code == "250000")
                      .Sum(x => x.Debit - x.Credit));

                    #endregion

                    #region Payable And Receiavble Chart
                    var payableReceivableList = new List<PayableReceivableLookUpModel>();
                    var incomesAndExpenseList = new List<IncomeAndExpenseLookUpModel>();
                    for (int i = 1; i <= 12; i++)
                    {
                        incomesAndExpenseList.Add(new IncomeAndExpenseLookUpModel
                        {
                            IncomeAmount = Math.Round(Math.Abs(transactions.Where(x => x.AccountType == "Income" && x.Date.Month == i).Sum(x => x.Debit - x.Credit))),
                            ExpenseAmount = Math.Round(Math.Abs(transactions.Where(x => x.AccountType == "Expenses" && x.Date.Month == i).Sum(x => x.Debit - x.Credit))),

                        });

                    }

                    if (request.OverViewFilter == "Monthly")
                    {
                        var record = transactions.GroupBy(x => x.Date.Day);
                         payableReceivableList = record.Select(x => new PayableReceivableLookUpModel
                        {
                            DateIn = x.Key.ToString(),
                            Date = x.FirstOrDefault().Date,
                            PayableAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "200000").Sum(x=>x.Debit-x.Credit))),
                            ReceivableAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "120000").Sum(x => x.Debit - x.Credit))),
                        }).ToList();

                    }
                    else if (request.OverViewFilter == "3 Month")
                    {
                        var record = transactions.GroupBy(x => x.Date.Month);
                        payableReceivableList = record.Select(x => new PayableReceivableLookUpModel
                        {
                            DateIn = x.Key.ToString(),
                            Date = x.FirstOrDefault().Date,
                            PayableAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "200000").Sum(x => x.Debit - x.Credit))),
                            ReceivableAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "120000").Sum(x => x.Debit - x.Credit))),
                        }).ToList();
                    }
                    else if (request.OverViewFilter == "6 Month")
                    {
                        var record = transactions.GroupBy(x => x.Date.Month);
                         payableReceivableList = record.Select(x => new PayableReceivableLookUpModel
                        {
                            DateIn = x.Key.ToString(),
                            Date = x.FirstOrDefault().Date,
                            PayableAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "200000").Sum(x => x.Debit - x.Credit))),
                            ReceivableAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "120000").Sum(x => x.Debit - x.Credit))),
                        }).ToList();
                    }
                    else if (request.OverViewFilter == "Year")
                    {
                        var record = transactions.GroupBy(x => x.Date.Month);
                        payableReceivableList = record.Select(x => new PayableReceivableLookUpModel
                        {
                            DateIn = x.Key.ToString(),
                            Date = x.FirstOrDefault().Date,
                            PayableAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "200000").Sum(x => x.Debit - x.Credit))),
                            ReceivableAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "120000").Sum(x => x.Debit - x.Credit))),
                        }).ToList();
                        payableReceivableList = record.Select(x => new PayableReceivableLookUpModel
                        {
                            DateIn = x.Key.ToString(),
                            Date = x.FirstOrDefault().Date,
                            PayableAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "200000").Sum(x => x.Debit - x.Credit))),
                            ReceivableAmount = Math.Round(Math.Abs(x.Where(x => x.Code == "120000").Sum(x => x.Debit - x.Credit))),
                        }).ToList();
                     

                      


                    }
                    #endregion

                    return new AccountDashboardLookUpModel
                    {

                        Banks = bank,
                        Cash = cash,
                        AccountPayable = accountPayable,
                        VatPayable = vatPayable,
                        VatReceivable = vatReceivable,
                        AdvancePayable = advancePayable,
                        AdvanceReceivable = advanceReceivable,
                        AccountReceivable = accountReceivable,
                        PayableReceivableLookUpModel = payableReceivableList,
                        IncomeList = income,
                        ExpenseList = expenses,
                        IncomesAndExpenseList = incomesAndExpenseList,


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

