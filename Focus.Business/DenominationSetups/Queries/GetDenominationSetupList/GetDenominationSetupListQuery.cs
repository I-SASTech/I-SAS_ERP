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

namespace Focus.Business.DenominationSetups.Queries.GetDenominationSetupList
{
    public class GetDenominationSetupListQuery : IRequest<DenominationSetupListModel>
    {
        public bool isActive { get; set; }

        public class Handler : IRequestHandler<GetDenominationSetupListQuery, DenominationSetupListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetDenominationSetupListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<DenominationSetupListModel> Handle(GetDenominationSetupListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.isActive == false)
                    {
                        var brands = _context.DenominationSetups.AsQueryable();

                        var brandsList = await brands
                            .OrderBy(x => x.Number)
                            .ProjectTo<DenominationSetupLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new DenominationSetupListModel
                        {
                            DenominationSetups = brandsList
                        };
                    }
                    if (request.isActive)
                    {
                        var brands = _context.DenominationSetups.Where(x => x.isActive).AsQueryable();

                        var brandsList = await brands
                            .OrderBy(x => x.Number)
                            .ProjectTo<DenominationSetupLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new DenominationSetupListModel
                        {
                            DenominationSetups = brandsList
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
