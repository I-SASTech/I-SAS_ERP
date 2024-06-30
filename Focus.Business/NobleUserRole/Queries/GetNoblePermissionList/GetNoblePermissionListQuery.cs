using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.NobleUserRole.Queries.GetNobleUserRoleDetails;
using Focus.Business.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.NobleUserRole.Queries.GetNoblePermissionList
{
    public class GetNoblePermissionListQuery : IRequest<NobleModulePermissionLookUp>
    {
        public Guid RoleId { get; set; }

        public class Handler : IRequestHandler<GetNoblePermissionListQuery, NobleModulePermissionLookUp>
        {
            public readonly IApplicationDbContext _context;
            private readonly UserManager<ApplicationUser> _userManager;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetNobleUserRoleDetailQuery> logger, UserManager<ApplicationUser> userManager,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _userManager = userManager;
            }
            public async Task<NobleModulePermissionLookUp> Handle(GetNoblePermissionListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var nobleModule = _context.CompanyPermissions.AsNoTracking().Include(x=>x.NobleModules).AsEnumerable().GroupBy(x => x.NobleModules).ToList();
                    var noblePermission = _context.NobleRolePermissions.Where(x=>x.RoleId == request.RoleId).ToList();
                    var nobleModulePermissionLookUp = new NobleModulePermissionLookUp
                    {
                        NobleModuleLook = new List<NobleModuleLookUp>(),
                        NoblePermissionLook = new List<NoblePermissionLookUp>()
                    };
                    if (noblePermission.Count > 0)
                    {
                        //var nobleModule = noblePermission.GroupBy(x => x.NobleModules).ToList();
                        foreach (var permission in noblePermission)
                        {
                            var noblePermissionLookUp = new NoblePermissionLookUp()
                            {
                                PermissionId = permission.PermissionId,
                                Key = permission.CompanyPermissions.Key,
                                IsActive = permission.IsActive,
                                RoleId = permission.RoleId,
                                Id = permission.Id,

                            };
                            nobleModulePermissionLookUp.NoblePermissionLook.Add(noblePermissionLookUp);
                        }
                        foreach (var module in nobleModule)
                        {
                            var nobleModuleLookUp = new NobleModuleLookUp()
                            {
                                ModuleId = module.Key.Description
                            };
                            nobleModulePermissionLookUp.NobleModuleLook.Add(nobleModuleLookUp);
                        }

                        return nobleModulePermissionLookUp;
                    }

                    

                    
                    throw new NotFoundException(nameof(noblePermission), request.RoleId);
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
