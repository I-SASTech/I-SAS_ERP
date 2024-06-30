using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;

namespace Focus.Business.Designations.Queries.GetDesignationList
{
    public class DesignationLookUpModel:IMapFrom<Designation>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string NameArabic { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Designation, DesignationLookUpModel>();
        }
    }
}
