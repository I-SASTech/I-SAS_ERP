using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Focus.Business.Attachments.Commands;
using Focus.Business.JournalVouchers.Queries.NetDrCr;
using Focus.Business.PaymentRefunds.Models;
using Focus.Business.PaymentVouchers.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;

namespace Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherList
{
    public class PaymentVoucherLookUpModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Dates { get; set; }
        public string NatureOfPayment { get; set; }
        public string VoucherNumber { get; set; }
        public string Narration { get; set; }
        public string ChequeNumber { get; set; }
        public string BillsName { get; set; }
        public decimal Amount { get; set; }
        public string QRCode { get; set; }
        public decimal Vatamount { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public PaymentVoucherType PaymentVoucherType { get; set; }

        public PettyCash? PettyCash { get; set; }
        public virtual Guid? PurchaseInvoice { get; set; }
        public virtual Guid? BillsId { get; set; }
        public string PurchaseInvoiceCode { get; set; }
        public virtual Guid? SaleInvoice { get; set; }
        public string SaleInvoiceCode { get; set; }
        public Guid BankCashAccountId { get; set; }
        public string BankCashAccountName { get; set; }
        public Guid ContactAccountId { get; set; }
        public string ContactAccountName { get; set; }
        public string ContactAccountNameAr { get; set; }
        public string DraftBy { get; set; }
        public string ApprovedBy { get; set; }
        public string RejectBy { get; set; }
        public string VoidBy { get; set; }
        public string Reason { get; set; }
        public DateTime? DraftDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? RejectDate { get; set; }
        public DateTime? VoidDate { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string PaymentMethods { get; set; }
        public string PaymentModes { get; set; }
        public string Image { get; set; }
        public virtual ICollection<PaymentVoucherAttachment> PaymentVoucherAttachments { get; set; }
        public string TaxMethod { get; set; }
        public string TransactionId { get; set; }
        public string Prefix { get; set; }
        public string VatName { get; set; }
        public decimal? Rate { get;  set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public List<PaymentVoucherItemLookUp> PaymentVoucherItems { get; set; }
        public string BranchCode { get; set; }
        public bool IsRefund { get; set; }

        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public Guid? DocumentId { get; set; }

        public List<PaymentRefundModel> PaymentRefunds { get; set; }

        public decimal InvoiceAmount { get; set; }
        public decimal RemainingBalance { get; set; }
        public bool IsSaleInvoice { get; set; }
        public bool IsPurchaseInvoice { get; set; }
        public string FormName { get; set; }
    }
}