using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.LeavePeriod.Model;
using Focus.Business.LeaveManagement.LeaveType.Commands;
using Focus.Business.SyncRecords.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.LeavePeriod.Commands
{
    public class LeavePeriodAddUpdateCommand : IRequest<Message>
    {
        public LeavePeriodLookupModel leavePeriod { get; set; }

        public class Handler : IRequestHandler<LeavePeriodAddUpdateCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<LeavePeriodAddUpdateCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(LeavePeriodAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if(request.leavePeriod.Id != Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var leavesPeriod = await Context.LeavePeriods.FirstOrDefaultAsync(x => x.Id == request.leavePeriod.Id);
                        if (leavesPeriod == null)
                            throw new NotFoundException("Leave Type not found", "");

                        leavesPeriod.Name = request.leavePeriod.Name;
                        leavesPeriod.PeriodStart = request.leavePeriod.PeriodStart;
                        leavesPeriod.PeriodEnd = request.leavePeriod.PeriodEnd;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return new Message
                        {
                            Id = leavesPeriod.Id
                        };
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var leavesPeriod = new Focus.Domain.Entities.LeavePeriod()
                        {
                            Name = request.leavePeriod.Name,
                            PeriodEnd = request.leavePeriod.PeriodEnd,
                            PeriodStart = request.leavePeriod.PeriodStart,
                        };
                        await Context.LeavePeriods.AddAsync(leavesPeriod, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            Id = leavesPeriod.Id
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
