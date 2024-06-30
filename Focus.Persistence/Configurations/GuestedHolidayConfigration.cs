using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
  public  class GuestedHolidayConfigration : IEntityTypeConfiguration<GuestedHoliday>
    {
        public void Configure(EntityTypeBuilder<GuestedHoliday> builder)
        {

            builder.HasOne(j => j.Holiday)
                .WithMany(j => j.GuestedHolidays)
                .HasForeignKey(j => j.HolidayId);



        }
    }
}
