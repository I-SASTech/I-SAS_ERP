using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Focus.Business.BundleCategoriesItems.Models;
using Focus.Business.PriceRecords;
using Focus.Domain.Entities;

namespace Focus.Business.Products.Queries.GetProductDetails
{
    public class ProductDetailLookUpModel
    {
        public Guid Id { get; set; }
        public string HsCode { get; set; }
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? SizeId { get; set; }
        public Guid? ColorId { get; set; }
        public Guid? ProductMasterId { get; set; }
        public string Barcode { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public Guid? TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public decimal SalePrice { get; set; }
        public Guid? OriginId { get; set; }
        public string StockLevel { get; set; }
        public string SaleReturnDays { get; set; }
        public string Description { get; set; }
        public string Shelf { get; set; }
        public bool IsExpire { get; set; }
        public bool IsActive { get; set; }
        public string DisplayNameForPrint { get; set; }
        public string BundelOffer { get; set; }
        public decimal BundelBuy { get; set; }
        public decimal BundelGet { get; set; }
        public bool BundelisActive { get; set; }
        public decimal WholesalePrice { get; set; }
        public string PromotionsOffer { get; set; }
        public decimal PromotionsDiscountPercentage { get; set; }
        public decimal PromotionsFixedDiscount { get; set; }
        public decimal PromotionsQuantity { get; set; }
        public DateTime? PromotionsToDate { get; set; }
        public DateTime? PromotionsFromDate { get; set; }
        public string Image { get; set; }
        public ICollection<PriceRecordModelForProduct> PriceRecords { get; set; }
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
        public ICollection<Guid> SizeIdList { get; set; }
        public decimal? WholesaleQuantity { get; set; }
        public decimal? SchemeQuantity { get; set; }
        public decimal? Scheme { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal CostPrice { get; set; }
        public decimal CostValue { get; set; }
        public string CostSign { get; set; }

        public Guid? ProductGroupId { get; set; }
        public List<BundlesOffersBranchesLookupModel> BranchesIdList { get; set; }
        public string DisplayName { get; set; }
        public string SKU { get; set; }
        public string PartNumber { get; set; }
    }
}
