using System;
using System.Collections.Generic;

namespace Focus.Business.Holidays
{
    public class HolidayLookupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Description { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public virtual ICollection<WeekHolidaysLookupModel> WeekHolidays { get; set; }
        public virtual ICollection<GuestedHolidaysLookupModel> GuestedHolidays { get; set; }

    }
}
