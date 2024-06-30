using System;

namespace Focus.Business.Models
{
    public class UserDetailDto
    {
        public string Code { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int AcademyId { get; set; }
        public string RoleId { get; set; }
        //public List<RolesDto> Roles { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string RoleName { get; set; }
        public string DocumentType { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? BankId { get; set; }
        //public List<CompanyDto> Companies { get; set; }
        public string Action { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsProceed { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? TerminalId { get; set; }
        public Guid? OnlineTerminalId { get; set; }
        public string EmployeeNo { get; set; }
        public bool TemporaryCashReceiver { get; set; }
        public bool TemporaryCashIssuer { get; set; }
        public bool TemporaryCashRequester { get; set; }
        public decimal Days { get; set; }
        public decimal Limit { get; set; }
    }
}
