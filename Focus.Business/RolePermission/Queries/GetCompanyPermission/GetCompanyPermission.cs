using DocumentFormat.OpenXml.InkML;
using Focus.Business.Interface;
using Focus.Business.ModulesInformation.ModuleRights.Queries.GetModuleRightList;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using static Focus.Business.Claims.Command.ModuleWiseClaim.GetModuleWiseClaims;

namespace Focus.Business.RolePermission.Queries.GetCompanyPermission
{
    public class GetCompanyPermission : IRequest<List<ModuleRightLookUpModel>>
    {

        public string CategoryName { get; set; }
        public Guid? BranchId { get; set; }

        public string UserId { get; set; }


        public class Handler : IRequestHandler<GetCompanyPermission, List<ModuleRightLookUpModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetCompanyPermission> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<ModuleRightLookUpModel>> Handle(GetCompanyPermission request, CancellationToken cancellationToken)
            {
                try
                {
                    {



                        var roles = await _context.NobleUserRoles.Where(x => x.UserId == request.UserId).Select(x => x.RoleId).ToListAsync(cancellationToken: cancellationToken);
                        {
                            var modulesRightsList = await _context.NobleRolePermissions
                                .Include(x => x.CompanyPermissions)
                                .ThenInclude(x => x.NobleModules)
                                .Where(x => roles.Contains(x.RoleId) && x.IsActive).ToListAsync(cancellationToken: cancellationToken);


                            if (request.BranchId != null)
                            {
                                var branchList = _context.Branches.FirstOrDefault(x => x.Id == request.BranchId.Value);
                                var nobleModule = _context.NobleModules.FirstOrDefault(x => x.ModuleName == "Other");

                                if (branchList != null && !branchList.MainBranch)
                                {
                                    var branchWiseList = _context.BranchWisePermissions
                                        .Where(x => x.BranchId == request.BranchId.Value).ToList();
                                    var permission = new List<ModuleRightLookUpModel>();

                                    foreach (var per in modulesRightsList)
                                    {
                                        var climsList = new List<Claim>();

                                        var neWisePermission = branchWiseList.FirstOrDefault(X => X.Key == per.CompanyPermissions.Key);
                                        if (nobleModule != null)
                                        {
                                            if (per.CompanyPermissions.NobleModuleId == nobleModule.Id)
                                            {
                                                permission.Add(new ModuleRightLookUpModel()
                                                {
                                                    PermissionId = per.Id,
                                                    PermissionName = per.CompanyPermissions.PermissionName,
                                                    PermissionCategory = per.CompanyPermissions.Value,
                                                    IsActive = per.IsActive,
                                                    Key = per.CompanyPermissions.Key,
                                                    NobleModuleId = per.CompanyPermissions.NobleModuleId,
                                                    ModuleDescription = per.CompanyPermissions.NobleModules.Description,
                                                    ModuleName = per.CompanyPermissions.NobleModules.ModuleName,
                                                });

                                            }
                                        }

                                        if (neWisePermission != null)
                                        {

                                            permission.Add(new ModuleRightLookUpModel()
                                            {
                                                PermissionId = per.Id,
                                                PermissionName = per.CompanyPermissions.PermissionName,
                                                PermissionCategory = per.CompanyPermissions.Value,
                                                IsActive = per.IsActive,
                                                Key = per.CompanyPermissions.Key,
                                                NobleModuleId = per.CompanyPermissions.NobleModuleId,
                                                ModuleDescription = per.CompanyPermissions.NobleModules.Description,
                                                ModuleName = per.CompanyPermissions.NobleModules.ModuleName,
                                            });


                                        }




                                    }
                                    return permission;


                                }
                                else
                                {
                                    var permission = new List<ModuleRightLookUpModel>();
                                    foreach (var per in modulesRightsList)
                                    {
                                        permission.Add(new ModuleRightLookUpModel()
                                        {
                                            PermissionId = per.Id,
                                            PermissionName = per.CompanyPermissions.PermissionName,
                                            PermissionCategory = per.CompanyPermissions.Value,
                                            IsActive = per.IsActive,
                                            Key = per.CompanyPermissions.Key,
                                            NobleModuleId = per.CompanyPermissions.NobleModuleId,
                                            ModuleDescription = per.CompanyPermissions.NobleModules.Description,
                                            ModuleName = per.CompanyPermissions.NobleModules.ModuleName,
                                        });
                                    }

                                    return permission;

                                }
                            }
                            else
                            {
                                var permission = new List<ModuleRightLookUpModel>();
                                foreach (var per in modulesRightsList)
                                {
                                    permission.Add(new ModuleRightLookUpModel()
                                    {
                                        PermissionId = per.Id,
                                        PermissionName = per.CompanyPermissions.PermissionName,
                                        PermissionCategory = per.CompanyPermissions.Value,
                                        IsActive = per.IsActive,
                                        Key = per.CompanyPermissions.Key,
                                        NobleModuleId = per.CompanyPermissions.NobleModuleId,
                                        ModuleDescription = per.CompanyPermissions.NobleModules.Description,
                                        ModuleName = per.CompanyPermissions.NobleModules.ModuleName,
                                    });
                                }

                                return permission;
                            }


                        }
                        return null;
                    }
                    

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
