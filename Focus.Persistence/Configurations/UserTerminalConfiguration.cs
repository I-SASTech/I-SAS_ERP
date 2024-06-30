using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
    public class UserTerminalConfiguration : IEntityTypeConfiguration<UserTerminal>
    {
        public void Configure(EntityTypeBuilder<UserTerminal> builder)
        {
            builder.HasOne(x => x.Terminal).WithMany(x => x.UserTerminals)
                .HasForeignKey(x => x.TerminalId);
        }
    }

}

