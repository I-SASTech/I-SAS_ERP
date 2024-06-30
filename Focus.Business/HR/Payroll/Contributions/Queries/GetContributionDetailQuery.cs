using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.HR.Payroll.Contributions.Commands;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.Contributions.Queries
{
    public class GetContributionDetailQuery : IRequest<ContributionLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetContributionDetailQuery, ContributionLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetContributionDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<ContributionLookUpModel> Handle(GetContributionDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var contribution = await _context.Contributions.FindAsync(request.Id);

                    if (contribution != null)
                    {
                        return new ContributionLookUpModel()
                        {
                            Id = contribution.Id,
                            AmountType = contribution.AmountType,
                            Code = contribution.Code,
                            NameInPayslip = contribution.NameInPayslip,
                            NameInPayslipArabic = contribution.NameInPayslipArabic,
                            Amount = contribution.Amount,
                            Active = contribution.Active,
                        };
                    }
                    throw new NotFoundException(nameof(contribution), request.Id);
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
