using Focus.Domain.Entities;
using System;
using System.Linq;

namespace Focus.Business.SaleOrders.Queries.NetAmount
{
    public class TotalAmount
    {
        public decimal NetAmount { get; set; }

        public decimal CalculateTotalAmount(SaleOrder invoice)
        {
            NetAmount = !invoice.IsDiscountOnTransaction ? (((invoice.SaleOrderItems.Count(x => x.IsFree == false) > 0 ?
                            invoice.SaleOrderItems.Where(x => x.IsFree == false).Sum(x => (x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل") ? ((x.Discount == 0 ? ((x.Quantity * x.UnitPrice) - (x.FixDiscount)) : ((x.Quantity * x.UnitPrice) - ((x.Quantity * x.UnitPrice) * x.Discount / 100))) * (100 + x.TaxRate.Rate) / 100) :
                            (x.Discount == 0 ? ((x.Quantity * x.UnitPrice) - (x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100))) : ((x.Quantity * x.UnitPrice) - ((x.Quantity * x.UnitPrice) * x.Discount / 100)))) : 0) + invoice.Discount))
                            : Math.Round(invoice.SaleOrderItems.Where(x => !x.IsFree).Sum(x => x.UnitPrice * x.Quantity) + ((invoice.TaxMethod == "Exclusive" || invoice.TaxMethod == "غير شامل") ? CalculateTotalVat(invoice) : 0) - CalculateTransactionLevelDiscount(invoice) + invoice.Discount, 2);


            return NetAmount;
        }

        public decimal CalculateTotalVat(SaleOrder invoice)
        {
            var sumQuantity = invoice.SaleOrderItems.Sum(product => product.IsFree ? 0 : product.Quantity);
            var total = invoice.SaleOrderItems.Sum(prod => (prod.IsFree ? 0 : ((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? ((((prod.Quantity * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (prod.IsFree ? 0 : (invoice.IsBeforeTax ? (((prod.Quantity * prod.UnitPrice) * invoice.TransactionLevelDiscount) / 100) : 0))) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
           ((((prod.Quantity * prod.UnitPrice) - prod.FixDiscount - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (prod.IsFree ? 0 : (invoice.IsBeforeTax && !invoice.IsFixed && invoice.IsDiscountOnTransaction ? (((prod.Quantity * prod.UnitPrice) * invoice.TransactionLevelDiscount) / 100) : (invoice.IsBeforeTax && invoice.IsFixed && invoice.IsDiscountOnTransaction ? (invoice.TransactionLevelDiscount / sumQuantity * prod.Quantity) : 0)))) * prod.TaxRate.Rate) / 100)));

            return total;
        }
        public decimal CalculateTransactionLevelDiscount(SaleOrder sale)
        {
            var sumQuantity = sale.SaleOrderItems.Sum(product => product.IsFree ? 0 : product.Quantity);
            var total = (sale.IsBeforeTax && sale.IsDiscountOnTransaction) ? (sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? (sale.TransactionLevelDiscount * (sale.SaleOrderItems.Sum(x => x.IsFree ? 0 : (x.UnitPrice * x.Quantity))) / 100) : (sale.IsFixed ? sale.TransactionLevelDiscount : sale.TransactionLevelDiscount * sale.SaleOrderItems.Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) / 100) : (sale.IsFixed ? sale.TransactionLevelDiscount : sale.TransactionLevelDiscount * (sale.SaleOrderItems.Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) + ((sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? 0 : CalculateTotalVat(sale))) / 100);

            return total;
        }
    }
}
