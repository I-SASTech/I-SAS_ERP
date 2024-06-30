using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class barCodeData
    {
        public string type { get; set; }
        public string code { get; set; }
        public int value { get; set; }
        public string limit { get; set; }
        public string limitPerLine { get; set; }
        public string itemEngName { get; set; }
        public string itemArName { get; set; }
        public string companyName { get; set; }
        public decimal price { get; set; }
        public string show { get; set; }
        public string showPrice { get; set; }
        public string showProdName { get; set; }
        public string currency { get; set; }
        public string printerName { get; set; }
        public decimal vatPrice { get; set; }
        public decimal salePrice { get; set; }
    }
}