using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Dashboard.Queries.GetWareHouseInventory
{
   public class ProductInventoryLookUpModel
    {
        public Guid CategoryId { get; set; }
        public Guid ProductId { get; set; }
        public Guid WareHouseId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameArabic { get; set; }
        public string ProductArabicName { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseNameArabic { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal CurrentQuantity { get; set; }
        public decimal CurrentStockValue { get; set; }
        public decimal SalePrice { get; set; }
        public string DisplayName { get; set; }
    }
}
