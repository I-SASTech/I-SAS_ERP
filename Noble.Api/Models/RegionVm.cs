using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class RegionVm
    {
        public Guid? Id { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public Guid CityId { get; set; }
        public string Area { get; set; }
        public bool isActive { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
