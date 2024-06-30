using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class Discount : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Type { get; set; }
        public string DiscountName { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public virtual ICollection<SaleInvoiceDiscount> SaleInvoiceDiscounts { get; set; }
    }
}
