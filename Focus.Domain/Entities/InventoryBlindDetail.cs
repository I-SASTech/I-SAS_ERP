using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class InventoryBlindDetail:BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int CurrentQuantity { get; set; }
        public int PhysicalQuantity { get; set; }
        public string Remarks { get; set; }
        public Guid InventoryBlindId { get; set; }
        public virtual InventoryBlind  InventoryBlind { get; set; }
    }
}
