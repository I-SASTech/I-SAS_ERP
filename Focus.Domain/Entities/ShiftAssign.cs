using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class ShiftAssign : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public string Code { get; set; }
        public string ShiftName { get; set; }
        public Guid? EmployeeId { get; set; }
        public virtual EmployeeRegistration Employee { get; set; }
        public bool IsActive { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Description { get; set; }
        public string ReasonOfClosingShift { get; set; }

        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public virtual ICollection<ShiftEmployeeAssign> ShiftEmployeeAssigns { get; set; }
        public virtual ICollection<ShiftRun> ShiftRuns { get; set; }

    }
}
