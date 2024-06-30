using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
    class CompanyAccountSetupConfiguration : IEntityTypeConfiguration<CompanyAccountSetup>
    {
        public void Configure(EntityTypeBuilder<CompanyAccountSetup> builder)
        {
            builder.Property(d => d.InventoryAccountId).IsRequired(false);
            builder.Property(d => d.VatPayableAccountId).IsRequired(false);
            builder.Property(d => d.VatReceiableAccountId).IsRequired(false);
            builder.Property(d => d.DiscountPayableAccountId).IsRequired(false);
            builder.Property(d => d.DiscountReceivableAccountId).IsRequired(false);
            builder.Property(d => d.FreeofCostAccountId).IsRequired(false);
            builder.Property(d => d.StockInAccountId).IsRequired(false);
            builder.Property(d => d.StockOutAccountId).IsRequired(false);
            builder.Property(d => d.BundleAccountId).IsRequired(false);
            builder.Property(d => d.PromotionAccountId).IsRequired(false);
            builder.Property(d => d.SaleAccountId).IsRequired(false);
            builder.Property(d => d.BankId).IsRequired(false);
            builder.Property(d => d.CashId).IsRequired(false);
            builder.Property(d => d.TaxRateId).IsRequired(false);
            builder.Property(d => d.BarcodeType).IsRequired(false);
        }
    }
}
