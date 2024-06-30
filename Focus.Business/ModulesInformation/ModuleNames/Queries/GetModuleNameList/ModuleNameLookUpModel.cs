using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.ModulesInformation.ModuleNames.Queries.GetModuleNameList
{
    public class ModuleNameLookUpModel : IMapFrom<ModulesName>
    {
        public Guid Id { get; set; }
        public Guid NewId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ModulesName, ModuleNameLookUpModel>();
        }
    }
}
