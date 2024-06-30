using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Reports.PurchaseInvoiceReport
{
    public class PurchaseInvoiceModel
    {
        public string InvoiceNo { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public DateTime Date { get; set; }
        public double Discount { get; set; }
        public double AmountwithDiscount { get; set; }
        public double VATamount { get; set; }
        public double TotalAmount { get; set; }
    }
}
