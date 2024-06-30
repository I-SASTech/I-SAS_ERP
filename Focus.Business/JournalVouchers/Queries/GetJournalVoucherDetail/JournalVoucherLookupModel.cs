using System;
using System.Collections.Generic;
using Focus.Domain.Enum;
using Focus.Business.Attachments.Commands;

namespace Focus.Business.JournalVouchers.Queries.GetJournalVoucherDetail
{
    public class JournalVoucherLookupModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string VoucherNumber { get; set; }
        public string Comments { get; set; }
        public string Narration { get; set; }
        public bool IsStockTransfer { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public virtual ICollection<JournalVoucherItemLookupModel> JournalVoucherItems { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
    }
}