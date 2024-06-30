using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class TemporaryCashReturn : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string RegistrationNo { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public bool IsCashRequesterUser { get; set; }
        public Guid TemporaryCashIssueId { get; set; }
        public virtual TemporaryCashIssue TemporaryCashIssue { get; set; }
        public Guid? BranchId { get; set; }

        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}
