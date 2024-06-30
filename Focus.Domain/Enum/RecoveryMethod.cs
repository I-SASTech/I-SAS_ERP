using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Focus.Domain.Enum
{
    public enum RecoveryMethod
    {
        [Display(Name = "Salary")]
        Salary = 1,
        [Display(Name = "Cash")]
        Cash = 2,
       
    }
}
