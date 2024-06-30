using Focus.Domain.Entities;
using System.Collections.Generic;

namespace Focus.Business.MobileOrders.Queries
{
    public class TotalAmount
    {
        public decimal NetAmount { get; set; }

        public decimal CalculateTotalAmount(List<MobileOrderItem> purchaseItem )
        {
            foreach(var item in purchaseItem)
            {

              
             var   AmountWithDiscount = (item.Price*item.Quantity) ;
              
                NetAmount += AmountWithDiscount;

            }

            return NetAmount;
        }
    }
}
