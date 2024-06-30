using Focus.Business.Interface;
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
    public class BranchPermissionDetailQuery : IRequest<List<BranchWisePermission>>
    {
       
        public Guid BranchId { get; set; }
       

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<BranchPermissionDetailQuery, List<BranchWisePermission>>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<BranchPermissionDetailQuery> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<List<BranchWisePermission>> Handle(BranchPermissionDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {


                 var record=   Context.BranchWisePermissions.Where(x=>x.BranchId==request.BranchId).ToList();


                 return record;


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
