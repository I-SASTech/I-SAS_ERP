using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class InquiryConfiguration:IEntityTypeConfiguration<Inquiry>
    {
        public void Configure(EntityTypeBuilder<Inquiry> builder)
        {
            builder.HasOne(x => x.Contact).WithMany(x => x.Inquiries).HasForeignKey(x => x.CustomerId);
            builder.HasOne(x => x.MediaType).WithMany(x => x.Inquiries).HasForeignKey(x => x.MediaTypeId);
            builder.HasOne(x => x.InquiryPriority).WithMany(x => x.Inquiries).HasForeignKey(x => x.InquiryPriorityId);
            builder.HasOne(x => x.InquiryStatusDynamic).WithMany(x => x.Inquiries).HasForeignKey(x => x.InquiryStatusDynamicId);
            builder.HasOne(x => x.InquiryProcess).WithMany(x => x.Inquiries).HasForeignKey(x => x.InquiryProcessId);
            builder.HasOne(x => x.InquiryType).WithMany(x => x.Inquiries).HasForeignKey(x => x.InquiryTypeId);
            builder.HasOne(x => x.EmployeeRegistration).WithMany(x => x.Inquiries).HasForeignKey(x => x.ReferedBy);
        }
    }
}
