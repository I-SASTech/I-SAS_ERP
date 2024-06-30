using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
    public enum SupplierType
    {
        [Display(Name = "Wholesaler")]
        WholeSaler = 1,
        [Display(Name = "Retailer")]
        Retailer = 2,
        [Display(Name = "Dealer")]
        Dealer = 3,
        [Display(Name = "Distributor")]
        Distributor = 4,
        [Display(Name = "WholesalerAndRetailer")]
        WholesalerAndRetailer = 5,

        [Display(Name = "InternationalSupplier")]
        InternationalSupplier = 6,

        [Display(Name = "InternationalManufacturers")]
        InternationalManufacturers = 7,

        [Display(Name = "InternationalAgentExporter")]
        InternationalAgentExporter = 8,
    }
}