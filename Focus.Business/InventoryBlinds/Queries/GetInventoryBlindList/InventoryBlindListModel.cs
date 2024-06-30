using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.InventoryBlinds.Queries.GetInventoryBlindList
{
    public class InventoryBlindListModel
    {
        public Guid Id { get; set; }
        public string DateTime { get; set; }
        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string DocumentId { get; set; }
        public string WarehouseNameArabic { get; set; }
        public Guid ProductId { get; set; }
        public bool IsCounted { get; set; }
    }
}
