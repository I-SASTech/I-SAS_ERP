using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class DayStartConfiguration:IEntityTypeConfiguration<DayStart>
    {
        public void Configure(EntityTypeBuilder<DayStart> builder)
        {
            builder.Property(x => x.IsReceived).HasDefaultValue(true);
        }
    }
}
