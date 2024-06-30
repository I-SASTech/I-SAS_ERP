using System;
using System.Collections.Generic;
using Focus.Business.Attachments.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;

namespace Focus.Business.Reports.TemporaryCashAllocation
{
   public class TemporaryCashAllocationReportLookUp
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string VoucherNumber { get; set; }
        public string Narration { get; set; }
        public string ChequeNumber { get; set; }
        public decimal Amount { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public PaymentVoucherType PaymentVoucherType { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid BankCashAccountId { get; set; }
        public virtual Account Account { get; set; }
        public Guid UserEmployeeId { get; set; }
        public string UserName { get; set; }
        public string UserNameAr { get; set; }
        public string DraftBy { get; set; }
        public string ApprovedBy { get; set; }
        public string RejectBy { get; set; }
        public string VoidBy { get; set; }
        public string Reason { get; set; }
        public DateTime? DraftDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? RejectDate { get; set; }
        public DateTime? VoidDate { get; set; }
        public Guid? PeriodId { get; set; }
        public string TaxMethod { get; set; }
        public string BankCashAccountName { get; internal set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
    }
}
