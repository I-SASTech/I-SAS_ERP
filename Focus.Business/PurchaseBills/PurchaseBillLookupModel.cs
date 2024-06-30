using System;
using System.Collections.Generic;
using Focus.Business.Attachments.Commands;
using Focus.Domain.Enum;

namespace Focus.Business.PurchaseBills
{
    public class PurchaseBillLookupModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime DueDate { get; set; }
        public string RegistrationNo { get; set; }
        public string Narration { get; set; }
        public string Reference { get; set; }
        public string TaxMethod { get; set; }
        public Guid? BillerId { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public PartiallyInvoices PartiallyInvoices { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public virtual ICollection<PurchaseBillItemLookupModel> PurchaseBillItems { get; set; }
        public virtual ICollection<BillAttachmentLookupModel> BillAttachments { get; set; }
        public string BillerAccount { get;  set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public Guid? BranchId { get; set; }

        public DateTime? BillDate { get; set; }
    }
}
