using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Focus.Business.PurchaseOrders.Queries.NetAmount
{
    public class TotalAmount
    {
        public decimal NetAmount { get; set; }

        public decimal CalculateTotalAmount(PurchaseOrder purchase)
        {
            NetAmount = !purchase.IsDiscountOnTransaction ? (((
                           purchase.PurchaseOrderItems.Where(x => x.PurchaseOrderVersionId == purchase.PurchaseOrderVersions?.LastOrDefault()?.Id).Sum(x => (x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل") ? ((x.Discount == 0 ? ((x.Quantity * x.UnitPrice) - (x.FixDiscount)) : ((x.Quantity * x.UnitPrice) - ((x.Quantity * x.UnitPrice) * x.Discount / 100))) * (100 + x.TaxRate.Rate) / 100) :
                           (x.Discount == 0 ? ((x.Quantity * x.UnitPrice) - (x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100))) : ((x.Quantity * x.UnitPrice) - ((x.Quantity * x.UnitPrice) * x.Discount / 100))))) + purchase.Discount))
                           : Math.Round(purchase.PurchaseOrderItems.Where(x => x.PurchaseOrderVersionId == purchase.PurchaseOrderVersions?.LastOrDefault()?.Id).Sum(x => x.UnitPrice * x.Quantity) + ((purchase.TaxMethod == "Exclusive" || purchase.TaxMethod == "غير شامل") ? CalculateTotalVatPo(purchase) : 0) - CalculateTransactionLevelDiscountPo(purchase) + purchase.Discount, 2);


            return NetAmount;
        }
        public decimal CalculateTotalVatPo(PurchaseOrder purchase)
        {
            var sumQuantity = purchase.PurchaseOrderItems.Sum(product => product.Quantity);
            var total = purchase.PurchaseOrderItems.Where(x => x.PurchaseOrderVersionId == purchase.PurchaseOrderVersions?.LastOrDefault()?.Id).Sum(prod => (((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? ((((prod.Quantity * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - ((purchase.IsBeforeTax ? (((prod.Quantity * prod.UnitPrice) * purchase.TransactionLevelDiscount) / 100) : 0))) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
            ((((prod.Quantity * prod.UnitPrice) - prod.FixDiscount - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - ((purchase.IsBeforeTax && !purchase.IsFixed && purchase.IsDiscountOnTransaction ? (((prod.Quantity * prod.UnitPrice) * purchase.TransactionLevelDiscount) / 100) : (purchase.IsBeforeTax && purchase.IsFixed && purchase.IsDiscountOnTransaction ? (purchase.TransactionLevelDiscount / sumQuantity * prod.Quantity) : 0)))) * prod.TaxRate.Rate) / 100)));

            return total;
        }
        public decimal CalculateTransactionLevelDiscountPo(PurchaseOrder purchase)
        {
            var sumQuantity = purchase.PurchaseOrderItems.Sum(product => product.Quantity);
            var total = (purchase.IsBeforeTax && purchase.IsDiscountOnTransaction) ? (purchase.TaxMethod == "Inclusive" || purchase.TaxMethod == "شامل") ? (purchase.TransactionLevelDiscount * (purchase.PurchaseOrderItems.Where(x => x.PurchaseOrderVersionId == purchase.PurchaseOrderVersions?.LastOrDefault()?.Id).Sum(x => (x.UnitPrice * x.Quantity))) / 100) : (purchase.IsFixed ? purchase.TransactionLevelDiscount : purchase.TransactionLevelDiscount * purchase.PurchaseOrderItems.Where(x => x.PurchaseOrderVersionId == purchase.PurchaseOrderVersions?.LastOrDefault()?.Id).Sum(x => x.UnitPrice * x.Quantity) / 100) : (purchase.IsFixed ? purchase.TransactionLevelDiscount : purchase.TransactionLevelDiscount * (purchase.PurchaseOrderItems.Sum(x => x.UnitPrice * x.Quantity) + ((purchase.TaxMethod == "Inclusive" || purchase.TaxMethod == "شامل") ? 0 : CalculateTotalVatPo(purchase))) / 100);

            return total;
        }
    }
}
