using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.LeaveRules.Model
{
    public class LeaveRulesLookupModel
    {
        public Guid Id { get; set; }
        public Guid? LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public Guid? LeaveGroupId { get; set; }
        public string LeaveGroupName { get; set; }
        public Guid? DesignationId { get; set; }
        public string DesignationName { get; set; }
        public Guid? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal RequiredExperience { get; set; }
        public Guid? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Guid? LeavePeriodId { get; set; }
        public string LeavePeriodName { get; set; }
        public string LeavesPerLeavePeriod { get; set; }
        public bool AdminAssignLeave { get; set; }
        public bool EmployeesApplyForLeaveType { get; set; }
        public bool BeyondCurrentLeaveBalance { get; set; }
        public bool LeaveAccrueEnabled { get; set; }
        public bool LeaveCarriedForward1 { get; set; }
        public decimal PercentageLeaveCF { get; set; }
        public decimal MaximumCFAmount { get; set; }
        public int CFLeaveAvailabilityPeriod1 { get; set; }
        public bool ProportionateLeaves { get; set; }
    }
}
