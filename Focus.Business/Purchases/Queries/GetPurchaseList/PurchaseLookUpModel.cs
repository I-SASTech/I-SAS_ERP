using Focus.Business.Purchases.Queries.NetAmount;
using System;
using System.Linq;
using Focus.Domain.Entities;

namespace Focus.Business.Purchases.Queries.GetPurchaseOrderList
{
    public class PurchaseLookUpModel 
    {
        public Guid Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string SupplierName { get; set; }
        public string SupplierNameArabic { get; set; }
        public decimal NetAmount { get; set; }
        public bool IsInvoice { get; set; }
        public string Date { get; set; }
        
        public static PurchaseLookUpModel Create(Purchase purchaseOrder)
        {
            var netAmount = new TotalAmount();
            var lookUpModel = new PurchaseLookUpModel
            {
                Id = purchaseOrder.Id,
                RegistrationNumber=purchaseOrder.RegistrationNo,
                SupplierName = purchaseOrder.Supplier.EnglishName,
                SupplierNameArabic = purchaseOrder.Supplier.ArabicName,
                NetAmount = netAmount.CalculateTotalAmount(purchaseOrder.PurchaseItems.ToList(), purchaseOrder.TaxMethod),
                Date = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt")
            };
            return lookUpModel;
        }
    }
}
