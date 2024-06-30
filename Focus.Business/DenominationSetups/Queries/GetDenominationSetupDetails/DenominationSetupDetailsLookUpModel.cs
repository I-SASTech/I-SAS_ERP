using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.DenominationSetups.Queries.GetDenominationSetupDetails
{
    public class DenominationSetupDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public decimal Number { get; set; }
        public bool isActive { get; set; }

        public static Expression<Func<DenominationSetup, DenominationSetupDetailsLookUpModel>> Projection
        {
            get
            {
                return denominationSetup => new DenominationSetupDetailsLookUpModel
                {
                    Id = denominationSetup.Id,
                    Number = denominationSetup.Number,
                    isActive = denominationSetup.isActive
                };
            }
        }

        public static DenominationSetupDetailsLookUpModel Create(DenominationSetup denominationSetup)
        {
            return Projection.Compile().Invoke(denominationSetup);
        }
    }
}
