using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class SaleInvoiceDiscount : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid? DiscountId { get; set; }
        public virtual Discount Discount { get; set; }
        public Guid? SaleId { get; set; }
        public virtual Sale Sale { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
