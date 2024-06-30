using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class LoanPayConfiguration : IEntityTypeConfiguration<LoanPay>
    {
        public void Configure(EntityTypeBuilder<LoanPay> builder)
        {


            builder.HasOne(ed => ed.LoanPayment)
                .WithMany(d => d.LoanPays)
                .HasForeignKey(f => f.LoanPaymentId);


        }
    }
}
