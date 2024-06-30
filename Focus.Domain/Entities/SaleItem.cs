using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class SaleItem : BaseEntity, ISoftDelete, IRecordDailyEntry, ITenant
    {
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal FixDiscount { get; set; }
        public Guid TaxRateId { get; set; }
        public virtual TaxRate TaxRate { get; set; }
        public Guid WareHouseId { get; set; }
        public virtual Warehouse WareHouse { get; set; }
        public decimal Get { get; set; }
        public decimal Buy { get; set; }
        public decimal OfferQuantity { get; set; }
        [ForeignKey("SaleId")]
        public Guid SaleId { get; set; }
        public virtual Sale Sale { get; set; }

        public Guid? PromotionId { get; set; }
        public virtual PromotionOffer Promotion { get; set; }
        public Guid? PromotionItemId { get; set; }
        public virtual PromotionOfferItem PromotionOfferItem { get; set; }
        public Guid? BundleId { get; set; }
        public virtual BundleCategory Bundle { get; set; }

        public decimal RemainingQuantity { get; set; }
        public string BatchNo { get; set; }
        public DateTime? BatchExpiry { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string TaxMethod { get; set; }
        public int AutoNumber { get; set; }
        public bool ServiceItem { get; set; }
        public bool IsFree { get; set; }
        public string Description { get; set; }
        public string Serial { get; set; }
        public string UnitName { get; set; }
        public string SerialNo { get; set; }
        public string GuaranteeDate { get; set; }
        public Guid? SoItemId { get; set; }
        public Guid? ColorId { get; set; }
        public virtual Color Color { get; set; }
        public string StyleNumber { get; set; }
        public decimal? SchemeQuantity { get; set; }
        public decimal? Scheme { get; set; }
        public decimal? SchemePhysicalQuantity { get; set; }
        public virtual ICollection<SaleSizeAssortment> SaleSizeAssortments { get; set; }
        public decimal? PromotionOfferQuantity { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsFreeWithBundle { get; set; }
        public decimal TotalWithOutAmount { get; set; }

    }
}
