using System;
namespace Focus.Business.Sales
{
    public class OtherCurrencyLookupModel
    {
        public Guid? CurrencyId { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
