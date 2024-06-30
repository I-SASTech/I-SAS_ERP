using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
   public class ContractorItem : BaseEntity, ISoftDelete, ITenant
   {
       public Guid ProductId { get; set; }
       public virtual Product Product { get; set; }
       public int HighQuantity { get; set; }
       public int UnitPerPack { get; set; }
       public string BasicUnit { get; set; }
       public string LevelOneUnit { get; set; }
       public int Quantity { get; set; }
       public decimal Waste { get; set; }
       public Guid BatchProcessContractorId { get; set; }
       public virtual BatchProcessContractor BatchProcessContractor { get; set; }

       public Guid? WarehouseId { get; set; }



    }
}
