using System;
using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class PurchaseOrderVersion : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public string Code { get; set; }
        public string Version { get; set; }
        public Guid? PurchaseOrderVersionId { get; set; }
        public DateTime Date { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}
