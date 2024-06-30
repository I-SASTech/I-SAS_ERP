using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
  public  class GatePass : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
  {

      public DateTime? FromDate { get; set; }
      public DateTime? ToDate { get; set; }
      public DateTime Date { get; set; }
      public string RegistrationNo { get; set; }
      public string ContractorType { get; set; }
      public Guid? EmployeeId { get; set; }
      public virtual EmployeeRegistration EmployeeRegistration { get; set; }
      public string Refrence { get; set; }
      public ApprovalStatus ApprovalStatus { get; set; }
      public string Note { get; set; }
      public bool IsClose { get; set; }
      public bool IsGatePass { get; set; }
      public virtual ICollection<GatePassItem> GatePassItems { get; set; }
      public Guid? BatchProcessContractorId { get; set; }
      public virtual BatchProcessContractor BatchProcessContractor { get; set; }
        public Guid? ProductionBatchId { get; set; }
      public virtual ProductionBatch ProductionBatch { get; set; }
      public bool IsActive { get; set; }

      public string Year { get; set; }
      public Guid? PeriodId { get; set; }
    }
}
