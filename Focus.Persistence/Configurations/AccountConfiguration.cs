using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasIndex("Code", "CompanyId").IsUnique().IsClustered(false);
            builder.Property(e => e.Code).IsRequired();
            builder.Property(a => a.Name).HasMaxLength(100);
            builder.Property(a => a.Description).HasMaxLength(100);
            builder.Property(a => a.IsActive);

            builder.HasOne<CostCenter>(s => s.CostCenter)
                .WithMany(ad => ad.Accounts)
                .HasForeignKey(ad => ad.CostCenterId);
        }
    }
}
