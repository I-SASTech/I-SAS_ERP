using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class TemporaryCashIssueExpense : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid TemporaryCashIssueId { get; set; }
        public virtual TemporaryCashIssue TemporaryCashIssue { get; set; }
        public Guid? DocumentId { get; set; }
        public string DocumentType { get; set; }
        public Guid? BranchId { get; set; }
        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}
