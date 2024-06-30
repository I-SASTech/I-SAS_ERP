using System;

namespace Focus.Business.RolePermission.Queries.GetRolesPermissionsList
{
    public class RolesPermissionsLookUpModel
    {
        public Guid RoleId { get; set; }
        public bool IsActive { get; set; }
        public Guid PermissionId { get; set; }
        public string AllowAll { get; set; }
        public Guid CompanyId { get; set; }
    }
}
