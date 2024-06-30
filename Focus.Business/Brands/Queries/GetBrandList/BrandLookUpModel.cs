using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Brands.Queries.GetBrandList
{
    public class BrandLookUpModel : IMapFrom<Brand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Brand, BrandLookUpModel>();
        }
    }
}
