using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.BundleCategoriesItems.Queries.GetBundleCategoryList
{
    public class BundleCategoryLookUpModel : IMapFrom<BundleCategory>
    {
        public Guid Id { get; set; }
        public string Offer { get; set; }
        public decimal Buy { get; set; }
        public decimal Get { get; set; }
        public bool isActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<BundleCategory, BundleCategoryLookUpModel>();
        }
    }
}
