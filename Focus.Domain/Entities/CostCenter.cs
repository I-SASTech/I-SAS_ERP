using System;
using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class CostCenter : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public bool VatDeductible { get; set; }
        public bool IsActive { get; set; }
        public Guid AccountTypeId { get; set; }
        public virtual AccountType AccountTypes { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
