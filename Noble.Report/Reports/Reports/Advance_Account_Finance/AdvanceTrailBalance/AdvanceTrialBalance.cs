using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace Noble.Report.Reports.Reports.Advance_Account_Finance.AdvanceTrailBalance
{
    public partial class AdvanceTrialBalance : DevExpress.XtraReports.UI.XtraReport
    {
        public AdvanceTrialBalance(CompanyDto companyInfo, List<AdvanceTrialBalanceLookupModel> trailBalance,DateTime fromTime,DateTime toTime)
        {
            InitializeComponent();
            CompanyInfo.DataSource = companyInfo;
            xrLabel3.Text=fromTime.ToString("dd MMMM yyyy");
            xrLabel4.Text = toTime.ToString("dd MMMM yyyy");
            TrailBalanceInfo.DataSource = trailBalance;
            if (companyInfo.Base64Logo != null && companyInfo.Base64Logo != "" && companyInfo.Base64Logo != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(companyInfo.Base64Logo);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                xrPictureBox1.Image = image;
            }
        }

    }
}
