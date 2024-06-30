using Focus.Domain.Interface;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class PaymentOption : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<SalePayment> SalePayments { get; set; }
    }
}
