using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.PromotionOffersFolder.Queries.GetPromotionOffersList
{
    public class PromotionOffersDetailListModel
    {
        public Guid Id { get; set; }
        public string Offer { get; set; }
        public string ProductName { get; set; }
        public string Discount { get; set; }
        public decimal BaseQuantity { get; set; }
        public decimal Quantity { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }
        public string DiscountType { get; set; }
        public bool IsActive { get; set; }
        public bool IncludingBaseQuantity { get; set; }
        public decimal StockLimit { get; set; }
        public string GroupName { get; set; }
        public decimal QuantityOut { get; set; }
    }
}
