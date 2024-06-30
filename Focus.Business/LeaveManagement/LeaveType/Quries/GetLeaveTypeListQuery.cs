using Focus.Business.Common;
using Focus.Business.HR.Payroll.ShiftAssigns.Model;
using Focus.Business.HR.Payroll.ShiftAssigns.Queries;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.LeaveType.Model;
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

namespace Focus.Business.LeaveManagement.LeaveType.Quries
{
    public class GetLeaveTypeListQuery : PagedRequest, IRequest<PagedResult<List<LeaveTypeLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public class Handler : IRequestHandler<GetLeaveTypeListQuery, PagedResult<List<LeaveTypeLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetLeaveTypeListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<LeaveTypeLookUpModel>>> Handle(GetLeaveTypeListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await _context.LeaveTypes.Include(x => x.LeaveGroups).ToListAsync();

                    if (request.IsDropDown)
                    {
                        var leaveType = new List<LeaveTypeLookUpModel>();

                        foreach (var item in query)
                        {
                            leaveType.Add(new LeaveTypeLookUpModel
                            {
                                Id = item.Id,
                                LeaveName = item.LeaveName,
                                LeaveColor = item.LeaveColor,
                                LeavesPerLeavePeriod = item.LeavesPerLeavePeriod,
                                AdminAssignLeave = item.AdminAssignLeave,
                                EmployeesApplyForLeaveType = item.EmployeesApplyForLeaveType,
                                BeyondCurrentLeaveBalance = item.BeyondCurrentLeaveBalance,
                                PercentageLeaveCF = item.PercentageLeaveCF,
                                MaximumCFAmount = item.MaximumCFAmount,
                                CFLeaveAvailabilityPeriod1 = item.CFLeaveAvailabilityPeriod1,
                                CFLeaveAvailabilityPeriodToString = item.CFLeaveAvailabilityPeriod1.ToString(),
                                LeaveGroupId = item.LeaveGroupId,
                                LeaveAccrueEnabled = item.LeaveAccrueEnabled,
                                ProportionateLeaves = item.ProportionateLeaves,
                                EmployeeLeavePeriod = item.EmployeeLeavePeriod,
                                SendNotificationEmails = item.SendNotificationEmails,
                                LeaveCarriedForward1 = item.LeaveCarriedForward1,
                            });
                        }

                        return new PagedResult<List<LeaveTypeLookUpModel>>
                        {
                            Results = leaveType
                        };
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;
                        query = query.Where(x => x.LeaveName.Contains(searchTerm)).ToList();
                    }

                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();

                    var list = new List<LeaveTypeLookUpModel>();

                    foreach (var item in query)
                    {
                        list.Add(new LeaveTypeLookUpModel
                        {
                            Id = item.Id,
                            LeaveName = item.LeaveName,
                            LeaveAccrueEnabled = item.LeaveAccrueEnabled,
                            LeaveCarriedForward1 = item.LeaveCarriedForward1,
                            LeaveGroupId = item.LeaveGroupId,
                            LeaveGroup = item.LeaveGroups.Name,
                            LeavesPerLeavePeriod = item.LeavesPerLeavePeriod,
                        });
                    }

                    return new PagedResult<List<LeaveTypeLookUpModel>>
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
