using System;
using System.Collections.Generic;
using Focus.Business.PurchaseOrders.Commands;
using Focus.Domain.Entities;

namespace Focus.Business.Purchases
{
    public class PurchaseLookupModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public Guid SupplierId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public bool IsPurchaseReturn { get; set; }
        public string PurchaseOrderNo { get; set; }
        public Guid WareHouseId { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public decimal InvoiceFixDiscount { get; set; }
        public Guid TaxRateId { get; set; }
        public string TaxMethod { get; set; }
        public bool IsRaw { get; set; }
        public ICollection<PurchaseItemLookupModel> PurchaseItems { get; set; }
        public virtual List<ActionLookUpModel> ActionProcess { get; set; }
        public virtual List<PurchaseAttachment> PurchaseAttachments { get; set; }
        public string SupplierName { get; set; }
        public string TaxRatesName { get; set; }
        public string WareHouseName { get; set; }
    }
}
