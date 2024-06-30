using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DayStarts.Queries.GetDayStartList
{
    public class GetDayStartListQuery:IRequest<DayStartListModel>
    {
        public class Handler : IRequestHandler<GetDayStartListQuery, DayStartListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetDayStartListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<DayStartListModel> Handle(GetDayStartListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var dayStarts = _context.DayStarts.AsQueryable();

                    var dayStartList = await dayStarts
                        .OrderBy(x => x.Date)
                        .ProjectTo<DayStartLookUpModel>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

                    return new DayStartListModel
                    {
                        DayStarts = dayStartList
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
