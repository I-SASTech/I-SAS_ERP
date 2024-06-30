using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.ManualAttendance.Queries.EmployeeTodayAttendence
{
  public  class TodayAttendenceLookUpModel
    {
        public DateTime Date { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public bool IsOnLeave { get; set; }
        public bool IsCheckIn { get; set; }
        public bool IsCheckOut { get; set; }
        public string DepartmentEng { get; set; }
        public string DepartmentAr { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public decimal TotalHour { get; set; }
        public decimal OverTime { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid AttendenceId { get; set; }
        public bool IsAbsent { get;  set; }
    }
}
