using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Transaction = Focus.Domain.Entities.Transaction;

namespace Focus.Business.HR.Payroll.LoanPays.Commands
{
    public class AddUpdateLoanPay : IRequest<Guid>
    {
        //Get all variable in entity
        public LoanPayLookUpModel LoanPay { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateLoanPay, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateLoanPay> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateLoanPay request, CancellationToken cancellationToken)
            {

                try
                {
                    {
                        var loanPayment = await Context.LoanPayments.AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == request.LoanPay.LoanPaymentId, cancellationToken: cancellationToken);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        if (loanPayment != null)
                        {
                            loanPayment.RemainingLoan = request.LoanPay.RemainingLoan;
                            loanPayment.Payment = loanPayment.Payment + request.LoanPay.Amount;
                            if (loanPayment.RemainingLoan <= 0)
                            {
                                loanPayment.IsActive = true;
                            }
                            Context.LoanPayments.Update(loanPayment);
                        }


                        var loanPay = new LoanPay
                        {
                            PaymentDate = request.LoanPay.PaymentDate,
                            Amount = request.LoanPay.Amount,
                            RemainingLoan = request.LoanPay.RemainingLoan,
                            Comments = request.LoanPay.Comments,
                            LoanPaymentId = request.LoanPay.LoanPaymentId,

                        };
                        //Add Department to database
                        await Context.LoanPays.AddAsync(loanPay, cancellationToken);


                        //Transaction
                        var period = await Context.CompanySubmissionPeriods
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.PeriodStart.Date <= DateTime.UtcNow.Date
                                                      && x.PeriodEnd.Date >= DateTime.UtcNow.Date, cancellationToken: cancellationToken);

                        var cashAccounts = await Context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Code == "10100001", cancellationToken: cancellationToken);

                        var employees = await Context.EmployeeRegistrations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == loanPayment.EmployeeRegistrationId, cancellationToken: cancellationToken);
                        var transactions = new List<Transaction>();

                        if (employees != null)
                        {
                            //Banks/Cash Transaction
                            transactions.Add(new Transaction()
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = request.LoanPay.PaymentDate,
                                ApprovalDate = DateTime.UtcNow,
                                AccountId = cashAccounts.Id,
                                Debit = 0,
                                Credit = Math.Abs(Math.Round(request.LoanPay.Amount, 2)),
                                Description = TransactionType.Payroll.ToString(),
                                DocumentId = loanPay.Id,
                                TransactionType = TransactionType.Payroll,
                                Year = DateTime.UtcNow.Year.ToString(),
                                DocumentNumber = loanPay.PaymentDate.ToString("d"),
                                PeriodId = period.Id
                            });

                            //Employees Transaction
                            transactions.Add(new Transaction()
                            {
                                Date = DateTime.UtcNow,
                                DocumentDate = request.LoanPay.PaymentDate,
                                ApprovalDate = DateTime.UtcNow,
                                AccountId = employees.PayableAccountId,
                                Debit = Math.Abs(Math.Round(request.LoanPay.Amount, 2)),
                                Credit = 0,
                                Description = TransactionType.Payroll.ToString(),
                                DocumentId = loanPay.Id,
                                TransactionType = TransactionType.Payroll,
                                Year = DateTime.UtcNow.Year.ToString(),
                                DocumentNumber = loanPay.PaymentDate.ToString("d"),
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
                        return loanPay.Id;



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
