using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
    public enum InvoiceA4PrintTemplate
    {
        [Display(Name = "Default")]
        Default = 1,
        [Display(Name = "Template1")]
        Template1 = 2,
        [Display(Name = "Template2")]
        Template2 = 3,
        [Display(Name = "Template3")]
        Template3 = 4,
        [Display(Name = "Template5")]
        Template5 = 5,
        [Display(Name = "Template8")]
        Template8 = 8,

        [Display(Name = "Template9")]
        Template9 = 9
    }
}
