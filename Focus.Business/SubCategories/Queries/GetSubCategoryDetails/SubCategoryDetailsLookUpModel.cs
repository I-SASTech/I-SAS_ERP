using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.SubCategories.Queries.GetSubCategoryDetails
{
    public class SubCategoryDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }
        public Guid? CategoryId { get; set; }

        public static Expression<Func<SubCategory, SubCategoryDetailsLookUpModel>> Projection
        {
            get
            {
                return request => new SubCategoryDetailsLookUpModel
                {
                    Id=request.Id,
                    Name = request.Name,
                    NameArabic = request.NameArabic,
                    Description = request.Description,
                    Code = request.Code,
                    CategoryId = request.CategoryId,
                    isActive = request.isActive
                };
            }
        }

        public static SubCategoryDetailsLookUpModel Create(SubCategory subCategory)
        {
            return Projection.Compile().Invoke(subCategory);
        }
    }
}
