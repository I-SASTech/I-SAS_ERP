using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
   public class ProductionBatchItem : BaseEntity, ISoftDelete, ITenant
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int HighQuantity { get; set; }
        public int UnitPerPack { get; set; }
        public string BasicUnit { get; set; }
        public string LevelOneUnit { get; set; }
        public int Quantity { get; set; }
        public int CurrentQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public decimal Waste { get; set; }
        public Guid ProductionBatchId { get; set; }
        public virtual ProductionBatch ProductionBatch { get; set; }

        public Guid? WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }


    }
}
