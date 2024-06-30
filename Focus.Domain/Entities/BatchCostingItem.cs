using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class BatchCostingItem : BaseEntity, ISoftDelete, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public Guid BatchCostingId { get; set; }
        public virtual BatchCosting BatchCosting { get; set; }
    }
}
