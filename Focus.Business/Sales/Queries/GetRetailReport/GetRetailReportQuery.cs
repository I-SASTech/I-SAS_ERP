using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.DayStarts.Queries.GetOpeninigBalance;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleList;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;

namespace Focus.Business.Sales.Queries.GetRetailReport
{
    public class GetRetailReportQuery : IRequest<List<RetailReportLookUpModel>>
    {
        //Get all variable in entity
        public DateTime FromDate{ get; set; }

        public DateTime ToDate { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<GetRetailReportQuery, List<RetailReportLookUpModel>>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            public readonly IUserHttpContextProvider _ContextProvider;
            //Create logger interface variable for log error
            public readonly ILogger Logger;
            public readonly IMediator _Mediator;


            //Constructor
            public Handler(IApplicationDbContext context, ILogger<List<RetailReportLookUpModel>> logger,
                IUserHttpContextProvider contextProvider, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _ContextProvider = contextProvider;
                _Mediator = mediator;
            }

            public async Task<List<RetailReportLookUpModel>> Handle(GetRetailReportQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var lookUpModel = new List<RetailReportLookUpModel>();
                    var vatPayableId = Context.Accounts.AsNoTracking().FirstOrDefault(x => x.Code == "25000001");
                    var totalVat = Context.Transactions.Where(x => x.AccountId == vatPayableId.Id &&
                                                                   x.Date.Date >= request.FromDate.Date &&
                                                                   x.Date.Date <= request.ToDate.Date)
                        .GroupBy(x => x.Date.Date)
                        .Select(x => new{Date = x.Key, Total = x.Select(s => s.Debit - s.Credit).Sum()}).ToList();

                    foreach (var sale in totalVat)
                    {
                        int index = lookUpModel.FindIndex(a => a.Date == sale.Date);
                        if (index > -1)
                        {
                            lookUpModel.ElementAt(index).TotalVat = sale.Total;
                        }
                        else
                        {
                            lookUpModel.Add(new RetailReportLookUpModel()
                            {
                                TotalVat = sale.Total,
                                Date = sale.Date

                            });
                        }
                    }
                    var totalSale = Context.SalePayments.Where(x=> EF.Property<DateTime>(x, "CreatedOn").Date >= request.FromDate.Date && EF.Property<DateTime>(x, "CreatedOn").Date <= request.ToDate.Date)
                        .GroupBy(x => EF.Property<DateTime>(x, "CreatedOn").Date)
                        .Select(x => new { Date = x.Key, Total = x.Select(s => s.DueAmount).Sum() }).ToList();

                    var totalCashPayment = Context.SalePayments.Where(x => EF.Property<DateTime>(x, "CreatedOn").Date >= request.FromDate.Date && EF.Property<DateTime>(x, "CreatedOn").Date <= request.ToDate.Date && x.PaymentType == SalePaymentType.Cash && x.Name == "Cash")
                        .GroupBy(x => EF.Property<DateTime>(x, "CreatedOn").Date)
                        .Select(x => new { Date = x.Key, Total = x.Select(s => s.DueAmount).Sum() }).ToList();

                    var totalBankPayment = Context.SalePayments.Where(x => EF.Property<DateTime>(x, "CreatedOn").Date >= request.FromDate.Date && EF.Property<DateTime>(x, "CreatedOn").Date <= request.ToDate.Date && x.PaymentType == SalePaymentType.Bank)
                        .GroupBy(x => EF.Property<DateTime>(x, "CreatedOn").Date)
                        .Select(x => new { Date = x.Key, Total = x.Select(s => s.DueAmount).Sum() }).ToList();

                    foreach (var sale in totalSale)
                    {
                        int index = lookUpModel.FindIndex(a => a.Date == sale.Date);
                        if (index > -1)
                        {
                            lookUpModel.ElementAt(index).TotalSale = sale.Total;
                        }
                        else
                        {
                            lookUpModel.Add(new RetailReportLookUpModel()
                            {
                                TotalSale = sale.Total,
                                Date = sale.Date

                            });
                        }
                    }

                    foreach (var sale in totalCashPayment)
                    {
                        int index = lookUpModel.FindIndex(a => a.Date == sale.Date);
                        if (index > -1)
                        {
                            lookUpModel.ElementAt(index).TotalCash = sale.Total;
                        }
                        else
                        {
                            lookUpModel.Add(new RetailReportLookUpModel()
                            {
                                TotalCash = sale.Total,
                                Date = sale.Date

                            });
                        }
                    }
                    foreach (var sale in totalBankPayment)
                    {
                        int index = lookUpModel.FindIndex(a => a.Date == sale.Date);
                        if (index > -1)
                        {
                            lookUpModel.ElementAt(index).TotalBank = sale.Total;
                        }
                        else
                        {
                            lookUpModel.Add(new RetailReportLookUpModel()
                            {
                                TotalBank = sale.Total,
                                Date = sale.Date

                            });
                        }
                    }
                    var purchaseOrder = await _Mediator.Send(new GetSaleListQuery
                    {
                        Satus = InvoiceType.Credit,
                        
                        IsDropdown = true,
                       
                        IsExpense = true
                    }, cancellationToken);

                    

                    var expenseAccount = Context.Accounts.AsNoTracking().FirstOrDefault(x => x.Code == "60505001");
                    var totalExpense = Context.Transactions.Where(x => x.AccountId == expenseAccount.Id &&
                                                                   x.Date.Date >= request.FromDate.Date &&
                                                                   x.Date.Date <= request.ToDate.Date)
                        .GroupBy(x => x.Date.Date)
                        .Select(x => new { Date = x.Key, Total = x.Select(s => s.Debit - s.Credit).Sum() }).ToList();

                    foreach (var expense in totalExpense)
                    {
                        int index = lookUpModel.FindIndex(a => a.Date == expense.Date);
                        if (index > -1)
                        {
                            lookUpModel.ElementAt(index).TotalExpence = expense.Total;
                        }
                        else
                        {
                            lookUpModel.Add(new RetailReportLookUpModel()
                            {
                                TotalExpence = expense.Total ,
                                Date = expense.Date

                            });
                        }
                    }
                    var totalCredit = purchaseOrder.Results.Sales.Where(x => x.Date.Date >= request.FromDate.Date &&
                                                                       x.Date.Date <= request.ToDate.Date)
                        .GroupBy(x => x.Date.Date)
                        .Select(x => new {Date = x.Key, Total = x.Select(s => s.NetAmount).Sum()})
                        .ToList();

                    foreach (var credit in totalCredit)
                    {
                        int index = lookUpModel.FindIndex(a => a.Date == credit.Date);
                        if (index > -1)
                        {
                            lookUpModel.ElementAt(index).TotalCredit = credit.Total;
                        }
                        else
                        {
                            lookUpModel.Add(new RetailReportLookUpModel()
                            {
                                TotalCredit = credit.Total,
                                Date = credit.Date

                            });
                        }
                    }
                    return lookUpModel;
                    //.Select(x => x.Select(s => s.Debit-s.Credit).Sum());
                    //var dayStartMasterList = Context.DayStarts.Where(x =>
                    //    x.Date.Value.Date >= request.FromDate.Date && x.Date.Value.Date <= request.ToDate.Date &&
                    //    x.DayStartId == null).ToList();
                    //var opening = new OpeningBalanceLookUpModel();

                    //var lastDayStart = Context.DayStarts.LastOrDefault(x => x.CounterId == request.Id && !x.IsDayStart);

                    //if (lastDayStart == null)
                    //{
                    //    var terminalData = await Context.Terminals.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                    //    if (terminalData == null)
                    //        throw new NotFoundException("Terminal not found: ", request.Id);
                    //    opening.CashInHand = Context.Transactions.Where(x => x.AccountId == terminalData.CashAccountId).Sum(x => x.Debit - x.Credit);
                    //    opening.Bank = 0;
                    //    opening.DraftExpense = 0;
                    //    opening.PostExpense = 0;
                    //    opening.NoOfTransaction = 0;
                    //    opening.OpeningBalance = 0;
                    //    opening.TotalInvoices = 0;
                    //    opening.FromInvoices = "";
                    //    opening.ToInvoices = "";
                    //    return opening;
                    //}

                    //opening.OpeningBalance = lastDayStart.OpeningCash;
                    //opening.StartBy = lastDayStart.StartTerminalBy;
                    //opening.StartFor = lastDayStart.StartTerminalFor;
                    //opening.StartTime = lastDayStart.FromTime.ToString();
                    //if (lastDayStart.Date != null)
                    //    opening.Date = lastDayStart.Date.Value.ToString("MM/dd/yyyy");

                    ////CashIn Hand

                    //var terminal = await Context.Terminals.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                    //if (terminal == null)
                    //    throw new NotFoundException("Terminal not found: ", request.Id);

                    //if (terminal.CashAccountId != null)
                    //    opening.CashInHand = Context.Transactions.Where(x => x.AccountId == terminal.CashAccountId).Sum(x => x.Debit - x.Credit);
                    ////var year = DateTime.UtcNow.Year;

                    ////Terminal Name
                    //opening.TerminalCode = terminal.Code;

                    ////Bank Account Balance

                    //opening.Bank = Context.Transactions.Where(x => EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId && EF.Property<Guid>(x, "CounterId") == request.Id && x.AccountId == terminal.AccountId)
                    //    .Sum(x => x.Debit - x.Credit);
                    //var vatPayableId = Context.Accounts.AsNoTracking().FirstOrDefault(x => x.Code == "25000001");
                    //opening.TotalVat = Context.Transactions.Where(x => EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId && EF.Property<Guid>(x, "CounterId") == request.Id && x.AccountId == vatPayableId.Id)
                    //    .Sum(x => x.Debit - x.Credit);
                    //var bankDetailsList = new List<BankDetailLookUpModel>();
                    //var bankList = Context.SalePayments.AsNoTracking()
                    //    .Where(x => EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId
                    //                && x.PaymentType == SalePaymentType.Bank
                    //                && EF.Property<Guid>(x, "CounterId") == request.Id)
                    //    .GroupBy(x => x.Name)
                    //    .ToList();
                    //var bankName = bankList.Select(x => x.Key).ToList();
                    //var paymentOption = Context.PaymentOptions.Where(x =>
                    //    (!bankName.Contains(x.Name == String.Empty ? x.NameArabic : x.Name)) && x.IsActive).ToList();
                    //foreach (var banks in bankList)
                    //{
                    //    bankDetailsList.Add(new BankDetailLookUpModel
                    //    {
                    //        BankName = banks.Key,
                    //        TotalAmount = banks.Sum(x => x.Received).ToString(CultureInfo.InvariantCulture),
                    //    });
                    //}
                    //foreach (var banks in paymentOption)
                    //{
                    //    bankDetailsList.Add(new BankDetailLookUpModel
                    //    {
                    //        BankName = banks.Name == String.Empty ? banks.NameArabic : banks.Name,
                    //        TotalAmount = "0"
                    //    });
                    //}
                    //opening.BankDetails = bankDetailsList;

                    ////No of Transaction
                    //opening.NoOfTransaction = Context.SalePayments.Count(x =>
                    //    (EF.Property<Guid>(x, "DayId") == lastDayStart.Id || EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId) &&
                    //    EF.Property<Guid>(x, "CounterId") == request.Id && x.PaymentType == SalePaymentType.Bank);

                    //// Expense Amount
                    //opening.DraftExpense = Context.DailyExpenses.AsNoTracking()
                    //    .Include(x => x.DailyExpenseDetails)
                    //    .Where(x => (EF.Property<Guid>(x, "DayId") == lastDayStart.Id || EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId) &&
                    //                EF.Property<Guid>(x, "CounterId") == request.Id && x.ApprovalStatus == ApprovalStatus.Draft)
                    //    .Sum(x => x.DailyExpenseDetails.Sum(y => y.Amount));

                    //opening.PostExpense = Context.DailyExpenses.AsNoTracking()
                    //                           .Include(x => x.DailyExpenseDetails)
                    //                           .Where(x => (EF.Property<Guid>(x, "DayId") == lastDayStart.Id || EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId) &&
                    //                                       EF.Property<Guid>(x, "CounterId") == request.Id && x.ApprovalStatus == ApprovalStatus.Approved)
                    //                           .Sum(x => x.DailyExpenseDetails.Sum(y => y.Amount));

                    //////Total Invoices
                    ////opening.TotalInvoices = Context.Sales.Count(x =>
                    ////    (EF.Property<Guid>(x, "DayId") == lastDayStart.Id || EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId) &&
                    ////    EF.Property<Guid>(x, "CounterId") == request.Id);//Total Invoices

                    //var data = Context.Sales.AsNoTracking().Where(x =>
                    //     (EF.Property<Guid>(x, "DayId") == lastDayStart.Id || EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId) &&
                    //     EF.Property<Guid>(x, "CounterId") == request.Id).OrderBy(x => x.RegistrationNo).ToList();
                    //opening.TotalInvoices = data.Count();
                    //opening.FromInvoices = data.FirstOrDefault()?.RegistrationNo;
                    //opening.ToInvoices = data.LastOrDefault()?.RegistrationNo;

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }

            }
        }
    }
}
