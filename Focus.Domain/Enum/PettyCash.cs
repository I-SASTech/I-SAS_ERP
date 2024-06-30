using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Focus.Domain.Enum
{
    public enum PettyCash
    {
        [Display(Name = "Default")]
        Default = 0, 
        [Display(Name = "Temporary")]
        Temporary  = 1,
        [Display(Name = "General")]
        General  = 2,
        [Display(Name = "Advance")]
        Advance = 3,
    }
    
}
