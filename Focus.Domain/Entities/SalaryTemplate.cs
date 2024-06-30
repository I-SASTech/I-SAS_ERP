using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class SalaryTemplate : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Code { get; set; }
        public string StructureName { get; set; }
        public virtual ICollection<SalaryAllowance> SalaryAllowances { get; set; }
        public virtual ICollection<SalaryDeduction> SalaryDeductions { get; set; }
        public virtual ICollection<SalaryContribution> SalaryContributions { get; set; }


    }
}
