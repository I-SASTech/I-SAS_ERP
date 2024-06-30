using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.HR.Payroll.LoanPayments.Commands;
using Focus.Business.HR.Payroll.LoanPays.Commands;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.LoanPayments.Queries
{
    public class GetLoanPaymentDetailQuery : IRequest<LoanPaymentLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetLoanPaymentDetailQuery, LoanPaymentLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetLoanPaymentDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<LoanPaymentLookUpModel> Handle(GetLoanPaymentDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var loanPayment = await _context.LoanPayments.AsNoTracking()
                        .Include(x => x.EmployeeRegistration)
                        .Include(x => x.LoanPays)
                        .FirstOrDefaultAsync(x=>x.Id==request.Id,cancellationToken);

                    if (loanPayment != null)
                    {
                        return new LoanPaymentLookUpModel()
                        {
                            Id = loanPayment.Id,
                            EmployeeRegistrationId = loanPayment.EmployeeRegistrationId,
                            Description = loanPayment.Description,
                            LoanType = loanPayment.LoanType,
                            RecoveryMethod = loanPayment.RecoveryMethod,
                            InstallmentMethod = loanPayment.InstallmentMethod,
                            LoanDate = loanPayment.LoanTakenDate.ToString("MM/dd/yyyy"),
                            PaymentStartDate = loanPayment.PaymentStartDate,
                            LoanTakenDate = loanPayment.LoanTakenDate,
                            LoanAmount = loanPayment.LoanAmount,
                            RecoveryLoanAmount = loanPayment.RecoveryLoanAmount,
                            Payment = loanPayment.Payment,
                            InstallmentBaseSalary = loanPayment.InstallmentBaseSalary,
                            EmployeeSalary = loanPayment.EmployeeSalary,
                            EmployeeName = loanPayment.EmployeeRegistration.EnglishName,
                            EmployeeNameArabic = loanPayment.EmployeeRegistration.ArabicName,
                            DeductionValue = loanPayment.DeductionValue,
                            RemainingLoan = loanPayment.RemainingLoan,
                            ProvidentFundType = loanPayment.ProvidentFundType,
                            IsActive = loanPayment.IsActive,
                            LoanPays = loanPayment.LoanPays.Select(x =>
                                new LoanPayLookUpModel()
                                {
                                    Id = x.Id,
                                    RecoveryDate = x.PaymentDate.ToString("MM/dd/yyyy"),
                                    LoanPaymentId = x.LoanPaymentId,
                                    Comments = x.Comments,
                                    Amount = x.Amount,
                                    RemainingLoan = x.RemainingLoan,



                                }).ToList(),
                        };
                    }
                    throw new NotFoundException(nameof(loanPayment), request.Id);
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
