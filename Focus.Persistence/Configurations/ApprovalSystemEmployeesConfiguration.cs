using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Persistence.Configurations
{
    public class ApprovalSystemEmployeesConfiguration : IEntityTypeConfiguration<ApprovalSystemEmployees>
    {
        public void Configure(EntityTypeBuilder<ApprovalSystemEmployees> builder)
        {
            builder.HasOne(x => x.EmployeeRegistration)
                .WithMany(x => x.ApprovalSystemEmployees)
                .HasForeignKey(x => x.EmployeeId);

            builder.HasOne(x => x.ApprovalSystem)
                .WithMany(x => x.ApprovalSystemEmployees)
                .HasForeignKey(x => x.ApprovalSystemId);
        }
    }
}
