using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.NobleUserRole.Queries.GetNoblePermissionList
{
    public class NoblePermissionLookUp
    {
        public Guid Id { get; set; }
        public Guid PermissionId { get; set; }
        public Guid RoleId { get; set; }
        public string Key { get; set; }
        public bool IsActive { get; set; }
    }
}
