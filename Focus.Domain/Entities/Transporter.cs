using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
   public class Transporter : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string City { get; set; }
        public string Name { get; set; }
        public Guid? BranchId { get; set; }

    }
}
