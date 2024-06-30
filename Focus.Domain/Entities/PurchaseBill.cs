using System;
using System.Collections.Generic;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public  class PurchaseBill : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant

    {
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public string RegistrationNo { get; set; }
        public string Narration { get; set; }
        public string Reference { get; set; }
        public string TaxMethod { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public PartiallyInvoices PartiallyInvoices { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public Guid? BillerId { get; set; }
        public virtual Account BillerAccount { get; set; }
        public virtual ICollection<PurchaseBillItem> PurchaseBillItems { get; set; }
        public virtual ICollection<DailyExpense> DailyExpenses { get; set; }
        public virtual ICollection<BillAttachment> BillAttachments { get; set; }
        public virtual ICollection<PaymentVoucher> PaymentVouchers { get; set; }
        public virtual ICollection<PurchasePostExpense> PurchasePostExpenses { get; set; }
        public virtual ICollection<PurchaseOrderExpense> PurchaseOrderExpenses { get; set; }
        public Guid? BranchId { get; set; }
        public DateTime? BillDate { get; set; }
    }
}
