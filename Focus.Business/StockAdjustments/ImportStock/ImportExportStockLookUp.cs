using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.StockAdjustments.ImportStock
{
    public class ImportExportStockLookUp
    {
        public string ProductCode { get; set; }
        public string ProductNameEnglish { get; set; }
        public string ProductNameArabic { get; set; }
        public string DisplayName { get; set; }
        public string Quantity { get; set; }
        public string UnitPrice { get; set; }
        public string ErrorDescription { get; set; }
        public bool IsEqualStock { get; set; }

    }
}
