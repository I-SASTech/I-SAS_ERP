using System;
using System.Collections.Generic;

namespace Focus.Business.HR.Payroll.RunPayrolls.Commands
{
    public class RunPayrollLookUpModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid PayrollScheduleId { get; set; }
        public bool Status { get; set; }
        public bool Hourly { get; set; }
        public virtual ICollection<RunPayrollDetailLookUpModel> SalaryTemplateList { get; set; }
    }
}
