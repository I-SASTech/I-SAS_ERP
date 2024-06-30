using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.ManualAttendance
{
   public class EmployeeManualAttendence
    {
        public Guid Id { get; set; }
        //Personal Information
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public virtual List<ManualAttendenceLookUpModel> Attendence { get; set; }
        public virtual List<bool> BoolCheckIn { get; set; }
        public virtual List<bool> BoolCheckOut { get; set; }
        public virtual List<Guid> CheckInIds { get; set; }

    }
}
