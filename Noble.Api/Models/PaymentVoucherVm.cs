using System;
using System.Collections.Generic;
using Focus.Business.Attachments.Commands;
using Focus.Business.PaymentVouchers.Commands;
using Focus.Domain.Enum;

namespace Noble.Api.Models
{
    public class PaymentVoucherVm
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime PaymentDate { get; set; }
        public string VoucherNumber { get; set; }
        public string NatureOfPayment { get; set; }
        public string Narration { get; set; }
        public decimal Amount { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public PaymentVoucherType PaymentVoucherType { get; set; }
        public PettyCash? PettyCash { get; set; }
        public virtual Guid? PurchaseInvoice { get; set; }
        public virtual Guid? SaleInvoice { get; set; }
        public Guid? ReparingOrderId { get; set; }

        public Guid BankCashAccountId { get; set; }
        public Guid ContactAccountId { get; set; }
        public string DraftBy { get; set; }
        public string ApprovedBy { get; set; }
        public string RejectBy { get; set; }
        public string VoidBy { get; set; }
        public string Reason { get; set; }
        public DateTime? DraftDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? RejectDate { get; set; }
        public DateTime? VoidDate { get; set; }
        public string UserName { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Path { get; set; }
        public string ChequeNumber { get; set; }
        public Guid? TaxRateId { get; set; }
        public Guid? VatAccountId { get; set; }
        public Guid? BillsId { get; set; }
        public string TaxMethod { get; set; }
        public bool IsPurchasePostExpense { get; set; }
        public bool IsView { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public List<PaymentVoucherItemLookUp> PaymentVoucherItems { get; set; }
        public Guid? TemporaryCashIssueId { get; set; }
        public string TransactionId { get; set; }
        public string Prefix { get; set; }
        public Guid? BranchId { get; set; }

        public string DocumentType { get; set; }
        public Guid? DocumentId { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal RemainingBalance { get; set; }
        public List<Guid?> MultipleDocument { get; set; }

    }
}
