using Focus.Business.PurchaseOrders.Queries.NetAmount;
using System;
using System.Linq;

namespace Focus.Business.MobileOrders.Queries.GetMobileOrderList
{
    public class MobileOrderLookUpModel
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string OrderNo { get; set; }
        public string OrderDate { get; set; }
        public decimal NetAmount { get; set; }
        public static MobileOrderLookUpModel Create(Focus.Domain.Entities.MobileOrder mobileOrder)
        {
            var netAmount = new TotalAmount();
            var lookUpModel = new MobileOrderLookUpModel
            {
                Id = mobileOrder.Id,
                CustomerName = mobileOrder.Name,
                NetAmount = netAmount.CalculateTotalAmount(mobileOrder.MobileOrderItems.ToList()),
                OrderNo = mobileOrder.OrderNo,

                OrderDate = mobileOrder.OrderDate.ToString("dd/MM/yyyy")
            };
            return lookUpModel;
        }
    }
}
