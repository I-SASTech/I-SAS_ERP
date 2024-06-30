using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.LeaveType.Model
{
    public class LeaveTypeLookUpModel
    {
        public Guid Id { get; set; }
        public string LeaveName { get; set; }
        public string LeaveColor { get; set; }
        public decimal LeavesPerLeavePeriod { get; set; }
        public bool AdminAssignLeave { get; set; }
        public bool EmployeesApplyForLeaveType { get; set; }
        public bool BeyondCurrentLeaveBalance { get; set; }
        public decimal PercentageLeaveCF { get; set; }
        public decimal MaximumCFAmount { get; set; }
        public bool CFLeaveAvailabilityPeriod { get; set; }
        public string LeaveGroup { get; set; }
        public bool LeaveCarriedForward1 { get; set; }
        public int CFLeaveAvailabilityPeriod1 { get; set; }
        public string CFLeaveAvailabilityPeriodToString { get; set; }
        public bool LeaveAccrueEnabled { get; set; }
        public bool ProportionateLeaves { get; set; }
        public bool EmployeeLeavePeriod { get; set; }
        public bool SendNotificationEmails { get; set; }

        public Guid? LeaveGroupId { get; set; }
    }
}
