using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class BranchItems : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid? ProductId { get; set; }
        public Guid? BranchId { get; set; }
        public virtual Product Products { get; set; }
    }
}
