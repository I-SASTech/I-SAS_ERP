using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
   public class ProcessContractor : BaseEntity
    {
        public Guid? ContractorId { get; set; }
        public virtual EmployeeRegistration Contractor { get; set; }
        public Guid? ProcessId { get; set; }
        public virtual Process Process { get; set; }
    }
}
