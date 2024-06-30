using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class LeaveGroupEmployee : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid? EmployeeId { get; set; }
        public virtual EmployeeRegistration EmployeeRegistration { get; set; }
        public Guid? LeaveGroupId { get; set; }
        public virtual LeaveGroup LeaveGroups { get; set; }
    }
}
