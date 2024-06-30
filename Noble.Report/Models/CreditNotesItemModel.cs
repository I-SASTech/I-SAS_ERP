using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class CreditNotesItemModel
    {
        public Guid? ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal FixDiscount { get; set; }
        public Guid? TaxRateId { get; set; }
        public Guid? WareHouseId { get; set; }
        public Guid CreditNoteId { get; set; }
        public string Description { get; set; }
        public string TaxMethod { get; set; }
        public decimal Total { get; set; }
        public string TaxRate { get; set; }
        public bool ServiceItem { get; set; }
        public bool IsFree { get; set; }
        public string Code { get; set; }
        public string ProductName
        {
            get; set;
        }
        public string UnitName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal GrossAmount { get; set; }
    }
}