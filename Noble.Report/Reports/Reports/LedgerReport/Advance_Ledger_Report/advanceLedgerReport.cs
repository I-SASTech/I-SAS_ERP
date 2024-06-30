using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.Globalization;
using System.IO;

namespace Noble.Report.Reports.Reports.LedgerReport.Advance_Ledger_Report
{
    public partial class advanceLedgerReport : DevExpress.XtraReports.UI.XtraReport
    {
        public advanceLedgerReport(CompanyDto company, AdvanceLedgerAccountLookupModel ledger,DateTime fromtime,DateTime totime)
        {
            InitializeComponent();
            CompanyInfo.DataSource= company;
            LedgerInfo.DataSource= ledger;
            xrLabel3.Text=fromtime.ToString("dd MMMM yyyy");
            xrLabel4.Text = totime.ToString("dd MMMM yyyy");
            if (company.Base64Logo != null && company.Base64Logo != "" && company.Base64Logo != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(company.Base64Logo);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                xrPictureBox1.Image = image;
            }
        }

    }
}
