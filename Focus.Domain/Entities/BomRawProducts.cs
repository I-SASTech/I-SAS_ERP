using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class BomRawProducts : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {

        public Guid SaleOrderId { get; set; }        
        public decimal Quantity { get; set; }
        public decimal CurrentQuantity { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Products { get; set; }
        public Guid BomSaleOrderItemId { get; set; }
        public virtual BomSaleOrderItems BomSaleOrderItems { get; set; }
    }
}
