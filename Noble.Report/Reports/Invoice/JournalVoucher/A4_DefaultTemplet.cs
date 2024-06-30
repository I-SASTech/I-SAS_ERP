using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Invoice.JournalVoucher
{
    public partial class A4_DefaultTemplet : DevExpress.XtraReports.UI.XtraReport
    {
        public static string ConvertNumberToWords (int number)
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
        public A4_DefaultTemplet (CompanyDto companyDto, PrintSetting printSetting, JournalVoucherLookupModel JournalVoucher,string formName)
        {
            InitializeComponent();
            A4_PrintSetting.DataSource = printSetting;
            A4_CompanyDto.DataSource = companyDto;
            JournalVoucherInfo.DataSource = JournalVoucher;
            xrLabel1.Text = formName == "JournalVoucher" ? "إيصال الدفاتر" : "فتح النقدية";
            xrLabel3.Text = formName == "JournalVoucher" ? "Journal Voucher" : "Opening Cash";
            xrLabel18.Text= formName == "JournalVoucher" ? "Vocher No:" : "OC No:";
            xrLabel32.Text = formName == "JournalVoucher" ? ":ناخب:" : "رقم الافتتاح:";
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
