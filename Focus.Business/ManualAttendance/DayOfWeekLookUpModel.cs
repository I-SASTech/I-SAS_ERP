using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.ManualAttendance
{
   public class DayOfWeekLookUpModel
    {

        public DateTime WeekDate { get; set; }
        public bool Disable { get; set; }
        public string HolidayType { get; set; }
        public string DayName { get; set; }

    }
}
