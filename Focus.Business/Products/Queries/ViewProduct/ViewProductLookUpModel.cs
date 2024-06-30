using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.Products.Queries.ViewProduct
{
    public class ViewProductLookUpModel
    {

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string CategoryNameEn { get; set; }
        public string CategoryNameAr { get; set; }
        public string SubCategoryNameEn { get; set; }
        public string SubCategoryNameAr { get; set; }
        public Guid? SubCategoryId { get; set; }
        public string BrandNameEn { get; set; }
        public string BrandNameAr { get; set; }
        public string UnitNameEn { get; set; }
        public string UnitNameAr { get; set; }
        public string SizeNameEn { get; set; }
        public string SizeNameAr { get; set; }
        public string ColorNameEn { get; set; }
        public string ColorNameAr { get; set; }
        public string OriginNameEn { get; set; }
        public string OriginNameAr { get; set; }
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
        public decimal WholesalePrice { get; set; }
        public string BundelOffer { get; set; }
        public decimal BundelBuy { get; set; }
        public decimal BundelGet { get; set; }
        public bool BundelisActive { get; set; }

        public string PromotionsOffer { get; set; }
        public decimal PromotionsDiscountPercentage { get; set; }
        public decimal PromotionsFixedDiscount { get; set; }
        public decimal PromotionsQuantity { get; set; }
        public DateTime? PromotionsToDate { get; set; }
        public DateTime? PromotionsFromDate { get; set; }
        public string Image { get; set; }

        public bool IsRaw { get; set; }
        public string LevelOneUnit { get; set; }
        public string BasicUnit { get; set; }
        public decimal? UnitPerPack { get; set; }
        public string SalePriceUnit { get; set; }
        public string Assortment { get; set; }
        public string StyleNumber { get; set; }
    }
}
