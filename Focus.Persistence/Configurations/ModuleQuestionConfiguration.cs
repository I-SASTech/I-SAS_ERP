using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class ModuleQuestionConfiguration:IEntityTypeConfiguration<ModuleQuestion>
    {
        public void Configure(EntityTypeBuilder<ModuleQuestion> builder)
        {
            builder.HasOne(x => x.InquiryModule).WithMany(x => x.ModuleQuestions)
                .HasForeignKey(x => x.InquiryModuleId);
        }
    }
}
