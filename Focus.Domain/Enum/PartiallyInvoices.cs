using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
    public enum PartiallyInvoices
    {
        [Display(Name = "UnPaid")]
        UnPaid = 1,
        [Display(Name = "Partially")]
        Partially = 2,
        [Display(Name = "Fully")]
        Fully = 3,
        [Display(Name = "Return")]
        Return = 4,
    }
}
