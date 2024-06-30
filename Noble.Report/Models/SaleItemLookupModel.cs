using System;

namespace Noble.Report.Models
{
    public class SaleItemLookupModel
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? ServiceProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public decimal Total { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? UnitPerPack { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal FixDiscount { get; set; }
        public Guid TaxRateId { get; set; }
        public decimal TaxRate { get; set; }
        public decimal ExclusiveVat { get; set; }
        public Guid SaleId { get; set; }
        public Guid WareHouseId { get; set; }
        public decimal OfferQuantity { get; set; }
        public decimal VatAmount { get; set; }
        public decimal Get { get; set; }
        public decimal Buy { get; set; }
        public string Code { get; set; }
        public string SaleReturnDays { get; set; }
        public Guid? PromotionId { get; set; }
        public Guid? BundleId { get; set; }
        public decimal InclusiveVat { get; set; }
        public string TaxMethod { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal BundleAmount { get; set; }
        public string ArabicName { get; set; }
        public string Description { get; set; }
        public decimal RemainingQuantity { get; set; }
        public decimal ReturnQuantity { get; set; }
        public decimal SoQty { get; set; }
        public string BatchNo { get; set; }
        public DateTime? BatchExpiry { get; set; }
        public int AutoNumber { get; set; }
        public bool ServiceItem { get; set; }
        public bool IsFree { get; set; }
        public bool IsActive { get; set; }
        public string Serial { get; set; }
        public string GuaranteeDate { get; set; }
        public bool IsSerial { get; set; }
        public bool IsGuarantee { get; set; }
        public Guid? SoItemId { get; set; }
        public Guid? ColorId { get; set; }
        public string ColorName { get; set; }
        public string StyleNumber { get; set; }
        public string SerialNo { get; set; }
        public string UnitName { get; set; }
        public string TaxRateName { get; set; }
        public decimal? SchemeQuantity { get; set; }
        public decimal? Scheme { get; set; }
        public decimal? SchemePhysicalQuantity { get; set; }
        public decimal? PromotionOfferQuantity { get; set; }
        public decimal LineTotal { get; set; }
        public decimal AfterDiscountAmount { get; set; }
      
    }

}