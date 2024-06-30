using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class DailyExpenseConfiguration : IEntityTypeConfiguration<DailyExpense>
    {
        public void Configure(EntityTypeBuilder<DailyExpense> builder)
        {

            builder.HasOne(j => j.PurchaseBill)
                .WithMany(j => j.DailyExpenses)
                .HasForeignKey(j => j.BillerAccountId);
            


        }
    }
}
