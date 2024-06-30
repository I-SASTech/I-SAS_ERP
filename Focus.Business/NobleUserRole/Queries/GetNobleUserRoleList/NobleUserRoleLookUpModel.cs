using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.NobleUserRole.Queries.GetNobleUserRoleList
{
    public class NobleUserRoleLookUpModel : IMapFrom<NobleUserRoles>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public bool isActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NobleUserRoles, NobleUserRoleLookUpModel>();
        }
    }
}
