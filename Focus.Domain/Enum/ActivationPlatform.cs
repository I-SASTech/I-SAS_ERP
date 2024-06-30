using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Focus.Domain.Enum
{
    public enum ActivationPlatform
    {
        [Display(Name = "Offline")]
        Offline = 1,
        [Display(Name = "Online")]
        Online = 2,
        [Display(Name = "Both")]
        Both = 3
    }
}
