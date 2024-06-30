
using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
    public enum StockAdjustmentType
    {
        [Display(Name = "StockIn")]
        StockIn = 5,
        [Display(Name = "StockOut")]
        StockOut = 6,
        [Display(Name = "StockProduction")]
        StockProduction = 15,
    }
}
