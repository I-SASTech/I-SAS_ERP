using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class SalePayment : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete, IRecordDailyEntry
    {
        public Decimal DueAmount { get; set; }
        public Decimal Received { get; set; }
        public Decimal Balance { get; set; }
        public Decimal Change { get; set; }
        public SalePaymentType PaymentType { get; set; } // Bank, Cash
        public Guid? PaymentOptionId { get; set; }
        public Guid? BankCashAccountId { get; set; }
        public virtual PaymentOption PaymentOption { get; set; }
        public Guid SaleId { get; set; }
        public virtual Sale Sale { get; set; }
        public Decimal Amount { get; set; }
        public Decimal Rate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? BranchId { get; set; }

    }
}
