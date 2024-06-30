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

namespace Focus.Business.ModulesInformation.ModuleRights.Queries.GetModuleRightList
{
    public class GetModuleRightListQuery : IRequest<List<ModuleRightLookUpModel>>
    {
        public bool isNobel;

        public bool IsActive { get; set; }
        public Guid RoleId { get; set; }
        public string ModuleName { get; set; }
        public Guid ModuleId { get; set; }
        public bool isRights { get; set; }
        public Guid CompanyId { get; set; }

        public class Handler : IRequestHandler<GetModuleRightListQuery, List<ModuleRightLookUpModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetModuleRightListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<ModuleRightLookUpModel>> Handle(GetModuleRightListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (!request.isNobel && !request.isRights)
                    {
                        var modulesRightsList =
                            _context.NobleRolePermissions.Include(x => x.CompanyPermissions)
                                .Where(x => x.CompanyPermissions.PermissionName == request.ModuleName &&
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

                    if (request.isNobel && request.isRights)
                    {
                        _context.DisableTenantFilter = true;
                        var modulesRightsList = _context.NobleRolePermissions.Include(x => x.CompanyPermissions).Where(
                                x =>
                                    EF.Property<Guid>(x, "CompanyId") == request.CompanyId &&
                                    x.CompanyPermissions.Value == request.ModuleName && x.RoleId == request.RoleId)
                            .ToList();

                        //var modulesRightsList =
                        //    _context.NobleRolePermissions.Include(x => x.CompanyPermissions)
                        //        .Where(x => x.CompanyPermissions.Value == request.ModuleName && x.RoleId == request.RoleId).ToList();

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
                    else if (request.isNobel)
                    {
                        var modulesRightsList =
                            _context.NoblePermissions
                                .Where(x => x.NobleModuleId == request.ModuleId).ToList();
                        var rightsList = modulesRightsList.GroupBy(x => x.Category).ToList();
                        var permission = new List<ModuleRightLookUpModel>();
                        foreach (var per in rightsList)
                        {
                            permission.Add(new ModuleRightLookUpModel()
                            {
                                PermissionName = per.Key
                            });
                        }

                        return permission;
                    }
                    else if (request.isRights)
                    {
                        var modulesRightsList =
                            _context.NoblePermissions
                                .Where(x => x.Category == request.ModuleName).ToList();

                        var permission = new List<ModuleRightLookUpModel>();
                        foreach (var per in modulesRightsList)
                        {
                            permission.Add(new ModuleRightLookUpModel()
                            {
                                //PermissionId = per.Id,
                                PermissionName = per.Description,
                                PermissionCategory = per.Category,
                                IsActive = false
                            });
                        }

                        return permission;
                    }
                    else
                    {
                        return null;
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
