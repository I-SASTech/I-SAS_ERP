using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class InquiryStatusConfiguration:IEntityTypeConfiguration<InquiryStatus>
    {
        public void Configure(EntityTypeBuilder<InquiryStatus> builder)
        {
            builder.HasOne(x => x.Inquiry)
                .WithMany(x => x.InquiryStatus)
                .HasForeignKey(x => x.InquiryId);
            builder.HasOne(x => x.InquiryStatusDynamic)
                .WithMany(x => x.InquiryStatus)
                .HasForeignKey(x => x.InquiryStatusDynamicId);
        }
    }
}
