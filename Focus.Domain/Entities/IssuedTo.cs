using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
   public class IssuedTo : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public Guid AccountId { get; set; }
        public string NameArabic { get; set; }
        public bool isActive { get; set; }
    }
}
