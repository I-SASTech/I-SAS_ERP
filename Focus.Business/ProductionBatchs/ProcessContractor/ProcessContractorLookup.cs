using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;

namespace Focus.Business.ProductionBatchs.ProcessContractor
{
    public class ProcessContractorLookup
    {
        public Guid? Id { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string ContractorNameEn { get; set; }
        public string Comments { get; set; }
        public string ContractorType { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public Guid? ProcessId { get; set; }
        public Guid? BatchProcessId { get; set; }
        public Guid? ProductionBatchId { get; set; }

        public virtual ICollection<ContractorItemsLookUpModel> ContractorItems { get; set; }
        //public virtual ICollection<PaymentVoucherVm> PurchaseOrderPayments { get; set; }




    }
}
