using DevExpress.Utils.Extensions;
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
    public partial class TrailBalanceReport : DevExpress.XtraReports.UI.XtraReport
    {
        public TrailBalanceReport(CompanyDto companyDto, List<TrialLookUp> TrailBalanceDtl, DateTime fromtime, DateTime totime)
        {
            InitializeComponent();
            xrTableCell13.Text=TrailBalanceDtl.Sum(x=>x.Debit).ToString("N2");
            xrTableCell19.Text=TrailBalanceDtl.Sum(x=>x.Credit).ToString("N2");
            xrTableCell20.Text=TrailBalanceDtl.Sum(x=>x.Total).ToString("N2");
            
            TrailBalanceDtl.Where(x => x.Total != null).ForEach(y => y.Total=y.Total < 0 ? y.Total * -1 : y.Total);
            TrailBalanceDtl.ForEach(x => x.TrialBalanceModel.ForEach(y => y.Total = y.Total < 0 ? -y.Total : y.Total));
            TrailBalance.DataSource = TrailBalanceDtl;
            CompanyDto.DataSource = companyDto;
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
