using Focus.Business.BundleCategoriesItems.Models;
using System;
using System.Collections.Generic;

namespace Noble.Api.Models
{
    public class PromotionOfferVm
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? GetProductId { get; set; }
        public string Offer { get; set; }
        public decimal Discount { get; set; }
        public string DiscountType { get; set; }
        public decimal BaseQuantity { get; set; }
        public decimal UpToQuantity { get; set; }
        public decimal StockLimit { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromDate { get; set; }
        public bool IsActive { get; set; }
        public bool IncludingBaseQuantity { get; set; }

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
        public Guid? BranchId { get; set; }

        public List<BundlesOffersBranchesLookupModel> BranchesIdList { get; set; }
    }
}
