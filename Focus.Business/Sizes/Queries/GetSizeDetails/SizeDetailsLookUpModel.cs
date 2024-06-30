using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.Sizes.Queries.GetSizeDetails
{
    public class SizeDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }

        public static Expression<Func<Size, SizeDetailsLookUpModel>> Projection
        {
            get
            {
                return size => new SizeDetailsLookUpModel
                {
                    Id = size.Id,
                    Name = size.Name,
                    NameArabic = size.NameArabic,
                    Description = size.Description,
                    Code = size.Code,
                    isActive = size.isActive
                };
            }
        }

        public static SizeDetailsLookUpModel Create(Size size)
        {
            return Projection.Compile().Invoke(size);
        }
    }
}
