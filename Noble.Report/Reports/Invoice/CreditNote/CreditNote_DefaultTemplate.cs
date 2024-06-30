using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Invoice.CreditNote
{
    public partial class CreditNote_DefaultTemplate : DevExpress.XtraReports.UI.XtraReport
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
        public CreditNote_DefaultTemplate(CompanyDto companyDto, PrintSetting printSetting, CreditNotesModel creditNotes)
        {
            InitializeComponent();
            A4_Default_PrintSetting.DataSource = printSetting;
            A4_Default_CompanyDto.DataSource = companyDto;
            A4_Default_CreditNote.DataSource = creditNotes;
            A4_Default_CreditNoteItems.DataSource = creditNotes.SaleOrderItems;
            xrLabel31.Text = creditNotes.InvoiceNote;
            var total = creditNotes.SaleOrderItems.Sum(b => b.TotalAmount);
            TotalinWords.Text = ConvertNumberToWords(Convert.ToInt32(total));

            var invoiceName = "";
            if(creditNotes.IsCreditNote)
            {
                invoiceName = "Credit Note";
            }
            else
            {
                invoiceName = "Debit Note";

            }
            InvoiceEn.Text = invoiceName;

            var qty = creditNotes.SaleOrderItems.Count();
            xrLabel47.Text = qty + ":الكمية الإجمالية";

            if (creditNotes.QRCode != null && creditNotes.QRCode != "" && creditNotes.QRCode != string.Empty)
            {
                byte[] Qrcode = Convert.FromBase64String(creditNotes.QRCode);
                MemoryStream QrCodems = new MemoryStream(Qrcode);
                Bitmap QrCodeImage = new Bitmap(QrCodems);
                xrPictureBox3.Image = QrCodeImage;
            }

            byte[] imageData = Convert.FromBase64String(companyDto.Base64Logo);
            MemoryStream ms = new MemoryStream(imageData);
            Bitmap image = new Bitmap(ms);
            XRPictureBox pictureBox = new XRPictureBox();
            Image.Image = image;
        }
    }

}

