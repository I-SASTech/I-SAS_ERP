using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.Collections.Generic;
using System.IO;

namespace Noble.Report.Reports.Reports.LedgerReport
{
    public partial class AccountLedgerCostCenterWiseReport : DevExpress.XtraReports.UI.XtraReport
    {
        public AccountLedgerCostCenterWiseReport(CompanyDto companyInfo, List<CostCenterLookUpModel> AccountDtl, DateTime fromTime,DateTime toTime)
        {
     
            InitializeComponent();
            companyDetail.DataSource = companyInfo;
            costCenter.DataSource = AccountDtl;
            xrLabel3.Text = fromTime.ToString("MM-dd-yyyy");
            xrLabel4.Text = toTime.ToString("MM-dd-yyyy");
            xrLabel24.Text = DateTime.Now.ToString("MM-dd-yyyy");
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
