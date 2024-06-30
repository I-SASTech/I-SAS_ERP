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

namespace Focus.Business.DenominationSetups.Queries.GetDenominationSetupDetails
{
    public class GetDenominationSetupDetailQuery : IRequest<DenominationSetupDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetDenominationSetupDetailQuery, DenominationSetupDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetDenominationSetupDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<DenominationSetupDetailsLookUpModel> Handle(GetDenominationSetupDetailQuery request, CancellationToken cancellationToken)
            {
                var denominationSetup = await _context.DenominationSetups.FindAsync(request.Id);

                try
                {
                    if (denominationSetup != null)
                    {
                        return DenominationSetupDetailsLookUpModel.Create(denominationSetup);
                    }
                    throw new NotFoundException(nameof(denominationSetup), request.Id);
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
