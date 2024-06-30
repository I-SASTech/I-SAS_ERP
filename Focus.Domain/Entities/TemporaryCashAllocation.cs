using System;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class TemporaryCashAllocation : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity, ISoftDelete, IRecordDailyEntry
    {
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
        public Guid UserAccountId { get; set; }
        public Guid UserEmployeeId { get; set; }
        public string UserName { get; set; }
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
        public Guid? BranchId { get; set; }

        public string Year { get; set; }
    }
}
