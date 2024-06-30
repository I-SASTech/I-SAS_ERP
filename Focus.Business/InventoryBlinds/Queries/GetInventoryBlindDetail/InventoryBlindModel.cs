using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.InventoryBlinds.Queries.GetInventoryBlindDetail
{
    public class InventoryBlindModel
    {
        public Guid Id { get; set; }
        public Guid WarehouseId { get; set; }
        public string DocumentId { get; set; }
        public string Note { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsCounted { get; set; }
        public IList<InventoryBlindDetailModel> InventoryBlindDetailModels { get; set; }
    }
}
