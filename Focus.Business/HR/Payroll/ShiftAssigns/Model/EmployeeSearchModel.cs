using System;
using System.Collections.Generic;

namespace Focus.Business.HR.Payroll.ShiftAssigns.Model
{
    public class EmployeeSearchModel
    {
        public List<Guid> DesignationIds { get; set; }
        public List<Guid> DepartmentIds { get; set; }
    }
}
