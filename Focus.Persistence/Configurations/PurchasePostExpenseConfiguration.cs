using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class PurchasePostExpenseConfiguration : IEntityTypeConfiguration<PurchasePostExpense>
    {
        public void Configure(EntityTypeBuilder<PurchasePostExpense> builder)
        {
            builder.HasOne(s => s.PurchasePost)
                .WithMany(ad => ad.PurchasePostExpenses)
                .HasForeignKey(ad => ad.PurchasePostId).OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(s => s.TaxRate)
                .WithMany(ad => ad.PurchasePostExpenses)
                .HasForeignKey(ad => ad.TaxRateId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.PaymentVoucher)
                .WithMany(ad => ad.PurchasePostExpenses)
                .HasForeignKey(ad => ad.PaymentVoucherId).OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(s => s.PurchaseBill)
                .WithMany(ad => ad.PurchasePostExpenses)
                .HasForeignKey(ad => ad.BillId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
