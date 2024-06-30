using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class CashRequestUser : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public Guid? AccountId { get; set; }
        public bool IsActive { get; set; }
    }
}
