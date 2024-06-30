using System;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    class AccountTemplateConfiguration : IEntityTypeConfiguration<AccountTemplate>
    {
        public void Configure(EntityTypeBuilder<AccountTemplate> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(80);
            builder.Property(e => e.Description).HasMaxLength(200);
            builder.Property(e => e.Type).HasMaxLength(50);
            builder.Property(e => e.JsonTemplate).HasMaxLength(Int32.MaxValue);
        }
    }
}
