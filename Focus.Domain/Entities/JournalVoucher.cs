using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;
using Focus.Domain.Enums;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class JournalVoucher : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity, ISoftDelete
    {
        public DateTime Date { get; set; }
        public string VoucherNumber { get; set; }
        public string Comments { get; set; }
        public string Narration { get; set; }
        public bool OpeningCash { get; set; }
        public bool IsStockTransfer { get; set; }
        public bool OneTimeEntry { get; set; }
        public Guid? PeriodId { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public virtual ICollection<JournalVoucherItem> JournalVoucherItems { get; set; }
        public virtual ICollection<JournalVoucherAttachment> JournalVoucherAttachments { get; set; }
        public Guid? BranchId { get; set; }

        public string Year { get; set; }
    }
}
