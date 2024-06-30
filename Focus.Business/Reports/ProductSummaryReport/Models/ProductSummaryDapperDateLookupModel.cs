using Focus.Domain.Entities;
using System;

namespace Focus.Business.Reports.ProductSummaryReport.Models
{
    public class ProductSummaryDapperDateLookupModel
    {
        public DateTime Date { get; set; }
        public Guid Productid { get; set; }
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetTotal { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string CompareWith { get; set; }
        public string ApexChartValue { get; set; }
        public Guid? BranchId { get; set; }
    }
}
