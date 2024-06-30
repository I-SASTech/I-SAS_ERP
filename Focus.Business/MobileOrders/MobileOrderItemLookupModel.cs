using System;

namespace Focus.Business.MobileOrders
{
    public class MobileOrderItemLookupModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Guid MobileOrderId { get; set; }
    }
}
