using System;
using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;

namespace Focus.Business.Departments.Queries.GetDepartmentList
{
    public class DepartmentLookUpModel:IMapFrom<Department>
    {
        public Guid Id { get; set; }
        public Guid? BankId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string NameArabic { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Department, DepartmentLookUpModel>();
        }
    }
}
