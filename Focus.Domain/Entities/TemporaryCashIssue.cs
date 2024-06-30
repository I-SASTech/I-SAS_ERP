using System;
using System.Collections.Generic;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class TemporaryCashIssue : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public string NewUser { get; set; }
        public string Note { get; set; }
        public string Reason { get; set; }
        public string RegistrationNo { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public bool IsCashRequesterUser { get; set; }
        public Guid? TemporaryCashRequestId { get; set; }
        public virtual TemporaryCashRequest TemporaryCashRequest { get; set; }
        public virtual ICollection<TemporaryCashIssueItem> TemporaryCashIssueItems { get; set; }
        public virtual ICollection<TemporaryCashReturn> TemporaryCashReturns { get; set; }
        public virtual ICollection<TemporaryCashIssueExpense> TemporaryCashIssueExpenses { get; set; }
        public Guid? CashIssuerId { get; set; }
        public Guid? BranchId { get; set; }

        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}
