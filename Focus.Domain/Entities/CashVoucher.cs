using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class CashVoucher : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public string VoucherNo { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public Guid? SaleReturnId { get; set; }
        public Guid? SaleInvoiceId { get; set; }

    }
}
