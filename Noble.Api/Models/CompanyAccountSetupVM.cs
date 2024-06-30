using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class CompanyAccountSetupVM
    {
        public Guid? Id { get; set; }
        public Guid? InventoryAccountId { get; set; }
        public Guid? VatPayableAccountId { get; set; }
        public Guid? VatReceiableAccountId { get; set; }
        public Guid? SaleAccountId { get; set; }
        public Guid? DiscountPayableAccountId { get; set; }
        public Guid? DiscountReceivableAccountId { get; set; }
        public Guid? FreeofCostAccountId { get; set; }
        public Guid? StockInAccountId { get; set; }
        public Guid? StockOutAccountId { get; set; }
        public Guid? BundleAccountId { get; set; }
        public Guid? PromotionAccountId { get; set; }
        public Guid? BankId { get; set; }
        public Guid? CashId { get; set; }
        public Guid? TaxRateId { get; set; }
        public string CurrencyId { get; set; }
        public string BarcodeType { get; set; }
        public string TaxMethod { get; set; }
    }
}
