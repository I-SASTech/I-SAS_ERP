using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Reports.Advance_Account_Finance.AdvanceIncomeStatement
{
    public partial class AdvanceIncomeStatement : DevExpress.XtraReports.UI.XtraReport
    {
        public AdvanceIncomeStatement (CompanyDto companyInfo, BalanceSheetListModel BalanceDtl, DateTime fromDate, DateTime toDate)
        {
            InitializeComponent();
            xrLabel3.Text = fromDate.ToString("dd MMMM-yyyy");
            xrLabel4.Text = toDate.ToString("dd MMMM-yyyy");
            xrTableCell11.Text = BalanceDtl.YearlyIncome.ToString("N2");

            var Expense = BalanceDtl.Expense.GroupBy(item => item.CostCenter).Select(y => new BalanceSheetModel
            {
                Code = y.FirstOrDefault()?.Code,
                CostCenter = y.FirstOrDefault()?.CostCenter,
                CostCenterArabic = y.FirstOrDefault()?.CostCenterArabic,
                AccountType = y.FirstOrDefault()?.AccountType,
                AccountTypeArabic = y.FirstOrDefault()?.AccountTypeArabic,
                Date = y.FirstOrDefault().Date,
                Amount = y.Sum(x => Convert.ToDecimal(x.Amount.ToString("N2")))
                
            }).ToList();

            BalanceDtl.Expense = Expense;
            var Revinue = BalanceDtl.Income.GroupBy(item => item.CostCenter).Select(y => new BalanceSheetModel
            {
                Code = y.FirstOrDefault()?.Code,
                CostCenter = y.FirstOrDefault()?.CostCenter,
                CostCenterArabic = y.FirstOrDefault()?.CostCenterArabic,
                AccountType = y.FirstOrDefault()?.AccountType,
                AccountTypeArabic = y.FirstOrDefault()?.AccountTypeArabic,
                Date = y.FirstOrDefault().Date,
                Amount = y.Sum(x => x.Amount)
            }).ToList();
            BalanceDtl.Income = Revinue;

            CompanyInfo.DataSource = companyInfo;
            IncomeStatement.DataSource = BalanceDtl;
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
