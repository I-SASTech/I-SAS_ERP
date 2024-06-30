using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice.JournalVoucher
{
    public partial class SA_A4_Template6 : DevExpress.XtraReports.UI.XtraReport
    {
        public SA_A4_Template6 (CompanyDto companyDto, PrintSetting printSetting, JournalVoucherLookupModel JournalVoucher, string formName)
        {
            InitializeComponent();
            PrintSetting.DataSource = printSetting;
            CompanyDetail.DataSource = companyDto;
            Journal.DataSource = JournalVoucher;
            //xrLabel2.Text = formName == "JournalVoucher" ? "إيصال الدفاتر" : "فتح النقدية";
            //xrLabel1.Text = formName == "JournalVoucher" ? "Journal Voucher" : "Opening Cash";
            var removeinvoicetagesEn = "";
            var removeinvoicetagesAr = "";
            xrLabel25.Text = formName == "JournalVoucher" ? "Vocher No:" : "OC No:";
            xrLabel24.Text = formName == "JournalVoucher" ? ":ناخب:" : "رقم الافتتاح:";
            removeinvoicetagesEn = (printSetting.TermsInEng != "" && printSetting.TermsInEng != null) ? printSetting.TermsInEng.Replace("<p>", " ").Replace("</p>", " ") : "";
                removeinvoicetagesAr = (printSetting.TermsInAr != "" && printSetting.TermsInAr != null) ? printSetting.TermsInAr.Replace("<p>", " ").Replace("</p>", " ") : "";

            xrLabel54.Text = removeinvoicetagesEn.ToString();
            xrLabel55.Text = removeinvoicetagesAr.ToString();



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
                xrPictureBox2.Image = FooterImg;
                xrPictureBox3.Image = FooterImg;
            }
        }

    }
}
