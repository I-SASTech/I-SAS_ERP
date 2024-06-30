using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
    public class DiscountSetupConfiguration : IEntityTypeConfiguration<DiscountSetup>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DiscountSetup> builder)
        {
            builder.Property(x => x.DiscountOverQty).HasDefaultValue(true);
            builder.Property(x => x.LineItemBeforeVat).HasDefaultValue(true);
        }
    }
}
