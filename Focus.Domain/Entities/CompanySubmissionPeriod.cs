using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class CompanySubmissionPeriod : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant, ISoftDelete
    {
        public string Year { get; set; }
        public PeriodName PeriodName { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public bool IsPeriodClosed { get; set; }
        public Guid? YearlyPeriodId { get; set; }
        public virtual YearlyPeriod YearlyPeriod { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
