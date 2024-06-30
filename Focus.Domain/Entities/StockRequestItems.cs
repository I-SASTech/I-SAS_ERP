using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class StockRequestItems : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity, ISoftDelete
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal RemainingQuantity { get; set; }
        public Guid StockRequestId { get; set; }
        public virtual StockRequest StockRequests { get; set; }
        public Guid? BranchId { get; set; }
    }
}
