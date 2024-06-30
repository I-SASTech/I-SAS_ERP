using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class AdvanceInventoryItemLookupMdoel
    {
        public string CompareWith { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int TransactionType { get; set; }
        public string ProductArabicName { get; set; }
        public string AccounCode { get; set; }
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public decimal OpeningPurchase { get; set; }
        public decimal OpeningCOGS { get; set; }
        public decimal OpeningSale { get; set; }
        public decimal OpeningInventory { get; set; }

        public decimal CurrentPurchase { get; set; }
        public decimal CurrentCOGS { get; set; }
        public decimal CurrentSale { get; set; }
        public decimal CurrentInventory { get; set; }

        public decimal ClosingPurchase { get; set; }
        public decimal ClosingCOGS { get; set; }
        public decimal ClosingSale { get; set; }
        public decimal ClosingInventory { get; set; }
    }
}