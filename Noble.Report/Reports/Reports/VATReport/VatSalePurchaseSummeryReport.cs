using DevExpress.XtraReports.UI;
using System;
using System.Drawing;
using Noble.Report.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Reports.VATReport
{
    public partial class VatSalePurchaseSummeryReport : DevExpress.XtraReports.UI.XtraReport
    {
        public VatSalePurchaseSummeryReport (CompanyDto companyDtl,List<VatSalePurchaseModel> summary, DateTime fromTime,DateTime toTime)
        {
            InitializeComponent();
            companyInfo.DataSource= companyDtl;
            summary = summary.Select(x =>
            {
                int year = x.Year;
                int month = x.Month;
                DateTime newDate = new DateTime(year, month, 1); 
                x.Date = newDate; 
                return x;
            }).ToList();
            summeryInfo.DataSource= summary;
            XtraReport report = new VatSummeryReport(companyDtl, summary);
            xrSubreport1.ReportSource = report;
            xrLabel3.Text=fromTime.ToString("MMMM dd-yy")+" To "+toTime.ToString("MMMM dd-yy");
            if (companyDtl.Base64Logo != null && companyDtl.Base64Logo != "" && companyDtl.Base64Logo != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(companyDtl.Base64Logo);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                xrPictureBox1.Image = image;
            }
        }
    }
}
