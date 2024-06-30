using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class PurchaseItem: BaseEntity, ISoftDelete, ITenant
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal FixDiscount { get; set; }
        public Guid TaxRateId { get; set; }
        public virtual TaxRate TaxRate { get; set; }
        public Guid PurchaseId { get; set; }
        public Guid WareHouseId { get; set; }
        public virtual Warehouse WareHouse { get; set; }
        public virtual Purchase Purchase { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string BatchNo { get; set; }
        public decimal? HighQty { get; set; }
        public decimal? UnitPerPack { get; set; }
        public string SerialNo { get; set; }
        public DateTime? GuaranteeDate { get; set; }
        public int AutoNumber { get; set; }
    }
}
