using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.RolePermission.Queries.RemoveUserRole
{
    public class RemoveUserRoleQuery : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }

        public class Handler : IRequestHandler<RemoveUserRoleQuery, Guid>
        {
            //Create an object of IApplicationDbContext for delete the data from database
            public readonly IApplicationDbContext _context;

            //Create object of logger for all type of message show
            public readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            //Constructor
            public Handler(IApplicationDbContext context, ILogger<RemoveUserRoleQuery> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(RemoveUserRoleQuery request, CancellationToken cancellationToken)
            {
                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }
                if (request.Id!=Guid.Empty)
                {
                    var userRoles = await _context.NobleUserRoles.FindAsync(request.Id);

                    //Check data is exist
                    if (userRoles != null)
                    {
                        _context.NobleUserRoles.Remove(userRoles);
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return request.Id;
                    }
                    //throw exception which data not exist
                    throw new NotFoundException("Not Found", request.Id);

                 
                }
                else
                {
                    var userRoles = _context.NobleUserRoles.FirstOrDefault(x => x.UserId == request.UserId);

                    if (userRoles != null)
                    {
                        _context.NobleUserRoles.Remove(userRoles);
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                        return request.Id;
                    }
                    else
                    {
                        return Guid.Empty;
                    }



                }
                //Get Data from database in specific id which we want to delete
               
            }
        }

    }
}
