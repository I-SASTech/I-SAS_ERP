using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Permission.Commands.AddUpdateNoblePaymentLimit
{
    public class PaymentLimitLookUp
    {
        public Guid Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
        public Guid CompanyId { get; set; }
    }
}
