using System;
using System.Collections.Generic;
using Focus.Business.Interfaces.Mapping;
using Focus.Domain.Entities;

namespace Focus.Business.Accounting.Queries
{
    public class CostCenterLookUpModel : IMapFrom<CostCenter>
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public Guid AccountTypeId { get; set; }
        public IList<AccountLookUpModel> Accounts { get; set; }
    }
}
