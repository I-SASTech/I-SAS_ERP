using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.MonthlyCosts.Commands
{
   public class MonthlyCostLookUpModel
    {
        public Guid Id { get; set; }

        public DateTime? Date { get; set; }
        public Guid? BranchId { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal MonthlySaleries { get; set; }
        public decimal MonthlyUtilityBills { get; set; }
        public decimal MonthlyGovtFees { get; set; }
        public decimal MonthlyGovtZakat { get; set; }
        public decimal GovtFeeForLabour { get; set; }
        public virtual ICollection<MonthlyCostItemLookUpModel> MonthlyCostItems { get; set; }
    }
}
