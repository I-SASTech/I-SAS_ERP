using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice.JournalVoucher
{
    public partial class SA_A4_Template2 : DevExpress.XtraReports.UI.XtraReport
    {
        public SA_A4_Template2 (CompanyDto companyDto, PrintSetting printSetting, JournalVoucherLookupModel JournalVoucherinfo, string formName)
        {
            InitializeComponent();

            SA_A4_Temp2_PrintSetting.DataSource = printSetting;
            SA_A4_Temp2_CompanyDto.DataSource = companyDto;
            JournalVoucher.DataSource = JournalVoucherinfo;

            var removeinvoicetagesEn = "";
            var removeinvoicetagesAr = "";
                removeinvoicetagesEn = (printSetting.TermsInEng != "" && printSetting.TermsInEng != null) ? printSetting.TermsInEng.Replace("<p>", " ").Replace("</p>", " ") : "";
                removeinvoicetagesAr = (printSetting.TermsInAr != "" && printSetting.TermsInAr != null) ? printSetting.TermsInAr.Replace("<p>", " ").Replace("</p>", " ") : "";
            xrLabel36.Text = removeinvoicetagesEn.ToString();
            xrLabel37.Text = removeinvoicetagesAr.ToString();
            xrLabel9.Text = formName == "JournalVoucher" ? "إيصال الدفاتر" : "فتح النقدية";
            xrLabel10.Text = formName == "JournalVoucher" ? "Journal Voucher" : "Opening Cash";
            xrLabel18.Text = formName == "JournalVoucher" ? "Vocher No / ناخب" : "OC No / رقم الافتتاح";
            //xrLabel2.Text = formName == "JournalVoucher" ? ":ناخب:" : "رقم الافتتاح:";
            BankDetails.Text = printSetting.BankIcon1 + "   " + printSetting.BankAccount1;
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
