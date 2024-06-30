using DevExpress.XtraReports.UI;
using iTextSharp.text.pdf;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.TuchInvoice
{
    public partial class SA_TP_Default_ReturnInv : DevExpress.XtraReports.UI.XtraReport
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
        public SA_TP_Default_ReturnInv(CompanyDto companyDto, PrintSetting printSetting, SaleDetailLookupModel saleDetail)
        {
            InitializeComponent();
            PrinterSetting.DataSource = printSetting;
            CompanyDto.DataSource = companyDto;
            SaleDtl.DataSource = saleDetail;
            var total = saleDetail.SaleItems.Sum(b => b.TotalAmount);
            Total_Amount.Text = ConvertNumberToWords(Convert.ToInt32(total));
            SaleItem.DataSource = saleDetail.SaleItems;
            if (saleDetail.QRCode != null && saleDetail.QRCode != "" && saleDetail.QRCode != string.Empty)
            {
                byte[] Qrcode = Convert.FromBase64String(saleDetail.QRCode);
                MemoryStream QrCodems = new MemoryStream(Qrcode);
                Bitmap QrCodeImage = new Bitmap(QrCodems);
                QrCode.Image = QrCodeImage;
            }
            if (saleDetail.BarCode != null && saleDetail.BarCode != "" && saleDetail.BarCode != string.Empty)
            {
                byte[] barCode = Convert.FromBase64String(saleDetail.BarCode);
                MemoryStream barCodes = new MemoryStream(barCode);
                Bitmap barCodesImage = new Bitmap(barCodes);
                xrPictureBox2.Image = barCodesImage;
            }
        }
    }
      
}
