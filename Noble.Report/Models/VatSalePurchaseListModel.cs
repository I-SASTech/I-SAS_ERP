using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class VatSalePurchaseListModel
    {
        public string Month { get; set; }
        public string Date { get; set; }
        public VatSalePurchaseModel VatSalePurchaseModel { get; set; }
    }
}