using System;
using System.Linq.Expressions;
using Focus.Domain.Entities;

namespace Focus.Business.HR.Payroll.AllowanceTypes.Queries.GetAllowanceTypeDetails
{
    public class AllowanceTypeDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public static Expression<Func<AllowanceType, AllowanceTypeDetailsLookUpModel>> Projection
        {
            get
            {
                return allowanceType => new AllowanceTypeDetailsLookUpModel
                {
                    Id = allowanceType.Id,
                    Name = allowanceType.Name,
                    NameArabic = allowanceType.NameArabic,
                    Description = allowanceType.Description,
                    IsActive = allowanceType.IsActive
                };
            }
        }

        public static AllowanceTypeDetailsLookUpModel Create(AllowanceType allowanceType)
        {
            return Projection.Compile().Invoke(allowanceType);
        }
    }
}
