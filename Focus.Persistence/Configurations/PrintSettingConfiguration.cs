using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PrintSettingConfiguration:IEntityTypeConfiguration<PrintSetting>
    {
        public void Configure(EntityTypeBuilder<PrintSetting> builder)
        {
            builder.Property(d => d.TermsInEng).HasColumnType("varchar(MAX)"); ;
            builder.HasOne(x => x.CashAccount)
                .WithMany(x => x.CashAccountPrintSetting)
                .HasForeignKey(x => x.CashAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Account)
                .WithMany(x => x.BankAccountPrintSetting)
                .HasForeignKey(x => x.BankAccountId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Bank1)
                .WithMany(x => x.Banks1)
                .HasForeignKey(x => x.Bank1Id)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Bank2)
                .WithMany(x => x.Banks2)
                .HasForeignKey(x => x.Bank2Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
