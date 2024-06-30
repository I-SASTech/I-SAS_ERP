using System;
using System.Collections.Generic;

namespace Focus.Business.ManualAttendance
{
    public class ManualAttendanceListModel
    {
        public Guid Id { get; set; }
        public string CurrentMonth { get; set; }
        public string CurrentYear { get; set; }
        public virtual List<DayOfWeekLookUpModel> DayOfWeekLookUpModel { get; set; }
        public virtual List<EmployeeManualAttendence> EmployeeManualAttendence { get; set; }

    }
}
