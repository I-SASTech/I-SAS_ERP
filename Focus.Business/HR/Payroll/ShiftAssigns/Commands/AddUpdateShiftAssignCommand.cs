using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.HR.Payroll.ShiftAssigns.Model;
using Focus.Business.HR.Payroll.ShiftAssigns.Queries;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.HR.Payroll.ShiftAssigns.Commands
{
    public class AddUpdateShiftAssignCommand : IRequest<Message>
    {
        public ShiftAssignModel ShiftAssign { get; set; }

        public class Handler : IRequestHandler<AddUpdateShiftAssignCommand, Message>
        {
            public readonly IApplicationDbContext Context;

            public readonly ILogger Logger;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<AddUpdateShiftAssignCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Message> Handle(AddUpdateShiftAssignCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.ShiftAssign.Id != Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var shift = await Context.ShiftAssigns.FindAsync(request.ShiftAssign.Id);
                        if (shift == null)
                            throw new NotFoundException("Shift not found", "");

                        Context.ShiftEmployeeAssigns.RemoveRange(shift.ShiftEmployeeAssigns);

                        shift.ShiftName = request.ShiftAssign.ShiftName;
                        shift.EmployeeId = request.ShiftAssign.EmployeeId;
                        shift.IsActive = request.ShiftAssign.IsActive;
                        shift.FromDate = request.ShiftAssign.FromDate;
                        shift.ToDate = request.ShiftAssign.ToDate;
                        shift.Description = request.ShiftAssign.Description;
                        shift.ReasonOfClosingShift = request.ShiftAssign.ReasonOfClosingShift;
                        shift.Monday = request.ShiftAssign.Monday;
                        shift.Tuesday = request.ShiftAssign.Tuesday;
                        shift.Wednesday = request.ShiftAssign.Wednesday;
                        shift.Thursday = request.ShiftAssign.Thursday;
                        shift.Friday = request.ShiftAssign.Friday;
                        shift.Saturday = request.ShiftAssign.Saturday;
                        shift.Sunday = request.ShiftAssign.Sunday;
                        shift.ShiftEmployeeAssigns = request.ShiftAssign.ShiftEmployeeAssigns.Select(x =>
                            new ShiftEmployeeAssign
                            {
                                ShiftAssignId = request.ShiftAssign.ShiftId,
                                EmployeeId = x.EmployeeId,
                                IsActive = x.IsActive,
                                FromDate = x.FromDate,
                                ToDate = x.ToDate,
                                Description = x.Description,
                                ReasonOfClosingShift = x.ReasonOfClosingShift
                            }).ToList();

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return new Message
                        {
                            Id = shift.Id
                        };
                    }
                    else
                    {
                        request.ShiftAssign.Code= await _mediator.Send(new GetAutoNoQuery(), cancellationToken);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var shiftAssign = new ShiftAssign()
                        {
                            ShiftName = request.ShiftAssign.ShiftName,
                            Code = request.ShiftAssign.Code,
                            EmployeeId = request.ShiftAssign.EmployeeId,
                            IsActive = request.ShiftAssign.IsActive,
                            FromDate = request.ShiftAssign.FromDate,
                            ToDate = request.ShiftAssign.ToDate,
                            Description = request.ShiftAssign.Description,
                            ReasonOfClosingShift = request.ShiftAssign.ReasonOfClosingShift,
                            Monday = request.ShiftAssign.Monday,
                            Tuesday = request.ShiftAssign.Tuesday,
                            Wednesday = request.ShiftAssign.Wednesday,
                            Thursday = request.ShiftAssign.Thursday,
                            Friday = request.ShiftAssign.Friday,
                            Saturday = request.ShiftAssign.Saturday,
                            Sunday = request.ShiftAssign.Sunday,

                            ShiftEmployeeAssigns = request.ShiftAssign.ShiftEmployeeAssigns.Select(x=>new ShiftEmployeeAssign
                            {
                                ShiftAssignId = request.ShiftAssign.ShiftId,
                                EmployeeId = x.EmployeeId,
                                IsActive = x.IsActive,
                                FromDate = request.ShiftAssign.FromDate,
                                ToDate = request.ShiftAssign.ToDate,
                                Description = x.Description,
                                ReasonOfClosingShift = x.ReasonOfClosingShift
                            }).ToList(),
                        };
                        await Context.ShiftAssigns.AddAsync(shiftAssign, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            Id = shiftAssign.Id
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
