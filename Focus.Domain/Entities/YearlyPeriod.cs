using System;
using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class YearlyPeriod : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public string Name { get; set; }
        public bool IsYearlyPeriodClosed { get; set; }
        public string MonthType { get; set; }
        public virtual ICollection<CompanySubmissionPeriod> CompanySubmissionPeriods { get; set; }
        public Guid? BranchId { get; set; }

    }
}
