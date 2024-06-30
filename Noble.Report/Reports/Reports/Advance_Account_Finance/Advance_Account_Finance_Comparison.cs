using DevExpress.CodeParser;
using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Noble.Report.Reports.Reports.Advance_Account_Finance
{
    public partial class Advance_Account_Finance_Comparison : DevExpress.XtraReports.UI.XtraReport
    {
        public Advance_Account_Finance_Comparison (CompanyDto companyInfo, BalanceSheetListModel BalanceDtl, AdvanceCostCenterLookupModel accounts, string comparePeriod,string compairWith)
        {
            InitializeComponent();
            DateTime fromDates=DateTime.Now;
            DateTime toDates= DateTime.Now;
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
                xrLabel2.Visible = false;
                xrLabel7.Text = "Period";

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
                    if(q ==1)
                    {
                        toDates = fromDates.AddMonths(3).AddDays(-1);
                    }


                }
                xrLabel3.Text = fromDates.ToString("dd MMMM-yyyy");
                xrLabel1.Text = toDates.ToString("dd MMMM-yyyy");
            }

            CompanyDtl.DataSource = companyInfo;
            var comparisonList = new List<BalanceSheetComparisonLookupModel>();
            var BalanceModelDtl = new List<BalanceSheetModel>();
            foreach (var item in BalanceDtl.BalanceSheetComparison)
            {
                var assetsModel = new BalanceSheetModel();

                var listAssets = item.Asset.GroupBy(x => x.CostCenter).Select(x => new BalanceSheetModel()
                {
                    CostCenter = x.Key,
                    Code = x.FirstOrDefault()?.Code,
                    Amount = Convert.ToDecimal(x.Sum(y => y.Amount).ToString("N2")),
                    CompareWith = item.CompareWithForReport
                }).ToList();

                var combinedListAssets = listAssets.Concat(accounts.Assets
                .Where(account => !listAssets.Any(a => a.Code == account.Code))
                .Select(account => new BalanceSheetModel
                {
                    Code = account.Code,
                    CostCenter = account.CostCenterName,
                    Amount = 0,
                    CompareWith = item.CompareWithForReport,
                })).ToList();

              
                if (BalanceDtl.Asset == null)
                {
                    BalanceDtl.Asset = combinedListAssets;
                }
                else
                {
                    foreach (var account in combinedListAssets)
                {

                        BalanceDtl.Asset.Add(new BalanceSheetModel
                        {
                            Code = account.Code,
                            CostCenter = account.CostCenter,
                            CompareWith = account.CompareWith,
                            Amount = account.Amount,

                        });
                    }
                }

                comparisonList.Add(new BalanceSheetComparisonLookupModel()
                {
                    CompareWith = item.CompareWith,
                    TotalAssets = Convert.ToDecimal(item.TotalAssets.ToString("N2")),
                    TotalEquities = Convert.ToDecimal(item.TotalEquities.ToString("N2")),
                    TotalLiabilities = Convert.ToDecimal(item.TotalLiabilities.ToString("N2")),
                    NetAmount = Convert.ToDecimal(item.NetAmount.ToString("N2")),
                    totalLiabilitiesAndEquities = Convert.ToDecimal((item.NetAmount + item.TotalEquities + item.TotalLiabilities).ToString("N2")),
                    Total = Convert.ToDecimal((item.NetAmount + item.TotalEquities).ToString("N2")),
                    
                });
            }
            foreach (var item in BalanceDtl.BalanceSheetComparison)
            {
                var assetsModel = new BalanceSheetModel();
                var listLiability = item.Liability.GroupBy(x => x.CostCenter).Select(x => new BalanceSheetModel()
                {
                    CostCenter = x.Key,
                    Code = x.FirstOrDefault()?.Code,
                    Amount = Convert.ToDecimal(x.Sum(y => y.Amount).ToString("N2")),
                    CompareWith = item.CompareWithForReport
                }).ToList();

                var combinedListLiabilities = listLiability.Concat(accounts.Liabilities
                   .Where(account => !listLiability.Any(a => a.Code == account.Code))
                   .Select(account => new BalanceSheetModel
                   {
                       Code = account.Code,
                       CostCenter = account.CostCenterName,
                       Amount = 0,
                       CompareWith = item.CompareWithForReport,
                   })).ToList();
                if (BalanceDtl.Liability == null)
                {
                    BalanceDtl.Liability = combinedListLiabilities;
                }
                else
                {
                    foreach (var account in combinedListLiabilities)
                    {

                        BalanceDtl.Liability.Add(new BalanceSheetModel
                        {
                            Code = account.Code,
                            CostCenter = account.CostCenter,
                            CompareWith = account.CompareWith,
                            Amount = account.Amount,

                        });
                    }
                }
            }
            foreach (var item in BalanceDtl.BalanceSheetComparison)
            {
                var assetsModel = new BalanceSheetModel();
                var listEquity = item.Equity.GroupBy(x => x.CostCenter).Select(x => new BalanceSheetModel()
                {
                    CostCenter = x.Key,
                    Code = x.FirstOrDefault()?.Code,
                    Amount = Convert.ToDecimal(x.Sum(y => y.Amount).ToString("N2")),
                    CompareWith = item.CompareWithForReport,
                }).ToList();

                var combinedListEquities = listEquity.Concat(accounts.Equities
                  .Where(account => !listEquity.Any(a => a.Code == account.Code))
                  .Select(account => new BalanceSheetModel
                  {
                      Code = account.Code,
                      CostCenter = account.CostCenterName,
                      Amount = 0,
                      CompareWith = item.CompareWithForReport,
                  })).ToList();



                if (BalanceDtl.Equity == null)
                {
                    BalanceDtl.Equity = combinedListEquities;
                }
                else
                {
                    foreach (var account in combinedListEquities)
                    {

                        BalanceDtl.Equity.Add(new BalanceSheetModel
                        {
                            Code = account.Code,
                            CostCenter = account.CostCenter,
                            CompareWith = account.CompareWith,
                            Amount = account.Amount,

                        });
                    }
                }


            }
            BalanceDtl.BalanceSheetComparison = null;
            BalanceDtl.BalanceSheetComparison = comparisonList;
            BalanceSheet.DataSource = BalanceDtl;
            var CostCenterAsset = new Noble.Report.Reports.Reports.CostCenterAssets(accounts);
                // xrSubreport1.ReportSource = CostCenterAsset;
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
