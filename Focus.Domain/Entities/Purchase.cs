using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class Purchase : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public Guid SupplierId { get; set; }
        public virtual Contact Supplier { get; set; }
        public string InvoiceNo { get; set; }
        public bool IsPurchaseReturn { get; set; }
        public bool IsPost { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string PurchaseOrderNo { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public Guid TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public Guid WareHouseId { get; set; }
        public decimal InvoiceFixDiscount { get; set; }
        public bool Raw { get; set; }
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
        public virtual ICollection<PurchaseInvoiceAttachment> PurchaseInvoiceAttachments { get; set; }
        public virtual ICollection<PurchaseInvoiceAction> PurchaseInvoiceActions { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? SaleOrderId { get; set; }

        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}
