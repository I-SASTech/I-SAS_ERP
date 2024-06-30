﻿using Noble.Report.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class JournalVoucherItemLookupModel
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string PaymentModes { get; set; }
        public SalePaymentType PaymentMode { get; set; }
        public string PaymentMethods { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Image { get; set; }
        public string ChequeNo { get; set; }

        public Guid JournalVoucherId { get; set; }
    }
}