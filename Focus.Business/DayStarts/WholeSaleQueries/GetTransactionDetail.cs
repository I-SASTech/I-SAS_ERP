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
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DayStarts.WholeSaleQueries
{
    public class GetTransactionDetail : IRequest<TransactionDetailLookUpModel>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        public Guid? DayId { get; set; }
        public Guid? CounterId { get; set; }
        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<GetTransactionDetail, TransactionDetailLookUpModel>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            public readonly IUserHttpContextProvider _ContextProvider;
            //Create logger interface variable for log error
            public readonly ILogger Logger;


            //Constructor
            public Handler(IApplicationDbContext context, ILogger<GetTransactionDetail> logger, IUserHttpContextProvider contextProvider)
            {
                Context = context;
                Logger = logger;
                _ContextProvider = contextProvider;
            }

            public async Task<TransactionDetailLookUpModel> Handle(GetTransactionDetail request, CancellationToken cancellationToken)
            {
                try
                {
                    var accounts = Context.Accounts.ToList();
                    var cashInHandAccounts = accounts.Where(x => x.CostCenter.Code == "101000");
                    var bankAccounts = accounts.Where(x => x.CostCenter.Code == "105000");
                    var transactions = Context.Transactions.Where(x=>EF.Property<Guid>(x, "DayId") == request.Id).ToList();
                    var transactionDetailLookUp = new TransactionDetailLookUpModel
                    {
                        CashSale = new List<AccountDetailsLookUpModel>(),
                        BankSale = new List<AccountDetailsLookUpModel>(),
                        CashPurchase = new List<AccountDetailsLookUpModel>(),
                        BankPurchase = new List<AccountDetailsLookUpModel>(),
                        CashExpense = new List<AccountDetailsLookUpModel>(),
                        BankExpense = new List<AccountDetailsLookUpModel>(),
                        CashCustomerReceipt = new List<AccountDetailsLookUpModel>(),
                        BankCustomerReceipt = new List<AccountDetailsLookUpModel>(),
                        CashSupplierPay = new List<AccountDetailsLookUpModel>(),
                        BankSupplierPay = new List<AccountDetailsLookUpModel>(),
                        SaleReturn = new List<AccountDetailsLookUpModel>(),
                        CashPurchaseExpense = new List<AccountDetailsLookUpModel>(),
                        BankPurchaseExpense = new List<AccountDetailsLookUpModel>(),
                        OtherCashPayments = new List<AccountDetailsLookUpModel>(),
                        OtherBankPayments = new List<AccountDetailsLookUpModel>(),
                    };

                    if (transactions.Count > 0)
                    {
                        // Total Data via cash in hand
                        foreach (var cashAccount in cashInHandAccounts)
                        {
                            //Cash Sale Data
                            transactionDetailLookUp.CashSale.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(cashAccount.Name)?cashAccount.NameArabic:cashAccount.Name,
                                Amount = transactions.Where(x =>
                                        x.AccountId == cashAccount.Id && x.TransactionType == TransactionType.SaleInvoice && x.DocumentNumber.Contains("SI-"))
                                    .Sum(x => x.Debit - x.Credit)
                            });

                            //Cash Expense Data
                            transactionDetailLookUp.CashExpense.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(cashAccount.Name) ? cashAccount.NameArabic : cashAccount.Name,
                                Amount = transactions.Where(x =>
                                        x.AccountId == cashAccount.Id && x.TransactionType == TransactionType.ExpenseVoucher && x.DocumentNumber.Contains("DE-"))
                                    .Sum(x => x.Debit - x.Credit)
                            });


                            //Cash Customer Receivable Data
                            transactionDetailLookUp.CashCustomerReceipt.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(cashAccount.Name) ? cashAccount.NameArabic : cashAccount.Name,
                                Amount = transactions.Where(x =>
                                        x.AccountId == cashAccount.Id && (x.TransactionType == TransactionType.CashReceipt || x.TransactionType == TransactionType.AdvanceCashReceipt) && x.DocumentNumber.Contains("CP-"))
                                    .Sum(x => x.Debit - x.Credit)
                            });

                            //Cash Supplier Payable Data
                            transactionDetailLookUp.CashSupplierPay.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(cashAccount.Name) ? cashAccount.NameArabic : cashAccount.Name,
                                Amount = transactions.Where(x =>
                                        x.AccountId == cashAccount.Id && (x.TransactionType == TransactionType.CashPay || x.TransactionType == TransactionType.AdvanceCashPay) && x.DocumentNumber.Contains("SP-"))
                                    .Sum(x => x.Debit - x.Credit)
                            });

                            //Cash Purchase Data
                            transactionDetailLookUp.CashPurchase.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(cashAccount.Name) ? cashAccount.NameArabic : cashAccount.Name,
                                Amount = transactions.Where(x =>
                                        x.AccountId == cashAccount.Id && x.TransactionType == TransactionType.PurchasePost && x.DocumentNumber.Contains("PIP-"))
                                    .Sum(x => x.Debit - x.Credit)
                            });
                            //Cash Sale Return Data
                            transactionDetailLookUp.SaleReturn.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(cashAccount.Name) ? cashAccount.NameArabic : cashAccount.Name,
                                Amount = transactions.Where(x =>
                                        x.AccountId == cashAccount.Id && x.TransactionType == TransactionType.SaleReturn && x.DocumentNumber.Contains("SIR-"))
                                    .Sum(x => x.Debit - x.Credit)
                            });

                            //Cash Purchase Expense Data
                            transactionDetailLookUp.CashPurchaseExpense.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(cashAccount.Name) ? cashAccount.NameArabic : cashAccount.Name,
                                Amount = transactions.Where(x =>
                                        x.AccountId == cashAccount.Id && x.TransactionType == TransactionType.AdvanceExpense && x.DocumentNumber.Contains("AE-"))
                                    .Sum(x => x.Debit - x.Credit)
                            });

                            //Cash Other Payment(Jv, Op, Pc, STI) Data
                            transactionDetailLookUp.OtherCashPayments.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(cashAccount.Name) ? cashAccount.NameArabic : cashAccount.Name,
                                Amount = transactions.Where(x =>
                                        x.AccountId == cashAccount.Id && (x.TransactionType == TransactionType.JournalVoucher || x.TransactionType == TransactionType.PettyCash || x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.AdvanceBankPay))
                                    .Sum(x => x.Debit - x.Credit)
                            });
                        }

                        // Total Data via Bank Account
                        foreach (var bank in bankAccounts)
                        {
                            //Bank Sale Data
                            transactionDetailLookUp.BankSale.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(bank.Name) ? bank.NameArabic : bank.Name,
                                Amount = transactions.Where(x =>
                                        x.AccountId == bank.Id && x.TransactionType == TransactionType.SaleInvoice && x.DocumentNumber.Contains("SI-"))
                                    .Sum(x => x.Debit - x.Credit)
                            });

                            //Bank Expense Data
                            transactionDetailLookUp.BankExpense.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(bank.Name) ? bank.NameArabic : bank.Name,
                                Amount = transactions.Where(x =>
                                        x.AccountId == bank.Id && x.TransactionType == TransactionType.ExpenseVoucher && x.DocumentNumber.Contains("DE-"))
                                    .Sum(x => x.Debit - x.Credit)
                            });

                            //Bank Customer Receipt Data
                            transactionDetailLookUp.BankCustomerReceipt.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(bank.Name) ? bank.NameArabic : bank.Name,
                                Amount = transactions.Where(x =>
                                        x.AccountId == bank.Id && (x.TransactionType == TransactionType.BankReceipt || x.TransactionType == TransactionType.AdvanceReceipt || x.TransactionType == TransactionType.AdvanceBankReceipt) && x.DocumentNumber.Contains("CP-"))
                                    .Sum(x => x.Debit - x.Credit)
                            });
                            //Bank Supplier Payable Data
                            transactionDetailLookUp.BankSupplierPay.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(bank.Name) ? bank.NameArabic : bank.Name,
                                Amount = transactions.Where(x =>
                                        x.AccountId == bank.Id && (x.TransactionType == TransactionType.BankPay || x.TransactionType == TransactionType.AdvancePay || x.TransactionType == TransactionType.AdvanceBankPay) && x.DocumentNumber.Contains("SP-"))
                                    .Sum(x => x.Debit - x.Credit)
                            });
                            //Bank Purchase Data
                            transactionDetailLookUp.BankPurchase.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(bank.Name) ? bank.NameArabic : bank.Name,
                                Amount = transactions.Where(x =>
                                        x.AccountId == bank.Id && x.TransactionType == TransactionType.PurchasePost && x.DocumentNumber.Contains("PIP-"))
                                    .Sum(x => x.Debit - x.Credit)
                            });
                            //Bank Purchase Expense Data
                            transactionDetailLookUp.BankPurchaseExpense.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(bank.Name) ? bank.NameArabic : bank.Name,
                                Amount = transactions.Where(x =>
                                        x.AccountId == bank.Id && x.TransactionType == TransactionType.AdvanceExpense && x.DocumentNumber.Contains("AE-"))
                                    .Sum(x => x.Debit - x.Credit)
                            });
                            //Bank Other Payment(JV, OC, PC, STI) Data
                            transactionDetailLookUp.OtherBankPayments.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(bank.Name) ? bank.NameArabic : bank.Name,
                                Amount = transactions.Where(x =>
                                        x.AccountId == bank.Id && (x.TransactionType == TransactionType.JournalVoucher || x.TransactionType == TransactionType.PettyCash || x.TransactionType == TransactionType.StockIn || x.TransactionType == TransactionType.AdvanceBankPay))
                                    .Sum(x => x.Debit - x.Credit)
                            });

                            transactionDetailLookUp.GrandTotal = transactions.Sum(x => x.Debit - x.Credit);
                        }
                    }
                    else
                    {
                        // Total Data via cash in hand
                        foreach (var cashAccount in cashInHandAccounts)
                        {
                            //Cash Sale Data
                            transactionDetailLookUp.CashSale.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(cashAccount.Name) ? cashAccount.NameArabic : cashAccount.Name,
                                Amount = 0
                            });

                            //Cash Expense Data
                            transactionDetailLookUp.CashExpense.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(cashAccount.Name) ? cashAccount.NameArabic : cashAccount.Name,
                                Amount = 0
                            });


                            //Cash Customer Receivable Data
                            transactionDetailLookUp.CashCustomerReceipt.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(cashAccount.Name) ? cashAccount.NameArabic : cashAccount.Name,
                                Amount = 0
                            });

                            //Cash Supplier Payable Data
                            transactionDetailLookUp.CashSupplierPay.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(cashAccount.Name) ? cashAccount.NameArabic : cashAccount.Name,
                                Amount = 0
                            });

                            //Cash Purchase Data
                            transactionDetailLookUp.CashPurchase.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(cashAccount.Name) ? cashAccount.NameArabic : cashAccount.Name,
                                Amount = 0
                            });
                            //Cash Sale Return Data
                            transactionDetailLookUp.SaleReturn.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(cashAccount.Name) ? cashAccount.NameArabic : cashAccount.Name,
                                Amount = 0
                            });

                            //Cash Purchase Expense Data
                            transactionDetailLookUp.CashPurchaseExpense.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(cashAccount.Name) ? cashAccount.NameArabic : cashAccount.Name,
                                Amount = 0
                            });

                            //Cash Other Payment(Jv, Op, Pc, STI) Data
                            transactionDetailLookUp.OtherCashPayments.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(cashAccount.Name) ? cashAccount.NameArabic : cashAccount.Name,
                                Amount = 0
                            });

                            transactionDetailLookUp.GrandTotal = 0;
                        }

                        // Total Data via Bank Account
                        foreach (var bank in bankAccounts)
                        {
                            //Bank Sale Data
                            transactionDetailLookUp.BankSale.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(bank.Name) ? bank.NameArabic : bank.Name,
                                Amount = 0
                            });

                            //Bank Expense Data
                            transactionDetailLookUp.BankExpense.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(bank.Name) ? bank.NameArabic : bank.Name,
                                Amount = 0
                            });

                            //Bank Customer Receipt Data
                            transactionDetailLookUp.BankCustomerReceipt.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(bank.Name) ? bank.NameArabic : bank.Name,
                                Amount = 0
                            });
                            //Bank Supplier Payable Data
                            transactionDetailLookUp.BankSupplierPay.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(bank.Name) ? bank.NameArabic : bank.Name,
                                Amount = 0
                            });
                            //Bank Purchase Data
                            transactionDetailLookUp.BankPurchase.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(bank.Name) ? bank.NameArabic : bank.Name,
                                Amount = 0
                            });
                            //Bank Purchase Expense Data
                            transactionDetailLookUp.BankPurchaseExpense.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(bank.Name) ? bank.NameArabic : bank.Name,
                                Amount = 0
                            });
                            //Bank Other Payment(JV, OC, PC, STI) Data
                            transactionDetailLookUp.OtherBankPayments.Add(new AccountDetailsLookUpModel()
                            {
                                Name = string.IsNullOrEmpty(bank.Name) ? bank.NameArabic : bank.Name,
                                Amount = 0
                            });
                        }
                    }
                    return transactionDetailLookUp;
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
