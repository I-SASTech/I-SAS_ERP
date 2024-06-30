using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.NobleRole.Queries.GetNobleRoleList
{
    public class NobleRoleLookUpModel : IMapFrom<NobleRoles>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string NormalizedName { get; set; }
        public bool isActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NobleRoles, NobleRoleLookUpModel>();
        }
    }
}
