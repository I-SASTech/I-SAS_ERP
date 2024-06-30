using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.Collections.Generic;
using System.IO;

namespace Noble.Report.Reports.Reports.SalePurchaseReport
{
    public partial class MonthlySalesComparisionReport : DevExpress.XtraReports.UI.XtraReport
    {
        public MonthlySalesComparisionReport(CompanyDto companydtl,List<MonthlyComparisionReportModel> compDtl,string month,string year)
        {
            InitializeComponent();
            string[] monthNames = new string[]{"","January", "February", "March", "April", "May", "June",
                        "July", "August", "September", "October", "November", "December"};
            companyInfo.DataSource=companydtl;
            comparisonInfo.DataSource=compDtl;
            xrLabel3.Text = monthNames[int.Parse(month)] + " " + year;
            if (companydtl.Base64Logo != null && companydtl.Base64Logo != "" && companydtl.Base64Logo != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(companydtl.Base64Logo);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                xrPictureBox1.Image = image;
            }
        }

    }
}
