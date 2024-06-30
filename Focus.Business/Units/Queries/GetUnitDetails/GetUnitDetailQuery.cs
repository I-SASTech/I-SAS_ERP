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

namespace Focus.Business.Units.Queries.GetUnitDetails
{
    public class GetUnitDetailQuery : IRequest<UnitDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetUnitDetailQuery, UnitDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetUnitDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<UnitDetailsLookUpModel> Handle(GetUnitDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var unit = await _context.Units.FindAsync(request.Id);

                    if (unit != null)
                    {
                        return UnitDetailsLookUpModel.Create(unit);
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
