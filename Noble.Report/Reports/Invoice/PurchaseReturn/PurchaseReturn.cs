using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice.PurchaseReturn
{
    public partial class PurchaseReturn : DevExpress.XtraReports.UI.XtraReport
    {
        public PurchaseReturn(CompanyDto companyDto, PrintSetting printSetting, PurchasePostLookupModel purchaseDetails)
        {
            InitializeComponent();
            A4_PrintSetting.DataSource = printSetting;
            A4_CompanyDto.DataSource = companyDto;
            A4_PurchaseDetails.DataSource = purchaseDetails;
            A4_PurchaseItems.DataSource = purchaseDetails.PurchasePostItems;
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
            int transitionIndex = -1;

            for (int i = 0; i < purchaseDetails.SupplierName.Length; i++)
            {
                // Check if the character is an Arabic character
                if (purchaseDetails.SupplierName[i] >= 0x0600 && purchaseDetails.SupplierName[i] <= 0x06FF)
                {
                    transitionIndex = i;
                    break;
                }
            }

            string englishText = purchaseDetails.SupplierName.Substring(0, transitionIndex).Trim();
            string arabicText = purchaseDetails.SupplierName.Substring(transitionIndex).Trim();



            if (string.IsNullOrEmpty(englishText))
            {
                englishName.Text = "";
            }
            else
            {
                englishName.Text = englishText;
            }

            if (string.IsNullOrEmpty(arabicText))
            {
                arabicName.Text = "";
            }
            else
            {
                arabicName.Text = arabicText;
            }

            AmountInWords.Text = ConvertNumberToWords(Convert.ToInt32(purchaseDetails.TotalAmount));
        }
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

    }
}
