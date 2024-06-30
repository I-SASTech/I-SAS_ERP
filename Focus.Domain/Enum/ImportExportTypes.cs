
using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
    public enum   ImportExportTypes
    {
        [Display(Name = "StuffingLocation")]
        StuffingLocation = 1,
        [Display(Name = "PortOfLoading")]
        PortOfLoading = 2,
        [Display(Name = "PortOfDestination")]
        PortOfDestination = 3,
        [Display(Name = "OrderType")]
        OrderType = 4,
        [Display(Name = "Service")]
        Service = 5,
        [Display(Name = "Incoterms")]
        Incoterms = 6,
        [Display(Name = "Commodity")]
        Commodity = 7,
        [Display(Name = "Carrier")]
        Carrier = 8,
        [Display(Name = "ExportEXW")]
        ExportEXW = 9,
        [Display(Name = "ImportFOB")]
        ImportFOB = 10,
        [Display(Name = "QuantityContainer")]
        QuantityContainer = 11,
    }
}
