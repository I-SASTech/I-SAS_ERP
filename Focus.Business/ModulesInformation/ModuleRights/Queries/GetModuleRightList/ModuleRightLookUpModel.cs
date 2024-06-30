using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;

namespace Focus.Business.ModulesInformation.ModuleRights.Queries.GetModuleRightList
{
    public class ModuleRightLookUpModel : IMapFrom<CompanyPermission>
    {
        public Guid PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string PermissionCategory { get; set; }
        public Guid NobleModuleId { get; set; }
        public string Key { get; set; }
        public string ModuleDescription { get; set; }
        public string ModuleName { get; set; }
        public bool IsActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CompanyPermission, ModuleRightLookUpModel>();
        }
    }
}
