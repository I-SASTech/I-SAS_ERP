using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enums
{
  public  enum Print
    {
        [Display(Name = "A4")]
        A4 = 1,
        [Display(Name = "Thermal")]
        Thermal = 2,
      
    }
}
