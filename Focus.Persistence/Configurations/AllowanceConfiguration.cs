using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class AllowanceConfiguration : IEntityTypeConfiguration<Allowance>
    {
        public void Configure(EntityTypeBuilder<Allowance> builder)
        {


            builder.HasOne(ed => ed.AllowanceType)
                .WithMany(d => d.Allowances)
                .HasForeignKey(f => f.AllowanceTypeId);


        }
    }
}
