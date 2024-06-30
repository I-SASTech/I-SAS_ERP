using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class LoanPaymentConfiguration : IEntityTypeConfiguration<LoanPayment>
   {
       public void Configure(EntityTypeBuilder<LoanPayment> builder)
       {


           builder.HasOne(ed => ed.EmployeeRegistration)
               .WithMany(d => d.LoanPayments)
               .HasForeignKey(f => f.EmployeeRegistrationId);


       }
   }
}
