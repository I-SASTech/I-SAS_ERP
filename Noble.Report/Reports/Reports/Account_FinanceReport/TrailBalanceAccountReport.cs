using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Reports.Account_FinanceReport
{
    public partial class TrailBalanceAccountReport : DevExpress.XtraReports.UI.XtraReport
    {
        public TrailBalanceAccountReport(CompanyDto companyDto, List<TrialBalanceAccountReportLookup> TrailBalanceAccount, DateTime fromtime, DateTime totime)
        {
            InitializeComponent();

            CompanyDto.DataSource = companyDto;
            TrailBalanceAccount.Where(x => x.Total != null).ToList().ForEach(b => b.Total = Convert.ToDecimal(string.Format("{0:0.##}", b.Total)));
            TrailBalanceAccount.Where(x => x.Debit != null).ToList().ForEach(b => b.Debit = Convert.ToDecimal(string.Format("{0:0.##}", b.Debit)));
            TrailBalanceAccount.Where(x => x.Credit != null).ToList().ForEach(b => b.Credit = Convert.ToDecimal(string.Format("{0:0.##}", b.Credit)));
            TrailAccount.DataSource = TrailBalanceAccount;
            xrLabel3.Text = fromtime.ToString("dd MMMM yyyy");
            xrLabel4.Text = totime.ToString("dd MMMM yyyy");
            xrLabel24.Text = DateTime.Now.ToString("dd MMMM yyyy");
            if (companyDto.Base64Logo != null && companyDto.Base64Logo != "" && companyDto.Base64Logo != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(companyDto.Base64Logo);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                xrPictureBox1.Image = image;
            }
        }

    }
}
