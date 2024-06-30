using Focus.Business.Reports.ProductLedgerReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.Reports.ProductComparisionReport
{
    public class ComparisionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal InventoryBalance { get; set; }
        public decimal CogsBalance { get; set; }
        public decimal SaleBalance { get; set; }
        public Guid? BranchId { get; set; }
        public ICollection<ComparisonDetailModel> ComparisonDetailModels { get; set; }


    }
}
