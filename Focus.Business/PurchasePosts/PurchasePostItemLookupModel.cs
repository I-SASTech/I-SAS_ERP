using Focus.Business.Sales.Queries.GetSaleDetail;
using System;
using System.Collections.Generic;
using Focus.Business.Sales.Commands.AddSale;

namespace Focus.Business.PurchasePosts
{
    public class PurchasePostItemLookupModel
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public string ProductName { get; set; }
        public string Code { get; set; }
        public string ProductNameArabic { get; set; }
        public string CategoryName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal QuantityforCanvas { get; set; }
        public decimal UnitExpense { get; set; }
        public decimal ReceiveQuantity { get; set; }
        public decimal Discount { get; set; }
        public decimal FixDiscount { get; set; }
        public decimal Total { get; set; }
        public decimal IncludingVat { get; set; }
        public decimal NetTotal { get; set; }
        public decimal AfterDiscountAmount { get; set; }
        public Guid TaxRateId { get; set; }
        public decimal TaxRate { get; set; }
        public Guid PurchaseId { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public ProductDropdownLookUpModel Product { get; set; }
        public string BatchNo { get; set; }
        public decimal InclusiveVat { get; set; }
        public string TaxMethod { get; set; }
        public decimal? HighQty { get; set; }
        public decimal? UnitPerPack { get; set; }
        public decimal RemainingQuantity { get; set; }
        public string SerialNo { get; set; }
        public DateTime? GuaranteeDate { get; set; }
        public bool Serial { get; set; }
        public decimal SalePrice { get; set; }
        public decimal DiscountAmount { get;  set; }
        public decimal GrossAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public Guid? WarrantyTypeId { get; set; }
        public string Description { get; set; }
        public bool IsService { get; set; }
        public List<SaleSizeAssortmentModel> SaleSizeAssortment { get; set; }
        public Guid? ColorId { get; set; }
        public string ColorName { get; set; }
        public string UnitName { get; set; }
    }
}
