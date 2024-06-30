using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Reports.Advance_Account_Finance.AdvanceIncomeStatement
{
    public partial class AdvanceIncomeStatementComparison : DevExpress.XtraReports.UI.XtraReport
    {
        public AdvanceIncomeStatementComparison (CompanyDto companyInfo, BalanceSheetListModel BalanceDtl, AdvanceCostCenterLookupModel accounts,string comparePeriod, string compairWith)
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
                xrLabel5.Text = fromDates.ToString("dd MMMM-yyyy");
                xrLabel3.Text = toDates.ToString("dd MMMM-yyyy");
            }
            else if (compairWith == "Previous Year(s)")
            {
                for (int i = 0; i < Convert.ToInt32(comparePeriod); i++)
                {
                    int year = DateTime.Now.Year;
                    int years = year - i;
                    fromDates = new DateTime(years, 1, 1);
                }
                xrLabel5.Text = fromDates.ToString("yyyy");
                xrLabel3.Text = toDates.ToString("yyyy");
            }
            else if (compairWith == "Previous Period(s)")
            {
                xrLabel5.Text = comparePeriod;
                xrLabel3.Visible = false;
                xrLabel4.Visible = false;
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
                    if (q == 1)
                    {
                        toDates = fromDates.AddMonths(3).AddDays(-1);
                    }


                }
                xrLabel5.Text = fromDates.ToString("dd MMMM-yyyy");
                xrLabel3.Text = toDates.ToString("dd MMMM-yyyy");
            }


            var BalanceModelDtl = new List<BalanceSheetModel>();
            foreach (var item in BalanceDtl.BalanceSheetComparison)
            {
                var assetsModel = new BalanceSheetModel();

                var listIncome = item.Income.GroupBy(x => x.CostCenter).Select(x => new BalanceSheetModel()
                {
                    CostCenter = x.Key,
                    Code = x.FirstOrDefault()?.Code,
                    Amount = Convert.ToDecimal(x.Sum(y => y.Amount).ToString("N2")),
                    CompareWith = item.CompareWithForReport
                }).ToList();

                var combinedListincome = listIncome.Concat(accounts.Incomes
                .Where(account => !listIncome.Any(a => a.Code == account.Code))
                .Select(account => new BalanceSheetModel
                {
                    Code = account.Code,
                    CostCenter = account.CostCenterName,
                    Amount = 0,
                    CompareWith = item.CompareWithForReport,
                })).ToList();


                if (BalanceDtl.Income == null)
                {
                    BalanceDtl.Income = combinedListincome;
                }
                else
                {
                    foreach (var account in combinedListincome)
                    {

                        BalanceDtl.Income.Add(new BalanceSheetModel
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
                var listExpense = item.Expense.GroupBy(x => x.CostCenter).Select(x => new BalanceSheetModel()
                {
                    CostCenter = x.Key,
                    Code = x.FirstOrDefault()?.Code,
                    Amount = Convert.ToDecimal(x.Sum(y => y.Amount).ToString("N2")),
                    CompareWith = item.CompareWithForReport
                }).ToList();

                var combinedListExpense = listExpense.Concat(accounts.Expenses
                   .Where(account => !listExpense.Any(a => a.Code == account.Code))
                   .Select(account => new BalanceSheetModel
                   {
                       Code = account.Code,
                       CostCenter = account.CostCenterName,
                       Amount = 0,
                       CompareWith = item.CompareWithForReport,
                   })).ToList();


                if (BalanceDtl.Expense == null)
                {
                    BalanceDtl.Expense = combinedListExpense;
                }
                else
                {
                    foreach (var account in combinedListExpense)
                    {

                        BalanceDtl.Expense.Add(new BalanceSheetModel
                        {
                            Code = account.Code,
                            CostCenter = account.CostCenter,
                            CompareWith = account.CompareWith,
                            Amount = account.Amount,

                        });
                    }
                }


            }
            IncomeStatement.DataSource = BalanceDtl;
            CompanyInfo.DataSource = companyInfo;
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
