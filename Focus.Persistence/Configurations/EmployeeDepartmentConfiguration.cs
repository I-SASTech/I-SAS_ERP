using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    class EmployeeDepartmentConfiguration:IEntityTypeConfiguration<EmployeeDepartment>
    {
        public void Configure(EntityTypeBuilder<EmployeeDepartment> builder)
        {
            builder.HasOne(ed => ed.Employee)
                .WithMany(e => e.EmployeeDepartments)
                .HasForeignKey(ed => ed.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(ed => ed.Department)
            //    .WithMany(d => d.EmployeeDepartments)
            //    .HasForeignKey(ed => ed.DepartmentId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
