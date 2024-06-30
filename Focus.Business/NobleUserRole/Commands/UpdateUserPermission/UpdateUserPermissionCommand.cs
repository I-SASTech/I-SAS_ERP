using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.NoblePermission.Commands.AddUpdateNoblePermission;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.NobleUserRole.Commands.UpdateUserPermission
{
    public class UpdateUserPermissionCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public List<PermissionsLookUp> Permissions { get; set; }
        public string RoleName { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<UpdateUserPermissionCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<UpdateUserPermissionCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(UpdateUserPermissionCommand request, CancellationToken cancellationToken)
            {
                try
                {
                  
                    var userRolePermission = Context.NobleRolePermissions.ToList();

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var companyPermissions = Context.CompanyPermissions.Where(x => x.NobleModules.ModuleName == "Other").ToList();
                    if (companyPermissions != null)
                    {
                        var otherId = Context.NobleRoles.FirstOrDefault(x=>x.Name==request.RoleName);
                        if (otherId != null)
                        {
                            foreach (var companyPermission in companyPermissions)
                            {
                                var record = userRolePermission.FirstOrDefault(x => x.PermissionId == companyPermission.Id);
                                if (record != null)
                                {
                                    userRolePermission.Remove(record);
                                }

                            }
                            foreach (var item in companyPermissions)
                            {
                                var permission = new NobleRolePermission();
                                {


                                    permission = new NobleRolePermission()
                                    {

                                        IsActive = true,
                                        RoleId = otherId.Id,
                                        PermissionId = item.Id,
                                        
                                    };
                                }

                                await Context.NobleRolePermissions.AddAsync(permission, cancellationToken);


                            }


                        }

                    }



                    foreach (var permission in request.Permissions)
                    {
                        var userRole = userRolePermission.FirstOrDefault(x=>x.Id == permission.Id);
                        if (userRole != null)
                        {
                            userRole.IsActive = permission.Checked;
                        }
                    }

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);

                    await Context.SaveChangesAsync(cancellationToken);
                    return Guid.NewGuid();
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Role With User Already Exist");
                }
            }
        }
    }
}
