using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.BankPosTerminals.Queries.GetBankPosTerminalList
{
    public class BankPosTerminalLookUpModel : IMapFrom<BankPosTerminal>
    {
        public Guid Id { get; set; }
        public string TerminalId { get; set; }
        public Guid BankId { get; set; }
        public string BankName { get; set; }
        public bool isActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<BankPosTerminal, BankPosTerminalLookUpModel>().ForMember(x => x.BankName, prod => prod.MapFrom(z => z.Account.Name));
        }
    }
}
