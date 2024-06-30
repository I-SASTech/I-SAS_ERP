using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.HR.Payroll.Deductions.Commands;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.Deductions.Queries
{
    public class GetDeductionDetailQuery : IRequest<DeductionLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetDeductionDetailQuery, DeductionLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetDeductionDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<DeductionLookUpModel> Handle(GetDeductionDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var deduction = await _context.Deductions.FindAsync(request.Id);

                    if (deduction != null)
                    {
                        return new DeductionLookUpModel()
                        {
                            Id = deduction.Id,
                            AmountType = deduction.AmountType,
                            TaxPlan = deduction.TaxPlan,
                            Code = deduction.Code,
                            NameInPayslip = deduction.NameInPayslip,
                            NameInPayslipArabic = deduction.NameInPayslipArabic,
                            Amount = deduction.Amount,
                            Active = deduction.Active,
                        };
                    }
                    throw new NotFoundException(nameof(deduction), request.Id);
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
