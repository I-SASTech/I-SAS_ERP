using Focus.Business.Reports.SaleInvoiceReport;
using System.Collections.Generic;


namespace Focus.Business.Reports.VatSalePurchaseComparisonReport
{
    public class VatSalePurchaseListModel
    {
        public string Month { get; set; }
        public string Date { get; set; }
        public VatSalePurchaseModel VatSalePurchaseModel { get; set; }
    }
}
