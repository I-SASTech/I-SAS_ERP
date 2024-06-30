using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Enum
{
    public enum TerminalUserType
    {
       
        [Display(Name = "Online")]
        Online = 1,
        [Display(Name = "Offline")]
        Offline = 2,
        [Display(Name = "Both")]
        Both = 3,
    }
}
