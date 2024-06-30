using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Focus.Domain.Enum
{
    public enum TerminalType
    {
        [Display(Name = "Terminal")]
        Terminal = 1,
        [Display(Name = "CashCounter")]
        CashCounter = 2
    }
}
