using System;
using System.Linq;
using Focus.Domain.Enum;

namespace Focus.Business.PurchaseBills.Queries.GetPurchaseBillList
{
    public class PurchaseBillLookUpModel 
    {
        public Guid? BillerId { get; set; }
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string BillDate { get; set; }
        public string DueDate { get; set; }
        public string RegistrationNo { get; set; }
        public string Reference { get; set; }
        public string Narration { get; set; }
        public string BillerAccount { get; set; }
        public string BillerAccountAr { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public PartiallyInvoices PartiallyInvoices { get; set; }

        public decimal NetAmount { get; set; }
        public decimal RemainingAmount { get; set; }

        public static PurchaseBillLookUpModel Create(Domain.Entities.PurchaseBill PurchaseBill)
        {
            var lookUpModel = new PurchaseBillLookUpModel
            {
                Id = PurchaseBill.Id,

                PartiallyInvoices = PurchaseBill.PartiallyInvoices,
                RegistrationNo = PurchaseBill.RegistrationNo,
                BillerId = PurchaseBill.BillerId,
                RemainingAmount = PurchaseBill.RemainingAmount,
                DueDate = PurchaseBill.DueDate.ToString("MM/dd/yyyy hh:mm tt"),
                BillerAccount = PurchaseBill.BillerAccount?.Name,
                BillerAccountAr = PurchaseBill.BillerAccount?.NameArabic,
                NetAmount = PurchaseBill.PurchaseBillItems.Sum(x=>x.Amount),
                Date = PurchaseBill.Date.ToString("MM/dd/yyyy hh:mm tt"),
                BillDate = PurchaseBill.BillDate?.ToString("MM/dd/yyyy hh:mm tt"),
                Reference = PurchaseBill.Reference

            };
            return lookUpModel;
        }
    }
}
