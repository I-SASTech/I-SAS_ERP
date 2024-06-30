using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.Brands.Queries.GetBrandDetails
{
    public class CompanyAccountSetupDetailsLookUpModel
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

        public static Expression<Func<CompanyAccountSetup, CompanyAccountSetupDetailsLookUpModel>> Projection
        {
            get
            {
                return companyAccountSetup => new CompanyAccountSetupDetailsLookUpModel
                {
                    Id = companyAccountSetup.Id,
                    InventoryAccountId = companyAccountSetup.InventoryAccountId,
                    VatPayableAccountId = companyAccountSetup.VatPayableAccountId,
                    VatReceiableAccountId = companyAccountSetup.VatReceiableAccountId,
                    SaleAccountId = companyAccountSetup.SaleAccountId,
                    DiscountPayableAccountId = companyAccountSetup.DiscountPayableAccountId,
                    DiscountReceivableAccountId = companyAccountSetup.DiscountReceivableAccountId,
                    FreeofCostAccountId = companyAccountSetup.FreeofCostAccountId,
                    StockInAccountId = companyAccountSetup.StockInAccountId,
                    StockOutAccountId = companyAccountSetup.StockOutAccountId,
                    BundleAccountId = companyAccountSetup.BundleAccountId,
                    PromotionAccountId = companyAccountSetup.PromotionAccountId,
                    BankId = companyAccountSetup.BankId,
                    CashId = companyAccountSetup.CashId,
                    TaxRateId = companyAccountSetup.TaxRateId,
                    BarcodeType = companyAccountSetup.BarcodeType,
                    CurrencyId = companyAccountSetup.CurrencyId,
                    TaxMethod = companyAccountSetup.TaxMethod,
                };
            }
        }

        public static CompanyAccountSetupDetailsLookUpModel Create(CompanyAccountSetup companyAccountSetup)
        {
            return Projection.Compile().Invoke(companyAccountSetup);
        }
    }
}
