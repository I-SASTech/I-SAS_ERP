using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class SalaryContributionConfiguration : IEntityTypeConfiguration<SalaryContribution>
    {
        public void Configure(EntityTypeBuilder<SalaryContribution> builder)
        {

            builder.HasOne(j => j.Contribution)
                .WithMany(j => j.SalaryContributions)
                .HasForeignKey(j => j.ContributionId);
            builder.HasOne(j => j.SalaryTemplate)
                .WithMany(j => j.SalaryContributions)
                .HasForeignKey(j => j.SalaryTemplateId);



        }
    }
}
