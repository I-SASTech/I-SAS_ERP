using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {


            //Dont Use THIS table ...............
            builder.Property(e => e.EmployeeNo).IsRequired();
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(e => e.LastName).HasMaxLength(30);
            builder.Property(e => e.PhoneNumber).HasMaxLength(14);
            builder.Property(e => e.EmailAddress);
            builder.Property(e => e.Address).HasMaxLength(250);
            builder.Property(e => e.Nic).IsRequired().HasMaxLength(13);
            builder.Property(e => e.JobType);
            builder.Property(e => e.ImagePath);
            builder.Property(e => e.ParentId);
            builder.Property(e => e.SearchingKey).HasMaxLength(300);

            builder.HasMany(e => e.Children)
                .WithOne(e => e.Parent)
                .HasForeignKey(e => e.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.EmployeeAttachments)
                .WithOne(e => e.Employee)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(d => d.Designation)
            //    .WithMany(d => d.Employees)
            //    .HasForeignKey(e=>e.DesignationId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.HasMany(d => d.EmployeeDepartments)
            //    .WithOne(e => e.Employee)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
