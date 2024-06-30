using Focus.Business.Sales.Queries.GetSaleDetail;
using System;

namespace Focus.Business.Holidays
{
    public class GuestedHolidaysLookupModel
    {
        public Guid? Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Guid? HolidayId { get; set; }
    }
}
