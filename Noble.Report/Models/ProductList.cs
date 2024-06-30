using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class ProductList
    {
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string ProductNameAr { get; set; }
        public List<ProductLookUpModel> ProductInventoryReport { get; set; }
    }
}