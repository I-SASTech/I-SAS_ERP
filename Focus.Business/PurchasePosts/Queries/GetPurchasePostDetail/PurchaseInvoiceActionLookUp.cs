using System;
using Focus.Domain.Entities;

namespace Focus.Business.PurchasePosts.Queries.GetPurchasePostDetail
{
    public class PurchaseInvoiceActionLookUp
    {
        public Guid Id { get; set; }
        public Guid ProcessId { get; set; }
        public virtual CompanyProcess CompanyProcess { get; set; }
        public Guid? PurchaseInvoiceId { get; set; }
        public virtual Purchase Purchase { get; set; }
        public Guid? PurchaseInvoicePostId { get; set; }
        public virtual PurchasePost PurchasePost { get; set; }
        public DateTime? Date { get; set; }
        public DateTime CurrentDate { get; set; }
        public string Description { get; set; }
        public string ProcessName { get; set; }
        public string ProcessNameArabic { get; set; }
    }
}
