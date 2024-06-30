using Noble.Report.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class PaymentRefundModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string VoucherNumber { get; set; }
        public string Narration { get; set; }
        public string ChequeNumber { get; set; }
        public string NatureOfPayment { get; set; }
        public decimal Amount { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public virtual Guid? PurchaseInvoice { get; set; }
        public virtual Guid? SaleInvoice { get; set; }
        public Guid? PaymentVoucherId { get; set; }
        public Guid BankCashAccountId { get; set; }
        public Guid ContactAccountId { get; set; }
        public string TaxMethod { get; set; }
        public Guid? TaxRateId { get; set; }
        public string BankName { get; set; }
        public string ContactName { get; set; }
    }
}