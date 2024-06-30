using System;
using Focus.Domain.Entities;

namespace Focus.Business.StockAdjustments.Commands.AddUpdateStockAdjustment
{
    public class StockAdjustmentDetailLookUpModel
    {
        public Guid Id;
        public decimal SalePrice;
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public Guid StockAdjustmentId { get; set; }
        public virtual StockAdjustment StockAdjustments { get; set; }
        public string Serial { get; set; }

        public bool IsSerial { get; set; }
        public string GuaranteeDate { get; set; }
        public string SerialNo { get; set; }
        public Guid? WarrantyTypeId { get; set; }
        public virtual WarrantyType WarrantyType { get; set; }
        public DateTime? BatchExpiry { get; set; }
        public string BatchNo { get; set; }
    }
}
