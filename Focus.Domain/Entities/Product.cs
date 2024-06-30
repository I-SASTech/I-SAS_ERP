using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
   public class Product : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
      
        public Guid? SubCategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Unit Unit { get; set; }
        public Guid? UnitId { get; set; }
        public virtual Size Size { get; set; }
        public Guid? SizeId { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
        public Guid? ProductMasterId { get; set; }
        public virtual Color Color { get; set; }
        public Guid? ColorId { get; set; }
        public string BarCode { get; set; }
        public string BarCodeType { get; set; }
        public string HsCode { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public virtual TaxRate TaxRate { get; set; }
        public Guid? TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public decimal WholesalePrice { get; set; }
        public decimal SalePrice { get; set; }
        public virtual Origin Origin { get; set; }

        public Guid? OriginId { get; set; }
        public string StockLevel { get; set; }
        public string SaleReturnDays { get; set; }
        public string Description { get; set; }
        public string Shelf { get; set; }
        public bool IsExpire { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }
        public virtual ICollection<PriceRecord> PriceRecords { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
        public virtual ICollection<MobileOrderItem> MobileOrderItems { get; set; }
        public virtual ICollection<PurchasePostItem> PurchasePostItems { get; set; }
        public virtual ICollection<WareHouseTransferItem> WareHouseTransferItems { get; set; }
        public virtual ICollection<SaleItem> SaleItems { get; set; }
        public virtual ICollection<RecipeItem> RecipeItems { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<InventoryBlindDetail> InventoryBlindDetails { get; set; }
        public virtual ICollection<InquiryItem> InquiryItems { get; set; }
        public virtual ICollection<ReparingItem> ReparingItems { get; set; }
        public virtual ICollection<DeliveryChallanItem> DeliveryChallanItems { get; set; }
        public virtual ICollection<CreditNoteItem> CreditNoteItems { get; set; }
        public virtual ICollection<BranchItems> BranchItems { get; set; }
        public virtual ICollection<StockRequestItems> StockRequestItems { get; set; }
        public virtual ICollection<StockReceivedItems> StockReceivedItems { get; set; }
        public bool IsRaw { get; set; }
        public string LevelOneUnit { get; set; }
        public string BasicUnit { get; set; }
        public decimal? UnitPerPack { get; set; }
        public string SalePriceUnit { get; set; }
        public string Assortment { get; set; }
        public string StyleNumber { get; set; }
        public bool Guarantee { get; set; }
        public bool Serial { get; set; }
        public bool ServiceItem { get; set; }
        public bool HighUnitPrice { get; set; }
        public virtual ICollection<RecipeNo> RecipeNos { get; set; }
        public virtual ICollection<ProcessItem> ProcessItems { get; set; }
        public virtual ICollection<SampleRequest> SampleRequests { get; set; }
        public virtual ICollection<StockTransferItems> StockTransferItems { get; set; }
        public virtual ICollection<SampleRequestItem> SampleRequestItems { get; set; }
        public virtual ICollection<ContractorItem> ContractorItems { get; set; }
        public virtual ICollection<ProductSize> ProductSizes { get; set; }
        public virtual ICollection<QuotationTemplateItem> QuotationTemplateItems { get; set; }
        public virtual ICollection<DeliveryChallanReserverdItem> DeliveryChallanReserverdItems { get; set; }
        public virtual ICollection<PromotionOffer> GetPromotionOffers { get; set; }
        public virtual ICollection<PromotionOffer> PromotionOffers { get; set; }
        public virtual ICollection<BundleCategory> BundleCategories { get; set; }
        public virtual ICollection<PromotionOfferItem> PromotionOfferItems { get; set; }
        public virtual ICollection<PromotionOfferItem> GetPromotionOfferItems { get; set; }
        public virtual ICollection<BomSaleOrderItems> BomSaleOrderItems { get; set; }
        public virtual ICollection<BomRawProducts> BomRawProducts { get; set; }

        public virtual PromotionOffer PromotionOffer { get; set; }
        public Guid? PromotionOfferId { get; set; }
        public virtual BundleCategory BundleCategory { get; set; }
        public Guid? BundleCategoryId { get; set; }

        public Guid? CogsAccountId { get; set; }
        public Guid? InventoryAccountId { get; set; }
        public Guid? SaleAccountId { get; set; }
        public decimal? WholesaleQuantity { get; set; }
        public decimal? SchemeQuantity { get; set; }
        public decimal? Scheme { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? CostValue { get; set; }
        public string CostSign { get; set; }
        public Guid? ProductGroupId { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }
        public string DisplayName { get; set; }
        public string DisplayNameForPrint { get; set; }
        public string SKU { get; set; }
        public string PartNumber { get; set; }
        public string BarCodeDisplayName { get; set; }
    }
}
