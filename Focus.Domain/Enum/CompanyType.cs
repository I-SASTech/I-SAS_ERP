using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
   public enum CompanyType
    {
        [Display(Name = "Trail")]
        Trail = 1,
        [Display(Name = "Basic")]
        Basic = 2,
        [Display(Name = "Standard")]
        Standard = 3,
        [Display(Name = "Advanced")]
        Advanced = 4,
    }
}
