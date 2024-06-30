

using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class ContractorPaymentConfiguration : IEntityTypeConfiguration<ContractorPayment>
   {
       public void Configure(EntityTypeBuilder<ContractorPayment> builder)
       {
          


           builder.HasOne(s => s.BatchProcessContractor)
               .WithMany(ad => ad.ContractorPayments)
               .HasForeignKey(ad => ad.BatchProcessContractorId);

           builder.HasOne(s => s.PaymentVoucher)
               .WithMany(ad => ad.ContractorPayments)
               .HasForeignKey(ad => ad.PaymentVoucherId);


        }
   }
}
