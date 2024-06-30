using System;
using Focus.Business.Sales.Queries.GetSaleDetail;
using System.Text;

namespace Focus.Business.ProductionModule.Processes
{
    public class ProcessItemLookUpModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProcessId { get; set; }
        public bool IsActive { get; set; }

        public ProductDropdownLookUpModel Product { get; set; }

    }
}
