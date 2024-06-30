using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class SalaryDeductionConfiguration : IEntityTypeConfiguration<SalaryDeduction>
   {
       public void Configure(EntityTypeBuilder<SalaryDeduction> builder)
       {

           builder.HasOne(j => j.Deduction)
               .WithMany(j => j.SalaryDeductions)
               .HasForeignKey(j => j.DeductionId);
           builder.HasOne(j => j.SalaryTemplate)
               .WithMany(j => j.SalaryDeductions)
               .HasForeignKey(j => j.SalaryTemplateId);



       }
   }
}
