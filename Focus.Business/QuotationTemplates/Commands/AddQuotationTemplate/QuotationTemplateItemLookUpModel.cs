using Focus.Business.Sales.Queries.GetSaleDetail;
using System;

namespace Focus.Business.QuotationTemplates.Commands.AddQuotationTemplate
{
  public  class QuotationTemplateItemLookUpModel
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public Guid QuotationTemplateId { get; set; }
        public ProductDropdownLookUpModel Product { get; set; }
        public string ProductName { get; internal set; }
        public string ProductNameEn { get; internal set; }
        public string ProductNameAr { get; internal set; }
    }
}
