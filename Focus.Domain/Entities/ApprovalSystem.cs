using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class ApprovalSystem : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public Guid? DepartmentId { get; set; }
        public Guid? DesignationId { get; set; }
        public virtual ICollection<ApprovalSystemEmployees> ApprovalSystemEmployees { get; set; }
        public ApprovaType ApprovaType { get; set; }
        public bool Status { get; set; }

    }
}
