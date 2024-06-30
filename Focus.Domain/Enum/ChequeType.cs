using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Focus.Domain.Enum
{
    public enum ChequeType
    {
        [Display(Name = "Normal")]
        Normal = 1,
        [Display(Name = "Advance")]
        Advance = 2,
        [Display(Name = "Security")]
        Security = 3,
        [Display(Name = "Guarantee")]
        Guarantee = 4,
    }
}
