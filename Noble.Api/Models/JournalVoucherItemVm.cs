using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using Focus.Domain.Enum;

namespace Focus.Web.ViewModel
{
    public class JournalVoucherItemVm
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        public string Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public Guid? ContactId { get; set; }
        public virtual Contact Contact { get; set; }
        public Guid JournalVoucherId { get; set; }
        public virtual JournalVoucher JournalVoucher { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Image { get; set; }
        public string ChequeNo { get; set; }
    }
}
