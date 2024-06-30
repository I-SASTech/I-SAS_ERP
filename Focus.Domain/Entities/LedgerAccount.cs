using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class LedgerAccount : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }

        public Guid? AccountId { get; set; }
        public virtual Account Account { get; set; }
        public Guid AccountLedgerId { get; set; }
        public AccountLeaderType AccountLeaderType { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsLock { get; set; }
        public bool IsLeave { get; set; }
    }
}
