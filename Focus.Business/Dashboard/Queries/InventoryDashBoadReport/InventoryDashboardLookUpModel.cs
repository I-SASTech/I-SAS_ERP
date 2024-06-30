

using Focus.Business.Dashboard.Queries.GetWareHouseInventory;
using System.Collections.Generic;

namespace Focus.Business.Dashboard.Queries.InventoryDashBoadReport
{
  public  class InventoryDashboardLookUpModel
    {
        public List<TrendingProductLookUpModel> TrendingProducts { get; set; }
        public List<ProductInventoryLookUpModel> WrostInventoires { get; set; }
        public List<TrendingProductLookUpModel> TrendingCategoryProducts { get; set; }
        public List<ProductInventoryLookUpModel> WrostCategoryInventoires { get; set; }

    }
}
