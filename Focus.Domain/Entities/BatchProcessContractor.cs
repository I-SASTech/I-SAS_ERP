using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class BatchProcessContractor : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
    
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string ContractorType { get; set; }
        public string Comments { get; set; }
        public string Completions { get; set; }
    
        public Guid? ProductionBatchId { get; set; }
        public Guid? BatchProcessId { get; set; }
        public virtual BatchProcess BatchProcess { get; set; }
        public Guid? ContractorId { get; set; }
        public Guid? ContractorId2 { get; set; }

        public virtual EmployeeRegistration Contractor { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<ContractorItem> ContractorItems { get; set; }
        public virtual ICollection<ContractorPayment> ContractorPayments { get; set; }
        public virtual ICollection<GatePass> GatePasses { get; set; }





    }
}
