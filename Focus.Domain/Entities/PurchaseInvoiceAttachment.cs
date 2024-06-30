using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class PurchaseInvoiceAttachment : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public string Path { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public Guid? PurchaseInvoiceId { get; set; }
        public virtual Purchase Purchase { get; set; }
        public Guid? PurchaseInvoicePostId { get; set; }
        public virtual PurchasePost PurchasePost { get; set; }
    }
}
