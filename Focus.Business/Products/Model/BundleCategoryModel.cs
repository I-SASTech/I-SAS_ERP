using System;

namespace Focus.Business.Products.Model
{
    public class BundleCategoryModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
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
    }
}
