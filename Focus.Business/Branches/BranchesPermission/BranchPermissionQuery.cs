using Focus.Business.Interface;
using Focus.Business.NoblePermission.Commands.AddUpdateNoblePermission;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Branches.BranchesPermission
{
    public class BranchPermissionQuery : IRequest<Guid>
    {
        //Get all variable in entity
        public List<PermissionsLookUp> Permissions { get; set; }

        public Guid? BranchId { get; set; }
        public bool IsBranch { get; set; }
        public string RoleName { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<BranchPermissionQuery, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<BranchPermissionQuery> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(BranchPermissionQuery request, CancellationToken cancellationToken)
            {
                try
                {


                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                  

                    var list = new List<BranchWisePermission>();

                    if (request.BranchId != null)
                    {
                        var previousRecord = Context.BranchWisePermissions
                            .Where(x => x.BranchId == request.BranchId.Value).ToList();
                        if (previousRecord.Count > 0)
                        {
                            Context.BranchWisePermissions.RemoveRange(previousRecord);
                        }
                        foreach (var permission in request.Permissions)
                        {
                            if (permission.Checked)
                            {
                                list.Add(new BranchWisePermission
                                {
                                    PermissionName = permission.PermissionName,
                                    Key = permission.Key,
                                    Value = permission.Value,
                                    NobleGroupId = permission.GroupId,
                                    NobleModuleId = permission.ModuleId,
                                    BranchId = request.BranchId.Value

                                });
                            }

                           
                        }

                    }

                       
                   

                    await Context.BranchWisePermissions.AddRangeAsync(list,cancellationToken);



                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        BranchId=request.BranchId.Value,
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
