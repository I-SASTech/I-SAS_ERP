using System;
using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class TaxSalary : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string TaxYear { get; set; }
        public bool IsActive { get; set; }
        public string FinancialYear { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        // TaxSlab 1..*
        public virtual ICollection<SalaryTaxSlab> SalaryTaxSlabs { get; set; }
    }
}
