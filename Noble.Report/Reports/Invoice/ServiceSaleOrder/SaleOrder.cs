using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Invoice.ServiceSaleOrder
{
    public partial class SaleOrder : DevExpress.XtraReports.UI.XtraReport
    {

        public SaleOrder(CompanyDto companyDto, PrintSetting printSetting,SaleOrderDetailLookUpModel saleOrderDetail,bool report)
        {
            InitializeComponent();
            SA_A4_SaleOrder_PrintSetting.DataSource = printSetting;
            SA_A4_SaleOrder_CompanyDto.DataSource = companyDto;
            SA_A4_SaleOrder_SaleOrderDetails.DataSource = saleOrderDetail;
            englishName.Text = saleOrderDetail?.CustomerName;
            if (!string.IsNullOrEmpty(saleOrderDetail?.CustomerPrefix))
            {
                attension.Text = saleOrderDetail?.CustomerPrefix + " " + saleOrderDetail?.CustomerNameEn + " " + saleOrderDetail?.CustomerNameAr;

            }
            else
            {
                attension.Text =  saleOrderDetail?.CustomerNameEn + " " + saleOrderDetail?.CustomerNameAr;

            }
            saleOrderDetail.SaleOrderItems.Where(x => x.Code == null).ToList().ForEach(b => b.Code = b.StyleNumber);

            var removeinvoicetagesEn = "";
            var removeinvoicetagesAr = "";
            if ((saleOrderDetail.Note != "" && saleOrderDetail.Note != null) || (saleOrderDetail.NoteAr != "" && saleOrderDetail.NoteAr != null))
            {
                removeinvoicetagesEn = (saleOrderDetail.Note != "" && saleOrderDetail.Note != null) ? saleOrderDetail.Note.Replace("<p>", " ").Replace("</p>", " ") : "";
                removeinvoicetagesAr = (saleOrderDetail.NoteAr != "" && saleOrderDetail.NoteAr != null) ? saleOrderDetail.NoteAr.Replace("<p>", " ").Replace("</p>", " ") : "";

            }
            else
            {
                removeinvoicetagesEn = (printSetting.TermsInEng != "" && printSetting.TermsInEng != null) ? printSetting.TermsInEng.Replace("<p>", " ").Replace("</p>", " ") : "";
                removeinvoicetagesAr = (printSetting.TermsInAr != "" && printSetting.TermsInAr != null) ? printSetting.TermsInAr.Replace("<p>", " ").Replace("</p>", " ") : "";
            }
            var result = removeinvoicetagesEn +
                         (!string.IsNullOrEmpty(removeinvoicetagesEn) && !string.IsNullOrEmpty(removeinvoicetagesAr) ? "\n" : "") +
                         removeinvoicetagesAr;
            xrLabel4.Text = result;
            xrLabel6.Text = saleOrderDetail.InvoiceNote;
            BankDetails.Text = printSetting.BankIcon1 + "   " + printSetting.BankAccount1;

            int transitionIndex = -1;

            for (int i = 0; i < saleOrderDetail.CustomerName.Length; i++)
            {
                // Check if the character is an Arabic character
                if (saleOrderDetail.CustomerName[i] >= 0x0600 && saleOrderDetail.CustomerName[i] <= 0x06FF)
                {
                    transitionIndex = i;
                    break;
                }
            }

            string englishText = saleOrderDetail.CustomerName.Substring(0, transitionIndex).Trim();
            string arabicText = saleOrderDetail.CustomerName.Substring(transitionIndex).Trim();

            
                if (englishText == null)
                {
                    englishText = "";
                }
                if (arabicText == null)
                {
                    arabicText = "";
                }

                englishName.Text = englishText+ arabicText;
            

           


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
            if (report == true)
            {
                InvoiceEn.Text = "Sale Order";
                InvoiceAr.Text = "طلب المبيعات";
                ISaleOrderOrQuotation.Text = "Sale Order No/ رقم الفاتورة";
            }
            else if(report == false)
            {
                InvoiceEn.Text = "Quotation";
                InvoiceAr.Text = "عرض سعر";
                ISaleOrderOrQuotation.Text = "Quotation No/ رقم الفاتورة";
            }
            AmountInWords.Text = ConvertNumberToWords(Convert.ToInt32(saleOrderDetail.TotalAmount)) + " SAUDI RIYAL";

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
