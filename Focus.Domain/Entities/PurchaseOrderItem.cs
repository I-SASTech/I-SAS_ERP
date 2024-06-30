using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
  public  class PurchaseOrderItem : BaseEntity, ISoftDelete, ITenant
    {
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string Description { get; set; }
        public bool IsService { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal RemainingQuantity { get; set; }
        public decimal ReceiveQuantity { get; set; }
        public decimal Discount { get; set; }
        public decimal FixDiscount { get; set; }
        public Guid TaxRateId { get; set; }
        public virtual TaxRate TaxRate { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string BatchNo { get; set; }
        public decimal? HighQty { get; set; }
        public decimal? UnitPerPack { get; set; }
        public string SerialNo { get; set; }
        public DateTime? GuaranteeDate { get; set; }
        public Guid? PurchaseOrderVersionId { get; set; }
        public virtual PurchaseOrderVersion PurchaseOrderVersion { get; set; }
        public Guid? WarrantyTypeId { get; set; }
        public string TaxMethod { get; set; }
        public string UnitName { get; set; }

        public decimal TotalWithOutAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
    