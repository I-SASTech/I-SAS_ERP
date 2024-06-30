using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class AccountType :BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
      
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<CostCenter> CostCenters { get; set; }

    }
}
