using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.Origins.Queries.GetOriginDetails
{
    public class OriginDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }

        public static Expression<Func<Origin, OriginDetailsLookUpModel>> Projection
        {
            get
            {
                return Origin => new OriginDetailsLookUpModel
                {
                    Id = Origin.Id,
                    Name = Origin.Name,
                    NameArabic = Origin.NameArabic,
                    Description = Origin.Description,
                    Code = Origin.Code,
                    isActive = Origin.isActive
                };
            }
        }

        public static OriginDetailsLookUpModel Create(Origin Origin)
        {
            return Projection.Compile().Invoke(Origin);
        }
    }
}
