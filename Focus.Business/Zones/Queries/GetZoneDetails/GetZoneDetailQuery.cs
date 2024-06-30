using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Zones.Queries.GetZoneDetails
{
  public  class GetZoneDetailQuery : IRequest<ZoneDetailLookUpModel>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<GetZoneDetailQuery, ZoneDetailLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, ILogger<GetZoneDetailQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<ZoneDetailLookUpModel> Handle(GetZoneDetailQuery request, CancellationToken cancellationToken)
            {
                var zone = await _context.Zones.FindAsync(request.Id);
                if (zone == null)
                {
                    throw new NotFoundException(nameof(Zone), request.Id);
                }

                return ZoneDetailLookUpModel.Create(zone);
               
            }
        }
    
    }
}
