using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
    public enum Logistics
    {
        [Display(Name = "Transporter")]
        Transporter = 1,
        [Display(Name = "ClearanceAgent")]
        ClearanceAgent = 2,
        [Display(Name = "ShippingLinear")]
        ShippingLinear = 3,
    }
}
