using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Noble.Report.Enums
{
    public enum PartiallyInvoices
    {
        [Display(Name = "UnPaid")]
        UnPaid = 1,
        [Display(Name = "Partially")]
        Partially = 2,
        [Display(Name = "Fully")]
        Fully = 3,
    }
}