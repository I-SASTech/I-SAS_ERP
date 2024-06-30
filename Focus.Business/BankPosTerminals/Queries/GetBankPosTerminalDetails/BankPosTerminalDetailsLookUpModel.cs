using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.BankPosTerminals.Queries.GetBankPosTerminalDetails
{
    public class BankPosTerminalDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string TerminalId { get; set; }
        public Guid BankId { get; set; }
        public bool isActive { get; set; }

        public static Expression<Func<BankPosTerminal, BankPosTerminalDetailsLookUpModel>> Projection
        {
            get
            {
                return bankPosTerminal => new BankPosTerminalDetailsLookUpModel
                {
                    Id = bankPosTerminal.Id,
                    TerminalId = bankPosTerminal.TerminalId,
                    BankId = bankPosTerminal.BankId,
                    isActive = bankPosTerminal.isActive
                };
            }
        }

        public static BankPosTerminalDetailsLookUpModel Create(BankPosTerminal bankPosTerminal)
        {
            return Projection.Compile().Invoke(bankPosTerminal);
        }
    }
}
