using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
   public class BankConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {

            builder.Property(d => d.AccoutNameArabic).IsRequired(false);

            builder.HasOne(ed => ed.Account)
                .WithMany(d => d.Banks)
                .HasForeignKey(d=>d.AccountId);

            builder.HasOne(ed => ed.Currency)
                .WithMany(d => d.Banks)
                .HasForeignKey(d => d.CurrencyId);
        }
    }
}
