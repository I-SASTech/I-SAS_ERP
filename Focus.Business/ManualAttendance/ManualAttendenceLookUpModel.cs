using System;


namespace Focus.Business.ManualAttendance
{
   public class ManualAttendenceLookUpModel
    {
        public Guid Id { get; set; }

        public string OverTimeValue { get; set; }
        public string Description { get; set; }
        public int EarlyGoing { get; set; }
        public DateTime Date { get; set; }
        public decimal WorkingPercentage { get; set; }
        public decimal TotalHour { get; set; }
        public decimal OfficeHour { get; set; } 
        public decimal TotalMinute { get; set; }
        public decimal OfficeMinute { get; set; }
        public DateTime? OverTime { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public DateTime? CompanyTimeIn { get; set; }
        public DateTime? CompanyTimeOut { get; set; }
        public Guid? EmployeeId { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public string CheckType { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNameAr { get; set; }
        public bool IsOnLeave { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsCheckIn { get; set; }
        public bool IsCheckOut { get; set; }
        public bool IsPreviousAttendence { get; set; }
    }
}
