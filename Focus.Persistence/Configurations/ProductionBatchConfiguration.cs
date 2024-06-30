using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class ProductionBatchConfiguration : IEntityTypeConfiguration<ProductionBatch>
    {
        public void Configure(EntityTypeBuilder<ProductionBatch> builder)
        {
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.RecipeNoId);
            builder.Property(x => x.RegistrationNo).IsRequired();
            builder.Property(x => x.DamageStock).HasColumnType("decimal(18,4)");
            builder.Property(x => x.RemainingStock).HasColumnType("decimal(18,4)");
            builder.Property(x => x.RecipeQuantity).HasColumnType("decimal(18,4)");

            builder.HasOne(s => s.RecipeNo)
                .WithMany(ad => ad.ProductionBatchs)
                .HasForeignKey(ad => ad.RecipeNoId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(s => s.SaleOrder)
                .WithMany(ad => ad.ProductionBatchs)
                .HasForeignKey(ad => ad.SaleOrderId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(s => s.EmployeeRegistration)
                .WithMany(ad => ad.ProductionBatchs)
                .HasForeignKey(ad => ad.EmployeeRegistrationId);

        }
    }
}
