namespace Focus.Business.Reports.SaleInvoiceSummaryReport.Models
{
    public class SaleInvoiceSummaryLookupModel
    {
        public decimal TotalSales { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal TotalNetTotal { get; set; }
        public decimal TotalVatAmount { get; set; }
        public decimal TotalTotalAmount { get; set; }
        public string CompareWith { get; set; }
        public string ApexChartValue { get; set; }
    }
}
