using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Transaction = Focus.Domain.Entities.Transaction;

namespace Focus.Business.HR.Payroll.LoanPayments.Commands
{
    public class AddUpdateLoanPayment : IRequest<Guid>
    {
        //Get all variable in entity
        public LoanPaymentLookUpModel LoanPayment { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateLoanPayment, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateLoanPayment> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateLoanPayment request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.LoanPayment.Id != Guid.Empty)
                    {

                        var loanPayment = await Context.LoanPayments.FindAsync(request.LoanPayment.Id);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        loanPayment.EmployeeRegistrationId = request.LoanPayment.EmployeeRegistrationId;
                        loanPayment.Description = request.LoanPayment.Description;
                        loanPayment.LoanType = request.LoanPayment.LoanType;
                        loanPayment.RecoveryMethod = request.LoanPayment.RecoveryMethod;
                        loanPayment.InstallmentMethod = request.LoanPayment.InstallmentMethod;
                        loanPayment.LoanTakenDate = request.LoanPayment.LoanTakenDate;
                        loanPayment.PaymentStartDate = request.LoanPayment.PaymentStartDate;
                        loanPayment.LoanAmount = request.LoanPayment.LoanAmount;
                        loanPayment.RecoveryLoanAmount = request.LoanPayment.RecoveryLoanAmount;
                        loanPayment.Payment = request.LoanPayment.Payment;
                        loanPayment.InstallmentBaseSalary = request.LoanPayment.InstallmentBaseSalary;
                        loanPayment.EmployeeSalary = request.LoanPayment.EmployeeSalary;
                        loanPayment.DeductionValue = request.LoanPayment.DeductionValue;
                        loanPayment.IsActive = request.LoanPayment.IsActive;
                        loanPayment.ProvidentFundType = request.LoanPayment.ProvidentFundType;

                        //Transaction
                        var period = await Context.CompanySubmissionPeriods
                            .FirstOrDefaultAsync(x => x.PeriodStart.Date <= DateTime.UtcNow.Date
                                                      && x.PeriodEnd.Date >= DateTime.UtcNow.Date, cancellationToken: cancellationToken);

                        var cashAccounts = await Context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "10100001", cancellationToken: cancellationToken);

                        var employees = await Context.EmployeeRegistrations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.LoanPayment.EmployeeRegistrationId, cancellationToken: cancellationToken);
                        var transactions = new List<Transaction>();

                        if (employees != null)
                        {
                            //Banks/Cash Transaction
                            transactions.Add(new Transaction()
                            {
                                Date = DateTime.UtcNow,
                                AccountId = cashAccounts.Id,
                                Debit = 0,
                                Credit = Math.Abs(Math.Round(request.LoanPayment.LoanAmount, 2)),
                                Description = TransactionType.Payroll.ToString(),
                                DocumentId = loanPayment.Id,
                                TransactionType = TransactionType.Payroll,
                                Year = DateTime.UtcNow.Year.ToString(),
                                DocumentNumber = loanPayment.PaymentStartDate.ToString("d"),
                                PeriodId = period.Id
                            });

                            //Employees Transaction
                            transactions.Add(new Transaction()
                            {
                                Date = DateTime.UtcNow,
                                AccountId = employees.PayableAccountId,
                                Debit = Math.Abs(Math.Round(request.LoanPayment.LoanAmount, 2)),
                                Credit = 0,
                                Description = TransactionType.Payroll.ToString(),
                                DocumentId = loanPayment.Id,
                                TransactionType = TransactionType.Payroll,
                                Year = DateTime.UtcNow.Year.ToString(),
                                DocumentNumber = loanPayment.PaymentStartDate.ToString("d"),
                                PeriodId = period.Id
                            });
                            await Context.Transactions.AddRangeAsync(transactions, cancellationToken);
                        }

                        //Save changes to database
                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                        scope.Complete();

                        return loanPayment.Id;

                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var loanPayment = new LoanPayment
                        {
                            EmployeeRegistrationId = request.LoanPayment.EmployeeRegistrationId,
                            Description = request.LoanPayment.Description,
                            LoanType = request.LoanPayment.LoanType,
                            RecoveryMethod = request.LoanPayment.RecoveryMethod,
                            InstallmentMethod = request.LoanPayment.InstallmentMethod,
                            LoanTakenDate = request.LoanPayment.LoanTakenDate,
                            PaymentStartDate = request.LoanPayment.PaymentStartDate,
                            LoanAmount = request.LoanPayment.LoanAmount,
                            Payment = request.LoanPayment.Payment,
                            EmployeeSalary = request.LoanPayment.EmployeeSalary,
                            DeductionValue = request.LoanPayment.DeductionValue,
                            RecoveryLoanAmount = request.LoanPayment.RecoveryLoanAmount,
                            RemainingLoan = request.LoanPayment.RecoveryLoanAmount,
                            InstallmentBaseSalary = request.LoanPayment.InstallmentBaseSalary,
                            ProvidentFundType = request.LoanPayment.ProvidentFundType,
                            IsActive = request.LoanPayment.IsActive,
                        };
                        //Add Department to database
                        await Context.LoanPayments.AddAsync(loanPayment, cancellationToken);


                        //Transaction
                        var period = await Context.CompanySubmissionPeriods
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.PeriodStart.Date <= DateTime.UtcNow.Date
                                                      && x.PeriodEnd.Date >= DateTime.UtcNow.Date, cancellationToken: cancellationToken);

                        var cashAccounts = await Context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "10100001", cancellationToken: cancellationToken);

                        var employees = await Context.EmployeeRegistrations.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==request.LoanPayment.EmployeeRegistrationId, cancellationToken: cancellationToken);
                        var transactions = new List<Transaction>();

                        if (employees!=null)
                        {
                            //Banks/Cash Transaction
                            transactions.Add(new Transaction()
                            {
                                Date = DateTime.UtcNow,
                                AccountId = cashAccounts.Id,
                                Debit = 0,
                                Credit = Math.Abs(Math.Round(request.LoanPayment.LoanAmount, 2)),
                                Description = TransactionType.Payroll.ToString(),
                                DocumentId = loanPayment.Id,
                                TransactionType = TransactionType.Payroll,
                                Year = DateTime.UtcNow.Year.ToString(),
                                DocumentNumber = loanPayment.PaymentStartDate.ToString("d"),
                                PeriodId = period.Id
                            });

                            //Employees Transaction
                            transactions.Add(new Transaction()
                            {
                                Date = DateTime.UtcNow,
                                AccountId = employees.PayableAccountId,
                                Debit = Math.Abs(Math.Round(request.LoanPayment.LoanAmount, 2)),
                                Credit = 0,
                                Description = TransactionType.Payroll.ToString(),
                                DocumentId = loanPayment.Id,
                                TransactionType = TransactionType.Payroll,
                                Year = DateTime.UtcNow.Year.ToString(),
                                DocumentNumber = loanPayment.PaymentStartDate.ToString("d"),
                                PeriodId = period.Id
                            });
                            await Context.Transactions.AddRangeAsync(transactions, cancellationToken);
                        }

                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                        scope.Complete();

                        return loanPayment.Id;
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
