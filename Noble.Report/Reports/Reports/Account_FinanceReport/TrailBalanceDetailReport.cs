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
    public partial class TrailBalanceDetailReport : DevExpress.XtraReports.UI.XtraReport
    {
        public TrailBalanceDetailReport(CompanyDto companyDto, List<AccountTypeLookupModel> BalanceDtl, DateTime fromtime, DateTime totime)
             {
            InitializeComponent();
            var acc = BalanceDtl
                .SelectMany(x => x.CostCenterLookUpModel
                    .SelectMany(y => y.TrialBalanceModel)
                    .GroupBy(group => group.AccountName)
                    .Select(z => new TrialBalanceModel
                    {
                        Date = Convert.ToDateTime(z.FirstOrDefault().Date.ToString("MMMM-yy")),
                        AccountName = z.Key,
                        Debit = z.Sum(item => item.Debit),
                        Credit = z.Sum(item => item.Credit),
                        Total = z.Sum(item => item.Total)
                    }))
                .ToList();

            foreach (var balance in BalanceDtl)
            {
                foreach (var costCenter in balance.CostCenterLookUpModel)
                {

                }
            }


            TrailBalancedtl.DataSource = BalanceDtl;
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
