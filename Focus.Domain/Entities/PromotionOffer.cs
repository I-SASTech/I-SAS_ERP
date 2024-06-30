using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class PromotionOffer : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Offer { get; set; }
        public decimal Discount { get; set; }
        public string DiscountType { get; set; }
        public decimal BaseQuantity { get; set; }
        public decimal UpToQuantity { get; set; }
        public bool IncludingBaseQuantity { get; set; }
        public decimal QuantityOut { get; set; }
        public decimal StockLimit { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromDate { get; set; }
        public bool IsActive { get; set; }

        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public decimal Buy { get; set; }
        public decimal Get { get; set; }

        public string PromotionType { get; set; }
        public Guid? ProductGroupId { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }

        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<SaleItem> SaleItems { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<PromotionOfferItem> PromotionOfferItems { get; set; }
        public virtual ICollection<BundleOfferBranches> BundleOfferBranches { get; set; }
        public Guid? GetProductId { get; set; }
        public virtual Product GetProduct { get; set; }
        public Guid? BranchId { get; set; }
    }
}
