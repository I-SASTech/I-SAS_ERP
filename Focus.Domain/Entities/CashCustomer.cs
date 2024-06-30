using Focus.Domain.Interface;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class CashCustomer : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
