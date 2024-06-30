using DocumentFormat.OpenXml.Spreadsheet;
using Focus.Business.Exceptions;
using Focus.Business.HR.Payroll.ShiftAssigns.Model;
using Focus.Business.HR.Payroll.ShiftAssigns.Queries;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.LeaveType.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.LeaveType.Quries
{
    public class GetLeaveTypeDetailQuery : IRequest<LeaveTypeLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetLeaveTypeDetailQuery, LeaveTypeLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetLeaveTypeDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<LeaveTypeLookUpModel> Handle(GetLeaveTypeDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var leave = await _context.LeaveTypes.FindAsync(request.Id);

                    if (leave != null)
                    {
                        return new LeaveTypeLookUpModel
                        {
                            Id = leave.Id,
                            LeaveName = leave.LeaveName,
                            LeaveColor = leave.LeaveColor,
                            LeavesPerLeavePeriod = leave.LeavesPerLeavePeriod,
                            AdminAssignLeave = leave.AdminAssignLeave,
                            EmployeesApplyForLeaveType = leave.EmployeesApplyForLeaveType,
                            BeyondCurrentLeaveBalance = leave.BeyondCurrentLeaveBalance,
                            PercentageLeaveCF = leave.PercentageLeaveCF,
                            MaximumCFAmount = leave.MaximumCFAmount,
                            CFLeaveAvailabilityPeriod1 = leave.CFLeaveAvailabilityPeriod1,
                            CFLeaveAvailabilityPeriodToString = leave.CFLeaveAvailabilityPeriod1.ToString(),
                            LeaveGroupId = leave.LeaveGroupId,
                            LeaveAccrueEnabled = leave.LeaveAccrueEnabled,
                            ProportionateLeaves = leave.ProportionateLeaves,
                            EmployeeLeavePeriod = leave.EmployeeLeavePeriod,
                            SendNotificationEmails = leave.SendNotificationEmails,
                            LeaveCarriedForward1 = leave.LeaveCarriedForward1,
                        };
                    }

                    throw new NotFoundException(nameof(leave), request.Id);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
