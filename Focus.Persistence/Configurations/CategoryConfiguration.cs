using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
    class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(d => d.Name).IsRequired().HasMaxLength(50);
            builder.Property(d => d.Description).HasMaxLength(150);
            builder.Property(d => d.Code).HasMaxLength(30);

            builder.HasOne(ed => ed.COGSAccount)
                .WithMany(d => d.COGSAccounts)
                .HasForeignKey(f => f.COGSAccountId).OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(ed => ed.IncomeAccount)
                .WithMany(d => d.IncomeAccounts)
                .HasForeignKey(f => f.IncomeAccountId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ed => ed.InventoryAccount)
                .WithMany(d => d.InventoryAccounts)
                .HasForeignKey(f => f.InventoryAccountId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ed => ed.PurchaseAccount)
                .WithMany(d => d.PurchaseAccounts)
                .HasForeignKey(f => f.PurchaseAccountId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ed => ed.SaleAccount)
                .WithMany(d => d.SaleAccounts)
                .HasForeignKey(f => f.SaleAccountId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
