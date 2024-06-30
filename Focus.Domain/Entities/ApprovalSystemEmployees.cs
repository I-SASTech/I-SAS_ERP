using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class ApprovalSystemEmployees : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public Guid? EmployeeId { get; set; }
        public virtual EmployeeRegistration EmployeeRegistration { get; set; }
        public Guid ApprovalSystemId { get; set; } 
        public virtual ApprovalSystem ApprovalSystem { get; set; } 
    }
}
