using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Focus.Business.RolePermission.Queries.GetRolesPermissionsList;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.RolePermission.Commands.AddUpdateRolesPermissions
{
    public class UpdatePermissionCommand : IRequest<Message>
    {
        public List<NobleRolesPermissionsLookUpModel> NobleRolesPermissionsLookUpModel;



        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<UpdatePermissionCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private readonly IMediator _mediator;
            private string _code;

            //Constructor

            public Handler(IApplicationDbContext context, ILogger<UpdatePermissionCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }

            public async Task<Message> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    {

                        var companyId = request.NobleRolesPermissionsLookUpModel.Select(x => x.CompanyId).FirstOrDefault();
                        var companyPermissionsGuid = new List<Guid>();
                        Context.DisableTenantFilter = true;

                        foreach (var item in request.NobleRolesPermissionsLookUpModel)
                        {
                          var  companyPermissions = Context.NobleRolePermissions.Include(x => x.CompanyPermissions)
                                    .Where(x => EF.Property<Guid>(x, "CompanyId") == item.CompanyId &&
                                                x.Id == item.PermissionId && x.RoleId==item.RoleId).ToList();
                            foreach (var x in companyPermissions)
                            {
                                x.IsActive = item.IsActive;
                                companyPermissionsGuid.Add(x.Id);

                            }
                            Context.NobleRolePermissions.UpdateRange(companyPermissions);
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels =  Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken: cancellationToken);
                        var updatedPermissions = Context.NobleRolePermissions.Where(x => companyPermissionsGuid.Contains(x.Id)).ToList();
                        foreach (var item in updatedPermissions)
                        {


                            Context.SetModified(item, "CompanyId", companyId);
                        }

                        Context.SaveChangesAfter();


                        var message = new Message
                        {
                            IsAddUpdate = "Data Updated Successfully"
                        };
                        return message;

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


