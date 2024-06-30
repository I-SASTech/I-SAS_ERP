using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
   public class AllowanceType : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
   {
       public string Name { get; set; }
       public string NameArabic { get; set; }
       public string Description { get; set; }
       public bool IsActive { get; set; }
       public virtual ICollection<Allowance> Allowances { get; set; }

    }
}
