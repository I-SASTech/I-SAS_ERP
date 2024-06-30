using System;
using System.Collections.Generic;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class TemporaryCashRequest : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public string NewUser { get; set; }
        public string Note { get; set; }
        public string RegistrationNo { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public bool IsCashRequesterUser { get; set; }
        public bool IsClose { get; set; }
        public virtual ICollection<TemporaryCashRequestItem> TemporaryCashRequestItems { get; set; }
        public virtual ICollection<TemporaryCashIssue> TemporaryCashIssues { get; set; }
        public Guid? BranchId { get; set; }

        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}
