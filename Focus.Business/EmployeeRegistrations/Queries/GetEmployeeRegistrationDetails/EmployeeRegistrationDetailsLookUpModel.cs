using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using Focus.Business.Attachments.Commands;

namespace Focus.Business.EmployeeRegistrations.Queries.GetEmployeeRegistrationDetails
{
    public class EmployeeRegistrationDetailsLookUpModel
    {
        public Guid Id { get; set; }
        //Personal Information
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string FatherName { get; set; }
        public string ArabicName { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string SalaryType { get; set; }
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
        public bool IsActive { get; set; }

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
        public DateTime? MedicalPolicExpiryDate { get; set; }
        public string CNIC { get; set; }
        public string Photo { get; set; }
        public string DrivingLicense { get; set; }
        public string Passport { get; set; }
        public Guid? DesignationId { get; set; }
        public Guid? DepartmentId { get; set; }
        public string EmployeeType { get; set; }
        public List<EmployeeRegistrationAttachment> EmployeeAttachments { get; set; }
        public string NationalOrForeign { get; set; }
        public string SpouseName1 { get; set; }
        public string SpouseName2 { get; set; }
        public string SpouseName3 { get; set; }
        public string SpouseName4 { get; set; }
        public bool TemporaryCashReceiver { get; set; }
        public bool TemporaryCashIssuer { get; set; }
        public bool TemporaryCashRequester { get; set; }
        public decimal Days { get; set; }
        public decimal Limit { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public decimal? PerDayWorkHour { get; set; }
        public string Reason { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string IbanNumber { get; set; }
        public string AccountNumber { get; set; }
        public string BranchName { get; set; }
        public string BankAddress { get; set; }
        public string AccountType { get; set; }
        public string EmployeeAccess { get; set; }
        public string DesigantionName { get; internal set; }
        public string DepartmentName { get; internal set; }
    }
}
