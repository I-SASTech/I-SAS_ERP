using System;
using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class RunPayroll : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public DateTime Date { get; set; }
        public Guid PayrollScheduleId { get; set; }
        public virtual PaySchedule PaySchedule { get; set; }
        public bool Status { get; set; }
        public bool Hourly { get; set; }
        public virtual ICollection<RunPayrollDetail> RunPayrollDetails { get; set; }
    }
}
