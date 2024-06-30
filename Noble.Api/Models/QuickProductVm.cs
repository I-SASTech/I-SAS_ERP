using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class QuickProductVm
    {
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public Guid? TaxRateId { get; set; }
        public Guid? CategoryIdQuick { get; set; }
        public Guid? UnitId { get; set; }
        public string LevelOneUnit { get; set; }
        public string TaxMethod { get; set; }
        public string Barcode { get; set; }
        public decimal SalePrice { get; set; }
        public bool ServiceItem { get; set; }
    }
}
