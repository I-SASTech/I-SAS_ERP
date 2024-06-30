using Focus.Domain.Interface;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class CreditCustomer : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Mobile { get; set; }
        public string Code { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
