using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class ProductLookUpModel
    {

        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CurrentStockValue { get; set; }
        public decimal AveragePrice { get; set; } // (old stock + currentStock)/2
        public decimal Quantity { get; set; }
        public decimal CurrentQuantity { get; set; }
        public string Date { get; set; }
        public string DocumentNumber { get; set; }
        public string TransactionType { get; set; }
        public Guid DocumentId { get; set; }
        public Guid StockId { get; set; }
        public Guid ProductId { get; set; }
        public Guid WareHouseId { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public Guid? PromotionId { get; set; }
        public Guid? BundleId { get; set; }
        public decimal OfferQuantity { get; set; }
        public decimal Get { get; set; }
        public decimal Buy { get; set; }
        public string WareHouseName { get; set; }
        public string WareHouseNameArabic { get; set; }
        public string ProductName { get; set; }
        public string ProductNameAr { get; set; }
    }
}