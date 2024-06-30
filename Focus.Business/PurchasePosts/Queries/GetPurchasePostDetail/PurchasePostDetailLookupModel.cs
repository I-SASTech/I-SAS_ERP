using Focus.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Focus.Business.PurchasePosts.Queries.GetPurchasePostDetail
{
    public class PurchasePostDetailLookupModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public Guid SupplierId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string PurchaseOrderNo { get; set; }
        public bool IsPurchaseReturn { get; set; }
        public Guid WareHouseId { get; set; }
        public decimal InvoiceFixDiscount { get; set; }
        public virtual ICollection<PurchasePostItem> PurchasePostItems { get; set; }
    }
}
