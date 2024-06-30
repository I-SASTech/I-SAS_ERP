using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.Units.Queries.GetUnitDetails
{
    public class UnitDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }

        public static Expression<Func<Unit, UnitDetailsLookUpModel>> Projection
        {
            get
            {
                return Unit => new UnitDetailsLookUpModel
                {
                    Id = Unit.Id,
                    Name = Unit.Name,
                    NameArabic = Unit.NameArabic,
                    Description = Unit.Description,
                    Code = Unit.Code,
                    isActive = Unit.isActive
                };
            }
        }

        public static UnitDetailsLookUpModel Create(Unit Unit)
        {
            return Projection.Compile().Invoke(Unit);
        }
    }
}
