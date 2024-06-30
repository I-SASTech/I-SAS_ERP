using Focus.Domain.Entities;
using System;

namespace Focus.Business.HR.Payroll.ShiftAssigns.Model
{
    public class ShiftAssignItemModel
    {
        public Guid Id { get; set; }
        public Guid? ShiftId { get; set; }
        public Guid? EmployeeId { get; set; }
        public EmployeeRegistration Employee { get; set; }
        public Guid? ShiftAssignId { get; set; }
        public bool IsActive { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Description { get; set; }
        public string ReasonOfClosingShift { get; set; }
        public string EmployeeName { get; set; }
    }
}
