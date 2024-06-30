
using System;
using Focus.Business.Sales.Queries.GetSaleDetail;

namespace Focus.Business.ReparingOrders.Commands.AddReparingOrder
{
   public class ReparingOrderItemLookUpModel
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid? ProductId { get; set; }

        public ProductDropdownLookUpModel Product { get; set; }
        
    }
}
