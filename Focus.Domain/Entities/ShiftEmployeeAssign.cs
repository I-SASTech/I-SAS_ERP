using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class ShiftEmployeeAssign : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public Guid? EmployeeId { get; set; }
        public virtual EmployeeRegistration Employee { get; set; }
        public Guid? ShiftAssignId { get; set; }
        public virtual ShiftAssign ShiftAssign { get; set; }
        public bool IsActive { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Description { get; set; }
        public string ReasonOfClosingShift { get; set; }
    }
}
