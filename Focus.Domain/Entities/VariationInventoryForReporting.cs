using System;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class VariationInventoryForReporting : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid DocumentId { get; set; }
        public TransactionType TransactionType { get; set; }
        public Guid ProductId { get; set; }
        public Guid? SizeId { get; set; }
        public Guid? ColorId { get; set; }
        public decimal Quantity { get; set; }
        public Guid? BranchId { get; set; }

    }
}
