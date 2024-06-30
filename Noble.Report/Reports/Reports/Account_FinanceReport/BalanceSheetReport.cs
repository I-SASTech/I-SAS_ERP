using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Reports.Account_FinanceReport
{
    public partial class BalanceSheetReport : DevExpress.XtraReports.UI.XtraReport
    {
        public BalanceSheetReport(CompanyDto companyDto,BalanceSheetListModel balanceSheet,DateTime fromtime,DateTime totime)
        {
            InitializeComponent();
            balanceSheet.Liability.ForEach(x => x.Amount=Convert.ToDecimal(x.Amount.ToString("N2")));
            balanceSheet.Equity.ForEach(x => x.Amount=Convert.ToDecimal(x.Amount.ToString("N2")));
            balanceSheet.Asset.ForEach(x => x.Amount=Convert.ToDecimal(x.Amount.ToString("N2")));
            BalanceSheet.DataSource = balanceSheet;
            CompanyDtl.DataSource = companyDto;
            decimal total = balanceSheet.Equity.Sum(x => x.Amount);
            decimal litotal = balanceSheet.Liability.Sum(x => x.Amount);
            decimal astotal = balanceSheet.Asset.Sum(x => x.Amount);
            xrLabel20.Text = astotal.ToString("#.##");
            xrLabel17.Text = ((total + balanceSheet.YearlyIncome)+ litotal).ToString("#.##");
            xrLabel7.Text = (total + balanceSheet.YearlyIncome).ToString("#.##");
            xrLabel15.Text=balanceSheet.YearlyIncome.ToString("#.##");
            xrLabel3.Text=fromtime.ToString("MM-dd-yyyy");
            xrLabel4.Text=totime.ToString("MM-dd-yyyy");
            xrLabel24.Text = DateTime.Now.ToString("MM-dd-yyyy");
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
