using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
  public  class RolesPermissions : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid RoleId { get; set; }
        public virtual NobleRoles Roles { get; set; }
        public string ModuleName { get; set; }
        public string NameArabic { get; set; }
        public bool Save { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool List { get; set; }
        public bool Print { get; set; }
        public bool Draft { get; set; }
        public bool Approved { get; set; }
    }
}