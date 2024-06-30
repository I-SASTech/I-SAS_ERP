using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class InquireModuleQuestionConfiguration:IEntityTypeConfiguration<InquiryModuleQuestion>
    {
        public void Configure(EntityTypeBuilder<InquiryModuleQuestion> builder)
        {
            builder.HasOne(x => x.InquiryModule).WithMany(x => x.InquiryModuleQuestions)
                .HasForeignKey(x => x.InquiryModuleId);
            builder.HasOne(x => x.Inquiry).WithMany(x => x.InquiryModuleQuestions)
                .HasForeignKey(x => x.InquiryId);
        }
    }
}
