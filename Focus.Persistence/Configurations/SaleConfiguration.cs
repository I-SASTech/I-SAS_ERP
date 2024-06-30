using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.RegistrationNo).IsRequired();
            builder.Property(x => x.DueDate);
            builder.Property(x => x.WareHouseId).IsRequired();

            builder.HasOne(s => s.Customer)
                .WithMany(ad => ad.Sales)
                .HasForeignKey(ad => ad.CustomerId);

            builder.HasOne(s => s.CashCustomer)
                .WithMany(ad => ad.Sales)
                .HasForeignKey(ad => ad.CashCustomerId);
            builder.HasOne(s => s.Employee)
                .WithMany(ad => ad.Sales)
                .HasForeignKey(ad => ad.EmployeeId);
            builder.HasOne(s => s.Area)
             .WithMany(ad => ad.Sales)
             .HasForeignKey(ad => ad.AreaId);

            builder.HasOne(s => s.OtherCurrency)
                .WithOne(ad => ad.Sale)
                .HasForeignKey<Sale>(ad => ad.OtherCurrencyId);


            builder.HasOne(s => s.SaleOrder)
                .WithMany(ad => ad.Sales)
                .HasForeignKey(ad => ad.SaleOrderId)
                .OnDelete(DeleteBehavior.Restrict);

             builder.HasOne(s => s.Quotation)
                .WithMany(ad => ad.QuotationSales)
                .HasForeignKey(ad => ad.QuotationId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(s => s.Logistic)
                .WithMany(ad => ad.Sales)
                .HasForeignKey(ad => ad.LogisticId);
            
            builder.HasOne(s => s.TaxRate)
                .WithMany(ad => ad.Sales)
                .HasForeignKey(ad => ad.TaxRateId);

            builder.HasOne(s => s.UnRegisteredVAT)
              .WithMany(ad => ad.SaleUnRegisteredTaxes)
              .HasForeignKey(ad => ad.UnRegisteredVatId);

        }
    }
}
