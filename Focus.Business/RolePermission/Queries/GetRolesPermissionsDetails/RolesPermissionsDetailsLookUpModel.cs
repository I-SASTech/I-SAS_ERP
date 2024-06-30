using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.RolesPermission.Queries.GetRolesPermissionsDetails
{
    public class RolesPermissionsDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string ModuleName { get; set; }
        public bool Save { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool List { get; set; }
        public bool Print { get; set; }
        public bool Draft { get; set; }
        public bool Approved { get; set; }

        //public static Expression<Func<UsersPermissions, UserPermissionDetailsLookUpModel>> Projection
        //{
        //    get
        //    {
        //        return usersPermissions => new UserPermissionDetailsLookUpModel
        //        {

        //            Id = usersPermissions.Id,
        //            ModuleName = usersPermissions.ModuleName,
        //            UserId = usersPermissions.UserId,
        //            RoleId = usersPermissions.RoleId,
        //            Save = usersPermissions.Save,
        //            Edit = usersPermissions.Edit,
        //            Delete = usersPermissions.Delete,
        //            List = usersPermissions.List,
        //            Print = usersPermissions.Print,
        //            Draft = usersPermissions.Draft,
        //            Approved = usersPermissions.Approved
        //        };
        //    }
        //}

        //public static UserPermissionDetailsLookUpModel Create(UsersPermissions usersPermissions)
        //{
        //    return Projection.Compile().Invoke(usersPermissions);
        //}
    }
}
