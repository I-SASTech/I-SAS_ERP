using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
    public enum StockRequestStatus
    {
        [Display(Name = "Default")]
        Default = 0,
        [Display(Name = "Partially")]
        Partially = 1,
        [Display(Name = "Fully")]
        Fully = 2,
    }
}
