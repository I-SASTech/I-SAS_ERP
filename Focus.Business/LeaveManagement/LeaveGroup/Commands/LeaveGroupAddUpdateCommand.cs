using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.Holiday.Commands;
using Focus.Business.LeaveManagement.LeaveGroup.Model;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.LeaveGroup.Commands
{
    public class LeaveGroupAddUpdateCommand : IRequest<Message>
    {
        public LeaveGroupLookupModel LeaveGroup { get; set; }

        public class Handler : IRequestHandler<LeaveGroupAddUpdateCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<LeaveGroupAddUpdateCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(LeaveGroupAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.LeaveGroup.Id != Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var leave = await Context.LeaveGroups.Include(x => x.LeaveGroupEmployees).FirstOrDefaultAsync(x => x.Id == request.LeaveGroup.Id);
                        if (leave == null)
                            throw new NotFoundException("Leave Type not found", "");

                        leave.Name = request.LeaveGroup.Name;
                        Context.LeaveGroupEmployees.RemoveRange(leave.LeaveGroupEmployees);

                        var leaveGroupEmployees = new List<LeaveGroupEmployee>();
                        foreach (var item in request.LeaveGroup.LeaveGroupEmployees)
                        {
                            if(item.EmployeeId != null)
                            {
                                leaveGroupEmployees.Add(new LeaveGroupEmployee
                                {
                                    LeaveGroupId = leave.Id,
                                    EmployeeId = item.EmployeeId,
                                });
                            }
                        }
                        await Context.LeaveGroupEmployees.AddRangeAsync(leaveGroupEmployees, cancellationToken);


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return new Message
                        {
                            Id = leave.Id
                        };
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var work = new Focus.Domain.Entities.LeaveGroup()
                        {
                            Name = request.LeaveGroup.Name,
                            Details = request.LeaveGroup.Details,
                        };
                        await Context.LeaveGroups.AddAsync(work, cancellationToken);
                        var leaveGroupEmployees = new List<LeaveGroupEmployee>();
                        foreach (var item in request.LeaveGroup.LeaveGroupEmployees)
                        {
                            if (item.EmployeeId != null)
                            {
                                leaveGroupEmployees.Add(new LeaveGroupEmployee
                                {
                                    LeaveGroupId = work.Id,
                                    EmployeeId = item.EmployeeId,
                                });
                            }
                        }
                        await Context.LeaveGroupEmployees.AddRangeAsync(leaveGroupEmployees, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            Id = work.Id
                        };
                    }
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            } 
        }
    }
}
