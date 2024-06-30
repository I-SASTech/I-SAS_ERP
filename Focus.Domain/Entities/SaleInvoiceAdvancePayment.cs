using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class SaleInvoiceAdvancePayment : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public Guid? SaleInvoiceId { get; set; }
        public virtual Sale Sale { get; set; }
        public Guid? SaleOrderId { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
        public Guid? BranchId { get; set; }

    }
}
