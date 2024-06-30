using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.Citys.Queries.GetCityDetails
{
    public class CityDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }

        public static Expression<Func<City, CityDetailsLookUpModel>> Projection
        {
            get
            {
                return city => new CityDetailsLookUpModel
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description,
                    Code = city.Code,
                    isActive = city.isActive
                };
            }
        }

        public static CityDetailsLookUpModel Create(City city)
        {
            return Projection.Compile().Invoke(city);
        }
    }
}
