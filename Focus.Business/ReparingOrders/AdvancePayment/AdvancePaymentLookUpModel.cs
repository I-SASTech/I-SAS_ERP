

using Focus.Domain.Enum;
using System;

namespace Focus.Business.ReparingOrders.AdvancePayment
{
  public  class AdvancePaymentLookUpModel
    {
        public Guid? Id { get; set; }
        public DateTime Date { get; set; }
        public string VoucherNumber { get; set; }
        public string Narration { get; set; }
        public string ChequeNumber { get; set; }
        public decimal Amount { get; set; }
        public PaymentVoucherType PaymentVoucherType { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid BankCashAccountId { get; set; }
        public Guid ContactAccountId { get; set; }
     
        public Guid? ReparingOrderId { get; set; }
    }
}
