using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.SubCategories.Queries.GetSubCategoryList
{
    public class SubCategoryLookUpModel 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameArabic { get; set; }
        public bool isActive { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
