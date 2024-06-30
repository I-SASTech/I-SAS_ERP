using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Focus.Persistence.Configurations
{
    public class ExpenseTypeConfiguration : IEntityTypeConfiguration<ExpenseType>
    {
        public void Configure(EntityTypeBuilder<ExpenseType> builder)
        {

            builder.HasOne(j => j.Account)
                .WithMany(j => j.ExpenseTypes)
                .HasForeignKey(j => j.AccountId);



        }
    }
}
