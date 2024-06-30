using System;

namespace Focus.Business.HR.Payroll.PayrollSchedule.Commands
{
    public class PayrollScheduleLookUpModel
    {
        public Guid Id { get; set; }
        public string PayPeriod { get; set; }
        public DateTime PayPeriodStartDate { get; set; }
        public string Name { get; set; }
        public DateTime PayPeriodEndDate { get; set; }
        public string IfPayDayFallOnHoliday { get; set; }
        public DateTime PayDate { get; set; }
        public bool Default { get; set; }
        public bool IsHourlyPay { get; set; }
    }
}
