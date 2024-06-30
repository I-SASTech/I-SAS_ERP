using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class VariationInventory : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid ProductId { get; set; }
        public Guid? SizeId { get; set; }
        public Guid? ColorId { get; set; }
        public decimal Quantity { get; set; }
        public Guid? BranchId { get; set; }

    }
}
