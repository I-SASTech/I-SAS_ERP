using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class YearlyPeriodConfiguration:IEntityTypeConfiguration<YearlyPeriod>
    {
        public void Configure(EntityTypeBuilder<YearlyPeriod> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(50);
        }
    }
}
