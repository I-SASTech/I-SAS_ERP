using System;
using Focus.Business.Interfaces.Mapping;
using Focus.Domain.Entities;


namespace Focus.Business.Accounting.Queries
{
    public class AccountLookUpModel : IMapFrom<Account>
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Guid? CostCenterId { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal RuningBalance { get; set; }

    }
}
