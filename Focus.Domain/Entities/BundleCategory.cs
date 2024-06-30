using System;
using Focus.Domain.Interface;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class BundleCategory : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Offer { get; set; }
        public string NameArabic { get; set; }
        public decimal Buy { get; set; }
        public decimal Get { get; set; }
        public bool IsActive { get; set; }
        public int QuantityLimit { get; set; }
        public decimal QuantityOut { get; set; }
        public decimal StockLimit { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromDate { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<SaleItem> SaleItems { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public Guid? BranchId { get; set; }
        public virtual ICollection<BundleOfferBranches> BundleOfferBranches { get; set; }
    }
}
