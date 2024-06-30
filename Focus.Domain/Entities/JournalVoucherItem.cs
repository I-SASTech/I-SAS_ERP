using Focus.Domain.Interface;
using System;
using Focus.Domain.Enum;

namespace Focus.Domain.Entities
{
    public class JournalVoucherItem : BaseEntity, ISoftDelete, ITenant
    {
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        public string Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string ChequeNo { get; set; }
        public Guid? ContactId { get; set; }
        public virtual Contact Contact { get; set; }
        public string ChequeNumber { get; set; }
        public Guid JournalVoucherId { get; set; }
        public virtual JournalVoucher JournalVoucher { get; set; }
    }
}
