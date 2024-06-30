using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using Noble.Report.Reports.Reports.Account_FinanceReport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Reports.Advance_Account_Finance.AdvanceTrailBalanceAccount
{
    public partial class advanceTrailBalanceAccountComparison : DevExpress.XtraReports.UI.XtraReport
    {
        public advanceTrailBalanceAccountComparison (CompanyDto companyInfo, List<AdvanceTrialBalanceAccountLookupModel> trailBalance, List<AllAccountsLookupModel> accounts, string comparePeriod, string compairWith)
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
                xrLabel4.Text = toDates.ToString("dd MMMM-yyyy");
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
                xrLabel4.Text = toDates.ToString("yyyy");
            }
            else if (compairWith == "Previous Period(s)")
            {
                xrLabel3.Text = comparePeriod;
                xrLabel5.Visible = false;
                xrLabel4.Visible = false;
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
                xrLabel4.Text = toDates.ToString("dd MMMM-yyyy");
            }
            var trailBalanceReport = new List<AdvanceTrialBalanceAccountReportDtl>();
            CompanyDtl.DataSource = companyInfo;
            foreach (var item in trailBalance)
            {
                var listTrailBalance = item.CompareWithList.GroupBy(x => x.CostCenter).Select(x => new AdvanceTrialBalanceAccountLookupModel()
                {
                    CostCenter = x.Key,
                    AccountCode = x.FirstOrDefault()?.AccountCode,
                    Debit = Convert.ToDecimal(x.Sum(y => y.Debit).ToString("N2")),
                    Credit = Convert.ToDecimal(x.Sum(y => y.Credit).ToString("N2")),
                    Total = Convert.ToDecimal(x.Sum(y => y.Total).ToString("N2")),
                    CompareWith = item.CompairWithReport,
                    AccountName = x.FirstOrDefault().AccountName,
                }).ToList();
                //var combinedListTrailBalance = listTrailBalance.Concat(accounts
                //.Where(account => !listTrailBalance.Any(a => a.AccountCode == account.Code))
                //.Select(account => new AdvanceTrialBalanceAccountLookupModel
                //{
                //    AccountCode = account.Code,
                //    CostCenter = account.CostCenterName,
                //    AccountName = account.AccountName,
                //    Debit = 0,
                //    Credit = 0,
                //    Total = 0,
                //    CompareWith = item.CompareWith,
                //}))
                //.ToList();
                if (trailBalanceReport == null)
                {
                    foreach (var account in listTrailBalance)
                    {

                        trailBalanceReport.Add(new AdvanceTrialBalanceAccountReportDtl
                        {
                            AccountCode = account.AccountCode,
                            CostCenter = account.CostCenter,
                            CompareWith = account.CompareWith,
                            Debit = account.Debit,
                            Credit = account.Credit,
                            Total = account.Total,
                            AccountName = account.AccountName,

                        });
                    }
                }
                else
                {
                    foreach (var account in listTrailBalance)
                    {

                        trailBalanceReport.Add(new AdvanceTrialBalanceAccountReportDtl
                        {
                            AccountCode = account.AccountCode,
                            CostCenter = account.CostCenter,
                            CompareWith = account.CompareWith,
                            Debit=account.Debit,
                            Credit=account.Credit,
                            Total=account.Total,
                            AccountName=account.AccountName,

                        });
                    }
                }
            }
            trailBalanceSource.DataSource = trailBalanceReport;
            CompanyDtl.DataSource = companyInfo;
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
