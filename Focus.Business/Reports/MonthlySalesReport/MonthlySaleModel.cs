using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Reports.MonthlySalesReport
{
    public class MonthlySaleModel
    {
        public string Date { get; set; }
        public decimal Cash { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TotalReturns { get; set; }
        public decimal Credit { get; set; }
        public decimal Bank { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal TotalTax{ get; set; }
        public decimal TotalTaxReturn{ get; set; }
        public decimal NetSale { get; set; }
        public string TaxMethod { get; set; }
    }
}
