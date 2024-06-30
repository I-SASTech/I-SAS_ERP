using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Interface;
using Focus.Business.ModulesInformation.ModuleRights.Queries.GetModuleRightList;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.RolePermission.Queries.GetByCategoryPermission
{
   public class GetByCategoryPermissionQuery : IRequest<List<ModuleRightLookUpModel>>
    {
        public Guid RoleId { get; set; }
        public string CategoryName { get; set; }
        
        public Guid CompanyId { get; set; }

        public class Handler : IRequestHandler<GetByCategoryPermissionQuery, List<ModuleRightLookUpModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetByCategoryPermissionQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<ModuleRightLookUpModel>> Handle(GetByCategoryPermissionQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.CompanyId != Guid.Empty)
                    {
                        _context.DisableTenantFilter = true;
                        var modulesRightsList =
                            _context.NobleRolePermissions.Include(x => x.CompanyPermissions)
                                .Where(x => EF.Property<Guid>(x,"CompanyId")==request.CompanyId &&
                                    x.CompanyPermissions.Value == request.CategoryName &&
                                            x.RoleId == request.RoleId).ToList();

                        var permission = new List<ModuleRightLookUpModel>();
                        foreach (var per in modulesRightsList)
                        {
                            permission.Add(new ModuleRightLookUpModel()
                            {
                                PermissionId = per.Id,
                                PermissionName = per.CompanyPermissions.PermissionName,
                                PermissionCategory = per.CompanyPermissions.Value,
                                IsActive = per.IsActive
                            });
                        }

                        return permission;
                    }
                    else
                    {
                        var modulesRightsList =
                            _context.NobleRolePermissions.Include(x => x.CompanyPermissions)
                                .Where(x => x.CompanyPermissions.Value == request.CategoryName &&
                                            x.RoleId == request.RoleId).ToList();

                        var permission = new List<ModuleRightLookUpModel>();
                        foreach (var per in modulesRightsList)
                        {
                            permission.Add(new ModuleRightLookUpModel()
                            {
                                PermissionId = per.Id,
                                PermissionName = per.CompanyPermissions.PermissionName,
                                PermissionCategory = per.CompanyPermissions.Value,
                                IsActive = per.IsActive
                            });
                        }

                        return permission;
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
