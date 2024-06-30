using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DayStarts.Queries.GetOpeninigBalance
{
    public class GetOpeningBalanceQuery : IRequest<OpeningBalanceLookUpModel>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        public Guid? DayId { get; set; }

        public bool IsOpeningCash { get; set; }
        public bool IsReport { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<GetOpeningBalanceQuery, OpeningBalanceLookUpModel>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            public readonly IUserHttpContextProvider _ContextProvider;
            //Create logger interface variable for log error
            public readonly ILogger Logger;


            //Constructor
            public Handler(IApplicationDbContext context, ILogger<GetOpeningBalanceQuery> logger, IUserHttpContextProvider contextProvider)
            {
                Context = context;
                Logger = logger;
                _ContextProvider = contextProvider;
            }

            public async Task<OpeningBalanceLookUpModel> Handle(GetOpeningBalanceQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    DayStart lastDayStart = null;
                    Terminal terminal = null;
                    if (request.IsOpeningCash)
                    {
                        var opening = new OpeningBalanceLookUpModel();
                        terminal = await Context.Terminals.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                        if (terminal == null)
                            throw new NotFoundException("Terminal not found: ", request.Id);

                        if (terminal.CashAccountId == null)
                            opening.OpeningBalance = 0M;
                        //var year = DateTime.UtcNow.Year;

                        opening.OpeningBalance = Context.Transactions.Where(x => x.AccountId == terminal.CashAccountId).AsEnumerable().Sum(x => x.Debit - x.Credit);

                        return opening;
                    }
                    else
                    {
                        //Opening balance 
                        var opening = new OpeningBalanceLookUpModel();
                        if (!request.IsReport)
                        {
                            //CashIn Hand
                            lastDayStart = Context.DayStarts.ToList().LastOrDefault(x => x.CounterId == request.Id && !x.IsDayStart);
                            terminal = await Context.Terminals.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                            if (terminal == null)
                                throw new NotFoundException("Terminal not found: ", request.Id);

                            if (terminal.CashAccountId != null)
                                opening.CashInHand = Context.Transactions.Where(x => x.AccountId == terminal.CashAccountId).AsEnumerable().Sum(x => x.Debit - x.Credit);
                        }
                        else
                        {
                            lastDayStart = Context.DayStarts.ToList().LastOrDefault(x => x.Id == request.DayId && !x.IsDayStart);
                            terminal = await Context.Terminals.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                            if (terminal == null)
                                throw new NotFoundException("Terminal not found: ", request.Id);

                            opening.CashInHand = lastDayStart==null?0:lastDayStart.CashInHand;

                        }
                        
                        if (lastDayStart == null)
                        {
                            var terminalData = await Context.Terminals.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                            if (terminalData == null)
                                throw new NotFoundException("Terminal not found: ", request.Id);
                            opening.CashInHand = Context.Transactions.Where(x => x.AccountId == terminalData.CashAccountId).AsEnumerable().Sum(x => x.Debit - x.Credit);
                            opening.Bank = 0;
                            opening.DraftExpense = 0;
                            opening.PostExpense = 0;
                            opening.BankExpense = 0;
                            opening.NoOfTransaction = 0;
                            opening.OpeningBalance = 0;
                            opening.TotalInvoices = 0;
                            opening.FromInvoices = "";
                            opening.ToInvoices = "";
                            opening.TotalCash = 0;
                            opening.VerifyCash = 0;
                            return opening;
                        }

                        opening.OpeningBalance = lastDayStart.OpeningCash;
                        
                        opening.StartBy = lastDayStart.StartTerminalBy;
                        opening.StartFor = lastDayStart.StartTerminalFor;
                        opening.StartTime = lastDayStart.FromTime?.ToString("MM/dd/yyyy hh:mm tt");
                        opening.EndTime = lastDayStart.ToTime?.ToString("MM/dd/yyyy hh:mm tt");
                        opening.CarryCash = lastDayStart.CarryCash;
                        opening.VerifyCash = lastDayStart.VerifyCash;
                        opening.TotalCash = lastDayStart.TotalCash;
                        if (lastDayStart.Date != null)
                            opening.Date = lastDayStart.Date.Value.ToString("MM/dd/yyyy");

                       
                        //var year = DateTime.UtcNow.Year;

                        //Terminal Name
                        opening.TerminalCode = terminal.Code;

                        //Bank Account Balance
                        if (request.FromTime != null && request.ToTime != null)
                        {
                            opening.Bank = Context.Transactions.Where(x => EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId && EF.Property<Guid>(x, "CounterId") == request.Id && x.AccountId == terminal.AccountId && x.Date >= request.FromTime && x.Date <= request.ToTime)
                         .AsEnumerable().Sum(x => x.Debit - x.Credit);
                            var vatPayableId = Context.Accounts.AsNoTracking().FirstOrDefault(x => x.Code == "25000001");
                            opening.TotalVat = Context.Transactions.Where(x => EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId && EF.Property<Guid>(x, "CounterId") == request.Id && x.AccountId == vatPayableId.Id && x.Date >= request.FromTime && x.Date <= request.ToTime)
                                .AsEnumerable().Sum(x => x.Debit - x.Credit);
                            var bankDetailsList = new List<BankDetailLookUpModel>();
                            //var bankList = Context.SalePayments.AsNoTracking()
                            //    .Where(x => EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId
                            //                && x.PaymentType == SalePaymentType.Bank)
                            //    .GroupBy(x => x.Name).ToList();
                            var bankList = Context.SalePayments.AsNoTracking()
                             .Where(x => EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId
                                         && x.PaymentType == SalePaymentType.Bank
                                         && EF.Property<Guid>(x, "CounterId") == request.Id && EF.Property<DateTime>(x, "CreatedOn").TimeOfDay >= request.FromTime.Value.TimeOfDay && EF.Property<DateTime>(x, "CreatedOn").TimeOfDay <= request.ToTime.Value.TimeOfDay)
                             .AsEnumerable().GroupBy(x => x.Name)
                             .ToList();
                            var bankName = bankList.Select(x => x.Key).ToList();
                            var paymentOption = Context.PaymentOptions.Where(x =>
                                (!bankName.Contains(x.Name == String.Empty ? x.NameArabic : x.Name)) && x.IsActive).ToList();
                            foreach (var banks in bankList)
                            {
                                bankDetailsList.Add(new BankDetailLookUpModel
                                {
                                    BankName = banks.Key,
                                    TotalAmount = banks.Sum(x => x.Received).ToString(CultureInfo.InvariantCulture),
                                });
                            }
                            foreach (var banks in paymentOption)
                            {
                                bankDetailsList.Add(new BankDetailLookUpModel
                                {
                                    BankName = banks.Name == String.Empty ? banks.NameArabic : banks.Name,
                                    TotalAmount = "0"
                                });
                            }
                            opening.BankDetails = bankDetailsList;

                            //No of Transaction
                            opening.NoOfTransaction = Context.SalePayments.Count(x =>
                                (EF.Property<Guid>(x, "DayId") == lastDayStart.Id || EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId) &&
                                EF.Property<Guid>(x, "CounterId") == request.Id && x.PaymentType == SalePaymentType.Bank && EF.Property<DateTime>(x, "CreatedOn") >= request.FromTime && EF.Property<DateTime>(x, "CreatedOn") <= request.FromTime);

                            // Expense Amount
                            //opening.DraftExpense = Context.DailyExpenses.AsNoTracking()
                            //    .Include(x => x.DailyExpenseDetails)
                            //    .Where(x => (EF.Property<Guid>(x, "DayId") == lastDayStart.Id || EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId) &&
                            //                EF.Property<Guid>(x, "CounterId") == request.Id && x.ApprovalStatus == ApprovalStatus.Draft && x.Date >= request.FromTime && x.Date <= request.ToTime && (x.PaymentMode == SalePaymentType.Default || x.PaymentMode == SalePaymentType.Cash))
                            //    .Sum(x => x.DailyExpenseDetails.Sum(y => y.Amount));

                            opening.DraftExpense = 0;
                            opening.PostExpense = Context.DailyExpenses.AsNoTracking()
                                                       .Include(x => x.DailyExpenseDetails)
                                                       .Where(x => (EF.Property<Guid>(x, "DayId") == lastDayStart.Id || EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId) &&
                                                                   EF.Property<Guid>(x, "CounterId") == request.Id && !x.IsExpenseAccount && x.ApprovalStatus == ApprovalStatus.Approved && x.Date >= request.FromTime && x.Date <= request.ToTime && (x.PaymentMode == SalePaymentType.Default || x.PaymentMode == SalePaymentType.Cash))
                                                       .AsEnumerable().Sum(x => x.DailyExpenseDetails.Sum(y => y.Amount));
                            opening.BankExpense = Context.DailyExpenses.AsNoTracking()
                                                       .Include(x => x.DailyExpenseDetails)
                                                       .Where(x => (EF.Property<Guid>(x, "DayId") == lastDayStart.Id || EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId) &&
                                                                   EF.Property<Guid>(x, "CounterId") == request.Id && x.Date >= request.FromTime  && x.Date <= request.ToTime && x.ApprovalStatus == ApprovalStatus.Approved && x.PaymentMode == SalePaymentType.Bank)
                                                       .AsEnumerable().Sum(x => x.DailyExpenseDetails.Sum(y => y.Amount));
                            
                            var data = Context.Sales.AsNoTracking().Where(x =>
                                 (EF.Property<Guid>(x, "DayId") == lastDayStart.Id || EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId) &&
                                 EF.Property<Guid>(x, "CounterId") == request.Id && x.Date >= request.FromTime && x.Date <= request.ToTime).OrderBy(x => x.RegistrationNo).ToList();
                            opening.TotalInvoices = data.Count();
                            opening.FromInvoices = data.FirstOrDefault()?.RegistrationNo;
                            opening.ToInvoices = data.LastOrDefault()?.RegistrationNo;
                        }
                        else
                        {
                            opening.Bank = Context.Transactions
                                .Where(x => EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId && EF.Property<Guid>(x, "CounterId") == request.Id && x.AccountId == terminal.AccountId)
                                .AsEnumerable().Sum(x => x.Debit - x.Credit);

                            var vatPayableId = Context.Accounts.AsNoTracking().FirstOrDefault(x => x.Code == "25000001");

                            opening.TotalVat = Context.Transactions.Where(x => EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId && EF.Property<Guid>(x, "CounterId") == request.Id && x.AccountId == vatPayableId.Id)
                                .AsEnumerable().Sum(x => x.Debit - x.Credit);

                            var bankDetailsList = new List<BankDetailLookUpModel>();

                            var bankList = Context.SalePayments.AsNoTracking()
                                .Where(x => EF.Property<Guid?>(x, "DayId") == lastDayStart.DayStartId
                                            && x.PaymentType == SalePaymentType.Bank
                                            && EF.Property<Guid?>(x, "CounterId") == request.Id)
                                .AsEnumerable()
                                .GroupBy(x => x.Name)
                                .ToList();
                            var bankName = bankList.Select(x => x.Key).ToList();

                            var bankAccounts = await Context.CostCenters
                                .AsNoTracking()
                                .Include(x => x.Accounts)
                                .FirstOrDefaultAsync(x => x.Code == "105000", cancellationToken: cancellationToken);

                            var bankDetailList = new List<BankTransactionDetailLookUp>();
                            if (bankAccounts.Accounts != null)
                            {
                                foreach (var bank in bankAccounts.Accounts)
                                {
                                    bankDetailList.Add(new BankTransactionDetailLookUp()
                                    {
                                        BankName = bank.Name,
                                        BankNameArabic = bank.NameArabic,
                                        Amount = Context.Transactions.AsNoTracking().Where(x => x.AccountId == bank.Id && EF.Property<Guid>(x, "CounterId") == request.Id && EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId)
                                            .AsEnumerable()
                                        .Sum(x => x.Debit - x.Credit)

                                    });
                                }
                            }


                            opening.BankDetailListTotal = bankDetailList.Sum(x => x.Amount);
                            opening.BankDetailList = bankDetailList;

                            var paymentOption = Context.PaymentOptions.Where(x =>
                                (!bankName.Contains(x.Name == String.Empty ? x.NameArabic : x.Name)) && x.IsActive).ToList();

                            foreach (var banks in bankList)
                            {
                                bankDetailsList.Add(new BankDetailLookUpModel
                                {
                                    BankName = banks.Key,
                                    TotalAmount = banks.Sum(x => x.Received).ToString(CultureInfo.InvariantCulture),
                                });
                            }
                            foreach (var banks in paymentOption)
                            {
                                bankDetailsList.Add(new BankDetailLookUpModel
                                {
                                    BankName = banks.Name == String.Empty ? banks.NameArabic : banks.Name,
                                    TotalAmount = "0"
                                });
                            }
                            opening.BankDetails = bankDetailsList;

                            //No of Transaction
                            opening.NoOfTransaction = Context.SalePayments.Count(x =>
                                (EF.Property<Guid>(x, "DayId") == lastDayStart.Id || EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId) &&
                                EF.Property<Guid>(x, "CounterId") == request.Id && x.PaymentType == SalePaymentType.Bank);

                            // Expense Amount
                            //opening.DraftExpense = Context.DailyExpenses.AsNoTracking()
                            //    .Include(x => x.DailyExpenseDetails)
                            //    .Where(x => (EF.Property<Guid>(x, "DayId") == lastDayStart.Id || EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId) &&
                            //                EF.Property<Guid>(x, "CounterId") == request.Id && x.ApprovalStatus == ApprovalStatus.Draft && (x.PaymentMode == SalePaymentType.Default || x.PaymentMode == SalePaymentType.Cash))
                            //    .Sum(x => x.DailyExpenseDetails.Sum(y => y.Amount));

                            opening.DraftExpense = 0;
                            opening.PostExpense = Context.DailyExpenses.AsNoTracking()
                                                       .Include(x => x.DailyExpenseDetails)
                                                       .Where(x => (EF.Property<Guid>(x, "DayId") == lastDayStart.Id || EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId) &&
                                                                   EF.Property<Guid>(x, "CounterId") == request.Id && !x.IsExpenseAccount && x.ApprovalStatus == ApprovalStatus.Approved && (x.PaymentMode == SalePaymentType.Default || x.PaymentMode == SalePaymentType.Cash))
                                                       .AsEnumerable()
                                                       .Sum(x => x.DailyExpenseDetails.Sum(y => y.Amount));
                            opening.BankExpense = Context.DailyExpenses.AsNoTracking()
                                                                                  .Include(x => x.DailyExpenseDetails)
                                                                                  .Where(x => (EF.Property<Guid>(x, "DayId") == lastDayStart.Id || EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId) &&
                                                                                              EF.Property<Guid>(x, "CounterId") == request.Id && x.ApprovalStatus == ApprovalStatus.Approved && x.PaymentMode == SalePaymentType.Bank)
                                                                                  .AsEnumerable()
                                                                                  .Sum(x => x.DailyExpenseDetails.Sum(y => y.Amount));


                            ////Total Invoices
                            //opening.TotalInvoices = Context.Sales.Count(x =>
                            //    (EF.Property<Guid>(x, "DayId") == lastDayStart.Id || EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId) &&
                            //    EF.Property<Guid>(x, "CounterId") == request.Id);//Total Invoices

                            var data = Context.Sales.AsNoTracking().Where(x =>
                                 (EF.Property<Guid>(x, "DayId") == lastDayStart.Id || EF.Property<Guid>(x, "DayId") == lastDayStart.DayStartId) &&
                                 EF.Property<Guid>(x, "CounterId") == request.Id).OrderBy(x => x.RegistrationNo).ToList();
                            opening.TotalInvoices = data.Count();
                            opening.FromInvoices = data.FirstOrDefault()?.RegistrationNo;
                            opening.ToInvoices = data.LastOrDefault()?.RegistrationNo;
                        }


                        return opening;

                    }
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
