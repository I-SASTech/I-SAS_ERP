using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Regions.Queries.GetRegionList
{
    public class RegionLookUpModel : IMapFrom<Region>
    {
        public Guid Id { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public Guid CityId { get; set; }
        public string CityName { get; set; }
        public string Area { get; set; }
        public bool isActive { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Region, RegionLookUpModel>()
                     .ForMember(x => x.CityName, prod => prod.MapFrom(z => z.City.Name));

        }
    }
}
