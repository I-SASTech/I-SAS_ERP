using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
  public  class NobleUserRoles : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid RoleId { get; set; }
        public virtual NobleRoles Roles { get; set; }
        public string UserId { get; set; }
        public bool isActive { get; set; }
    }
}