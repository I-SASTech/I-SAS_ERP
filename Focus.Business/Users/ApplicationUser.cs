using System;
using Focus.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Focus.Business.Users
{
  public  class ApplicationUser: IdentityUser
    {
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? TerminalId { get; set; }
        public Guid? OnlineTerminalId { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public bool IsProceed { get; set; }
        public string Password { get; set; }
        public string DocumentType { get; set; }
        public Guid? BankId { get; set; }
        public virtual Bank Bank { get; set; }

    }
}
