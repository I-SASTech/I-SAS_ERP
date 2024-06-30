using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class DesignationConfiguration:IEntityTypeConfiguration<Designation>
    {
        public void Configure(EntityTypeBuilder<Designation> builder)
        {
            builder.Property(d => d.Name).HasMaxLength(50);
            builder.Property(d => d.Description).HasMaxLength(150);
            builder.Property(d => d.Code).HasMaxLength(30);

           
        }
    }
}
