using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
   public class ChequeBookConfiguration : IEntityTypeConfiguration<ChequeBook>
    {
        public void Configure(EntityTypeBuilder<ChequeBook> builder)
        {
            builder.HasOne(ed => ed.Bank)
                .WithMany(d => d.ChequeBooks)
                .HasForeignKey(f => f.BankId);


        }
    }
}
