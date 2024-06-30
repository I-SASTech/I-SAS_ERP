using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.Brands.Queries.GetBrandDetails
{
    public class BrandDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }

        public static Expression<Func<Brand, BrandDetailsLookUpModel>> Projection
        {
            get
            {
                return brand => new BrandDetailsLookUpModel
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    NameArabic = brand.NameArabic,
                    Description = brand.Description,
                    Code = brand.Code,
                    isActive = brand.isActive
                };
            }
        }

        public static BrandDetailsLookUpModel Create(Brand brand)
        {
            return Projection.Compile().Invoke(brand);
        }
    }
}
