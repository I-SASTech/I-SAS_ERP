using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class AutoPurchaseTemplateItem : BaseEntity, ISoftDelete, ITenant
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal FixDiscount { get; set; }
        public Guid TaxRateId { get; set; }
        public virtual TaxRate TaxRate { get; set; }
        public Guid AutoPurchaseTemplateId { get; set; }
        public virtual AutoPurchaseTemplate AutoPurchaseTemplate { get; set; }
        public decimal? HighQty { get; set; }
        public decimal? UnitPerPack { get; set; }
    }
}
