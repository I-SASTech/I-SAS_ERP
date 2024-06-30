using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Focus.Domain.Enum
{
    public enum GroupType
    {
        [Display(Name = "Basic")]
        Basic = 1,
        [Display(Name = "Advance")]
        Advance = 2,
        [Display(Name = "Premium")]
        Premium = 3,
        [Display(Name = "Customize")]
        Customize = 4,
    }
}
