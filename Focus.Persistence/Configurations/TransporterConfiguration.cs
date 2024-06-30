using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class TransporterConfiguration : IEntityTypeConfiguration<Transporter>
    {
        public void Configure(EntityTypeBuilder<Transporter> builder)
        {
            builder.Property(e => e.City)
                
                .HasMaxLength(50);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
