using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;

namespace Focus.Business.JournalVouchers.Queries.NetDrCr
{
    public class TotalDrCr
    {
        public decimal TotalDr { get; set; }
        public decimal TotalCr { get; set; }

        public decimal CalculateTotalDr(List<JournalVoucherItem> journalVoucherItem)
        {
            foreach (var item in journalVoucherItem)
            {
                TotalDr += item.Debit;
            }

            return (TotalDr);
        }
        public decimal CalculateTotalCr(List<JournalVoucherItem> journalVoucherItem)
        {
            foreach (var item in journalVoucherItem)
            {
                TotalCr += item.Credit;
            }

            return (TotalCr);
        }
    }
}
