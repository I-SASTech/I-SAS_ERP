using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
   public class PriceLabeling : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
   {
       public string Name { get; set; }
       public string NameArabic { get; set; }
       public decimal Price { get; set; }
       public string Description { get; set; }
       public string Code { get; set; }
       public bool IsActive { get; set; }
       public virtual ICollection<PriceRecord> PriceRecords { get; set; }


    }
}
