using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
  public  class SampleRequestItem : BaseEntity, ISoftDelete, ITenant, IAuditedEntityBase, ITenantFilterableEntity
  {
      public Guid ProductId { get; set; }
      public virtual Product Product { get; set; }
      public string EnglishName { get; set; }
      public string ArabicName { get; set; }
      public string Description { get; set; }
      public int HighQuantity { get; set; }
      public int UnitPerPack { get; set; }

      public int Quantity { get; set; }
      public Guid SampleRequestId { get; set; }
      public virtual SampleRequest SampleRequests { get; set; }
      public Guid? BranchId { get; set; }

    }
}
