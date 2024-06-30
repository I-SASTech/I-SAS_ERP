using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
    public enum ReparingOrderTypeEnum
    {
        [Display(Name = "WarrantyCategory")]
        WarrantyCategory = 1,
        [Display(Name = "UpsDescription")]
        UpsDescription = 2,
        [Display(Name = "Problem")]
        Problem = 3,
        [Display(Name = "AcessoryIncluded")]
        AcessoryIncluded = 4,
       
       
    }
}
