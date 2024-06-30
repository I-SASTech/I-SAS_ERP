using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Focus.Domain.Enum
{
    public enum DuringSaleClose
    {
        [Display(Name = "Sale Close")]
        SaleClose = 1,
        [Display(Name = "Sale Transfer")]
        SaleTransfer = 2,
    }
}
