using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class SaleOrderConfiguration : IEntityTypeConfiguration<SaleOrder>
    {
        public void Configure(EntityTypeBuilder<SaleOrder> builder)
        {
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.RegistrationNo).IsRequired();

            builder.HasOne(s => s.Customer)
                .WithMany(ad => ad.SaleOrders)
                .HasForeignKey(ad => ad.CustomerId);
            builder.HasOne(s => s.Logistic)
                .WithMany(ad => ad.SaleOrders)
                .HasForeignKey(ad => ad.LogisticId);
            builder.HasOne(s => s.OrderType)
            .WithMany(ad => ad.OrderTypes)
            .HasForeignKey(ad => ad.OrderTypeId);

            builder.HasOne(s => s.Incoterms)
          .WithMany(ad => ad.Incoterms)
          .HasForeignKey(ad => ad.IncotermsId);

            builder.HasOne(s => s.Commodity)
          .WithMany(ad => ad.Commodities)
          .HasForeignKey(ad => ad.CommodityId);
            
            builder.HasOne(s => s.TaxRate)
          .WithMany(ad => ad.SaleOrders)
          .HasForeignKey(ad => ad.TaxRateId);
            
        }
    }
}
