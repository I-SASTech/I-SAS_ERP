using Focus.Business.NoblePermission.Commands.AddUpdateNoblePermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.Branches.BranchesPermission
{
    public class BranchPermissionModel
    {
        public List<PermissionsLookUp> Permissions { get; set; }

        public Guid? BranchId { get; set; }
        public bool IsBranch { get; set; }
        public string RoleName { get; set; }
    }
}
