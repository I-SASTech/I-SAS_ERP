using Focus.Business.Sales.Queries.GetSaleDetail;
using System;

namespace Focus.Business.PurchaseBills
{
    public class PurchaseBillItemLookupModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Guid? AccountId { get; set; }
        public Guid PurchaseBillId { get; set; }
        public string AccountName { get;  set; }
    }
}
