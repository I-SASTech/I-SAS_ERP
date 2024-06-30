using Focus.Domain.Interface;
using System;
using Focus.Domain.Enum;

namespace Focus.Domain.Entities
{
    public class BranchVoucher : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public DateTime Date { get; set; }
        public string VoucherNumber { get; set; }
        public string Narration { get; set; }
        public string ChequeNumber { get; set; }
        public string NatureOfPayment { get; set; }
        public decimal Amount { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid BankCashAccountId { get; set; }
        public virtual Account Account { get; set; }

        public Guid ContactAccountId { get; set; }
        public virtual Account ContactAccount { get; set; }
        public string TaxMethod { get; set; }
        public Guid? TaxRateId { get; set; }
        public Guid? BranchId { get; set; }
        public virtual TaxRate TaxRate { get; set; }
    }
}
