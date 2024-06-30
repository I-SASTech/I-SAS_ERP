using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class StockTransferItems : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity, ISoftDelete
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal TransferQuantity { get; set; }
        public decimal Amount { get; set; }
        public decimal TransferAmount { get; set; }
        public Guid? BranchId { get; set; }
        public Guid StockTransferId { get; set; }
        public decimal RemainingQuantity { get; set; }
        public decimal LineTotal { get; set; }
        public virtual StockTransfer StockTransfer { get; set; }
    }
}
