using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.LeaveRules.Model;
using Focus.Business.LeaveManagement.LeaveType.Model;
using Focus.Business.LeaveManagement.LeaveType.Quries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.LeaveRules.Queries
{
    public class LeaveRulesListQuery : PagedRequest, IRequest<PagedResult<List<LeaveRulesLookupModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public class Handler : IRequestHandler<LeaveRulesListQuery, PagedResult<List<LeaveRulesLookupModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<LeaveRulesListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<LeaveRulesLookupModel>>> Handle(LeaveRulesListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await _context.LeaveRules.AsNoTracking()
                                .Include(x => x.LeaveTypes)
                                .Include(x => x.LeaveGroups)
                                .Include(x => x.Designations)
                                .Include(x => x.Departments)
                                .Include(x => x.EmployeeRegistration)
                                .Include(x => x.LeavePeriods)
                                .ToListAsync();

                    if (request.IsDropDown)
                    {
                        var leaveType = new List<LeaveRulesLookupModel>();

                        foreach (var item in query)
                        {
                            leaveType.Add(new LeaveRulesLookupModel
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
                            }) ;
                        }
                        return new PagedResult<List<LeaveRulesLookupModel>>
                        {
                            Results = leaveType
                        };
                    }
                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;
                        query = query.Where(x => x.LeaveTypes.LeaveName.Contains(searchTerm)).ToList();
                    }

                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();

                    var list = new List<LeaveRulesLookupModel>();

                    foreach (var item in query)
                    {
                        list.Add(new LeaveRulesLookupModel
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
                        });
                    }
                    return new PagedResult<List<LeaveRulesLookupModel>>
                    {
                        Results = list,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = list.Count / request.PageSize
                    };
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            } 
        }
    }
}
