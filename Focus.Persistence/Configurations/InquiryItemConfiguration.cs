using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Focus.Persistence.Configurations
{
    public class InquiryItemConfiguration:IEntityTypeConfiguration<InquiryItem>
    {
        public void Configure(EntityTypeBuilder<InquiryItem> builder)
        {
            builder.HasOne(x => x.Inquiry).WithMany(x => x.InquiryItems).HasForeignKey(x => x.InquiryId);
            builder.HasOne(x => x.Product).WithMany(x => x.InquiryItems).HasForeignKey(x => x.ItemId);
        }
    }
}
