using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class NobleRolePermission : ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public virtual NobleRoles NobleRole { get; set; }
        public Guid RoleId { get; set; }
        public virtual CompanyPermission CompanyPermissions { get; set; }
        public Guid PermissionId { get; set; }
    }
}
