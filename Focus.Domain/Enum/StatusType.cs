using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Focus.Domain.Enum
{
    public enum StatusType
    {
        [Display(Name = "Cashed")]
        Cashed = 1,
        [Display(Name = "Blocked")]
        Blocked = 2,
        [Display(Name = "Cancelled")]
        Cancelled = 3,
        [Display(Name = "Returned")]
        Returned = 4,
    }
}
