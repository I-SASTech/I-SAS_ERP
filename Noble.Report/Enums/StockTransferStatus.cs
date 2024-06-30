using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Noble.Report.Enums
{
    public enum StockTransferStatus
    {
        [Display(Name = "Default")]
        Default = 0,
        [Display(Name = "Issued")]
        Issued = 1,
        [Display(Name = "Transit")]
        Transit = 2,
        [Display(Name = "Received")]
        Received = 3,
        [Display(Name = "PartiallyReceived")]
        PartiallyReceived = 4
    }
}
