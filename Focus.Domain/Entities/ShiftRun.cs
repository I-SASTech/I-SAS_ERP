using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class ShiftRun : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public string Code { get; set; }
        public Guid? ShiftAssignId { get; set; }
        public virtual ShiftAssign ShiftAssign { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ShiftRunEmployee> ShiftRunEmployees { get; set; }

    }
}
