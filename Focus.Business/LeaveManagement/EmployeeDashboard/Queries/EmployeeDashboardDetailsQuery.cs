using Focus.Business.Common;
using Focus.Business.Contacts.Queries;
using Focus.Business.Dashboard.Queries.EmployeeDashboardQuery;
using Focus.Business.EmployeeToAspNetUser.Command;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.EmployeeDashboard.Model;
using Focus.Business.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.EmployeeDashboard.Queries
{
    public class EmployeeDashboardDetailsQuery : IRequest<HrEmployeeDashboardLookupModel>
    {
        public Guid? UserId { get; set; }

        public class Handler : IRequestHandler<EmployeeDashboardDetailsQuery, HrEmployeeDashboardLookupModel>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context, ILogger<EmployeeDashboardDetailsQuery> logger, UserManager<ApplicationUser> userManager)
            {
                Context = context;
                Logger = logger;
                _userManager = userManager;
            }

            public async Task<HrEmployeeDashboardLookupModel> Handle(EmployeeDashboardDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    
                    var leaveEmployee = await Context.LeaveGroupEmployees.FirstOrDefaultAsync(x => x.EmployeeId == request.UserId);

                    var holidays = await Context.CompanyHolidays.AsNoTracking().Where(x => x.IsActive).ToListAsync();

                    var employeeRegistration  = await Context.EmployeeRegistrations.AsNoTracking().Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == request.UserId);

                    if(leaveEmployee == null && employeeRegistration == null)
                    {
                        return new HrEmployeeDashboardLookupModel
                        {
                            LeavePerLeaveYear = 0,
                            LeavePeriod = "",
                        };
                    }
                    var leaveType = await Context.LeaveTypes.Include(x => x.LeaveGroups).AsNoTracking().Where(x => x.LeaveGroups.Id == leaveEmployee.LeaveGroupId).ToListAsync();
                    
                    var leaves = new List<LeavesLookupModel>();
                    var empHolidays = new List<HolidaysLookupModel>();
                    var sum = 0;

                    if(leaveType != null)
                    {
                        foreach (var item in leaveType)
                        {
                            sum = sum + Convert.ToInt16(item.LeavesPerLeavePeriod);
                            leaves.Add(new LeavesLookupModel
                            {
                                LeaveType = item.LeaveName,
                                TotLeaves = Convert.ToInt16(item.LeavesPerLeavePeriod),
                                CarriedForwardLeaves = Convert.ToInt16(item.PercentageLeaveCF),
                            });
                        }
                    }
                    if(holidays != null)
                    {
                        foreach (var item in holidays)
                        {
                            empHolidays.Add(new HolidaysLookupModel
                            {
                                Title = item.HolidayType == Domain.Enum.HolidayType.National ? "National" : "Guested",
                                Date = item.Date.ToString("yyyy-MM-dd"),
                                Color = item.Color,
                            });;
                        }
                    }

                    var leaveRules = await Context.LeaveRules
                                            .Include(x => x.LeaveTypes)
                                            .Include(x => x.LeavePeriods)
                                            .Include(x => x.LeaveGroups)
                                            .ThenInclude(x => x.LeaveGroupEmployees)
                                            .FirstOrDefaultAsync(x => x.LeaveGroups.Id == leaveEmployee.LeaveGroupId);


                    if(leaveRules != null)
                    {
                        return new HrEmployeeDashboardLookupModel
                        {
                            LeavePerLeaveYear = sum,
                            LeavePeriod = leaveRules.LeavePeriods.PeriodStart.ToString("dd/MM/yy") + " to " + leaveRules.LeavePeriods.PeriodEnd.ToString("dd/MM/yy"),
                            EmployeeCode = employeeRegistration.Code,
                            Department = employeeRegistration.Department.Name,
                            Leaves = leaves,
                            Holidays = empHolidays,
                            Status = employeeRegistration.EmployeeType.ToString(),
                            Gender = employeeRegistration.Gender,
                            Born = employeeRegistration.DateOfBirth.ToString(),
                            EmpoloyeeFor  = employeeRegistration.RegistrationDate.ToString(),

                        };
                    }
                    else
                    {
                        return new HrEmployeeDashboardLookupModel
                        {
                            LeavePerLeaveYear = 0,
                            LeavePeriod = "",
                        };
                    }
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
