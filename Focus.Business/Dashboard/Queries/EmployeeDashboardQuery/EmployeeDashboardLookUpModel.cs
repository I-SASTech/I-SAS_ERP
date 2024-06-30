using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Dashboard.Queries.EmployeeDashboardQuery
{
   public class EmployeeDashboardLookUpModel
    {
        public int TotalEmployee { get; set; }
        public int TotalPresentEmployee { get;  set; }
        public int EmployeeOnLeave { get;  set; }
        public int TotalAbsentEmployee { get; internal set; }
        public List<int> GenderList { get;  set; }
        public List<DepartmentWiseSalary> DepartmentWiseSalaryList { get;  set; }
        public decimal GrossSalary { get; internal set; }
        public int TotalDepartment { get; internal set; }
        public decimal RemainingLoan { get; internal set; }
    }
}
