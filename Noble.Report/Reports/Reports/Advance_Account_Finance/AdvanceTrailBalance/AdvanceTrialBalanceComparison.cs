using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Reports.Advance_Account_Finance.AdvanceTrailBalance
{
    public partial class AdvanceTrialBalanceComparison : DevExpress.XtraReports.UI.XtraReport
    {
        public AdvanceTrialBalanceComparison(CompanyDto companyInfo, List<AdvanceTrialBalanceLookupModel> trailBalance, string compairWith, string comparePeriod)
        {
            InitializeComponent();
            DateTime fromDates = DateTime.Now;
            DateTime toDates = DateTime.Now;
            if (compairWith == "Previous Month(s)")
            {
                for (int i = 1; i <= Convert.ToInt32(comparePeriod); i++)
                {
                    fromDates = DateTime.Now.AddMonths(-i);
                }
                xrLabel3.Text = fromDates.ToString("dd MMMM-yyyy");
                xrLabel1.Text = toDates.ToString("dd MMMM-yyyy");
            }
            else if (compairWith == "Previous Year(s)")
            {
                for (int i = 0; i < Convert.ToInt32(comparePeriod); i++)
                {
                    int year = DateTime.Now.Year;
                    int years = year - i;
                    fromDates = new DateTime(years, 1, 1);
                }
                xrLabel3.Text = fromDates.ToString("yyyy");
                xrLabel1.Text = toDates.ToString("yyyy");
            }
            else if (compairWith == "Previous Period(s)")
            {
                xrLabel3.Text = comparePeriod;
                xrLabel1.Visible = false;
                xrLabel5.Visible = false;
                xrLabel2.Text = "Period";

            }
            else if (compairWith == "Previous Quarter(s)")
            {

                for (int q = 1; q <= Convert.ToInt32(comparePeriod); q++)
                {
                    int year = fromDates.Year;
                    int quarter = (fromDates.Month - 1) / 3 + 1;

                    if (quarter - q < 0)
                    {
                        year--;
                        quarter = quarter + 4 - q;
                    }
                    else
                    {
                        quarter = quarter - q;
                    }

                    fromDates = new DateTime(year, 3 * quarter + 1, 1);
                    if (q == 1)
                    {
                        toDates = fromDates.AddMonths(3).AddDays(-1);
                    }


                }
                xrLabel3.Text = fromDates.ToString("dd MMMM-yyyy");
                xrLabel1.Text = toDates.ToString("dd MMMM-yyyy");
            }
            CompanyInfo.DataSource = companyInfo;

            var trailBalanceList = new List<AdvanceTrialBalanceLookupModel>();
            foreach (var item in trailBalance)
            {

                var listAssets = item.ListOfAdvanceTrialBalances.GroupBy(x => x.Name).Select(x => new AdvanceTrialBalanceLookupModel()
                {
                    Name = x.Key,
                    Code=x.FirstOrDefault().Code,
                    Debit = x.FirstOrDefault().Debit,
                    Credit = x.FirstOrDefault().Credit,
                    Total = x.FirstOrDefault().Total,
                    TrialBalance = x.FirstOrDefault().TrialBalance,
                    CompareWith = item.CompareWithReport

                }).ToList();

                if (trailBalanceList == null)
                {
                    trailBalanceList = trailBalanceList;
                }
                else
                {
                    foreach (var account in listAssets)
                    {

                        trailBalanceList.Add(new AdvanceTrialBalanceLookupModel
                        {
                            Name=account.Name,
                            Code=account.Code,
                            Debit = account.Debit,
                            Credit = account.Credit,
                            CompareWith = account.CompareWith,
                            Total = account.Total,
                            TrialBalance= account.TrialBalance,

                        });
                    }
                }
            }
            trailBalanceInfo.DataSource = trailBalanceList;
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
