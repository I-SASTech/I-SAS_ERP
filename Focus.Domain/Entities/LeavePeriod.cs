using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class LeavePeriod : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public virtual ICollection<LeaveRules> LeaveRules { get; set; }
        public virtual ICollection<PaidTimeOff> PaidLeaveOffs { get; set; }
    }
}
