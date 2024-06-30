using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.PriceRecords
{
   public class PriceRecordProductList
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal CostPrice { get; set; }
        public List<PriceRecordLookupModel> PriceRecordLookupModel { get; set; }
    }
}
