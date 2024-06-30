using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class StockListLookUpModel
    {
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public List<InventoryLookUpModel> InventoryLook { get; set; }
        public string WareHouseName { get; set; }
        public string WareHouseNameArabic { get; set; }
        public Guid WarehouseId { get; set; }
    }
}