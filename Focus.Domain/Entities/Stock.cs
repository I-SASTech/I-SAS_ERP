using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class Stock:BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete, IRecordDailyEntry
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Guid WareHouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public decimal CurrentQuantity { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal CurrentStockValue { get; set; }
        public Guid? BranchId { get; set; }

    }
}
