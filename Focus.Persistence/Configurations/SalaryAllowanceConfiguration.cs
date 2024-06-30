using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class SalaryAllowanceConfiguration : IEntityTypeConfiguration<SalaryAllowance>
    {
        public void Configure(EntityTypeBuilder<SalaryAllowance> builder)
        {

            builder.HasOne(j => j.Allowance)
                .WithMany(j => j.SalaryAllowances)
                .HasForeignKey(j => j.AllowanceId);
            builder.HasOne(j => j.SalaryTemplate)
                .WithMany(j => j.SalaryAllowances)
                .HasForeignKey(j => j.SalaryTemplateId);



        }
    }
}
