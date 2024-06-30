using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
   public class UserTerminal:BaseEntity, ITenant
    {
        public Guid TerminalId { get; set; }
        public virtual Terminal Terminal { get; set; }
        public Guid UserId { get; set; }
        public Guid? BranchId { get; set; }

    }
}
