using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class ManualAttendance : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public DateTime Date { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public Guid? EmployeeId { get; set; }
        public decimal TotalHour { get; set; }
        public decimal OverTime { get; set; }
        public bool IsOnLeave { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsCheckIn { get; set; }
        public bool IsCheckOut { get; set; }
        public bool IsSmsSend { get; set; }
        public string Description { get; set; }
        public virtual EmployeeRegistration EmployeeRegistration { get; set; }
    }
}
