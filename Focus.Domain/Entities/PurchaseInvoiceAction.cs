using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class PurchaseInvoiceAction : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public Guid ProcessId { get; set; }
        public virtual CompanyProcess CompanyProcess { get; set; }
        public Guid? PurchaseInvoiceId { get; set; }
        public virtual Purchase Purchase { get; set; }
        public Guid? PurchaseInvoicePostId { get; set; }
        public virtual PurchasePost PurchasePost { get; set; }
        public DateTime? Date { get; set; }
        public DateTime CurrentDate { get; set; }
        public string Description { get; set; }
    }
}
