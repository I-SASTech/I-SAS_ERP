using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class CompanySubmissionPeriodConfiguration:IEntityTypeConfiguration<CompanySubmissionPeriod>
    {
        public void Configure(EntityTypeBuilder<CompanySubmissionPeriod> builder)
        {
            builder.Property(e => e.Year).HasMaxLength(20);
            builder.Property(e => e.PeriodName);
            builder.Property(e => e.PeriodStart);
            builder.Property(e => e.PeriodEnd);

            builder.HasOne(c => c.YearlyPeriod)
                .WithMany(s => s.CompanySubmissionPeriods)
                .HasForeignKey(c => c.YearlyPeriodId);
        }
    }
}
