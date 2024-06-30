using System;
using System.Collections.Generic;
using System.Text;
using Focus.Business.ProductionBatchs;
using Focus.Business.ProductionBatchs.ProcessContractor;
using Focus.Domain.Enum;

namespace Focus.Business.ProductionModule.Processes
{
   public class ProcessContractorLookUpModel
    {
        public Guid Id { get; set; }
        public Guid? ProductionBatchId { get; set; }
        public Guid? BatchProcessId { get; set; }

        public Guid? ContractorId { get; set; }
        public string ContractorNameAr { get; set; }
        public bool IsActive { get; set; }
        public bool IsSelect { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string FromDates { get; set; }
        public string ToDates { get; set; }
        public string ContractorNameEn { get; set; }
        public string ContractorType { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public Guid? ProcessId { get; set; }
        public Guid? ContractorAccountId { get; set; }
        public virtual ICollection<ContractorItemsLookUpModel> ContractorItems { get; set; }

    }
}
