using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class CompanyProcess : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string ProcessNameArabic { get; set; }
        public string ProcessName { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<PurchaseOrderAction> PurchaseOrderActions { get; set; }
        public virtual ICollection<PurchaseInvoiceAction> PurchaseInvoiceActions { get; set; }
    }
}
