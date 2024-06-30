using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Focus.Domain.Enum
{
   public enum EmployeeType
    {
        [Display(Name = "Default")]
        Default = 0,
        [Display(Name = "Manager")]
        Manager = 1,
        [Display(Name = "Contractor")]
        Contractor = 2,
        [Display(Name = "Supervisor")]
        Supervisor = 3,
        [Display(Name = "Admin")]
        Admin = 4,
        [Display(Name = "Labour")]
        Labour = 5,
        [Display(Name = "Permanent")]
        Permanent = 6,
        [Display(Name = "Probation")]
        Probation = 7,
        [Display(Name = "Internee")]
        Internee = 8,
        [Display(Name = "Temporary")]
        Temporary = 9,
    }
}
