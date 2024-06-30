using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
    public enum SaleHoldTypes
    {
        [Display(Name = "Default")]
        Default = 0, 
        [Display(Name = "Hold")]
        Hold = 1,
        [Display(Name = "Deleted")]
        Deleted = 2,
        [Display(Name = "Used")]
        Used = 3
    }
}
