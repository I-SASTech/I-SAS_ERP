using System.ComponentModel.DataAnnotations;


namespace Focus.Domain.Enum
{
    public enum HolidayType
    {
        [Display(Name = "National")]
        National = 1,
        [Display(Name = "Guested")]
        Guested = 2,
    }
}
