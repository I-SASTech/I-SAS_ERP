using System;
using Focus.Business.Sales.Queries.GetSaleDetail;
using System.Text;

namespace Focus.Business.ProductionModule.GatePasses
{
    public class GatePassesItemLookUpModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public bool IsActive { get; set; }

        public ProductDropdownLookUpModel Product { get; set; }



        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Guid GatePassId { get; set; }

    }
}
