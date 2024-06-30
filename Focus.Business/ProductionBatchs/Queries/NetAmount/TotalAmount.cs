using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.ProductionBatchs.Queries.NetAmount
{
    public class TotalAmount
    {
        public decimal NetAmount { get; set; }

        public decimal CalculateTotalAmount(List<ProductionBatchItem> purchaseItem)
        {
            foreach (var item in purchaseItem)
            {
                if(item.Waste!=0)
                {
                    var lineTotal = ((item.HighQuantity * item.UnitPerPack) +(item.Quantity)) - ((((item.HighQuantity * item.UnitPerPack) + (item.Quantity)) * item.Waste) / 100);
                    NetAmount += lineTotal;
                }
                else
                {
                    var lineTotal = ((item.HighQuantity * item.UnitPerPack) + (item.Quantity));
                    NetAmount += lineTotal;
                }
                

            }

            return NetAmount;
        }
    }
}
