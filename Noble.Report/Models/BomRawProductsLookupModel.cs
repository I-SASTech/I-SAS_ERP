using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noble.Report.Models
{
    public class BomRawProductsLookupModel
    {
        public Guid Id { get; set; }
        public Guid SaleOrderId { get; set; }
        public decimal Quantity { get; set; }
        public decimal CurrentQuantity { get; set; }
        public decimal salePrice { get; set; }
        public Guid ProductId { get; set; }
        public Guid BomSaleOrderItemId { get; set; }
        public ProductDropdownLookUpModel Product { get; set; }
        public decimal? UnitPerPack { get; set; }
        public string UnitName { get; set; }
    }
}
