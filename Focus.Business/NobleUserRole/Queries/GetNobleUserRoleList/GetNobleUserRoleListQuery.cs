using AutoMapper;
using Focus.Business.Interface;
using Focus.Business.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.NobleUserRole.Queries.GetNobleUserRoleList
{
    public class GetNobleUserRoleListQuery : IRequest<List<NobleUserRoleLookUpModel>>
    {
        public Guid RoleId { get; set; }

        public class Handler : IRequestHandler<GetNobleUserRoleListQuery, List<NobleUserRoleLookUpModel>>
        {
            public readonly IApplicationDbContext _context;
            private readonly UserManager<ApplicationUser> _userManager;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetNobleUserRoleListQuery> logger, UserManager<ApplicationUser> userManager,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _userManager = userManager;
            }
            public async Task<List<NobleUserRoleLookUpModel>> Handle(GetNobleUserRoleListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var nobleRoles = _context.NobleRoles.AsQueryable();
                    var nobleUserRoles = _context.NobleUserRoles.AsQueryable();
                    var userList = _userManager.Users.AsQueryable();

                    var nobleRolesList = from nr in nobleRoles
                                         join usr in nobleUserRoles on nr.Id equals usr.RoleId
                                         join users in userList on usr.UserId equals users.Id
                                         where usr.RoleId == request.RoleId
                                         select new NobleUserRoleLookUpModel
                                         {
                                             Id = usr.Id,
                                             RoleId = nr.Id,
                                             RoleName = nr.Name,
                                             UserId =  users.Id,
                                             UserName = users.UserName
                                         };

                    //var usersPermissionsList = roleBasedUsers
                    //    .OrderBy(x=> x.UserName)
                    //    .ProjectTo<UserPermissionLookUpModel>(_mapper.ConfigurationProvider)
                    //    .ToList();

                    //return roleBasedUsers.ToList();

                    //var nobleRoles = _context.NobleUserRoles.AsQueryable();

                    //var nobleRolesList = await nobleRoles
                    //    .Where(x => x.isActive == request.isActive)
                    //    .ProjectTo<NobleUserRoleLookUpModel>(_mapper.ConfigurationProvider)
                    //    .ToListAsync(cancellationToken);

                    return nobleRolesList.ToList();
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
