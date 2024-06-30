using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class RegisterVm
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid? EmployeeId { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public string RoleName { get; set; }
    }
}
