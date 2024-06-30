using System;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;

namespace Focus.Business.StockAdjustments.Queries.GetStockAdjustmentDetails
{
    public class StockAdjustmentLookDetailUpModel
    {
        public decimal SalePrice;

        public Guid ProductId { get; set; }
        public ProductDropdownLookUpModel Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public Guid StockAdjustmentId { get; set; }
        public virtual StockAdjustment StockAdjustments { get; set; }
        public Guid Id { get; set; }
        public string GuaranteeDate { get; set; }
        public string Serial { get; set; }
        public string SerialNo { get; set; }
        public Guid? WarrantyTypeId { get; set; }
        public string BatchNo { get; set; }
        public DateTime? BatchExpiry { get; set; }
    }
}
