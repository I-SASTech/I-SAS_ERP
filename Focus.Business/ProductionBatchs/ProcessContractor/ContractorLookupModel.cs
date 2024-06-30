using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;
using Focus.Domain.Enum;

namespace Focus.Business.ProductionBatchs.ProcessContractor
{
   public class ContractorLookupModel
    {
        public DateTime Date { get; set; }

        public string VoucherNumber { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string ChequeNumber { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public Guid? BatchProcessContractorId { get; set; }
        public Guid? PaymentVoucherId { get; set; }
    }
}
