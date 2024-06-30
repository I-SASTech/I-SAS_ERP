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

namespace Focus.Business.Citys.Queries.GetCityList
{
    public class GetCityListQuery : IRequest<CityListModel>
    {
        public bool isActive { get; set; }

        public class Handler : IRequestHandler<GetCityListQuery, CityListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetCityListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<CityListModel> Handle(GetCityListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.isActive == false)
                    {
                        var citys = _context.Cities.AsQueryable();

                        var citysList = await citys
                            .OrderBy(x => x.Name)
                            .ProjectTo<CityLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new CityListModel
                        {
                            Citys = citysList
                        };
                    }
                    if (request.isActive == true)
                    {
                        var citys = _context.Cities.AsQueryable();

                        var citysList = await citys
                            .OrderBy(x => x.Name)
                            .Where(x=> x.isActive == true)
                            .ProjectTo<CityLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new CityListModel
                        {
                            Citys = citysList
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
