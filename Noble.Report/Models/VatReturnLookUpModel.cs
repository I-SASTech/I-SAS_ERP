using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noble.Report.Models
{
    public class VatReturnLookUpModel
    {
        public decimal TotalPurchase { get; set; }
        public decimal TotalPurchaseVat { get; set; }
        public decimal TotalGrossPurchase { get; set; }
        public decimal TotalSale { get; set; }
        public decimal TotalSaleVat { get; set; }
        public decimal TotalGrossSale { get; set; }
    }
}
