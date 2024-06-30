using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Focus.Domain.Entities;

namespace Focus.Business.Designations.Queries.GetDesignationDetails
{
    public class DesignationDetailLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string NameArabic { get; set; }

        public static Expression<Func<Designation, DesignationDetailLookUpModel>> Projection
        {
            get
            {
                return designation => new DesignationDetailLookUpModel
                {
                    Id = designation.Id,
                    Name = designation.Name,
                    NameArabic = designation.NameArabic,
                    Code = designation.Code,
                    IsActive = designation.IsActive,
                    Description = designation.Description
                };
            }
        }

        public static DesignationDetailLookUpModel Create(Designation designation)
        {
            return Projection.Compile().Invoke(designation);
        }
    }
}
