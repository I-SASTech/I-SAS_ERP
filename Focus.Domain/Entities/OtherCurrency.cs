using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class OtherCurrency : BaseEntity, ITenant
    {
        public Guid CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
