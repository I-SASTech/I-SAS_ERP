using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
    public enum TaxPlan
    {
        [Display(Name = "Taxable")]
        Taxable = 1,
        [Display(Name = "Non Taxable")]
        NonTaxable = 2
    }
}
