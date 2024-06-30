using System;

namespace Focus.Business.NoblePermission.Commands.AddUpdateNoblePermission
{
    public class PermissionsLookUp
    {
        public Guid Id { get; set; }
        public string PermissionName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public Guid ModuleId { get; set; }
        public Guid GroupId { get; set; }
        public Guid TypeId { get; set; }
        public bool Checked { get; set; }
    }
}
