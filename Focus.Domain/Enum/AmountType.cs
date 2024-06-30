using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
    public enum AmountType
    {
        [Display(Name = "% of Salary")]
        PercentageOfSalary = 1,
        [Display(Name = "Fixed")]
        Fixed = 2
    }
}
