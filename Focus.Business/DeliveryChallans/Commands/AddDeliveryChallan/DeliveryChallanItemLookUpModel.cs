using Focus.Business.Sales.Queries.GetSaleDetail;
using System;

namespace Focus.Business.DeliveryChallans.Commands.AddDeliveryChallan
{
  public  class DeliveryChallanItemLookUpModel
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ServiceProductId { get; set; }
        public bool IsActive { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal SoQty { get; set; }
        public Guid DeliveryChallanId { get; set; }
        public ProductDropdownLookUpModel Product { get; set; }
        public string ProductName { get;  set; }
        public string ProductNameEn { get;  set; }
        public string ProductNameAr { get;  set; }
        public string Description { get;  set; }
        public string DisplayName { get;  set; }
        public string DisplayNameForPrint { get;  set; }
        public string StyleNumber { get; set; }
        public string UnitName { get; set; }
        public decimal UnitPerPack { get; set; }
    }
}
