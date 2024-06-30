using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.Holiday.Model;
using Focus.Business.LeaveManagement.WorkWeek.Commands;
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

namespace Focus.Business.LeaveManagement.Holiday.Commands
{
    public class LeaveHolidayAddUpdateCommand : IRequest<Message>
    {
        public LeaveHolidayLookupModel leaveHoliday { get; set; }

        public class Handler : IRequestHandler<LeaveHolidayAddUpdateCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<LeaveHolidayAddUpdateCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(LeaveHolidayAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if(request.leaveHoliday.Id != Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var holiday = await Context.LeaveHolidays.FirstOrDefaultAsync(x => x.Id == request.leaveHoliday.Id);
                        if (holiday == null)
                            throw new NotFoundException("Leave Type not found", "");

                        holiday.Name = request.leaveHoliday.Name;
                        holiday.Date = request.leaveHoliday.Date;
                        holiday.Status = request.leaveHoliday.Status;
                        holiday.Country = request.leaveHoliday.Country;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return new Message
                        {
                            Id = holiday.Id
                        };
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var work = new LeaveHoliday()
                        {
                            Name = request.leaveHoliday.Name,
                            Date = request.leaveHoliday.Date,
                            Status = request.leaveHoliday.Status,
                            Country = request.leaveHoliday.Country,
                        };
                        await Context.LeaveHolidays.AddAsync(work, cancellationToken);

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
