using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Focus.Business.JournalVouchers.Queries.NetDrCr;
using Focus.Domain.Entities;
using Focus.Domain.Enum;

namespace Focus.Business.Dashboard.Queries.GetInventoryList
{
    public class InventoryLookUpModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public Guid CustomerId { get; set; }
        public string SupplierName { get; set; }
        public Guid SupplierId { get; set; }
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public string WareHouseName { get; set; }
        public decimal QuantityIn { get; set; }
        public decimal Opening { get; set; }
        public decimal Balance { get; set; }
        public decimal QuantityOut { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountFixed { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal AvgPurchasePrice { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal AvgSalePrice { get; set; }
        public string TransactionType { get; set; }
        public Guid DocumentId { get; set; }
        public string ProductNameArabic { get; set; }
        public string WareHouseNameArabic { get; set; }
        public decimal? UnitPerPack { get; set; }
    }
}
