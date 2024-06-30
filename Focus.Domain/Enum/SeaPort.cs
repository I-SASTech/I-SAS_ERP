using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
   public enum SeaPort
    {
        [Display(Name = "DryPort")]
        DryPort = 1,
        [Display(Name = "SeaPort")]
        SeaPort = 2,
        [Display(Name = "AirPort")]
        AirPort = 3,
        [Display(Name = "DryPortSeaPort")]
        DryPortSeaPort = 4, 
        [Display(Name = "DryPortAirPort")]
        DryPortAirPort = 5,
        [Display(Name = "SeaPortAirPort")]
        SeaPortAirPort = 6, 
        [Display(Name = "All")]
        All = 7,

    }
}
