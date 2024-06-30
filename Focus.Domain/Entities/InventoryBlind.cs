using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class InventoryBlind:BaseEntity,ITenant,ITenantFilterableEntity, IAuditedEntityBase
    {
        public Guid WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public string DocumentId { get; set; }
        public string Note { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsCounted { get; set; }
        public virtual ICollection<InventoryBlindDetail> InventoryBlindDetails { get; set; }
    }
}
