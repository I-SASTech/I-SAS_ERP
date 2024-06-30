using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.TaxRates.Queries.GetTaxRateDetails
{
    public class GetTaxRateDetailQuery : IRequest<TaxRateDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetTaxRateDetailQuery, TaxRateDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetTaxRateDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<TaxRateDetailsLookUpModel> Handle(GetTaxRateDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var unit = await _context.TaxRates.FindAsync(request.Id);

                    if (unit != null)
                    {
                        return TaxRateDetailsLookUpModel.Create(unit);
                    }
                    throw new NotFoundException(nameof(unit), request.Id);
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
