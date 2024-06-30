using System;
using Focus.Business.Sales.Queries.GetSaleDetail;

namespace Focus.Business.Purchases
{
    public class PurchaseItemLookupModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal FixDiscount { get; set; }
        public Guid TaxRateId { get; set; }
        public Guid PurchaseId { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string BatchNo { get; set; }
        public ProductDropdownLookUpModel Product { get; set; }
        public bool IsExpire { get; set; }
        public string TaxMethod { get; set; }
        public decimal? HighQty { get; set; }
        public decimal? UnitPerPack { get; set; }
        public string SerialNo { get; set; }
        public DateTime? GuaranteeDate { get; set; }
        public bool Serial { get; set; }
    }
}
