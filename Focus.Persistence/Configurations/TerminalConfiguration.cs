using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Persistence.Configurations
{
    class TerminalConfiguration : IEntityTypeConfiguration<Terminal>
    {
        public void Configure(EntityTypeBuilder<Terminal> builder)
        {
            builder.HasOne(ed => ed.Account)
                .WithMany(d => d.Terminals)
                .HasForeignKey(f => f.AccountId);

            builder.HasOne(ed => ed.CashAccount)
                .WithMany(d => d.CashAccountTerminals)
                .HasForeignKey(f => f.CashAccountId);

            builder.HasOne(ea => ea.BankPosTerminal)
                .WithMany(a => a.Terminals)
                .HasForeignKey(ea => ea.PosTerminalId);

        }
    }
}
