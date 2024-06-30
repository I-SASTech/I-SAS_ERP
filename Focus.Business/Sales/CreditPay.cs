using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Sales
{
    public class CreditPay
    {
        public Guid Id { get; set; }
        public string PaymentMode { get; set; }
        public Guid? AccountId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
