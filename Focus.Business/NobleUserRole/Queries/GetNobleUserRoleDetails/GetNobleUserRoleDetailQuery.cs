using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Focus.Business.NobleUserRole.Queries.GetNobleUserRoleDetails
{
    public class GetNobleUserRoleDetailQuery : IRequest<List<NobleUserRoleDetailsLookUpModel>>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetNobleUserRoleDetailQuery, List<NobleUserRoleDetailsLookUpModel>>
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
            public async Task<List<NobleUserRoleDetailsLookUpModel>> Handle(GetNobleUserRoleDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var nobleRoles = _context.NobleRoles.AsQueryable();
                    var nobleUserRoles = _context.NobleUserRoles.AsQueryable();
                    var userList = _userManager.Users.AsQueryable();

                    var nobleRolesList = from nr in nobleRoles
                                         join usr in nobleUserRoles on nr.Id equals usr.RoleId
                                         join users in userList on usr.UserId equals users.Id
                                         where usr.RoleId == request.Id
                                         select new NobleUserRoleDetailsLookUpModel
                                         {
                                             Id = usr.Id,
                                             RoleId = nr.Id,
                                             RoleName = nr.Name,
                                             UserId = users.Id,
                                             UserName = users.UserName
                                         };

                    if (nobleRolesList != null)
                    {
                        return nobleRolesList.ToList();
                    }
                    throw new NotFoundException(nameof(nobleRoles), request.Id);
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
