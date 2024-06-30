using System;
using System.Collections.Generic;
using Focus.Business.HR.Payroll.Allowances.Commands.AddAllowance;
using Focus.Business.HR.Payroll.Contributions.Commands;
using Focus.Business.HR.Payroll.Deductions.Commands;

namespace Focus.Business.HR.Payroll.SalaryTemplates.Commands.AddSalaryTemplate
{
    public class SalaryTemplateLookUp
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string StructureName { get; set; }
        public virtual ICollection<AllowanceLookUp> SalaryAllowances { get; set; }
        public virtual ICollection<DeductionLookUpModel> SalaryDeductions { get; set; }
        public virtual ICollection<ContributionLookUpModel> SalaryContributions { get; set; }
    }
}
