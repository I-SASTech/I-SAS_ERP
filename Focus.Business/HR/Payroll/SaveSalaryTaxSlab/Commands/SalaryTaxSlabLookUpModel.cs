using System;
using System.Collections.Generic;
using Focus.Domain.Entities;

namespace Focus.Business.HR.Payroll.SaveSalaryTaxSlab.Commands
{
    public class SalaryTaxSlabLookUpModel
    {
        public Guid Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsActive { get; set; }
        public ICollection<SalaryTaxSlab> SalaryTaxSlabList { get; set; }
    }
}
