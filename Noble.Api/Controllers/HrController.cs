using System;
using Focus.Business.CompHoliday.Command;
using Focus.Business.CompHoliday.Models;
using Focus.Business.CompHoliday.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Focus.Business.HR.Payroll.ShiftAssigns.Commands;
using Focus.Business.HR.Payroll.ShiftAssigns.Queries;
using Focus.Business.HR.Payroll.ShiftAssigns.Model;
using Focus.Business.LeaveManagement.LeaveType.Model;
using Focus.Business.LeaveManagement.LeaveType.Commands;
using Focus.Business.LeaveManagement.LeaveType.Quries;
using Focus.Business.LeaveManagement.LeavePeriod.Model;
using Focus.Business.LeaveManagement.LeavePeriod.Commands;
using Focus.Business.LeaveManagement.LeavePeriod.Queries;
using Focus.Business.LeaveManagement.WorkWeek.Commands;
using Focus.Business.LeaveManagement.WorkWeek.Queries;
using Focus.Business.LeaveManagement.WorkWeek.Model;
using Focus.Business.LeaveManagement.Holiday.Queries;
using Focus.Business.LeaveManagement.Holiday.Commands;
using Focus.Business.LeaveManagement.Holiday.Model;
using Focus.Business.LeaveManagement.LeaveGroup.Model;
using Focus.Business.LeaveManagement.LeaveGroup.Commands;
using Focus.Business.LeaveManagement.LeaveGroup.Queries;
using Focus.Business.LeaveManagement.LeaveRules.Model;
using Focus.Business.LeaveManagement.LeaveRules.Commands;
using Focus.Business.LeaveManagement.LeaveRules.Queries;
using Focus.Business.EmployeeToAspNetUser.Command;
using Focus.Business.LeaveManagement.EmployeeDashboard.Queries;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HrController : BaseController
    {
        #region Holiday
        

        [Route("api/Hr/SaveHolidays")]
        [HttpPost("SaveHolidays")]
        public async Task<IActionResult> SaveHolidays([FromBody] CompanyHolidaysLookupModel company)
        {
            var message = await Mediator.Send(new AddUpdateCompanyHolidaysCommand()
            {
                companyHoliday = company
            });

            return Ok(message);
        }

        [Route("api/Hr/GetHolidaysDetails")]
        [HttpGet("GetHolidaysDetails")]
        public async Task<IActionResult> GetHolidaysDetails(Guid id)
        {
            var result = await Mediator.Send(new GetCompanyHolidaysDetails()
            {
                Id = id
            });
            return Ok(result);
        }

        [Route("api/Hr/GetHolidaysList")]
        [HttpGet("GetHolidaysList")]
        public async Task<IActionResult> GetHolidaysList(string searchTerm)
        {
            var result = await Mediator.Send(new GetCompanyHolidaysList()
            {
                SearchTerm = searchTerm
            });
            return Ok(result);
        }

        #endregion


        #region For Shift Assign

        [Route("api/Hr/SaveShiftAssign")]
        [HttpPost("SaveShiftAssign")]
        public async Task<IActionResult> SaveShiftAssign([FromBody] ShiftAssignModel shiftAssign)
        {
            var id = await Mediator.Send(new AddUpdateShiftAssignCommand()
            {
                ShiftAssign = shiftAssign
            });


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Hr/ShiftAssignList")]
        [HttpGet("ShiftAssignList")]
        public async Task<IActionResult> ShiftAssignList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var result = await Mediator.Send(new GetShiftAssignListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(result);
        }

        [Route("api/Hr/ShiftAssignDetail")]
        [HttpGet("ShiftAssignDetail")]
        public async Task<IActionResult> ShiftAssignDetail(Guid id)
        {
            var shift = await Mediator.Send(new GetShiftAssignDetailQuery()
            {
                Id = id
            });
            return Ok(shift);

        }

        [Route("api/Hr/SearchEmployeeList")]
        [HttpPost("SearchEmployeeList")]
        public async Task<IActionResult> SearchEmployeeList([FromBody] EmployeeSearchModel employeeSearch)
        {
            var result = await Mediator.Send(new SearchEmployeeListQuery
            {
                EmployeeSearch = employeeSearch
            });

            return Ok(result);
        }
        #endregion

        #region Leave Type
        [Route("api/Hr/SaveLeaveType")]
        [HttpPost("SaveLeaveType")]
        public async Task<IActionResult> SaveLeaveType([FromBody] LeaveTypeLookUpModel leaveTypes)
        {
            var id = await Mediator.Send(new AddUpdateLeaveTypeCommand()
            {
                leaveType = leaveTypes
            });


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Hr/LeaveTypeList")]
        [HttpGet("LeaveTypeList")]
        public async Task<IActionResult> LeaveTypeList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var result = await Mediator.Send(new GetLeaveTypeListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(result);
        }

        [Route("api/Hr/LeaveTypeDetail")]
        [HttpGet("LeaveTypeDetail")]
        public async Task<IActionResult> LeaveTypeDetail(Guid id)
        {
            var shift = await Mediator.Send(new GetLeaveTypeDetailQuery()
            {
                Id = id
            });
            return Ok(shift);

        }
        #endregion

        #region Leave Period
        [Route("api/Hr/SaveLeavePeriod")]
        [HttpPost("SaveLeavePeriod")]
        public async Task<IActionResult> SaveLeavePeriod([FromBody] LeavePeriodLookupModel leavePeriod)
        {
            var id = await Mediator.Send(new LeavePeriodAddUpdateCommand()
            {
                leavePeriod = leavePeriod
            });


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Hr/LeavePeriodList")]
        [HttpGet("LeavePeriodList")]
        public async Task<IActionResult> LeavePeriodList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var result = await Mediator.Send(new GetLeavePeriodListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(result);
        }

        [Route("api/Hr/LeavePeriodDetail")]
        [HttpGet("LeavePeriodDetail")]
        public async Task<IActionResult> LeavePeriodDetail(Guid id)
        {
            var leavePeriod = await Mediator.Send(new GetLeavePeriodDetailQuery()
            {
                Id = id
            });
            return Ok(leavePeriod);

        }
        #endregion

        #region Work Week
        [Route("api/Hr/SaveWorkWeek")]
        [HttpPost("SaveWorkWeek")]
        public async Task<IActionResult> SaveWorkWeek([FromBody] WorkWeekLookupModel workWeek)
        {
            var id = await Mediator.Send(new WorkWeekAddUpdateCommand()
            {
                workWeek = workWeek
            });


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Hr/WorkWeekList")]
        [HttpGet("WorkWeekList")]
        public async Task<IActionResult> WorkWeekList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var result = await Mediator.Send(new WorkWeekListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(result);
        }

        [Route("api/Hr/WorkWeekDetail")]
        [HttpGet("WorkWeekDetail")]
        public async Task<IActionResult> WorkWeekDetail(Guid id)
        {
            var leavePeriod = await Mediator.Send(new WorkWeekDetailQuery()
            {
                Id = id
            });
            return Ok(leavePeriod);

        }
        #endregion

        #region Leave Holiday
        [Route("api/Hr/SaveLeaveHoliday")]
        [HttpPost("SaveLeaveHoliday")]
        public async Task<IActionResult> SaveLeaveHoliday([FromBody] LeaveHolidayLookupModel leaveHoliday)
        {
            var id = await Mediator.Send(new LeaveHolidayAddUpdateCommand()
            {
                leaveHoliday = leaveHoliday
            });


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Hr/LeaveHolidayList")]
        [HttpGet("LeaveHolidayList")]
        public async Task<IActionResult> LeaveHolidayList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var result = await Mediator.Send(new GetLeaveHolidayListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(result);
        }

        [Route("api/Hr/LeaveHolidayDetail")]
        [HttpGet("LeaveHolidayDetail")]
        public async Task<IActionResult> LeaveHolidayDetail(Guid id)
        {
            var leave = await Mediator.Send(new GetLeaveHolidayDetailQuery()
            {
                Id = id
            });
            return Ok(leave);

        }
        #endregion

        #region Leave Groups
        [Route("api/Hr/SaveLeaveGroup")]
        [HttpPost("SaveLeaveGroup")]
        public async Task<IActionResult> SaveLeaveGroup([FromBody] LeaveGroupLookupModel leaveGroup)
        {
            var id = await Mediator.Send(new LeaveGroupAddUpdateCommand()
            {
                LeaveGroup = leaveGroup
            });


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Hr/LeaveGroupList")]
        [HttpGet("LeaveGroupList")]
        public async Task<IActionResult> LeaveGroupList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var result = await Mediator.Send(new LeaveGroupListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(result);
        }

        [Route("api/Hr/LeaveGroupDetail")]
        [HttpGet("LeaveGroupDetail")]
        public async Task<IActionResult> LeaveGroupDetail(Guid id)
        {
            var leave = await Mediator.Send(new LeaveGroupDetailsQuery()
            {
                Id = id
            });
            return Ok(leave);

        }
        #endregion
        
        #region Leave Rules
        [Route("api/Hr/SaveLeaveRules")]
        [HttpPost("SaveLeaveRules")]
        public async Task<IActionResult> SaveLeaveRules([FromBody] LeaveRulesLookupModel leaveRules)
        {
            var id = await Mediator.Send(new LeaveRulesAddUpdateCommand()
            {
                LeaveRules = leaveRules
            });


            return Ok(new { IsSuccess = true });
        }

        [Route("api/Hr/LeaveRulesList")]
        [HttpGet("LeaveRulesList")]
        public async Task<IActionResult> LeaveRulesList(string searchTerm, int? pageNumber, bool isDropdown)
        {
            var result = await Mediator.Send(new LeaveRulesListQuery
            {
                SearchTerm = searchTerm,
                IsDropDown = isDropdown,
                PageNumber = pageNumber ?? 1,
            });

            return Ok(result);
        }

        [Route("api/Hr/LeaveRulesDetail")]
        [HttpGet("LeaveRulesDetail")]
        public async Task<IActionResult> LeaveRulesDetail(Guid id)
        {
            var leave = await Mediator.Send(new LeaveRulesDetailsQuery()
            {
                Id = id
            });
            return Ok(leave);

        }
        #endregion

        #region Employees To User
        [Route("api/Hr/EmployeeToUser")]
        [HttpGet("EmployeeToUser")]
        public async Task<IActionResult> EmployeeToUser(Guid? employeeId, bool individual)
        {
            var data = await Mediator.Send(new EmployeeToAspNetUserAddCommand()
            {
                Individual = individual,
                EmployeeId = employeeId
            });
            return Ok(data);
        }
        #endregion

        #region Employees To User
        [Route("api/Hr/HrEmployeeDashboard")]
        [HttpGet("HrEmployeeDashboard")]
        public async Task<IActionResult> HrEmployeeDashboard(Guid? userId)
        {
            var data = await Mediator.Send(new EmployeeDashboardDetailsQuery()
            {
                UserId = userId,
            });
            return Ok(data);
        }
        #endregion

       
    }
}
