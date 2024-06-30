using System;
using System.Collections.Generic;
using Focus.Business.Attachments.Commands;
using Focus.Domain.Enum;

namespace Focus.Business.DailyExpenses
{
    public class DailyExpenseVm
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string VoucherNo { get; set; }
        public string Description { get; set; }
        public bool IsDraft { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
       
        public string Reason { get; set; }
        public string ReferenceNo { get; set; }
        public string Name { get; set; }
        public string TaxId { get; set; }
        public string PaymentType { get; set; }
        public bool IsExpenseAccount { get; set; }
        public bool IsDayStart { get; set; }
        public Guid CounterId { get; set; }
        public Guid? AccountId { get; set; }
        public string AccountName { get; set; }
        public Guid? TemporaryCashIssueId { get; set; }
        public Guid? BillerAccountId { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public ICollection<DailyExpenseDetailVm> DailyExpenseDetails { get; set; }
        public ICollection<DailyExpenseAttachmentLookUpModel> ExpenseAttachment { get; set; }
        public string NameArabic { get;  set; }
        public bool AllowPreviousFinancialPeriod { get;  set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid? BranchId { get; set; }
        public DateTime? ExpenseDate { get; set; }
    }
}
