using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class MonthlyCostVm
    {
        public Guid? Id { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal MonthlySaleries { get; set; }
        public decimal MonthlyUtilityBills { get; set; }
        public decimal MonthlyGovtFees { get; set; }
        public decimal MonthlyGovtZakat { get; set; }
        public decimal GovtFeeForLabour { get; set; }
    }
}
