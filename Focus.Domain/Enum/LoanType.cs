using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Focus.Domain.Enum
{
    public enum LoanType
    {
        [Display(Name = "Loan")]
        Loan = 1,
        [Display(Name = "Advance")]
        Advance = 2,
        [Display(Name = "Provident Fund")]
        ProvidentFund = 3
    }
}
