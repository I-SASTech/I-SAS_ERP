using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class TemporaryCashRequestItem : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Guid TemporaryCashRequestId { get; set; }
        public virtual TemporaryCashRequest TemporaryCashRequest { get; set; }
        public Guid? BranchId { get; set; }

    }
}
