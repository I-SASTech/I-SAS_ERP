using Focus.Domain.Entities;
using System;
using System.Linq.Expressions;


namespace Focus.Business.Zones.Queries.GetZoneDetails
{
  public  class ZoneDetailLookUpModel
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public static Expression<Func<Zone, ZoneDetailLookUpModel>> Projection
        {
            get
            {
                return zone => new ZoneDetailLookUpModel
                {
                    Id=zone.Id,
                  City=zone.City,
                  Name=zone.Name
                };
            }
        }
        public static ZoneDetailLookUpModel Create(Zone zone)
        {
            return Projection.Compile().Invoke(zone);
        }
    }
}
