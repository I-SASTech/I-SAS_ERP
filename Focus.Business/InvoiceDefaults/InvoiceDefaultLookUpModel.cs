using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.InvoiceDefaults
{
    public class InvoiceDefaultLookUpModel
    {
        public Guid Id { get; set; }
        public bool IsSalePrice { get; set; }
        public bool IsCustomerPrice { get; set; }
        public bool IsSalePriceLabel { get; set; }
        public bool IsCustomerPriceLabel { get; set; }
    }
}
