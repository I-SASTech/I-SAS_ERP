using DocumentFormat.OpenXml.Spreadsheet;
using Focus.Business.Colors.Commands.AddUpdateBrand;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.LeaveType.Model;
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

namespace Focus.Business.LeaveManagement.LeaveType.Commands
{
    public class AddUpdateLeaveTypeCommand : IRequest<Message>
    {
        public LeaveTypeLookUpModel leaveType { get;set; }

        public class Handler : IRequestHandler<AddUpdateLeaveTypeCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<AddUpdateLeaveTypeCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(AddUpdateLeaveTypeCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if(request.leaveType.Id != Guid.Empty)
                    {
                        var leaves = await Context.LeaveTypes.FirstOrDefaultAsync(x => x.Id== request.leaveType.Id);
                        if (leaves == null)
                            throw new NotFoundException("Leave Type not found", "");

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        leaves.LeaveName = request.leaveType.LeaveName;
                        leaves.LeaveColor = request.leaveType.LeaveColor;
                        leaves.AdminAssignLeave = request.leaveType.AdminAssignLeave;
                        leaves.EmployeesApplyForLeaveType = request.leaveType.EmployeesApplyForLeaveType;
                        leaves.BeyondCurrentLeaveBalance = request.leaveType.BeyondCurrentLeaveBalance;
                        leaves.PercentageLeaveCF = request.leaveType.PercentageLeaveCF;
                        leaves.MaximumCFAmount = request.leaveType.MaximumCFAmount;
                        leaves.CFLeaveAvailabilityPeriod1 = request.leaveType.CFLeaveAvailabilityPeriod1;
                        leaves.LeaveGroupId = request.leaveType?.LeaveGroupId;
                        leaves.LeaveAccrueEnabled = request.leaveType.LeaveAccrueEnabled;
                        leaves.ProportionateLeaves = request.leaveType.ProportionateLeaves;
                        leaves.EmployeeLeavePeriod = request.leaveType.EmployeeLeavePeriod;
                        leaves.SendNotificationEmails = request.leaveType.SendNotificationEmails;
                        leaves.LeaveCarriedForward1 = request.leaveType.LeaveCarriedForward1;
                        leaves.LeavesPerLeavePeriod = request.leaveType.LeavesPerLeavePeriod;

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return new Message
                        {
                            Id = leaves.Id
                        };
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var leaves = new LeaveTypes()
                        {
                            LeaveName = request.leaveType.LeaveName,
                            LeaveColor = request.leaveType.LeaveColor,
                            LeavesPerLeavePeriod= request.leaveType.LeavesPerLeavePeriod,
                            AdminAssignLeave = request.leaveType.AdminAssignLeave,
                            EmployeesApplyForLeaveType = request.leaveType.EmployeesApplyForLeaveType,
                            BeyondCurrentLeaveBalance = request.leaveType.BeyondCurrentLeaveBalance,
                            PercentageLeaveCF = request.leaveType.PercentageLeaveCF,
                            MaximumCFAmount = request.leaveType.MaximumCFAmount,
                            CFLeaveAvailabilityPeriod1 = request.leaveType.CFLeaveAvailabilityPeriod1,
                            LeaveCarriedForward1 = request.leaveType.LeaveCarriedForward1,
                            LeaveGroupId = request.leaveType.LeaveGroupId,
                            LeaveAccrueEnabled = request.leaveType.LeaveAccrueEnabled,
                            ProportionateLeaves = request.leaveType.ProportionateLeaves,
                            EmployeeLeavePeriod = request.leaveType.EmployeeLeavePeriod,
                            SendNotificationEmails = request.leaveType.SendNotificationEmails,
                        };

                        await Context.LeaveTypes.AddAsync(leaves, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            Id = leaves.Id
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
