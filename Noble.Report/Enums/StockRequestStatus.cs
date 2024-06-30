using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Noble.Report.Enums
{
    public enum StockRequestStatus
    {
        [Display(Name = "Partially")]
        Partially = 0,
        [Display(Name = "Fully")]
        Fully = 1,
    }
}