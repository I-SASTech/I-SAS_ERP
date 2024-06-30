using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class VatSalePurchaseModel
    {
        public Guid? SaleInvoiceId { get; set; }
        public Guid? PurchaseInvoiceId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public DateTime Date { get; set; }
        public decimal VatAmountIn { get; set; }
        public decimal VatAmountOut { get; set; }
        public decimal TotalSales { get; set; }
        public decimal GrossSales { get; set; }
        public decimal GrossPurchase { get; set; }
        public decimal TotalPurchase { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal GrossExpense { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalPurchaseReturn { get; set; }
        public decimal PurchaseReturnVat { get; set; }
        public decimal TotalSaleReturn { get; set; }
        public decimal SaleReturnVat { get; set; }
        public decimal VatPayable { get; set; }
        public decimal OtherVocher { get; set; }
        public decimal OtherVocherVat { get; set; }
    }
}