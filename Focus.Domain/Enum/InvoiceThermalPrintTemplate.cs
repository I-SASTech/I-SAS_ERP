using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
    public enum InvoiceThermalPrintTemplate
    {
        [Display(Name = "Default")]
        Default = 1,
        [Display(Name = "PkTemplate1")]
        PkTemplate1 = 2, 
        [Display(Name = "PkTemplate2")]
        PkTemplate2 = 3,
        [Display(Name = "RetailSaTemplate1")]
        RetailSaTemplate1 = 4,
        [Display(Name = "RetailSaTemplate2")]
        RetailSaTemplate2 = 5,
       
    }
}
