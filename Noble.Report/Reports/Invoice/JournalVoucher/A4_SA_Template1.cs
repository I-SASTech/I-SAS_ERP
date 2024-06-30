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
    public partial class A4_SA_Template1 : DevExpress.XtraReports.UI.XtraReport
    {
        public A4_SA_Template1 (CompanyDto companyDto, PrintSetting printSetting, JournalVoucherLookupModel JournalVoucherinfo, string formName)
        {
            InitializeComponent();
            A4_Default_PrintSetting.DataSource = printSetting;
            A4_Default_CompanyDto.DataSource = companyDto;
            JournalVoucher.DataSource = JournalVoucherinfo;
            xrLabel2.Text = formName == "JournalVoucher" ? "إيصال الدفاتر" : "فتح النقدية";
            xrLabel1.Text = formName == "JournalVoucher" ? "Journal Voucher" : "Opening Cash";
            xrLabel18.Text = formName == "JournalVoucher" ? "Vocher No:" : "OC No:";
            xrLabel32.Text = formName == "JournalVoucher" ? ":ناخب:" : "رقم الافتتاح:";
            if (printSetting.HeaderImageForPrint != null && printSetting.HeaderImageForPrint != "" && printSetting.HeaderImageForPrint != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(printSetting.HeaderImageForPrint);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                xrPictureBox1.Image = image;
            }

        }

    }
}
