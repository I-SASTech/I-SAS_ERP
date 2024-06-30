using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;

namespace Focus.Business.EmployeeRegistrations.Queries.GetEmployeeRegistrationList
{
    public class EmployeeRegistrationLookUpModel : IMapFrom<Domain.Entities.EmployeeRegistration>
    {
        public Guid Id { get; set; }
        //Personal Information
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string Gender { get; set; }
        public string MartialStatus { get; set; }
        public string Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string MobileNo { get; set; }
        public string OtherContact { get; set; }
        public string Email { get; set; }
        public string BloodGroup { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        //Reference Contact Information
        public string PrimaryNameOfPerson { get; set; }
        public string PrimaryRelation { get; set; }
        public string PrimaryContactNumber { get; set; }
        public string PrimaryReferenceEmail { get; set; }
        public string SecondaryNameOfPerson { get; set; }
        public string SecondaryRelation { get; set; }
        public string SecondaryContactNumber { get; set; }
        public string SecondaryReferenceEmail { get; set; }

        //Home Contact Information
        public string HomePersonName { get; set; }
        public string HomeRelation { get; set; }
        public string HomeContactNumber { get; set; }
        public string HomeReferenceEmail { get; set; }
        public string HomeCity { get; set; }
        public string HomeCountry { get; set; }

        //Legal Identity Information
        public string IDNumber { get; set; }
        public string IDType { get; set; }
        public string Title { get; set; }
        public string ExpiryDate { get; set; }
        public string PassportNumber { get; set; }
        public string PassportIssueDate { get; set; }
        public string PassportExpiryDate { get; set; }
        public string PassportIssuingAuthority { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public string DrivingLicenseType { get; set; }
        public string DrivingExpiryDate { get; set; }
        public string DrivingIssuingAuthority { get; set; }
        public string MedicalPolicNumber { get; set; }
        public string MedicalPolicType { get; set; }
        public string MedicalPolicProvider { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public DateTime? MedicalPolicExpiryDate { get; set; }
        public bool TemporaryCashReceiver { get; set; }
        public bool TemporaryCashIssuer { get; set; }
        public bool TemporaryCashRequester { get; set; }
        public decimal Days { get; set; }
        public decimal Limit { get; set; }
        public virtual ICollection<EmployeeSalary> EmployeeSalaries { get; set; }

        public bool IsActive { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? DesignationId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.EmployeeRegistration, EmployeeRegistrationLookUpModel>();
        }
    }
}
