using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.AllowanceTypes.Queries.GetAllowanceTypeDetails
{
    public class GetAllowanceTypeDetailQuery : IRequest<AllowanceTypeDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetAllowanceTypeDetailQuery, AllowanceTypeDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetAllowanceTypeDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<AllowanceTypeDetailsLookUpModel> Handle(GetAllowanceTypeDetailQuery request, CancellationToken cancellationToken)
            {
                var allowanceType = await _context.AllowanceTypes.FindAsync(request.Id);

                try
                {
                    if (allowanceType != null)
                    {
                        return AllowanceTypeDetailsLookUpModel.Create(allowanceType);
                    }
                    throw new NotFoundException(nameof(allowanceType), request.Id);
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
