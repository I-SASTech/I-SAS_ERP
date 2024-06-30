using Focus.Domain.Interface;
using System;
namespace Focus.Domain.Entities
{
   public class SaleOrderItem : BaseEntity, ISoftDelete, ITenant
    {
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal QuantityOut { get; set; }
        public decimal Discount { get; set; }
        public decimal FixDiscount { get; set; }
        public string TaxMethod { get; set; }
        public Guid TaxRateId { get; set; }
        public virtual TaxRate TaxRate { get; set; }
        public Guid SaleOrderId { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string BatchNo { get; set; }
        public Guid? WareHouseId { get; set; }
        public virtual Warehouse WareHouse { get; set; }
        public DateTime? BatchExpiry { get; set; }
        public string Description { get; set; }
        public bool ServiceItem { get; set; }
        public string GuaranteeDate { get; set; }
        public string Serial { get; set; }
        public bool IsFree { get; set; }
        public Guid? SaleOrderVersionId { get; set; }
        public virtual SaleOrderVersion SaleOrderVersion { get; set; }
        public string StyleNumber { get; set; }
        public decimal? SchemeQuantity { get; set; }
        public decimal? Scheme { get; set; }
        public decimal? SchemePhysicalQuantity { get; set; }
        public string UnitName { get; set; }

        public decimal TotalWithOutAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsUsedForBom { get; set; }
    }
}
