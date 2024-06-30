using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Zones.Queries.GetZoneList
{
  public  class GetzoneListQuery:IRequest<ZoneListModel>
    {
        public class Handler : IRequestHandler<GetzoneListQuery, ZoneListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, ILogger<GetzoneListQuery> logger,IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<ZoneListModel> Handle(GetzoneListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var zones = _context.Zones.AsQueryable();
                    var zoneList = await zones.OrderBy(x => x.Name)
                     .ProjectTo<ZoneLookUpModel>(_mapper.ConfigurationProvider)
                     .ToListAsync(cancellationToken);
                    return new ZoneListModel
                    {
                        Zones = zoneList
                    };
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
        }
}
