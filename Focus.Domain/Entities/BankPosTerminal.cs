using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
   public class BankPosTerminal : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
   {
       public string TerminalId { get; set; }
       public Guid BankId { get; set; }
       public virtual Account Account { get; set; }
       public bool isActive { get; set; }
       public virtual ICollection<Terminal> Terminals { get; set; }




    }
}
