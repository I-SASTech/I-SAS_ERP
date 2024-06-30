using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class BranchUser : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public string UserId { get; set; }
    }
}
