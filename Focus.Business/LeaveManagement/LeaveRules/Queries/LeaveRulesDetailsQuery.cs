using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.LeaveRules.Model;
using Focus.Business.LeaveManagement.LeaveType.Model;
using Focus.Business.LeaveManagement.LeaveType.Quries;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.LeaveRules.Queries
{
    public class LeaveRulesDetailsQuery : IRequest<LeaveRulesLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<LeaveRulesDetailsQuery, LeaveRulesLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<LeaveRulesDetailsQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<LeaveRulesLookupModel> Handle(LeaveRulesDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var item = await _context.LeaveRules.FindAsync(request.Id);

                    if (item != null)
                    {
                        return new LeaveRulesLookupModel
                        {
                            Id = item.Id,
                            LeaveTypeId = item.LeaveTypeId,
                            LeaveTypeName = item.LeaveTypes?.LeaveName,
                            LeaveGroupId = item.LeaveGroupId,
                            LeaveGroupName = item.LeaveGroups?.Name,
                            DesignationId = item.DesignationId,
                            DepartmentName = item.Designations?.Name,
                            EmployeeId = item.EmployeeId,
                            EmployeeName = item.EmployeeRegistration?.EnglishName,
                            RequiredExperience = item.RequiredExperience,
                            DepartmentId = item.DepartmentId,
                            DesignationName = item.Departments?.Name,
                            LeavePeriodId = item.LeavePeriodId,
                            LeavePeriodName = item.LeavePeriods?.Name,
                            LeavesPerLeavePeriod = item.LeavesPerLeavePeriod,
                            AdminAssignLeave = item.AdminAssignLeave,
                            EmployeesApplyForLeaveType = item.EmployeesApplyForLeaveType,
                            LeaveAccrueEnabled = item.LeaveAccrueEnabled,
                            LeaveCarriedForward1 = item.LeaveCarriedForward1,
                            PercentageLeaveCF = item.PercentageLeaveCF,
                            MaximumCFAmount = item.MaximumCFAmount,
                            CFLeaveAvailabilityPeriod1 = item.CFLeaveAvailabilityPeriod1,
                            BeyondCurrentLeaveBalance = item.BeyondCurrentLeaveBalance,
                            ProportionateLeaves = item.ProportionateLeaves,
                        };
                    }

                    throw new NotFoundException(nameof(item), request.Id);

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
