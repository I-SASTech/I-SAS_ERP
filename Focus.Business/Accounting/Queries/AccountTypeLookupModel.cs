using System;
using System.Collections.Generic;
using Focus.Business.Interfaces.Mapping;
using Focus.Domain.Entities;

namespace Focus.Business.Accounting.Queries
{
    public class AccountTypeLookupModel : IMapFrom<AccountType>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public IList<CostCenterLookUpModel> CostCenters { get; set; }
    }
}
