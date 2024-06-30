using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class TemporaryCashIssueItem : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Guid TemporaryCashIssueId { get; set; }
        public virtual TemporaryCashIssue TemporaryCashIssue { get; set; }
        public Guid? BranchId { get; set; }

    }
}
