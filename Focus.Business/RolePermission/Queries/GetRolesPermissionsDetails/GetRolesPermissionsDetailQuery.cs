using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.RolesPermission.Queries.GetRolesPermissionsDetails
{
    public class GetRolesPermissionsDetailQuery : IRequest<RolesPermissionsDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetRolesPermissionsDetailQuery, RolesPermissionsDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            private readonly UserManager<ApplicationUser> _userManager;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetRolesPermissionsDetailQuery> logger, UserManager<ApplicationUser> userManager,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _userManager = userManager;
            }
            public async Task<RolesPermissionsDetailsLookUpModel> Handle(GetRolesPermissionsDetailQuery request, CancellationToken cancellationToken)
            {
                var usersPermissions = await _context.RolesPermissions.FindAsync(request.Id);

                if (usersPermissions != null)
                {
                    var roles = await _context.NobleRoles.FindAsync(usersPermissions.RoleId);
                    return new RolesPermissionsDetailsLookUpModel
                    {
                        Id = usersPermissions.Id,
                        ModuleName = usersPermissions.ModuleName,
                        RoleId = usersPermissions.RoleId,
                        RoleName = roles.Name,
                        Save = usersPermissions.Save,
                        Edit = usersPermissions.Edit,
                        Delete = usersPermissions.Delete,
                        List = usersPermissions.List,
                        Print = usersPermissions.Print,
                        Draft = usersPermissions.Draft,
                        Approved = usersPermissions.Approved
                    };
                }
                throw new NotFoundException(nameof(usersPermissions), request.Id);
            }
        }
    }
}
