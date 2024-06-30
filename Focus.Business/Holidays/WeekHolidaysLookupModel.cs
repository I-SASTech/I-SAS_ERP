using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Holidays
{
   public class WeekHolidaysLookupModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public Guid? HolidayId { get; set; }
    }
}
