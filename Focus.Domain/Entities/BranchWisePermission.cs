using Focus.Domain.Interface;
using System;


namespace Focus.Domain.Entities
{
    public class BranchWisePermission : ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public string PermissionName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public Guid NobleGroupId { get; set; }
        public Guid NobleModuleId { get; set; }
    }
}
