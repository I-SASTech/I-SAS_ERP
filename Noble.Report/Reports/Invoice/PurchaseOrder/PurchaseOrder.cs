using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice.PurchaseOrder
{
    public partial class PurchaseOrder : DevExpress.XtraReports.UI.XtraReport
    {
        public PurchaseOrder(CompanyDto companyDto, PrintSetting printSetting, PurchaseOrderLookupModel purchaseOrderDetails)
        {
            InitializeComponent();
            A4_PrintSetting.DataSource = printSetting;
            A4_CompanyDto.DataSource = companyDto;
            A4_purchaseOrderDetails.DataSource = purchaseOrderDetails;
            var removeinvoicetagesEn = "";
            var removeinvoicetagesAr = "";
            SupplierCode.Text = purchaseOrderDetails.SupplierCode;
            CustomerAddress.Text = purchaseOrderDetails.CustomerAddress;
            if (!string.IsNullOrEmpty(purchaseOrderDetails.SupplierQuotationNo))
            {
                xrLabel7.Text = "SQ Number";
                xrLabel8.Text = purchaseOrderDetails.SupplierQuotationNo;
            }

            if (purchaseOrderDetails.RegistrationNo.StartsWith("SQ"))
            {
                InvoiceAr.Text = "اقتباس المورد";
                InvoiceEn.Text = "Supplier Quotation";
            }
            else
            {
                InvoiceAr.Text = "طلب الشراء";
                InvoiceEn.Text = "Purchase Order";
            }

            int transitionIndex = -1;

            for (int i = 0; i < purchaseOrderDetails.SupplierName.Length; i++)
            {
                if (purchaseOrderDetails.SupplierName[i] >= 0x0600 && purchaseOrderDetails.SupplierName[i] <= 0x06FF)
                {
                    transitionIndex = i;
                    break;
                }
            }

            string englishText = purchaseOrderDetails.SupplierName.Substring(0, transitionIndex).Trim();
            string arabicText = purchaseOrderDetails.SupplierName.Substring(transitionIndex).Trim();



            if (string.IsNullOrEmpty(englishText))
            {
                englishName.Text = "";
            }
            else
            {
                englishName.Text = englishText+" "+ arabicText;
            }

          

            attention.Text = purchaseOrderDetails.Prefix + " " + purchaseOrderDetails.EnglishName + @" " + purchaseOrderDetails.ArabicName;

            if (purchaseOrderDetails.Note != "" && purchaseOrderDetails.Note != null)
            {
                removeinvoicetagesEn = (purchaseOrderDetails.Note != "" && purchaseOrderDetails.Note != null) ? purchaseOrderDetails.Note.Replace("<p>", " ").Replace("</p>", " ") : "";
            }
            else
            {
                removeinvoicetagesEn = (printSetting.TermsInEng != "" && printSetting.TermsInEng != null) ? printSetting.TermsInEng.Replace("<p>", " ").Replace("</p>", " ") : "";
                removeinvoicetagesAr = (printSetting.TermsInAr != "" && printSetting.TermsInAr != null) ? printSetting.TermsInAr.Replace("<p>", " ").Replace("</p>", " ") : "";
            }
            var result = removeinvoicetagesEn +
                         (!string.IsNullOrEmpty(removeinvoicetagesEn) && !string.IsNullOrEmpty(removeinvoicetagesAr) ? "\n" : "") +
                         removeinvoicetagesAr;
            xrLabel36.Text = result;
            if (printSetting.FooterImageForPrint != null && printSetting.FooterImageForPrint != "" && printSetting.FooterImageForPrint != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(printSetting.HeaderImageForPrint);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                xrPictureBox1.Image = image;
            }
            if (printSetting.FooterImageForPrint != null && printSetting.FooterImageForPrint != "" && printSetting.FooterImageForPrint != string.Empty)
            {
                byte[] footerData = Convert.FromBase64String(printSetting.FooterImageForPrint);
                MemoryStream Footerms = new MemoryStream(footerData);
                Bitmap FooterImg = new Bitmap(Footerms);
                Footer.Image = FooterImg;
            }
            AmountInWords.Text = ConvertNumberToWords(Convert.ToInt32(purchaseOrderDetails.TotalAmount)) + " SAUDI RIYAL";

        }
        public static string ConvertNumberToWords(int number)
        {

            if (number == 0)
            {
                return "ZERO";
            }

            if (number < 0)
            {
                return "MINUS " + ConvertNumberToWords(Math.Abs(number));
            }

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += ConvertNumberToWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertNumberToWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertNumberToWords(number / 100) + " HUNDRED ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                {
                    words += "AND ";
                }

                string[] unitsMap = { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                string[] tensMap = { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

                if (number < 20)
                {
                    words += unitsMap[number];
                }
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                    {
                        words += "-" + unitsMap[number % 10];
                    }
                }
            }

            return words;
        }

    }

}
