using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.RolePermission.Queries.GetRolesPermissionsList
{
    public class GetRolesPermissionsListQuery : IRequest<List<RolesPermissionsLookUpModel>>
    {
        public bool isActive { get; set; }
        public Guid RoleId { get; set; }

        public class Handler : IRequestHandler<GetRolesPermissionsListQuery, List<RolesPermissionsLookUpModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetRolesPermissionsListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<RolesPermissionsLookUpModel>> Handle(GetRolesPermissionsListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    //var nobleRoles = _context.NobleRoles.AsQueryable();
                    //var rolePermissions = _context.RolesPermissions.AsQueryable();

                    //var roleBasedUsers = from nr in nobleRoles
                    //                     join usr in rolePermissions on nr.Id equals usr.RoleId
                    //                     where nr.Id == request.RoleId
                    //                     select new RolesPermissionsLookUpModel
                    //                     {
                    //                         Id = usr.Id,
                    //                         RoleId = usr.RoleId,
                    //                         RoleName = nr.Name
                    //                     };


                    //var modulePermissions = from mn in _context.ModulesNames
                    //    join mr in _context.ModulesRights on mn.Id equals mr.ModuleId
                    //    join rrp in _context.RolesPermissions on mn.Id.ToString() equals rrp.ModuleName
                    //    where mr.ModuleId == request.RoleId
                    //    select new
                    //    {
                    //        ModuleId = mn.Id,
                    //        ModuleName = mn.Name,
                    //        RightId = mr.Id,
                    //        RightName = mr.Description,
                    //        Save = (
                    //            rrp.Save == true ? true : false),
                    //        Edit = (
                    //            rrp.Edit == true ? true : false),
                    //        Delete = (
                    //            rrp.Delete == true ? true : false),
                    //        List = (
                    //            rrp.List == true ? true : false),
                    //    };



                    //var usersPermissionsList = roleBasedUsers
                    //    .OrderBy(x=> x.UserName)
                    //    .ProjectTo<UserPermissionLookUpModel>(_mapper.ConfigurationProvider)
                    //    .ToList();

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
