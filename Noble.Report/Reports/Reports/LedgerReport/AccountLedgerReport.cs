using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.IO;
using System.Globalization;

namespace Noble.Report.Reports.Reports.LedgerReport
{
    public partial class AccountLedgerReport : DevExpress.XtraReports.UI.XtraReport
    {
        public AccountLedgerReport(CompanyDto compantdtl, AccountLedgerReportLookUp accountLedger,DateTime fromTime,DateTime toTime)
        {
            InitializeComponent();

            AccountLedger.DataSource = accountLedger;
            CompanyDto.DataSource=compantdtl;
            xrLabel3.Text = fromTime.ToString("dd MMMM yyyy");
            xrLabel4.Text = toTime.ToString("dd MMMM yyyy");
            xrLabel24.Text = DateTime.Now.ToString("dd MMMM yyyy");
            accountLedger.TransactionList.ForEach(x => x.OpeningBalance = Math.Round(x.OpeningBalance, 2));
            if (compantdtl.Base64Logo != null && compantdtl.Base64Logo != "" && compantdtl.Base64Logo != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(compantdtl.Base64Logo);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                xrPictureBox1.Image = image;
            }
        }

    }
}
