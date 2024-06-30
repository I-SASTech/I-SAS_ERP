using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Dashboard.Queries.InventoryDashBoadReport
{
  public class TrendingProductLookUpModel
    {
       
            public decimal JanSale { get; set; }
            public decimal FebSale { get; set; }
            public decimal MarSale { get; set; }
            public decimal AprSale { get; set; }
            public decimal MaySale { get; set; }
            public decimal JunSale { get; set; }
            public decimal JulSale { get; set; }
            public decimal AugSale { get; set; }
            public decimal SepSale { get; set; }
            public decimal OctSale { get; set; }
            public decimal NovSale { get; set; }
            public decimal DecSale { get; set; }
            public string Name { get; set; }
            public string CategoryName { get; set; }
            public string CategoryNameAr { get; set; }
            public string Date { get; set; }
        
    }
}
