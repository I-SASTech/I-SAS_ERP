using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Focus.Business.JournalVouchers.Queries.NetDrCr;
using Focus.Domain.Entities;
using Focus.Domain.Enum;

namespace Focus.Business.StockAdjustments.Queries.GetStockAdjustmentList
{
    public class StockAdjustmentDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public Guid StockAdjustmentId { get; set; }
        public virtual StockAdjustment StockAdjustments { get; set; }
    }
}
