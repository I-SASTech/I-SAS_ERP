 using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Zones.Queries.GetZoneList
{
   public class ZoneLookUpModel :IMapFrom<Zone>
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Name { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Zone, ZoneLookUpModel>();
        }
    }
}
