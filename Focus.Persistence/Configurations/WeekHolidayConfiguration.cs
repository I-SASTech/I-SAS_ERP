using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class WeekHolidayConfiguration : IEntityTypeConfiguration<WeekHoliday>
    {
        public void Configure(EntityTypeBuilder<WeekHoliday> builder)
        {

            builder.HasOne(j => j.Holiday)
                .WithMany(j => j.WeekHolidays)
                .HasForeignKey(j => j.HolidayId);
          


        }
    }
}
