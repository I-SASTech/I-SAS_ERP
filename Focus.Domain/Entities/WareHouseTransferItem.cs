using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
   public class WareHouseTransferItem : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity, ISoftDelete
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal Quantity { get; set; }
        public Guid WareHouseTransferId { get; set; }
        public virtual WareHouseTransfer WareHouseTransfer { get; set; }
        public Guid? BranchId { get; set; }

    }
}
