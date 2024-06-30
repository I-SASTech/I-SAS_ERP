using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class LeaveRules : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid? LeaveTypeId { get; set; }
        public virtual LeaveTypes LeaveTypes { get; set; }
        public Guid? LeaveGroupId { get; set; }
        public virtual LeaveGroup LeaveGroups { get; set; }
        public Guid? DesignationId { get; set; }
        public virtual Designation Designations { get; set; }
        public Guid? EmployeeId { get; set; }
        public virtual EmployeeRegistration EmployeeRegistration { get; set; }
        public decimal RequiredExperience { get; set; }
        public Guid? DepartmentId { get; set; }
        public virtual Department Departments { get; set; }
        public Guid? LeavePeriodId { get; set; }
        public virtual LeavePeriod LeavePeriods { get; set; }
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
