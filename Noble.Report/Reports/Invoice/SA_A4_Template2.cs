using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Noble.Report.Reports.Invoice
{
    public partial class SA_A4_Template2 : DevExpress.XtraReports.UI.XtraReport
    {
        public SA_A4_Template2(CompanyDto companyDto, PrintSetting printSetting, SaleDetailLookupModel saleDetail)
        {
            InitializeComponent();
            
            SA_A4_Temp2_PrintSetting.DataSource = printSetting;
            SA_A4_Temp2_CompanyDto.DataSource = companyDto;
            SA_A4_Temp2SaleDetails.DataSource = saleDetail;
            saleDetail.SaleItems.Where(x=>x.Code==null).ToList().ForEach(b => b.Code = b.StyleNumber);
            var invoicenameenglish = "";
            var invoicenameengarabic = "";
            
            if (saleDetail.CustomerCategory== "B2B – Business to Business")
            {
                invoicenameenglish = "Tax Invoice";
                invoicenameengarabic = "فاتورة ضريبية";
            }
            else if(saleDetail.CustomerCategory== "B2C – Business to Client") {

                invoicenameenglish = "Simplified Tax Invoice";
                invoicenameengarabic = "فاتورة ضريبية مبسطة";
            }

            int transitionIndex = -1;

            for (int i = 0; i < saleDetail.CustomerName.Length; i++)
            {
                // Check if the character is an Arabic character
                if (saleDetail.CustomerName[i] >= 0x0600 && saleDetail.CustomerName[i] <= 0x06FF)
                {
                    transitionIndex = i;
                    break;
                }
            }

            string englishText = saleDetail.CustomerName.Substring(0, transitionIndex).Trim();
            string arabicText = saleDetail.CustomerName.Substring(transitionIndex).Trim();

            string refrence = saleDetail.RefrenceNo;

            xrLabel44.Text = refrence;

            if (string.IsNullOrEmpty(englishText))
            {
                englishName.Text = "";
            }
            else
            {
                englishName.Text = englishText+" "+ arabicText;
            }

            



            InvoiceType.Text = saleDetail.IsCredit ? "Credit ائتمان" : "Cash نقدي";
            InvoiceEn.Text = invoicenameenglish.ToString();
            InvoiceAr.Text= invoicenameengarabic.ToString();
            xrLabel53.Text = saleDetail.InvoiceNote;
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
            var result = removeinvoicetagesEn +
                         (!string.IsNullOrEmpty(removeinvoicetagesEn) && !string.IsNullOrEmpty(removeinvoicetagesAr) ? "\n" : "") +
                         removeinvoicetagesAr;
            xrLabel36.Text = result;

            BankDetails.Text = printSetting.BankIcon1 +"   "+ printSetting.BankAccount1;
            if(printSetting.FooterImageForPrint!=null && printSetting.FooterImageForPrint!=""&& printSetting.FooterImageForPrint!=string.Empty)
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
            if(saleDetail.InvoiceType == Enums.InvoiceType.Paid || saleDetail.InvoiceType == Enums.InvoiceType.Credit)
            {
                if (saleDetail.QRCode != null && saleDetail.QRCode != "" && saleDetail.QRCode != string.Empty)
                {
                    byte[] Qrcode = Convert.FromBase64String(saleDetail.QRCode);
                    MemoryStream QrCodems = new MemoryStream(Qrcode);
                    Bitmap QrCodeImage = new Bitmap(QrCodems);
                    QrCode.Image = QrCodeImage;
                }
            }
            AmountInWords.Text = ConvertNumberToWords(Convert.ToInt32(saleDetail.TotalAmount)) + " SAUDI RIYAL";

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
