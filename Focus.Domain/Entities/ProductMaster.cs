using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
  public  class ProductMaster : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
  {
      public string Name { get; set; }
      public string NameArabic { get; set; }
      public string Description { get; set; }
      public string Code { get; set; }
      public bool isActive { get; set; }
      public virtual ICollection<Product> Products { get; set; }

  }
}
