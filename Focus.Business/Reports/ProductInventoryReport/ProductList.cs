using System.Collections.Generic;

namespace Focus.Business.Reports.ProductInventoryReport
{
   public class ProductList
    {
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string ProductNameAr { get; set; }
        public List<ProductLookUpModel> ProductInventoryReport { get; set; }
    }
}
