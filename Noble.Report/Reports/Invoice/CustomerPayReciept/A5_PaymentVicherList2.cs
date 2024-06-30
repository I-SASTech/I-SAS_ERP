using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice.CustomerPayReciept
{
    public partial class A5_PaymentVicherList2 : DevExpress.XtraReports.UI.XtraReport
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
        public A5_PaymentVicherList2(CompanyDto companyDto, PrintSetting printSetting, PaymentVoucherLookUpModel paymentVoucherLookUpModel)
        {
            InitializeComponent();
            Date.Text = paymentVoucherLookUpModel.Date.ToString("dd MMMM yyyy");
            VocherNo.Text = paymentVoucherLookUpModel.VoucherNumber.ToString();
            CashAccount.Text = paymentVoucherLookUpModel.BankCashAccountName.ToString();
            CustAccount.Text = paymentVoucherLookUpModel.ContactAccountName.ToString();
            UserName.Text = paymentVoucherLookUpModel.ApprovedBy.ToString();
            Date2.Text = paymentVoucherLookUpModel.Date.ToString();
            int total = Convert.ToInt32(paymentVoucherLookUpModel.Amount);
            TotalInWords.Text=ConvertNumberToWords(total).ToString();
            //PaymentType.Text = paymentVoucherLookUpModel.t.ToString();
            TotalAmount.Text="SAR "+paymentVoucherLookUpModel.Amount.ToString("#.##");
            if (paymentVoucherLookUpModel.DocumentId != null)
            {
                xrLabel13.Visible = true;
                xrLabel9.Visible = true;
                xrLabel6.Visible = true;
                xrLabel6.Text=paymentVoucherLookUpModel.DocumentName.ToString();
                xrLabel15.Visible = true;
                xrLabel14.Visible = true;
                xrLabel16.Visible = true;
                xrLabel14.Text=paymentVoucherLookUpModel.DocumentType.ToString();
            }
            if (paymentVoucherLookUpModel.QRCode != null && paymentVoucherLookUpModel.QRCode != "" && paymentVoucherLookUpModel.QRCode != string.Empty)
            {
                byte[] Qrcode = Convert.FromBase64String(paymentVoucherLookUpModel.QRCode);
                MemoryStream QrCodems = new MemoryStream(Qrcode);
                Bitmap QrCodeImage = new Bitmap(QrCodems);
                QRCode.Image = QrCodeImage;
            }
            if (printSetting.HeaderImageForPrint != null && printSetting.HeaderImageForPrint != "" && printSetting.HeaderImageForPrint != string.Empty)
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
        }

    }
}
