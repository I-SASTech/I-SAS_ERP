using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Noble.Report.Enums
{
    public enum PettyCash
    {
        [Display(Name = "Temporary")]
        Temporary = 1,
        [Display(Name = "General")]
        General = 2,
        [Display(Name = "Advance")]
        Advance = 3,
    }
}