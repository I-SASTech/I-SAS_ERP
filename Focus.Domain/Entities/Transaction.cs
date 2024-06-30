using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class Transaction : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, IRecordDailyEntry
    {
        public Guid DocumentId { get; set; }
        public string DocumentNumber { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DocumentDate { get; set; }
        public DateTime? ApprovalDate { get; set; }

        public string Description { get; set; }
        public string Year { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? AccountId { get; set; }
        public virtual Account Account { get; set; }
        public Guid? PeriodId { get; set; }
        public virtual CompanySubmissionPeriod Period { get; set; }
        public bool IsArchived { get; set; }
        public Guid? BranchId { get; set; }

    }
}
