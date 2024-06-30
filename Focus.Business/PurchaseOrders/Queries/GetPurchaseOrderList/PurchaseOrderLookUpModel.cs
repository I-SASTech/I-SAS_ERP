//using Focus.Business.PurchaseOrders.Queries.NetAmount;
using Focus.Domain.Enum;
using System;
using System.Linq;

namespace Focus.Business.PurchaseOrders.Queries.GetPurchaseOrderList
{
    public class PurchaseOrderLookUpModel 
    {
        public Guid Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string SupplierName { get; set; }
        public string SupplierNameArabic { get; set; }
        public decimal NetAmount { get; set; }
        public bool IsInvoice { get; set; }
        public bool IsClose { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public string InvoiceNo { get; set; }
        public string Reference { get; set; }
        public string DocumentType { get; set; }
        public string CreatedFrom { get; set; }
        public string CreatedBy { get; set; }
        public string Reason { get; set; }
        public string CreatedById { get; set; }
        public Guid? SupplierAdvanceAccountId { get; set; }
        public Guid? SupplierQuotationId { get; set; }
        public Guid? SupplierId { get; set; }
        public PartiallyInvoices PartiallyReceived { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsDefault { get; set; }
        public string CustomerEmail { get; set; }



        //public static PurchaseOrderLookUpModel Create(Domain.Entities.PurchaseOrder purchaseOrder)
        //{
        //    var netAmount = new TotalAmount();
        //    var lookUpModel = new PurchaseOrderLookUpModel
        //    {
        //        Id = purchaseOrder.Id,
        //        PartiallyReceived = purchaseOrder.PartiallyReceived,

        //        RegistrationNumber = purchaseOrder.RegistrationNo,
        //        IsClose = purchaseOrder.IsClose,
        //        DocumentType = purchaseOrder.DocumentType,
        //        SupplierName = purchaseOrder.Supplier?.EnglishName,
        //        SupplierNameArabic = purchaseOrder.Supplier?.ArabicName,
        //        SupplierAdvanceAccountId = purchaseOrder.Supplier?.AdvanceAccountId,
        //        NetAmount = purchaseOrder.TotalAmount,
        //        Date = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
        //        Version = purchaseOrder.PurchaseOrderVersions?.LastOrDefault()?.Version

        //    };
        //    return lookUpModel;
        //}
    }
}
