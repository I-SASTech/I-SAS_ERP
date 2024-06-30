using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.NobleUserRole.Queries.GetNobleUserRoleDetails
{
    public class NobleUserRoleDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public bool isActive { get; set; }

        public static Expression<Func<NobleUserRoles, NobleUserRoleDetailsLookUpModel>> Projection
        {
            get
            {
                return nobleRole => new NobleUserRoleDetailsLookUpModel
                {
                    Id = nobleRole.Id,
                    UserId = nobleRole.UserId,
                    RoleId = nobleRole.RoleId,
                    isActive = nobleRole.isActive
                };
            }
        }

        public static NobleUserRoleDetailsLookUpModel Create(NobleUserRoles nobleRole)
        {
            return Projection.Compile().Invoke(nobleRole);
        }
    }
}
