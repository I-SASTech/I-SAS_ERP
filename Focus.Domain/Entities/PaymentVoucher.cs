using System;
using System.Collections.Generic;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class PaymentVoucher : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity, ISoftDelete, IRecordDailyEntry
    {
        public DateTime Date { get; set; }
        public DateTime PaymentDate { get; set; }
        public string VoucherNumber { get; set; }
        public string Narration { get; set; }
        public string ChequeNumber { get; set; }
        public string NatureOfPayment { get; set; }
        public decimal Amount { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public PettyCash PettyCash { get; set; }
        public PaymentVoucherType PaymentVoucherType { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public virtual Guid? PurchaseInvoice { get; set; }
        public virtual Guid? SaleInvoice { get; set; }
        public Guid BankCashAccountId { get; set; }
        public virtual Account Account { get; set; }
        public Guid ContactAccountId { get; set; }
        public virtual Account ContactAccount { get; set; }
        public Guid? VatAccountId { get; set; }
        public virtual Account VatAccount { get; set; }
        public Guid? BillsId { get; set; }
        public virtual PurchaseBill Bills { get; set; }
        public Guid? ReparingOrderId { get; set; }
        public virtual ReparingOrder ReparingOrder { get; set; }
        public string DraftBy { get; set; }
        public string TransactionId { get; set; }
        public string Prefix { get; set; }
        public string ApprovedBy { get; set; }
        public string RejectBy { get; set; }
        public string VoidBy { get; set; }
        public string Reason { get; set; }
        public DateTime? DraftDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? RejectDate { get; set; }
        public DateTime? VoidDate { get; set; }
        public Guid? PeriodId { get; set; }
        public virtual ICollection<PaymentVoucherAttachment> PaymentVoucherAttachments { get; set; }
        public virtual ICollection<PurchaseOrderPayment> PurchaseOrderPayments { get; set; }
        public virtual ICollection<SaleOrderPayment> SaleOrderPayments { get; set; }
        public virtual ICollection<PurchasePostExpense> PurchasePostExpenses { get; set; }
        public virtual ICollection<ContractorPayment> ContractorPayments { get; set; }
        public virtual ICollection<PaymentVoucherItem> PaymentVoucherItems { get; set; }
        public string TaxMethod { get; set; }
        public Guid? TaxRateId { get; set; }
        public virtual TaxRate TaxRate { get; set; } 
        public Guid? MultiUpId { get; set; }
        public virtual MultiUp MultiUp { get; set; }
        public Guid? BranchId { get; set; }
        public string DocumentType { get; set; }
        public Guid? DocumentId { get; set; }
        public bool IsRefund { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal RemainingBalance { get; set; }

        public string Year { get; set; }
    }
}
