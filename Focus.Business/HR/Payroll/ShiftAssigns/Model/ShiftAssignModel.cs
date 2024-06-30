using System;
using System.Collections.Generic;

namespace Focus.Business.HR.Payroll.ShiftAssigns.Model
{
    public class ShiftAssignModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public Guid? ShiftId { get; set; }
        public string ShiftName { get; set; }
        public Guid? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public bool IsActive { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Description { get; set; }
        public string ReasonOfClosingShift { get; set; }
        
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public Guid CompanyId { get; set; }
        public string CreatedById { get; set; }
        public List<ShiftAssignItemModel> ShiftEmployeeAssigns { get; set; }
    }
}
