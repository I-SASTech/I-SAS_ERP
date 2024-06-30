using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Invoice.ReturnInvoice
{
    public partial class ReturnInvoiceDefaultTemplate : DevExpress.XtraReports.UI.XtraReport
    {
        public static string ConvertNumberToWords(int number)
        {

            if (number == 0)
            {
                return "zero";
            }

            if (number < 0)
            {
                return "minus " + ConvertNumberToWords(Math.Abs(number));
            }

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += ConvertNumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertNumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertNumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                {
                    words += "and ";
                }

                string[] unitsMap = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                string[] tensMap = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

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
        public ReturnInvoiceDefaultTemplate(CompanyDto companyDto, PrintSetting printSetting, SaleDetailLookupModel saleDetail)
        {
            
                InitializeComponent();
                A4_Default_PrintSetting.DataSource = printSetting;
                A4_Default_CompanyDto.DataSource = companyDto;
                A4_Default_SaleDetails.DataSource = saleDetail;
                saleDetail.SaleItems.Where(x => x.Code == null).ToList().ForEach(b => b.Code = b.StyleNumber);
                A4_Default_SaleItem.DataSource = saleDetail.SaleItems;
                var total = saleDetail.SaleItems.Sum(b => b.TotalAmount);
                TotalinWords.Text = ConvertNumberToWords(Convert.ToInt32(total));

                var invoicenameenglish = "";
                var invoicenameengarabic = "";
                var qty = 0;

                if (saleDetail.CustomerCategory == "B2B – Business to Business")
                {
                    invoicenameenglish = "Tax Invoice";
                    invoicenameengarabic = "فاتورة ضريبية";
                }
                else if (saleDetail.CustomerCategory == "B2C – Business to Client")
                {

                    invoicenameenglish = "Simplified Tax Invoice";
                    invoicenameengarabic = "فاتورة ضريبية مبسطة";
                }
                InvoiceEn.Text = invoicenameenglish.ToString();
                InvoiceAr.Text = invoicenameengarabic.ToString();

                InvoiceType.Text = saleDetail.IsCredit ? "Credit ائتمان" : "Cash نقدي";

                foreach (var item in saleDetail.SaleItems)
                {
                    qty = qty + Convert.ToInt32(item.Quantity);
                }
                xrLabel47.Text = qty + ":الكمية الإجمالية";
            var removeinvoicetagesEn = "";
            var removeinvoicetagesAr = "";
            if ((saleDetail.TermAndCondition != "" && saleDetail.TermAndCondition != null) || (saleDetail.TermAndConditionAr != "" && saleDetail.TermAndConditionAr != null))
            {
                removeinvoicetagesEn = (saleDetail.TermAndCondition != "" && saleDetail.TermAndCondition != null) ? saleDetail.TermAndCondition.Replace("<p>", " ").Replace("</p>", " ") : "";
                removeinvoicetagesAr = (saleDetail.TermAndConditionAr != "" && saleDetail.TermAndConditionAr != null) ? saleDetail.TermAndConditionAr.Replace("<p>", " ").Replace("</p>", " ") : "";

            }
            else
            {
                removeinvoicetagesEn = (printSetting.TermsInEng != "" && printSetting.TermsInEng != null) ? printSetting.TermsInEng.Replace("<p>", " ").Replace("</p>", " ") : "";
                removeinvoicetagesAr = (printSetting.TermsInAr != "" && printSetting.TermsInAr != null) ? printSetting.TermsInAr.Replace("<p>", " ").Replace("</p>", " ") : "";
            }
            xrLabel43.Text = removeinvoicetagesEn.ToString();
            xrLabel56.Text = removeinvoicetagesAr.ToString();

            if (saleDetail.BarCode != null && saleDetail.BarCode != "" && saleDetail.BarCode != string.Empty)
                {
                    byte[] barCode = Convert.FromBase64String(saleDetail.BarCode);
                    MemoryStream barCodes = new MemoryStream(barCode);
                    Bitmap barCodesImage = new Bitmap(barCodes);
                    xrPictureBox2.Image = barCodesImage;
                }
            if (companyDto.Base64Logo != null && companyDto.Base64Logo != "" && companyDto.Base64Logo != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(companyDto.Base64Logo);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                Image.Image = image;
            }
            
        }

    }
}
