﻿using System;

namespace Focus.Business.Products.Model
{
    public class PromotionOfferModel
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public string Offer { get; set; }
        public decimal Discount { get; set; }
        public string DiscountType { get; set; }
        public decimal BaseQuantity { get; set; }
        public decimal UpToQuantity { get; set; }
        public bool IncludingBaseQuantity { get; set; }
        public decimal StockLimit { get; set; }
        public decimal QuantityOut { get; set; }
        public decimal Buy { get; set; }
        public decimal Get { get; set; }
        public string PromotionType { get; set; }
        public Guid? ProductGroupId { get; set; }
        public Guid? GetProductId { get; set; }
    }
}
