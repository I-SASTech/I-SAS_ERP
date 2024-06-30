
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class MobileOrderConfiguration : IEntityTypeConfiguration<MobileOrder>
    {
        public void Configure(EntityTypeBuilder<MobileOrder> builder)
        {

        }
    }
}
