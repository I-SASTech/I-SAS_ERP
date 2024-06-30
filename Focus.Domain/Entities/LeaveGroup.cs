using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class LeaveGroup : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public virtual ICollection<LeaveGroupEmployee> LeaveGroupEmployees { get; set; }
        public virtual ICollection<LeaveRules> LeaveRules { get; set; }
        public virtual ICollection<LeaveTypes> LeaveTypes { get; set; }
    }
}
