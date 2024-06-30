using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Focus.Domain.Enum
{
    public enum CashType
    {
        [Display(Name = "Reserved")]
        Reserved = 1,
        [Display(Name = "Not Reserved")]
        NotReserved = 2,
    }
}
