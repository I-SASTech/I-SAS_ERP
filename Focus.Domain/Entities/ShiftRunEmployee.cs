using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class ShiftRunEmployee : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public Guid? ShiftRunId { get; set; }
        public virtual ShiftRun ShiftRun { get; set; }
        public Guid? EmployeeId { get; set; }
        public virtual EmployeeRegistration Employee { get; set; }
    }
}
