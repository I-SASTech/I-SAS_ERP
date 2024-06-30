using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class FinancialYearClosing : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public DateTime Date { get; set; }
        public string PeriodName { get; set; }
        public string Description { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int ReOpen { get; set; }
        public virtual  ICollection<FinancialYearClosingBalance> FinancialYearClosingBalances { get; set; }
    }
}
