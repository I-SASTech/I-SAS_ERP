using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
  public  class DenominationSetup : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public decimal Number { get; set; }
        public bool isActive { get; set; }


    }
}
