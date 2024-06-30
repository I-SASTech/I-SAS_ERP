using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Users;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.NobleRole.Commands.AddUpdateNobleRole
{
    public class AddUpdateNobleRoleCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public bool isActive { get; set; }
        public string NameArabic { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateNobleRoleCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;
            private readonly RoleManager<IdentityRole> _roleManager;

            //Create logger interface variable for log error
            public readonly ILogger Logger;

            public Guid RoleId { get; private set; }
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateNobleRoleCommand> logger, RoleManager<IdentityRole> roleManager, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _roleManager = roleManager;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddUpdateNobleRoleCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var identityRole = new IdentityRole();

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.Id != Guid.Empty)
                    {
                        var nobleRole = await Context.NobleRoles.FindAsync(request.Id);
                        if (nobleRole == null)
                            throw new NotFoundException(request.Name, request.Id);

                        if (request.Name != "" && request.NameArabic != "")
                        {
                            //var isNobleRolesExist = await Context.NobleRoles.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken);
                            //if (nobleRole.Name != request.Name && isNobleRolesExist != null)
                            //    throw new ObjectAlreadyExistsException("NobleRole " + request.Name + " Already Exist");
                            //if (nobleRole.NameArabic != request.NameArabic && isNobleRolesExist != null)
                            //    throw new ObjectAlreadyExistsException("NobleRole " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            var isNobleRolesExist = await Context.NobleRoles.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
                            if (nobleRole.Name != request.Name && isNobleRolesExist != null)
                                throw new ObjectAlreadyExistsException("NobleRole " + request.Name + " Already Exist");
                        }
                        else
                        {
                            var isNobleRolesExist = await Context.NobleRoles.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken);
                            if (nobleRole.NameArabic != request.NameArabic && isNobleRolesExist != null)
                                throw new ObjectAlreadyExistsException("NobleRole " + request.NameArabic + " Already Exist");
                        }

                        nobleRole.Name = request.Name;
                        nobleRole.NameArabic = request.NameArabic;
                        nobleRole.NormalizedName = request.NormalizedName;
                        nobleRole.IsActive = request.isActive;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                        return nobleRole.Id;
                    }

                    if (request.Name!="" && request.NameArabic!="")
                    {
                        var isNobleRolesExist = await Context.NobleRoles.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken);
                        if (isNobleRolesExist != null)
                            throw new ObjectAlreadyExistsException("NobleRole " + request.Name +" "+ request.NameArabic + " Already Exist");
                    }
                    else if(request.Name != "")
                    {
                        var isNobleRolesExist = await Context.NobleRoles.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
                        if (isNobleRolesExist != null)
                            throw new ObjectAlreadyExistsException("NobleRole " + request.Name + " Already Exist");
                    }
                    else
                    {
                        var isNobleRolesExist = await Context.NobleRoles.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken);
                        if (isNobleRolesExist != null)
                            throw new ObjectAlreadyExistsException("NobleRole " + request.NameArabic + " Already Exist");
                    }



                    var nobleRoles = new NobleRoles
                    {
                        Name = request.Name,
                        NameArabic = request.NameArabic,
                        NormalizedName = request.NormalizedName,
                        IsActive = request.isActive
                    };

                    var isIdentityRoleExists = _roleManager.FindByNameAsync(nobleRoles.Name);
                    if (isIdentityRoleExists.Result == null)
                    {
                        identityRole.Name = nobleRoles.Name;
                        identityRole.NormalizedName = nobleRoles.NormalizedName;
                        await _roleManager.CreateAsync(identityRole);
                    }

                    await Context.NobleRoles.AddAsync(nobleRoles, cancellationToken);
                    RoleId = nobleRoles.Id;
                    var allRights = Context.CompanyPermissions.ToList();
                    foreach (var rights in allRights)
                    {
                        if (rights.NobleModules.ModuleName == "Other")
                        {
                            var permissions = new NobleRolePermission()
                            {
                                RoleId = RoleId,
                                PermissionId = rights.Id,
                                IsActive = true
                            };
                            await Context.NobleRolePermissions.AddAsync(permissions, cancellationToken);
                        }
                        else
                        {
                            var permissions = new NobleRolePermission()
                            {
                                RoleId = RoleId,
                                PermissionId = rights.Id,
                                IsActive = false
                            };
                            await Context.NobleRolePermissions.AddAsync(permissions, cancellationToken);
                        }
                            
                        
                    }

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                    }, cancellationToken);

                    await Context.SaveChangesAsync(cancellationToken);


                    return RoleId;
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Name Already Exist");
                }
            }
        }
    }
}
