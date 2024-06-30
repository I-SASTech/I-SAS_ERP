using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.RolePermission.Commands.AddUpdateRolesPermissions
{
    public class AddUpdateRolesPermissionsCommand : IRequest<Message>
    {
        public virtual ICollection<NobleRolePermission> RolePermissions { get; set; }
        public string AllowAll { get; set; }
        public Guid RoleId { get; set; }
        public List<CompanyPermission> CompanyPermissions { get; set; }
        public bool IsNoble { get; set; }
        public Guid CompanyId { get; set; }
        public string UserId { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateRolesPermissionsCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private readonly IMediator _mediator;
            private string _code;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateRolesPermissionsCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(AddUpdateRolesPermissionsCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    var nobleRolesPermissionList = new List<NobleRolePermission>();
                    if (!request.IsNoble && request.CompanyId == Guid.Empty)
                    {
                        if (request.AllowAll == "")  
                        {
                            var rolesPermissions =
                                await Context.NobleRolePermissions.ToListAsync(cancellationToken: cancellationToken);
                            foreach (var item in request.RolePermissions)
                            {
                                var nobleRolePermission = rolesPermissions.FirstOrDefault(x => x.Id == item.Id);
                                if (nobleRolePermission != null)
                                {
                                    nobleRolePermission.IsActive = item.IsActive;
                                }

                                nobleRolesPermissionList.Add(new NobleRolePermission()
                                {
                                    Id = item.Id
                                });
                            }
                        }

                        if (request.AllowAll == "true")
                        {
                            var rolesPermissions = await Context.NobleRolePermissions
                                .Where(x => x.RoleId == request.RoleId)
                                .ToListAsync(cancellationToken: cancellationToken);
                            foreach (var item in rolesPermissions)
                            {
                                var nobleRolePermission = rolesPermissions.Where(x => x.RoleId == request.RoleId);
                                if (nobleRolePermission != null)
                                {
                                    item.IsActive = true;
                                }

                                nobleRolesPermissionList.Add(new NobleRolePermission()
                                {
                                    Id = item.Id
                                });
                            }
                        }

                        if (request.AllowAll == "false")
                        {
                            var rolesPermissions = await Context.NobleRolePermissions
                                .Where(x => x.RoleId == request.RoleId)
                                .ToListAsync(cancellationToken: cancellationToken);
                            foreach (var item in rolesPermissions)
                            {
                                var nobleRolePermission = rolesPermissions.Where(x => x.RoleId == item.RoleId);
                                if (nobleRolePermission != null)
                                {
                                    item.IsActive = false;
                                }

                                nobleRolesPermissionList.Add(new NobleRolePermission()
                                {
                                    Id = item.Id
                                });
                            }
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        var success = await Context.SaveChangesAsync(cancellationToken);

                        if (success > 0)
                        {
                            var message = new Message
                            {
                                IsAddUpdate = "Data Updated Successfully"
                            };
                            return message;
                        }
                        else
                        {
                            var message = new Message
                            {
                                IsAddUpdate = "Your Record Already Updated"
                            };
                            return message;
                        }
                    }

                    if (!request.IsNoble && request.CompanyId != Guid.Empty)
                    {
                        if (request.AllowAll == "")
                        {
                            var rolesPermissions =
                                await Context.NobleRolePermissions
                                    .Where(x => EF.Property<Guid>(x, "CompanyId") == request.CompanyId)
                                    .ToListAsync(cancellationToken: cancellationToken);
                            foreach (var item in request.RolePermissions)
                            {
                                var nobleRolePermission = rolesPermissions.FirstOrDefault(x => x.Id == item.Id);
                                if (nobleRolePermission != null)
                                {
                                    nobleRolePermission.IsActive = item.IsActive;
                                }

                                nobleRolesPermissionList.Add(new NobleRolePermission()
                                {
                                    Id = item.Id
                                });
                            }
                        }

                        if (request.AllowAll == "true")
                        {
                            var rolesPermissions = await Context.NobleRolePermissions
                                .Where(x => EF.Property<Guid>(x, "CompanyId") == request.CompanyId &&
                                            x.RoleId == request.RoleId)
                                .ToListAsync(cancellationToken: cancellationToken);
                            foreach (var item in rolesPermissions)
                            {
                                var nobleRolePermission = rolesPermissions.Where(x => x.RoleId == request.RoleId);
                                if (nobleRolePermission != null)
                                {
                                    item.IsActive = true;
                                }

                                nobleRolesPermissionList.Add(new NobleRolePermission()
                                {
                                    Id = item.Id
                                });
                            }
                        }

                        if (request.AllowAll == "false")
                        {
                            var rolesPermissions = await Context.NobleRolePermissions
                                .Where(x => EF.Property<Guid>(x, "CompanyId") == request.CompanyId &&
                                            x.RoleId == request.RoleId)
                                .ToListAsync(cancellationToken: cancellationToken);
                            foreach (var item in rolesPermissions)
                            {
                                var nobleRolePermission = rolesPermissions.Where(x => x.RoleId == item.RoleId);
                                if (nobleRolePermission != null)
                                {
                                    item.IsActive = false;
                                }

                                nobleRolesPermissionList.Add(new NobleRolePermission()
                                {
                                    Id = item.Id
                                });
                            }
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                        Context.DisableTenantFilter = true;
                        var rolePermissionList =
                            await Context.NobleRolePermissions.ToListAsync(cancellationToken: cancellationToken);

                        foreach (var item in nobleRolesPermissionList)
                        {
                            var newRolePermission = rolePermissionList.FirstOrDefault(x => x.Id == item.Id);

                            Context.SetModified(newRolePermission, "CompanyId", request.CompanyId);
                        }

                        Context.SaveChangesAfter();
                        return new Message()
                        {
                            IsAddUpdate = "Data Updated Successfully"
                        };
                    }

                    else
                    {

                        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                        Context.DisableTenantFilter = true;
                        if (request.AllowAll == null)
                        {
                            var companyPermissions =
                                Context.CompanyPermissions
                                    .Where(x => EF.Property<Guid>(x, "CompanyId") == request.CompanyId)
                                    .ToList();
                            foreach (var item in request.CompanyPermissions)
                            {

                                var companyPermission =
                                    companyPermissions.FirstOrDefault(x => x.PermissionName == item.PermissionName);
                                if (companyPermission == null)
                                {
                                    var addCmpList = new CompanyPermission()
                                    {
                                        PermissionName = item.PermissionName,
                                        Value = item.Value,
                                        NobleModuleId = item.NobleModuleId
                                    };
                                    await Context.CompanyPermissions.AddAsync(addCmpList, cancellationToken);
                                    Context.SetModified(addCmpList, "CompanyId", request.CompanyId);
                                    var nobleRoleList = new NobleRolePermission()
                                    {
                                        IsActive = true,
                                        RoleId = request.RoleId,
                                        PermissionId = addCmpList.Id
                                    };
                                    await Context.NobleRolePermissions.AddAsync(nobleRoleList, cancellationToken);
                                    Context.SetModified(nobleRoleList, "CompanyId", request.CompanyId);

                                }

                            }
                        }
                        if (request.AllowAll == "true")
                        {

                            Context.DisableTenantFilter = false;
                            var noblePermissions =
                                Context.NoblePermissions
                                    .ToList();
                            foreach (var item in noblePermissions)
                            {


                                {
                                    var addCmpList = new CompanyPermission()
                                    {
                                        PermissionName = item.Description,
                                        Value = item.Category,
                                        NobleModuleId = item.NobleModuleId,
                                    };
                                    await Context.CompanyPermissions.AddAsync(addCmpList, cancellationToken);
                                    Context.SetModified(addCmpList, "CompanyId", request.CompanyId);
                                    var nobleRoleList = new NobleRolePermission()
                                    {
                                        IsActive = true,
                                        RoleId = request.RoleId,
                                        PermissionId = addCmpList.Id
                                    };
                                    await Context.NobleRolePermissions.AddAsync(nobleRoleList, cancellationToken);
                                    Context.SetModified(nobleRoleList, "CompanyId", request.CompanyId);
                                }

                            }
                            //   var getUserRole=aas
                            var userRole = new NobleUserRoles()
                            {
                                RoleId = request.RoleId,
                                UserId = request.UserId,
                                isActive = true
                            };
                            await Context.NobleUserRoles.AddAsync(userRole, cancellationToken);
                            Context.SetModified(userRole, "CompanyId", request.CompanyId);

                        }
                        if (request.AllowAll == "false")
                        {

                            //var companyPermissions =
                            // Context.CompanyPermissions
                            //     .Where(x => EF.Property<Guid>(x, "CompanyId") == request.CompanyId)
                            //     .ToList();


                            //Context.CompanyPermissions.RemoveRange(companyPermissions);
                            //Context.SetModified(companyPermissions, "CompanyId", request.CompanyId);
                        }


                        Context.SaveChangesAfter();

                        scope.Complete();
                        return new Message()
                        {
                            IsAddUpdate = "Data Updated Successfully"
                        };

                    }
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Module Name Already Exist");
                }
                finally
                {
                    Context.DisableTenantFilter = false;
                }
            }
        }
    }
}
