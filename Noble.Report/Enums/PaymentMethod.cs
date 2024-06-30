using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Noble.Report.Enums
{
    public enum PaymentMethod
    {
        [Display(Name = "Cheque")]
        Cheque = 1,
        [Display(Name = "Transfer")]
        Transfer = 2,
        [Display(Name = "Deposit")]
        Deposit = 3,
    }
}