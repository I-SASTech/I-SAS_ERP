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

namespace Focus.Business.Regions.Queries.GetRegionDetails
{
    public class GetRegionDetailQuery : IRequest<RegionDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetRegionDetailQuery, RegionDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetRegionDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<RegionDetailsLookUpModel> Handle(GetRegionDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var region = await _context.Regions.FindAsync(request.Id);

                    if (region != null)
                    {
                        return RegionDetailsLookUpModel.Create(region);
                    }
                    throw new NotFoundException(nameof(region), request.Id);
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
