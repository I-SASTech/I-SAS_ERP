using System;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class SaleOrderPayment : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public DateTime Date { get; set; }
        public string VoucherNumber { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string ChequeNumber { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public Guid? SaleOrderId { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
        public Guid? PaymentVoucherId { get; set; }
        public virtual PaymentVoucher PaymentVoucher { get; set; }
        public Guid? BranchId { get; set; }

    }
}
