using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class TerminalCategory : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public Guid TerminalId { get; set; }
        public virtual Terminal Terminal { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public Guid? BranchId { get; set; }

    }
}
