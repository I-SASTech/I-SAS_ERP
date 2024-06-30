using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class SyncRecordConfigurtion : IEntityTypeConfiguration<SyncRecord>
    {
        public void Configure(EntityTypeBuilder<SyncRecord> builder)
        {
            builder.Property(x => x.Table).HasMaxLength(30);
            builder.Property(x => x.Action).HasMaxLength(10);
            builder.Property(x => x.ColumnType).HasMaxLength(30);
            builder.Property(x => x.ColumnId).HasMaxLength(50);
            builder.Property(x => x.Pull).HasDefaultValue(false);
            builder.Property(x => x.Push).HasDefaultValue(false);
            builder.Property(x => x.IsGeneral).HasDefaultValue(false);
        }
    }
}


