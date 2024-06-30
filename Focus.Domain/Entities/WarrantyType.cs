using System;
using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class WarrantyType : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<PurchasePostItem> PurchasePostItems { get; set; }
        public virtual ICollection<StockAdjustmentDetail> StockAdjustmentDetails { get; set; }
        public Guid? BranchId { get; set; }

    }
}
