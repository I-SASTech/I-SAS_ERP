using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class FinancialYearClosingBalance : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        public Guid FinancialYearClosingId { get; set; }
        public virtual FinancialYearClosing FinancialYearClosing { get; set; }
        public DateTime Date { get; set; }
        public bool IsSoftDelete { get; set; }
        public int ReOpen { get; set; }
    }
}
