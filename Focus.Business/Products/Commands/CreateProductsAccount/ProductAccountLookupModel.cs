using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.Products.Commands.CreateProductsAccount
{
    public class ProductAccountLookupModel
    {
        public Guid RemoveId { get; set; }
        public Guid DocumentId { get; set; }
        public decimal InvDebit { get; set; }
        public decimal InvCredit { get; set; }
        public decimal COGSDebit { get; set; }
        public decimal COGSCredit { get; set; }
        public decimal SaleDebit { get; set; }
        public decimal SaleCredit { get; set; }
    }
}
