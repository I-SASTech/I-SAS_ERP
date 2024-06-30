using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
    public class NobleRoles : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string NormalizedName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<RolesPermissions> UsersPermission { get; set; }
        public virtual ICollection<NobleRolePermission> NobleRolePermissions { get; set; }
        public virtual ICollection<NobleUserRoles> NobleUserRoles { get; set; }
    }
}
