using System;

namespace Focus.Business.PurchaseTemplate.Queries
{
    public class PurchaseTemplateListLookUp
    {
        public Guid Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string SupplierName { get; set; }
        public string SupplierNameArabic { get; set; }
        public decimal NetAmount { get; set; }
        public bool IsInvoice { get; set; }
        public bool IsActive { get; set; }
        public string Date { get; set; }
        public int Day { get; set; }
        public Guid? SupplierAdvanceAccountId { get; set; }
        public string BranchCode { get; set; }
    }
    
}
