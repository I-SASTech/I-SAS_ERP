using System;
using System.Collections.Generic;
using Focus.Business.Categories.Queries.GetCategoryDetails;
using Focus.Business.Products.Model;
using Focus.Business.Sales.Commands.AddSale;
using Focus.Domain.Entities;

namespace Focus.Business.Products.Queries.GetProductList
{
    public class ProductLookUpModel 
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string HsCode { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; } 
        public string MasterProductEn { get; set; }
        public string OriginNameEn { get; set; }
        public string OriginNameAr { get; set; } 
        public string UnitNameEn { get; set; }
        public string UnitNameAr { get; set; }
        public string MasterProductAr { get; set; }
        public string CategoryNameEn { get; set; }
        public string CategoryNameAr { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDetailLookUpModel Category { get; set; }
        public Guid? SubCategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? UnitId { get; set; }
        public Unit Unit { get; set; }
        public Guid? SizeId { get; set; }
        public Guid? ColorId { get; set; }
        public Guid? ProductMasterId { get; set; }
        public string BarCode { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public Guid? TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public string TaxRate { get; set; }
        public decimal? TaxRateValue { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        public Guid? OriginId { get; set; }
        public string StockLevel { get; set; }
        public string SaleReturnDays { get; set; }
        public string Description { get; set; }
        public string Shelf { get; set; }
        public bool IsExpire { get; set; }
        public bool IsActive { get; set; }
        public PromotionOfferModel PromotionOffer { get; set; }
        public BundleCategoryModel BundleCategory { get; set; }
        public virtual Inventory Inventory { get; set; }
        public bool IsRaw { get; set; }
        public string LevelOneUnit { get; set; }
        public string BasicUnit { get; set; }
        public decimal? UnitPerPack { get; set; }
        public string SalePriceUnit { get; set; }
        public string SizeName { get; set; }
        public string SizeNameArabic { get; set; }
        public string ColorName { get; set; }
        public string ColorNameArabic { get; set; }
        public string Image { get; set; }
        public string Assortment { get; set; }
        public string StyleNumber { get; set; }
        public virtual Inventory PurchasePrice { get; set; }
        public decimal PurchasePrices { get; set; }
        public bool Guarantee { get; set; }
        public bool Serial { get; set; }
        public ICollection<InventoryBatchLookUpModel> InventoryBatch { get; set; }
        public bool ServiceItem { get; set; }
        public List<SaleSizeAssortmentModel> SaleSizeAssortment { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public decimal WholesalePrice { get; set; }
        public bool HighUnitPrice { get; set; }
        public decimal? SchemeQuantity { get; set; }
        public decimal? Scheme { get; set; }
        public decimal? WholesaleQuantity { get; set; }
        public Guid? ProductGroupId { get; set; }
        public string DisplayName { get; set; }
        public string BarCodeType { get; set; }
        public string NewBarCode { get; set; }
        public string SubCategoryEnglish { get; set; }
        public string SubCategoryArabic { get; set; }
        public string BrandArabic { get; set; }
        public string BrandEnglish { get; set; }
        public bool IsGenerated { get; set; }
        public string SKU { get; set; }
        public string PartNumber { get; set; }
        public string BarCodeDisplayName { get; set; }
        public decimal CurrentQuantity { get; set; }
    }
}
