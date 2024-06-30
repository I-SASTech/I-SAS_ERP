using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
   public class PrintOptionConfiguration : IEntityTypeConfiguration<PrintOption>
   {

       public void Configure(EntityTypeBuilder<PrintOption> builder)
       {
           builder.HasOne(x => x.PrintSetting)
               .WithMany(x => x.PrintOptions)
               .HasForeignKey(x => x.PrintSettingId);

          



        }
   }

}

