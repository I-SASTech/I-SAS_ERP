using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class AdvanceQuantityWiseInventoryItemLookupModel
    {
        public string CompareWith { get; set; }
        public List<AdvanceQuantityWiseInventoryItemLookupModel> CompareWithList { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int TransactionType { get; set; }
        public int Quantity { get; set; }
        public decimal PurchaseQuantity { get; set; }
        public decimal SaleQuantity { get; set; }
        public decimal InventoryQuantity { get; set; }
        public decimal CostOfGoodsSoldQuantity { get; set; }
        public decimal StockOutQuantity { get; set; }
        public string ProductArabicName { get; set; }
        public string AccounCode { get; set; }
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal PurchaseBalance { get; set; }
        public decimal CostOfGoodsSoldTotal { get; set; }
        public decimal SaleTotal { get; set; }
        public decimal InventoryTotal { get; set; }
        public decimal StockOutTotal { get; set; }
        public decimal ClosingBalance { get; set; }
        public DateTime Date { get; set; }
    }
}