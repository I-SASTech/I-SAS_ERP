using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
  public  class EmployeeRegistrationConfiguration : IEntityTypeConfiguration<EmployeeRegistration>
    {
        public void Configure(EntityTypeBuilder<EmployeeRegistration> builder)
        {
            builder.HasOne(ea => ea.EmployeeAccount)
                .WithMany(a => a.EmployeeAccounts)
                .HasForeignKey(ea => ea.EmployeeAccountId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ea => ea.PayableAccount)
               .WithMany(a => a.EmployeePayableAccounts)
               .HasForeignKey(ea => ea.PayableAccountId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ea => ea.Department)
               .WithMany(a => a.EmployeeRegistrations)
               .HasForeignKey(ea => ea.DepartmentId);
            builder.HasOne(ea => ea.Designation)
               .WithMany(a => a.EmployeeRegistrations)
               .HasForeignKey(ea => ea.DesignationId);

        }
    }
}
