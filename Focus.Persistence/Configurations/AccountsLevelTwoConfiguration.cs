using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class AccountsLevelTwoConfiguration
    {
        public void Configure(EntityTypeBuilder<AccountsLevelTwo> builder)
        {
            builder.HasIndex("Code", "CompanyId").IsUnique().IsClustered(false);
            builder.Property(e => e.Code).IsRequired();
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Description).HasMaxLength(100);
            builder.Property(a => a.IsActive);
        }
    }
}
