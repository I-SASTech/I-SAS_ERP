using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PurchaseOrderExpenseConfiguration : IEntityTypeConfiguration<PurchaseOrderExpense>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderExpense> builder)
        {
            builder.HasOne(s => s.ExpenseType)
                .WithMany(ad => ad.PurchaseOrderExpenses)
                .HasForeignKey(ad => ad.ExpenseTypeId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.PurchaseOrder)
                .WithMany(ad => ad.PurchaseOrderExpenses)
                .HasForeignKey(ad => ad.PurchaseOrderId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Account)
                .WithMany(x => x.PurchaseOrderExpenseForBanks)
                .HasForeignKey(x => x.BankCashAccountId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.ContactAccount)
                .WithMany(x => x.PurchaseOrderExpenseForContacts)
                .HasForeignKey(x => x.ContactAccountId).OnDelete(DeleteBehavior.Restrict);
           
            builder.HasOne(a => a.TaxRate)
                .WithMany(x => x.PurchaseOrderExpenses)
                .HasForeignKey(x => x.TaxRateId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.VatAccount)
                .WithMany(x => x.PurchaseOrderExpenseForVat)
                .HasForeignKey(x => x.VatAccountId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.PurchaseBill)
                .WithMany(x => x.PurchaseOrderExpenses)
                .HasForeignKey(x => x.BillId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
