using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.LeaveRules.Model;
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

namespace Focus.Business.LeaveManagement.LeaveRules.Commands
{
    public class LeaveRulesAddUpdateCommand : IRequest<Message>
    {
        public LeaveRulesLookupModel LeaveRules { get; set; }
        public class Handler : IRequestHandler<LeaveRulesAddUpdateCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<LeaveRulesAddUpdateCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(LeaveRulesAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if(request.LeaveRules.Id != Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var leaves = await Context.LeaveRules.FirstOrDefaultAsync(x => x.Id == request.LeaveRules.Id);
                        if (leaves == null)
                            throw new NotFoundException("Leave Type not found", "");

                        leaves.LeaveTypeId = request.LeaveRules.LeaveTypeId;
                        leaves.LeaveGroupId = request.LeaveRules.LeaveGroupId;
                        leaves.DesignationId = request.LeaveRules.DesignationId;
                        leaves.EmployeeId = request.LeaveRules.EmployeeId;
                        leaves.RequiredExperience = Convert.ToDecimal(request.LeaveRules.RequiredExperience);
                        leaves.DepartmentId = request.LeaveRules.DepartmentId;
                        leaves.LeavePeriodId = request.LeaveRules.LeavePeriodId;
                        leaves.LeavesPerLeavePeriod = request.LeaveRules.LeavesPerLeavePeriod;
                        leaves.AdminAssignLeave = request.LeaveRules.AdminAssignLeave;
                        leaves.EmployeesApplyForLeaveType = request.LeaveRules.EmployeesApplyForLeaveType;
                        leaves.LeaveAccrueEnabled = request.LeaveRules.LeaveAccrueEnabled;
                        leaves.LeaveCarriedForward1 = request.LeaveRules.LeaveCarriedForward1;
                        leaves.PercentageLeaveCF = Convert.ToDecimal(request.LeaveRules.PercentageLeaveCF);
                        leaves.MaximumCFAmount= Convert.ToDecimal(request.LeaveRules.MaximumCFAmount);
                        leaves.CFLeaveAvailabilityPeriod1 = request.LeaveRules.CFLeaveAvailabilityPeriod1;
                        leaves.BeyondCurrentLeaveBalance = request.LeaveRules.BeyondCurrentLeaveBalance;
                        leaves.ProportionateLeaves = request.LeaveRules.ProportionateLeaves;

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

                        var leaves = new Focus.Domain.Entities.LeaveRules()
                        {
                            LeaveTypeId = request.LeaveRules.LeaveTypeId,
                            LeaveGroupId = request.LeaveRules.LeaveGroupId,
                            DesignationId = request.LeaveRules.DesignationId,
                            EmployeeId = request.LeaveRules.EmployeeId,
                            RequiredExperience = Convert.ToDecimal(request.LeaveRules.RequiredExperience),
                            DepartmentId = request.LeaveRules.DepartmentId,
                            LeavePeriodId = request.LeaveRules.LeavePeriodId,
                            LeavesPerLeavePeriod = request.LeaveRules.LeavesPerLeavePeriod,
                            AdminAssignLeave = request.LeaveRules.AdminAssignLeave,
                            EmployeesApplyForLeaveType = request.LeaveRules.EmployeesApplyForLeaveType,
                            LeaveAccrueEnabled = request.LeaveRules.LeaveAccrueEnabled,
                            LeaveCarriedForward1 = request.LeaveRules.LeaveCarriedForward1,
                            PercentageLeaveCF = Convert.ToDecimal(request.LeaveRules.PercentageLeaveCF),
                            MaximumCFAmount = Convert.ToDecimal(request.LeaveRules.MaximumCFAmount),
                            CFLeaveAvailabilityPeriod1 = request.LeaveRules.CFLeaveAvailabilityPeriod1,
                            BeyondCurrentLeaveBalance = request.LeaveRules.BeyondCurrentLeaveBalance,
                            ProportionateLeaves = request.LeaveRules.ProportionateLeaves,
                         };

                        await Context.LeaveRules.AddAsync(leaves, cancellationToken);

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
