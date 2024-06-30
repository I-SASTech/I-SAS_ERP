using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.LeavePeriod.Commands;
using Focus.Business.LeaveManagement.LeavePeriod.Model;
using Focus.Business.LeaveManagement.WorkWeek.Model;
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

namespace Focus.Business.LeaveManagement.WorkWeek.Commands
{
    public class WorkWeekAddUpdateCommand : IRequest<Message>
    {
        public WorkWeekLookupModel workWeek { get; set; }

        public class Handler : IRequestHandler<WorkWeekAddUpdateCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<WorkWeekAddUpdateCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(WorkWeekAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if(request.workWeek.Id != Guid.Empty)
                    {
                        var work = await Context.WorkWeeks.FirstOrDefaultAsync(x => x.Id == request.workWeek.Id);
                        if (work == null)
                            throw new NotFoundException("Leave Type not found", "");

                        work.Day = request.workWeek.Day;
                        work.Status = request.workWeek.Status;
                        work.Country = request.workWeek.Country;

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
                    else
                    {
                        var work = new Focus.Domain.Entities.WorkWeek()
                        {
                            Day = request.workWeek.Day,
                            Status = request.workWeek.Status,
                            Country = request.workWeek.Country,
                        };
                        await Context.WorkWeeks.AddAsync(work, cancellationToken);

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
