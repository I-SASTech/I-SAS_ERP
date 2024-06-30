using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Focus.Domain.Entities;

namespace Focus.Business.Departments.Queries.GetDepartmentDetails
{
    public class DepartmentDetailLookUpModel
    {
        public Guid Id { get; set; }
        public Guid? BankId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string NameArabic { get; set; }

        public static Expression<Func<Department, DepartmentDetailLookUpModel>> Projection
        {
            get
            {
                return department => new DepartmentDetailLookUpModel
                {
                    Id = department.Id,
                    Name = department.Name,
                    BankId = department.BankId,
                    NameArabic = department.NameArabic,
                    Description = department.Description,
                    Code = department.Code,
                    IsActive = department.IsActive,
                    
                };
            }
        }

        public static DepartmentDetailLookUpModel Create(Department department)
        {
            return Projection.Compile().Invoke(department);
        }
    }
}
