using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    class CostCenterConfiguration : IEntityTypeConfiguration<CostCenter>
    {
        public void Configure(EntityTypeBuilder<CostCenter> builder)
        {
            builder.HasIndex("Code", "CompanyId").IsUnique().IsClustered(false);
            builder.Property(e => e.Code).IsRequired();
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Description).HasMaxLength(100);
            builder.Property(e => e.VatDeductible);
            builder.Property(e => e.IsActive);

            builder.HasOne<AccountType>(s => s.AccountTypes)
              .WithMany(ad => ad.CostCenters)
              .HasForeignKey(ad => ad.AccountTypeId);
        }
    }
}


