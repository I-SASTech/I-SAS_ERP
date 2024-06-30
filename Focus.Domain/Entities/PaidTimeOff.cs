using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class PaidTimeOff : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid? LeaveType { get; set; }
        public virtual LeaveTypes LeaveTypes { get; set; }
        public Guid? EmployeeId { get; set; }
        public virtual EmployeeRegistration EmployeeRegistration { get; set; }
        public Guid? LeavePeriodId { get; set; }
        public virtual LeavePeriod LeavePeriods { get; set; }
        public string Details { get; set; }
    }
}
