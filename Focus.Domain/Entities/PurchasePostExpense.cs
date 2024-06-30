using System;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class PurchasePostExpense : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity, ISoftDelete, IRecordDailyEntry
    {
        public DateTime Date { get; set; }
        public string VoucherNumber { get; set; }
        public string ExpenseType { get; set; }
        public Guid? BillId { get; set; }
        public virtual PurchaseBill PurchaseBill { get; set; }
        public string Narration { get; set; }
        public string ChequeNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal VatAmount { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public PettyCash PettyCash { get; set; }
        public PaymentVoucherType PaymentVoucherType { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid? PurchasePostId { get; set; }
        public virtual PurchasePost PurchasePost { get; set; }
        public Guid? PaymentVoucherId { get; set; }
        public virtual PaymentVoucher PaymentVoucher { get; set; }
        public Guid? BankCashAccountId { get; set; }
        public Guid? ContactAccountId { get; set; }
        public Guid? VatAccountId { get; set; }
        public string DraftBy { get; set; }
        public string ApprovedBy { get; set; }
        public string RejectBy { get; set; }
        public string VoidBy { get; set; }
        public string Reason { get; set; }
        public DateTime? DraftDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? RejectDate { get; set; }
        public DateTime? VoidDate { get; set; }
        public string TaxMethod { get; set; }
        public Guid? TaxRateId { get; set; }
        public virtual TaxRate TaxRate { get; set; }
    }
}
