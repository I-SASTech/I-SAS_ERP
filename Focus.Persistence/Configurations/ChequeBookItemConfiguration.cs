using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
   public class ChequeBookItemConfiguration : IEntityTypeConfiguration<ChequeBookItem>
    {
        public void Configure(EntityTypeBuilder<ChequeBookItem> builder)
        {
            builder.HasOne(ed => ed.ChequeBook)
                .WithMany(d => d.GetChequeBookItems)
                .HasForeignKey(f => f.ChequeBookId);


        }
    }
}
