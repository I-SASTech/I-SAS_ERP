using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Focus.Domain.Enum
{
    public enum ApprovaType
    {
        [Display(Name = "Must Be Approved")]
        MustBeApproved = 1,
        [Display(Name = "Can Be Approved ")]
        CanBeApproved = 2, 
        [Display(Name = "May Be Approved ")]
        MayBeApproved = 3,
    }
}
