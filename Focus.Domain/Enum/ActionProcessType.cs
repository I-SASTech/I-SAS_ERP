using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
    public enum ActionProcessType
    {
        [Display(Name = "Sales")]
        Sales = 1,
        [Display(Name = "Purchase")]
        Purchase = 2
    }
}
