using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class PaymentVoucherDetail : BaseEntity, ISoftDelete, ITenant
    {
        public Guid ContactAccountId { get; set; }
        public virtual Account Account { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public virtual Guid? PurchaseInvoice { get; set; }
        public virtual Guid? SaleInvoice { get; set; }
        public string ChequeNumber { get; set; }
        public Guid PaymentVoucherId { get; set; }
        public virtual PaymentVoucher PaymentVouchers { get; set; }
    }
}
