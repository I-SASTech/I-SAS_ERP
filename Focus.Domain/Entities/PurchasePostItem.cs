using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class PurchasePostItem : BaseEntity, ISoftDelete, ITenant
    {
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string Description { get; set; }
        public bool IsService { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal FixDiscount { get; set; }
        public Guid TaxRateId { get; set; }
        public virtual TaxRate TaxRate { get; set; }
        public Guid WareHouseId { get; set; }
        public virtual Warehouse WareHouse { get; set; }
        public Guid PurchasePostId { get; set; }
        public virtual PurchasePost PurchasePost { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string BatchNo { get; set; }
        public decimal? HighQty { get; set; }
        public decimal? UnitPerPack { get; set; }
        public string SerialNo { get; set; }
        public DateTime? GuaranteeDate { get; set; }
        public decimal RemainingQuantity { get; set; }
        public Guid? WarrantyTypeId { get; set; }
        public virtual WarrantyType WarrantyType { get; set; }
        public Guid? ColorId { get; set; }
        public virtual Color Color { get; set; }
        public virtual ICollection<SaleSizeAssortment> SaleSizeAssortments { get; set; }
        public string TaxMethod { get; set; }
        public string UnitName { get; set; }

        public decimal TotalWithOutAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
