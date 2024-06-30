using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Focus.Business.RolePermission.Queries.GetRolesPermissionsList;
using Focus.Domain.Entities;

namespace Noble.Api.Models
{
    public class RolePermissionsVm
    {
        public IList<RolesPermissionsLookUpModel> NobleRolePermission { get; set; }
    }
}