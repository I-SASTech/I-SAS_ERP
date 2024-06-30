namespace Focus.Business.Reports.PurchaseInvoiceSummaryReport.Models
{
    public class PurchaseInvoiceSummaryLookupModel
    {
        public decimal TotalPurchases { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal TotalNetTotal { get; set; }
        public decimal TotalVatAmount { get; set; }
        public decimal TotalTotalAmount { get; set; }
        public string CompareWith { get; set; }
        public string ApexChartValue { get; set; }
    }
}
