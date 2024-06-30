using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class SaleOrderItemLookupModel
    {
        public Guid Id { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal HighQty { get; set; }
        public decimal Discount { get; set; }
        public string Code { get; set; }
        public int Number { get; set; }
        public decimal FixDiscount { get; set; }
        public decimal SoQty { get; set; }
        public Guid TaxRateId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid SaleOrderId { get; set; }
        public string BatchNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsExpire { get; set; }
        public string TaxMethod { get; set; }
        public decimal QuantityOut { get; set; }
        public string ProductNameEn { get; set; }
        public string ProductName { get; set; }
        public string ProductNameAr { get; set; }
        public string UnitName { get; set; }
        public string TaxRate { get; set; }
        public decimal Total { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal InclusiveVat { get; set; }
        public decimal IncludingVat { get; set; }
        public decimal? UnitPerPack { get; set; }
        public string Description { get; set; }
        public bool ServiceItem { get; set; }
        public string Serial { get; set; }
        public string GuaranteeDate { get; set; }
        public string ProductImage { get; set; }
        public string DisplayNameForPrint { get; set; }
        public bool Guarantee { get; set; }
        public bool IsSerial { get; set; }
        public bool IsFree { get; set; }
        public string StyleNumber { get; set; }
        public decimal? SchemeQuantity { get; set; }
        public decimal? Scheme { get; set; }
        public decimal? SchemePhysicalQuantity { get; set; }

        public decimal GrossAmounts { get; set; }  
        public decimal VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal AfterDiscountAmount { get; set; }

        public List<BomRawProductsLookupModel> BomRawProducts { get; set; }
        public ProductDropdownLookUpModel Product { get; set; }

    }
}