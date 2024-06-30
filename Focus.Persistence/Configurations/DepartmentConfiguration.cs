using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
 public   class DepartmentConfiguration:IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Name).HasMaxLength(50);
            builder.Property(d => d.Description).HasMaxLength(150);
            builder.Property(d => d.Code).HasMaxLength(30);
            builder.HasOne(j => j.Bank)
                .WithMany(j => j.Departments)
                .HasForeignKey(j => j.BankId);



        }
    }
}
