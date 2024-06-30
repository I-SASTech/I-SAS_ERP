namespace Focus.Business.Reports.ProductSummaryReport.Models
{
    public class ProductSummaryLookupModel
    {
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string RegistrationNo { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal TotalPurchases { get; set; }
        public decimal TotalNetTotal { get; set; }
        public decimal TotalVatAmount { get; set; }
        public decimal TotalTotalAmount { get; set; }
        public decimal TotalPurchaseGrossAmount { get; set; }
        public decimal TotalPurchaseDiscountAmount { get; set; }
        public decimal TotalPurchaseNetTotal { get; set; }
        public decimal TotalPurchaseVatAmount { get; set; }
        public decimal TotalPurchaseAmount { get; set; }
        public string CompareWith { get; set; }
        public string ApexChartValue { get; set; }
    }
}
