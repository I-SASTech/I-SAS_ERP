using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace Noble.Report.Reports.Reports.LedgerReport.Advance_Ledger_Report
{
    public partial class advanceAccountLedger : DevExpress.XtraReports.UI.XtraReport
    {
        public advanceAccountLedger(CompanyDto company, AdvanceLedgerAccountLookupModel ledger, string compairWith, string comparePeriod)
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


            var trailBalanceList = new List<AdvanceLedgerAccountLookupModel>();
            foreach (var item in ledger.CompareWithList)
            {
                trailBalanceList.Add(new AdvanceLedgerAccountLookupModel
                {
                    TotalCredit = item.TotalDebit,
                    CompareWith = item.CompareWith,
                    TotalDebit= item.TotalDebit,
                    OpeningBalance= item.OpeningBalance,
                    RunningBalance=item.RunningBalance,
                });
            }
            ledgerInfo.DataSource= trailBalanceList;
            CompanyInfo.DataSource = company;
        }

    }
}
