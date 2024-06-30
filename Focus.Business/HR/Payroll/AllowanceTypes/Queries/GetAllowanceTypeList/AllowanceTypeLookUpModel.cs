using System;
using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;

namespace Focus.Business.HR.Payroll.AllowanceTypes.Queries.GetAllowanceTypeList
{
    public class AllowanceTypeLookUpModel : IMapFrom<AllowanceType>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AllowanceType, AllowanceTypeLookUpModel>();
        }
    }
}
