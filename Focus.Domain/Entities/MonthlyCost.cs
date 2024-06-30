using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
    public class MonthlyCost : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public DateTime? Date { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal MonthlySaleries { get; set; }
        public decimal MonthlyUtilityBills { get; set; }
        public decimal MonthlyGovtFees { get; set; }
        public decimal MonthlyGovtZakat { get; set; }
        public decimal GovtFeeForLabour { get; set; }
        public virtual ICollection<MonthlyCostItem> MonthlyCostItems { get; set; }
        public Guid? BranchId { get; set; }



    }
}
