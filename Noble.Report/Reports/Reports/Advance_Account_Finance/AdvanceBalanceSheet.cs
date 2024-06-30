using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.IO;
using System.Linq;
using DevExpress.Utils.Extensions;
using DevExpress.ClipboardSource.SpreadsheetML;
using System.Web.UI.WebControls;

namespace Noble.Report.Reports.Reports.Advance_Account_Finance
{
    public partial class AdvanceBalanceSheet : DevExpress.XtraReports.UI.XtraReport
    {
        public AdvanceBalanceSheet (CompanyDto companyInfo, BalanceSheetListModel BalanceDtl, DateTime fromDate, DateTime toDate)
        {
            InitializeComponent();

            xrLabel15.Text = BalanceDtl.YearlyIncome.ToString("N2");
            xrLabel23.Text = (BalanceDtl.YearlyIncome + BalanceDtl.TotalEquities).ToString("N2");

            if (Convert.ToDecimal(xrLabel15.Text) < 0)
            {
                xrLabel15.Text = "(" + (Convert.ToDecimal(xrLabel15.Text)*-1).ToString("N2") + ")";
                xrLabel15.ForeColor = Color.Red;

            }
            if (Convert.ToDecimal(xrLabel23.Text) < 0)
            {
                xrLabel23.Text = "(" + (Convert.ToDecimal(xrLabel23.Text) * -1).ToString("N2") + ")";
                xrLabel23.ForeColor = Color.Red;

            }
            xrLabel3.Text = fromDate.ToString("dd MMMM-yyyy");
            xrLabel4.Text = toDate.ToString("dd MMMM-yyyy");
            xrLabel20.Text = BalanceDtl.TotalAssets.ToString("N2");
            xrLabel17.Text = ((BalanceDtl.YearlyIncome + BalanceDtl.TotalEquities) + BalanceDtl.TotalLiabilities).ToString("N2");
            var Assets = BalanceDtl.Asset.GroupBy(item => item.CostCenter).Select(y => new BalanceSheetModel
            {
                Code = y.FirstOrDefault()?.Code,
                CostCenter = y.FirstOrDefault()?.CostCenter,
                CostCenterArabic = y.FirstOrDefault()?.CostCenterArabic,
                AccountType = y.FirstOrDefault()?.AccountType,
                AccountTypeArabic = y.FirstOrDefault()?.AccountTypeArabic,
                Date = y.FirstOrDefault().Date,
                Amount = y.Sum(x => Convert.ToDecimal(x.Amount.ToString("N2")))
            }).ToList();

            BalanceDtl.Asset = Assets;

            var Liabilities = BalanceDtl.Liability.GroupBy(item => item.CostCenter).Select(y => new BalanceSheetModel
            {
                Code = y.FirstOrDefault()?.Code,
                CostCenter = y.FirstOrDefault()?.CostCenter,
                CostCenterArabic = y.FirstOrDefault()?.CostCenterArabic,
                AccountType = y.FirstOrDefault()?.AccountType,
                AccountTypeArabic = y.FirstOrDefault()?.AccountTypeArabic,
                Date = y.FirstOrDefault().Date,
                Amount = y.Sum(x => Convert.ToDecimal(x.Amount.ToString("N2")))
            }).ToList();

            BalanceDtl.Liability = Liabilities;

            var equety = BalanceDtl.Equity.GroupBy(item => item.CostCenter).Select(y => new BalanceSheetModel
            {
                Code = y.FirstOrDefault()?.Code,
                CostCenter = y.FirstOrDefault()?.CostCenter,
                CostCenterArabic = y.FirstOrDefault()?.CostCenterArabic,
                AccountType = y.FirstOrDefault()?.AccountType,
                AccountTypeArabic = y.FirstOrDefault()?.AccountTypeArabic,
                Date = y.FirstOrDefault().Date,
                Amount = y.Sum(x => Convert.ToDecimal(x.Amount.ToString("N2")))
            }).ToList();

            BalanceDtl.Equity = equety;



            BalanceInfo.DataSource = BalanceDtl;
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
