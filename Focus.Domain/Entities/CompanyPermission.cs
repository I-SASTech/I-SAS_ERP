using System;
using System.Collections.Generic;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class CompanyPermission : ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid Id { get; set; }
        public string PermissionName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public Guid NobleGroupId { get; set; }
        public virtual NobleGroup NobleGroup { get; set; }
        public virtual NobleModule NobleModules { get; set; }
        public Guid NobleModuleId { get; set; }
        public virtual ICollection<NobleRolePermission> NobleRolePermissions { get; set; }
    }
}
