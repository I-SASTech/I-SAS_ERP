using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Categories.Queries.GetCategoryList
{
    public class CategoryLookUpModel : IMapFrom<Category>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }
        public bool IsService { get; set; }
        public int ReturnDays { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryLookUpModel>();
        }
    }
}
