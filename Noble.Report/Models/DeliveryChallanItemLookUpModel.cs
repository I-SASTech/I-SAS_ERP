using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class DeliveryChallanItemLookUpModel
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ServiceProductId { get; set; }
        public bool IsActive { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal SoQty { get; set; }
        public Guid DeliveryChallanId { get; set; }
        public string ProductName { get; set; }
        public string ProductNameEn { get; set; }
        public string ProductNameAr { get; set; }
        public string Description { get; set; }
        public string StyleNumber { get; set; }
        public string UnitName { get; set; }
        public string DisplayNameForPrint { get; set; }
        public decimal UnitPerPack { get; set; }
    }
}