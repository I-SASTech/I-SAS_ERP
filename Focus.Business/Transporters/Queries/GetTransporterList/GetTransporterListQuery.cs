using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Interface;
using Focus.Business.Zones.Queries.GetZoneList;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Transporters.Queries.GetTransporterList
{
    public class GetTransporterListQuery : IRequest<TransporterListModel>
    {
        public class Handler : IRequestHandler<GetTransporterListQuery, TransporterListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, ILogger<GetTransporterListQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<TransporterListModel> Handle(GetTransporterListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var transporters = _context
                        .Transporters.AsQueryable();
                    var transporterList = await transporters
                        .OrderBy(x => x.Name)
                        .ProjectTo<TransporterLookUpModel>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);
                    return new TransporterListModel
                    {
                        Transporters = transporterList
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
