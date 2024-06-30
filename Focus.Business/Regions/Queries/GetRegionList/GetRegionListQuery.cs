using AutoMapper;
using AutoMapper.QueryableExtensions;
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

namespace Focus.Business.Regions.Queries.GetRegionList
{
    public class GetRegionListQuery : IRequest<RegionListModel>
    {
        public bool isActive { get; set; }

        public class Handler : IRequestHandler<GetRegionListQuery, RegionListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetRegionListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<RegionListModel> Handle(GetRegionListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.isActive == false)
                    {
                        var regions = _context.Regions.AsQueryable();

                        var regionsList = await regions
                            .OrderBy(x => x.Code)
                            .ProjectTo<RegionLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new RegionListModel
                        {
                            Regions = regionsList
                        };
                    }
                    if (request.isActive == true)
                    {
                        var regions = _context.Regions.AsQueryable();

                        var regionsList = await regions
                            .OrderBy(x => x.Code)
                            .Where(x=> x.isActive == true)
                            .ProjectTo<RegionLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new RegionListModel
                        {
                            Regions = regionsList
                        };
                    }
                    return null;
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
