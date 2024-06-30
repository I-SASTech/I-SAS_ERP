using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Reports.ComparisionReport
{
    public class MonthlyComparisionReportModel
    {
        public DateTime Date { get; set; }
        public decimal Cash { get; set; }
        public decimal TotalCash { get; set; }
        public decimal TotalReturns { get; set; }
        public decimal Credit { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal Bank { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal TotalTax{ get; set; }
        public decimal NetSale { get; set; } 
        public DateTime PurchaseDate { get; set; }
        public decimal PurchaseCash { get; set; }
        public decimal PurchaseTotalCash { get; set; }
        public decimal PurchaseTotalReturns { get; set; }
        public decimal PurchaseCredit { get; set; }
        public decimal GrossPurchaseAmount { get; set; }
        public decimal PurchaseBank { get; set; }
        public decimal PurchaseDiscount { get; set; }
        public decimal PurchaseTotal { get; set; }
        public decimal PurchaseTotalTax { get; set; }
        public decimal PurchaseNetSale { get; set; }
        public decimal TotalTaxReturn { get; set; }
        public bool Purchase { get; set; }
        public bool Record { get; set; }
    }
}
