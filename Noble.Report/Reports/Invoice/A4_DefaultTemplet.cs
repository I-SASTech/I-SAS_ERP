using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using Noble.Report.NobleDefaultServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Invoice
{
    public partial class A4_DefaultTemplet : DevExpress.XtraReports.UI.XtraReport
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

        public A4_DefaultTemplet(CompanyDto companyDto, PrintSetting printSetting, SaleDetailLookupModel saleDetail)
        {
            InitializeComponent();
            A4_Default_PrintSetting.DataSource = printSetting;
            A4_Default_CompanyDto.DataSource = companyDto;
            A4_Default_SaleDetails.DataSource = saleDetail;
            saleDetail.SaleItems.Where(x => x.Code == null).ToList().ForEach(b => b.Code = b.StyleNumber);
            var total = saleDetail.SaleItems.Sum(b => b.TotalAmount);
            TotalinWords.Text = ConvertNumberToWords(Convert.ToInt32(total));

            var invoicenameenglish = "";
            var invoicenameengarabic = "";

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

            InvoiceType.Text = saleDetail.IsCredit ? "Credit ائتمان" : "Cash نقدي";
            CheckInvoiceType.Text = saleDetail.IsCredit ? "Credit" : "Cash";
            InvoiceEn.Text = invoicenameenglish.ToString();
            InvoiceAr.Text = invoicenameengarabic.ToString();
            xrLabel53.Text = saleDetail.InvoiceNote;


            if (saleDetail.Discount < 0)
            {
                saleDetail.DiscountAmount += Math.Abs(saleDetail.Discount);
            }

            TotalAmount.Text = saleDetail.GrossAmount.ToString();
            DiscountAmount.Text = saleDetail.DiscountAmount.ToString();
            VatAmount.Text = saleDetail.VatAmount.ToString();
            VatAmount.Text = saleDetail.VatAmount.ToString();
            Bundle.Text = saleDetail.AfterDiscountAmount.ToString();
            GrandTotal.Text = saleDetail.TotalAmount.ToString();
            var qty = 0;
            foreach (var item in saleDetail.SaleItems)
            {
                qty = qty + Convert.ToInt32(item.Quantity);
            }

            xrLabel47.Text = qty + ":الكمية الإجمالية";

            if (saleDetail.InvoiceType == Enums.InvoiceType.Paid || saleDetail.InvoiceType == Enums.InvoiceType.Credit)
            {
                if (saleDetail.QRCode != null && saleDetail.QRCode != "" && saleDetail.QRCode != string.Empty)
                {
                    byte[] Qrcode = Convert.FromBase64String(saleDetail.QRCode);
                    MemoryStream QrCodems = new MemoryStream(Qrcode);
                    Bitmap QrCodeImage = new Bitmap(QrCodems);
                    xrPictureBox3.Image = QrCodeImage;
                }
            }

            if (saleDetail.BarCode != null && saleDetail.BarCode != "" && saleDetail.BarCode != string.Empty)
            {
                byte[] barCode = Convert.FromBase64String(saleDetail.BarCode);
                MemoryStream barCodes = new MemoryStream(barCode);
                Bitmap barCodesImage = new Bitmap(barCodes);
                xrPictureBox2.Image = barCodesImage;
            }
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
            //byte[] imageData = Convert.FromBase64String(companyDto.Base64Logo);
            //MemoryStream ms = new MemoryStream(imageData);
            //Bitmap image = new Bitmap(ms);
            //XRPictureBox pictureBox = new XRPictureBox();
            //Image.Image = image;
            if (companyDto.Base64Logo != null && companyDto.Base64Logo != "" && companyDto.Base64Logo != string.Empty)
            {
                
                byte[] imageData = Convert.FromBase64String(companyDto.Base64Logo);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                // Assuming "Image" is a property or variable that references the picture box control
                Image.Image = image;
            }
        }

    }
}
