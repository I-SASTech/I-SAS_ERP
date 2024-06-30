using System;

namespace Focus.Business.PaymentVouchers.Queries.GetAdvanceBalance
{
    public class AdvanceBalanceLookupModel
    {
        public Guid? AccountId { get; set; }
        public Guid? SaleId { get; set; }
        public decimal NetSaleAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public decimal AdvanceBalance { get; set; }
        public bool IsSaleInvoice { get; set; }
        public bool IsPurchaseInvoice { get; set; }
        public bool IsForm { get; set; }
        public string FormName { get; set; }
    }
}
