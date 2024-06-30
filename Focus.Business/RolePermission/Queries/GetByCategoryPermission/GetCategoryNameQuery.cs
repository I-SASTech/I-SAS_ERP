using Focus.Business.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.RolePermission.Queries.GetByCategoryPermission
{
    public class GetCategoryNameQuery : IRequest<List<string>>
    {
        public Guid RoleId { get; set; }

        public Guid CompanyId { get; set; }

        public class Handler : IRequestHandler<GetCategoryNameQuery, List<string>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetCategoryNameQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<string>> Handle(GetCategoryNameQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.CompanyId != Guid.Empty)
                    {
                        _context.DisableTenantFilter = true;
                        var modulesRightsList =
                            _context.NobleRolePermissions.Include(x => x.CompanyPermissions)
                                .Where(x => EF.Property<Guid>(x, "CompanyId") == request.CompanyId  && x.RoleId==request.RoleId).ToList();
                        var list = modulesRightsList.GroupBy(x=>x.CompanyPermissions.Value).ToList();
                        return list.Select(x=>x.Key).ToList();
                    }
                    else
                    {
                  
                        var modulesRightsList =
                            _context.NobleRolePermissions.Include(x => x.CompanyPermissions)
                                .Where(x =>
                                          
                                            x.RoleId == request.RoleId).ToList();
                        var list = modulesRightsList.GroupBy(x => x.CompanyPermissions.Value).ToList();
                        return list.Select(x => x.Key).ToList();
                    }


                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
        }
    }
}
