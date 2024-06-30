using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class PurchaseOrderAction : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public Guid ProcessId { get; set; }
        public virtual CompanyProcess CompanyProcess { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public DateTime? Date { get; set; }
        public DateTime CurrentDate { get; set; }
        public string Description { get; set; }
    }
}
