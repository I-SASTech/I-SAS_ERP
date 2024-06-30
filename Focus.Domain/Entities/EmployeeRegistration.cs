using System;
using System.Collections.Generic;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class EmployeeRegistration : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        //Personal Information
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string FatherName { get; set; }
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
        public string Reason { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string IbanNumber { get; set; }
        public string AccountNumber { get; set; }
        public string BranchName { get; set; }
        public string BankAddress { get; set; }
        public string AccountType { get; set; }
        public string EmployeeAccess { get; set; }

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
        public Guid? PayableAccountId { get; set; }
        public virtual Account PayableAccount { get; set; }
        public Guid? EmployeeAccountId { get; set; }
        public virtual Account EmployeeAccount { get; set; }
        public Guid? DesignationId { get; set; }
        public virtual Designation Designation { get; set; }
        public Guid? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public bool IsActive { get; set; }
        public virtual ICollection<EmployeeRegistrationAttachment> EmployeeAttachments { get; set; }
        public virtual ICollection<LoanPayment> LoanPayments { get; set; }
        public virtual ICollection<ProductionBatch> ProductionBatchs { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<EmployeeSalary> EmployeeSalaries { get; set; }
        public string SalaryType { get; set; }
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
        public virtual ICollection<ProcessContractor> ProcessContractors { get; set; }
        public virtual ICollection<BatchProcessContractor> BatchProcessContractors { get; set; }
        public virtual ICollection<GatePass> GatePasses { get; set; }
        public virtual ICollection<InquiryMeetingAttendant> InquiryMeetingAttendants { get; set; }
        public virtual ICollection<ReparingOrder> ReparingOrders { get; set; }
        public virtual ICollection<Inquiry> Inquiries { get; set; }
        public virtual ICollection<ManualAttendance> ManualAttendances { get; set; }
        public decimal? PerDayWorkHour { get; set; }
        public virtual ICollection<MultiUp> MultiUps { get; set; }
        public virtual ICollection<ShiftAssign> ShiftAssigns { get; set; }
        public virtual ICollection<ShiftEmployeeAssign> ShiftEmployeeAssigns { get; set; }
        public virtual ICollection<ShiftRunEmployee> ShiftRunEmployees { get; set; }
        public virtual ICollection<LeaveGroupEmployee> LeaveGroupEmployees { get; set; }
        public virtual ICollection<LeaveRules> LeaveRules { get; set; }
        public virtual ICollection<PaidTimeOff> PaidLeaveOffs { get; set; }
        public virtual ICollection<ApprovalSystemEmployees> ApprovalSystemEmployees { get; set; }

    }
}
