using System;
using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class SaleOrderVersion : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public string DocumentNo { get; set; }
        public string Version { get; set; }
        public Guid? SaleOrderVersionId { get; set; }
        public DateTime Date { get; set; }
        public bool IsQuotation { get; set; }
        public Guid? SaleOrderId { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
        public virtual ICollection<SaleOrderItem> SaleOrderItems { get; set; }
        public Guid? BranchId { get; set; }

    }
}
