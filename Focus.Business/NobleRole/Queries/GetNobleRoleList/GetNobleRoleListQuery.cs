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

namespace Focus.Business.NobleRole.Queries.GetNobleRoleList
{
    public class GetNobleRoleListQuery : IRequest<NobleRoleListModel>
    {
        public bool isActive { get; set; }

        public class Handler : IRequestHandler<GetNobleRoleListQuery, NobleRoleListModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetNobleRoleListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<NobleRoleListModel> Handle(GetNobleRoleListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var nobleRoles = _context.NobleRoles.AsQueryable();

                    var nobleRolesList = await nobleRoles
                        .OrderBy(x => x.Name)
                        .Where(x => x.IsActive == request.isActive)
                        .ProjectTo<NobleRoleLookUpModel>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

                    return new NobleRoleListModel
                    {
                        NobleRoleModel = nobleRolesList
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
