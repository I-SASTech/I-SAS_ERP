using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.Regions.Queries.GetRegionDetails
{
    public class RegionDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public Guid CityId { get; set; }
        public string Area { get; set; }
        public bool isActive { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public static Expression<Func<Region, RegionDetailsLookUpModel>> Projection
        {
            get
            {
                return region => new RegionDetailsLookUpModel
                {
                    Id = region.Id,
                    CountryId = region.CountryId,
                    StateId = region.StateId,
                    CityId = region.CityId,
                    Area = region.Area,
                    Description = region.Description,
                    Code = region.Code,
                    isActive = region.isActive
                };
            }
        }

        public static RegionDetailsLookUpModel Create(Region region)
        {
            return Projection.Compile().Invoke(region);
        }
    }
}
