using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.DenominationSetups.Queries.GetDenominationSetupList
{
    public class DenominationSetupLookUpModel : IMapFrom<DenominationSetup>
    {
        public Guid Id { get; set; }
        public decimal Number { get; set; }
        public bool isActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DenominationSetup, DenominationSetupLookUpModel>();
        }
    }
}
