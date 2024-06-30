using Focus.Domain.Entities;
using System.Collections.Generic;

namespace Focus.Business.Purchases.Queries.NetAmount
{
    public class TotalAmount
    {
        public decimal NetAmount { get; set; }

        public decimal CalculateTotalAmount(List<PurchaseItem> purchaseItem, string taxMethod)
        {
            foreach (var item in purchaseItem)
            {
                var qty = ((item.HighQty ?? 0) * (item.UnitPerPack ?? 0)) + item.Quantity;
                var discount = item.Discount == 0 ? item.FixDiscount * qty : (item.UnitPrice * qty * item.Discount) / 100;
                var amountWithDiscount = (item.UnitPrice * qty) - discount;
                //var vat = (AmountWithDiscount* item.TaxRate.Rate) / 100;
                var vat = (taxMethod == "Exclusive" || taxMethod == "غير شامل") ? ((amountWithDiscount * item.TaxRate.Rate) / 100) : 0;
                NetAmount += amountWithDiscount + vat;

            }

            return NetAmount;
        }
    }
}
