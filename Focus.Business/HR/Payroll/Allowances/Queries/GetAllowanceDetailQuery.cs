using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.HR.Payroll.Allowances.Commands.AddAllowance;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.Allowances.Queries
{
    public class GetAllowanceDetailQuery:IRequest<AllowanceLookUp>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetAllowanceDetailQuery, AllowanceLookUp>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetAllowanceDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<AllowanceLookUp> Handle(GetAllowanceDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var allowance = await _context.Allowances.FindAsync(request.Id);

                    if (allowance != null)
                    {
                        return new AllowanceLookUp()
                        {
                            Id = allowance.Id,
                            AllowanceTypeId = allowance.AllowanceTypeId,
                            AmountType = allowance.AmountType,
                            TaxPlan = allowance.TaxPlan,
                            Code = allowance.Code,
                            Description = allowance.Description,
                            Amount = allowance.Amount,
                            IsActive = allowance.IsActive,
                        };
                    }
                    throw new NotFoundException(nameof(allowance), request.Id);
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
