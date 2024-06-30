using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class SalePaymentConfiguration : IEntityTypeConfiguration<SalePayment>
    {
        public void Configure(EntityTypeBuilder<SalePayment> builder)
        {
            builder.Property(x => x.DueAmount).IsRequired().HasColumnType("decimal(18,4)");
            builder.Property(x => x.Received).IsRequired().HasColumnType("decimal(18,4)");
            builder.Property(x => x.Balance).IsRequired().HasColumnType("decimal(18,4)");
            builder.Property(x => x.Change).IsRequired().HasColumnType("decimal(18,4)");
            builder.Property(x => x.DueAmount).IsRequired().HasColumnType("decimal(18,4)");

            builder.HasOne(s => s.PaymentOption)
                .WithMany(ad => ad.SalePayments)
                .HasForeignKey(ad => ad.PaymentOptionId);

            builder.HasOne(s => s.Sale)
                .WithMany(ad => ad.SalePayments)
                .HasForeignKey(ad => ad.SaleId).IsRequired();
        }
    }
}