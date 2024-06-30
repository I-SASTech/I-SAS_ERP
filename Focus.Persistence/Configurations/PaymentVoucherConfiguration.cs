using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PaymentVoucherConfiguration : IEntityTypeConfiguration<PaymentVoucher>
    {
        public void Configure(EntityTypeBuilder<PaymentVoucher> builder)
        {
            builder.Property(j => j.Narration).HasMaxLength(500);
            builder.Property(j => j.Date).IsRequired();
            builder.Property(j => j.VoucherNumber).IsRequired();
            builder.Property(j => j.BankCashAccountId).IsRequired();
            builder.Property(j => j.ContactAccountId).IsRequired();
            builder.Property(j => j.Amount).IsRequired().HasColumnType("decimal(18,4)");


            builder.HasOne(a => a.Account)
                .WithMany(x => x.PaymentVoucherForBanks)
                .HasForeignKey(x => x.BankCashAccountId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.ContactAccount)
                .WithMany(x => x.PaymentVoucherForContacts)
                .HasForeignKey(x => x.ContactAccountId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.TaxRate)
                .WithMany(x => x.PaymentVouchers)
                .HasForeignKey(x => x.TaxRateId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.VatAccount)
                .WithMany(x => x.PaymentVoucherForVat)
                .HasForeignKey(x => x.VatAccountId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Bills)
                .WithMany(x => x.PaymentVouchers)
                .HasForeignKey(x => x.BillsId);


            builder.HasOne(a => a.MultiUp)
                .WithMany(x => x.PaymentVouchers)
                .HasForeignKey(x => x.MultiUpId);

            builder.HasOne(a => a.ReparingOrder)
                .WithMany(x => x.PaymentVouchers)
                .HasForeignKey(x => x.ReparingOrderId);
        
        }
    }
}