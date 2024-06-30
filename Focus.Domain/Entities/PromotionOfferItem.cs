using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class PromotionOfferItem : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public decimal Discount { get; set; }
        public string DiscountType { get; set; }
        public decimal BaseQuantity { get; set; }
        public decimal UpToQuantity { get; set; }
        public bool IncludingBaseQuantity { get; set; }
        public decimal QuantityOut { get; set; }
        public decimal StockLimit { get; set; }
        public decimal Buy { get; set; }
        public decimal Get { get; set; }
        public string PromotionType { get; set; }

        public Guid PromotionOfferId { get; set; }
        public virtual PromotionOffer PromotionOffer { get; set; }
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Guid? GetProductId { get; set; }
        public virtual Product GetProduct { get; set; }
        public virtual ICollection<SaleItem> SaleItems { get; set; }

        public bool IsActive { get; set; }
    }
}
