using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class DailyExpenseDetailConfiguration : IEntityTypeConfiguration<DailyExpenseDetail>
    {
        public void Configure(EntityTypeBuilder<DailyExpenseDetail> builder)
        {
           
            builder.HasOne(j => j.DailyExpense)
                .WithMany(j => j.DailyExpenseDetails)
                .HasForeignKey(j => j.DailyExpenseId);
            builder.HasOne(j => j.TaxRate)
                .WithMany(j => j.DailyExpenseDetails)
                .HasForeignKey(j => j.VatId);


        }
    }
}
