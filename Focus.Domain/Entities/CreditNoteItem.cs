using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class CreditNoteItem : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal FixDiscount { get; set; }
        public string TaxMethod { get; set; }
        public string Description { get; set; }
        public Guid? TaxRateId { get; set; }
        public bool ServiceItem { get; set; }
        public bool IsFree { get; set; }
        public virtual TaxRate TaxRate { get; set; }
        public Guid? WareHouseId { get; set; }
        public virtual Warehouse WareHouse { get; set; }
        public Guid CreditNoteId { get; set; }
        public virtual CreditNote CreditNote { get; set; }

        public decimal TotalWithOutAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
