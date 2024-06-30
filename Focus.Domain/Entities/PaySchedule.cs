using System;
using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class PaySchedule : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string PayPeriod { get; set; }
        public DateTime PayPeriodStartDate { get; set; }
        public string Name { get; set; }
        public DateTime PayPeriodEndDate { get; set; }
        public string IfPayDayFallOnHoliday { get; set; }
        public DateTime PayDate { get; set; }
        public bool Default { get; set; }
        public bool IsHourlyPay { get; set; }
        public bool Proceed { get; set; }
        public virtual ICollection<RunPayroll> RunPayrolls { get; set; }
    }
}
