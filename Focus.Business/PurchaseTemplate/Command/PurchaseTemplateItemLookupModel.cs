using System;
using Focus.Business.Sales.Queries.GetSaleDetail;

namespace Focus.Business.PurchaseTemplate.Command
{
    public class PurchaseTemplateItemLookupModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal FixDiscount { get; set; }
        public Guid TaxRateId { get; set; }
        public Guid AutoPurchaseTemplateId { get; set; }
        public ProductDropdownLookUpModel Product { get; set; }
        public string TaxMethod { get; set; }
        public decimal? HighQty { get; set; }
        public decimal? UnitPerPack { get; set; }
    }
}
