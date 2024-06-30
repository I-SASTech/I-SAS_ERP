using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class PrintSetting
    {
        public Guid Id { get; set; }
        public bool isActive { get; set; }
        public decimal? ReturnDays { get; set; }
        public string PrintSize { get; set; }
        public string WarrantyImage { get; set; }
        public string WarrantyImageForPrint { get; set; }
        public string PrintTemplate { get; set; }
        public string TermsInAr { get; set; }
        public string TermsInEng { get; set; }
        public Guid? CashAccountId { get; set; }
        public Guid? BankAccountId { get; set; }
        public string PrinterName { get; set; }
        public bool IsHeaderFooter { get; set; }
        public bool EnglishName { get; set; }
        public bool ArabicName { get; set; }
        public bool CustomerAddress { get; set; }
        public bool CustomerVat { get; set; }
        public bool CustomerNumber { get; set; }
        public bool CargoName { get; set; }
        public bool CustomerTelephone { get; set; }
        public bool ItemPieceSize { get; set; }
        public bool StyleNo { get; set; }
        public bool BlindPrint { get; set; }
        public bool ShowBankAccount { get; set; }
        public string BankAccount1 { get; set; }
        public string BankIcon1 { get; set; }
        public string BankAccount2 { get; set; }
        public string BankIcon2 { get; set; }
        public string InvoicePrint { get; set; }
        public bool IsBlindPrint { get; set; }
        public bool IsDeliveryNote { get; set; }
        public bool AutoPaymentVoucher { get; set; }
        public bool CustomerNameEn { get; set; }
        public bool CustomerNameAr { get; set; }
        public decimal? ExchangeDays { get; set; }
        public Guid? Bank1Id { get; set; }
        public Guid? Bank2Id { get; set; }
        public string WelcomeLineEn { get; set; }
        public string WelcomeLineAr { get; set; }
        public string ClosingLineEn { get; set; }
        public string ClosingLineAr { get; set; }
        public string ContactNo { get; set; }
        public string ManagementNo { get; set; }
        public string BusinessAddressArabic { get; set; }
        public string BusinessAddressEnglish { get; set; }
        public string WalkInInvoiceType { get; set; }
        public string HeaderImage { get; set; }
        public string HeaderImageForPrint { get; set; }
        public string HeaderImage1ForPrint { get; set; }
        public string HeaderImage1 { get; set; }
        public string TagsImages { get; set; }
        public string ProposalImage { get; set; }
        public string FooterImage { get; set; }
        public string FooterImageForPrint { get; set; }
        public string TagImageForPrint { get; set; }
        public string ProposalImageForPrint { get; set; }
        public bool IsQuotationPrint { get; set; }
        public bool TermAndConditionLength { get; set; }
    //    public virtual IList<PrintOptionLookUp> PrintOptions { get; set; }

        public bool WithSubTotal { get; set; }
        public bool ContinueWithPage { get; set; }
        public bool SubTotalWithDashes { get; set; }

    }
}