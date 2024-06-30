using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Reports.Advance_Account_Finance.AdvanceTrailBalanceAccount
{
    public partial class advanceTrailBalanceAccount : DevExpress.XtraReports.UI.XtraReport
    {
        public advanceTrailBalanceAccount (CompanyDto companyInfo, List<AdvanceTrialBalanceAccountLookupModel> trailBalance, DateTime fromDate, DateTime toDate)
        {
            InitializeComponent();
            CompanyInfo.DataSource = companyInfo;
            trailBalanceDtl.DataSource = trailBalance;
            xrLabel3.Text = fromDate.ToString("dd MMMM-yyyy");
            xrLabel4.Text = toDate.ToString("dd MMMM-yyyy");
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
