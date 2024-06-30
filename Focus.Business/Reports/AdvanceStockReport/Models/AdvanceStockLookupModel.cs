using Focus.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Focus.Business.Reports.AdvanceStockReport.Models
{
    public class AdvanceStockLookupModel
    {
        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductNameArabic { get; set; }
        public decimal? UnitPerPack { get; set; }
        public Guid InventoryProductId { get; set; }
        public int Quantity { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal Opening { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal QuantityIn { get; set; }
        public decimal QuantityOut { get; set; }
        public decimal Balance { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime Date { get; set; }
        public string CompareWith { get; set; }

        public List<AdvanceStockLookupModel> CompareWithList { get; set; }

    }
}
