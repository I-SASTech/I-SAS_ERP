using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.NobleRole.Queries.GetNobleRoleDetails
{
    public class NobleRoleDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string NormalizedName { get; set; }
        public bool isActive { get; set; }

        public static Expression<Func<NobleRoles, NobleRoleDetailsLookUpModel>> Projection
        {
            get
            {
                return nobleRole => new NobleRoleDetailsLookUpModel
                {
                    Id = nobleRole.Id,
                    Name = nobleRole.Name,
                    NameArabic = nobleRole.NameArabic,
                    NormalizedName = nobleRole.NormalizedName,
                    isActive = nobleRole.IsActive
                };
            }
        }

        public static NobleRoleDetailsLookUpModel Create(NobleRoles nobleRole)
        {
            return Projection.Compile().Invoke(nobleRole);
        }
    }
}
