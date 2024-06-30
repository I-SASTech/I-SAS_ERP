using System;
using System.Collections.Generic;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class DailyExpense : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity, ISoftDelete, IRecordDailyEntry
    {
        public string VoucherNo { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool IsDraft { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public DateTime? OperationDate { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public string OperationPerson { get; set; }
        public bool IsExpenseAccount { get; set; }
        public string Reason { get; set; }
        public string PaymetType { get; set; }
        public Guid? OperationBy { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? BillerAccountId { get; set; }
        public virtual PurchaseBill PurchaseBill { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<DailyExpenseDetail> DailyExpenseDetails { get; set; }
        public virtual ICollection<DailyExpenseAttachment> DailyExpenseAttachments { get; set; }
        public string Name { get; set; }
        public string TaxId { get; set; }
        public string ReferenceNo { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid? BranchId { get; set; }

        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}
