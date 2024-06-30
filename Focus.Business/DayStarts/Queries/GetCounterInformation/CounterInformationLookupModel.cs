using System;

namespace Focus.Business.DayStarts.Queries.GetCounterInformation
{
    public class CounterInformationLookupModel
    {
        public string CounterCode { get; set; }
        public decimal OpeningCash { get; set; }
        public Guid CounterId { get; set; }
        public decimal CashInHand { get; set; }
        public decimal Bank { get; set; }
        public decimal Expense { get; set; }

    }
}
