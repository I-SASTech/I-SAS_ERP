using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.IO;
using System.Linq;
using DevExpress.Utils.Extensions;

namespace Noble.Report.Reports.Reports.Account_FinanceReport
{
    public partial class IncomeStatement : DevExpress.XtraReports.UI.XtraReport
    {
        public IncomeStatement(CompanyDto companyDto, IncomeStatementListModel IncomeStatement,DateTime fromtime, DateTime totime)
        {
            InitializeComponent();
            IncomeStatement.Income.Where(x=>x.Amount!=null).ForEach(x=>x.Amount=Convert.ToDecimal(string.Format("{0:0.##}", x.Amount)));
            IncomeStatement.Expenses.Where(x=>x.Amount!=null).ForEach(x=>x.Amount= Convert.ToDecimal(string.Format("{0:0.##}", x.Amount)));
            IncomeStatementDtl.DataSource = IncomeStatement;
            CompanyDto.DataSource = companyDto;
            xrLabel3.Text = fromtime.ToString("dd MMMM yyyy");
            xrLabel4.Text = totime.ToString("dd MMMM yyyy");
            xrLabel24.Text = DateTime.Now.ToString("dd MMMM yyyy");
            if(IncomeStatement.Expenses.Sum(x => x.Amount) < 0)
                {
                xrLabel7.Text = (IncomeStatement.Expenses.Sum(x => x.Amount) * -1).ToString("N2");
            }
            else
            {

                xrLabel7.Text = IncomeStatement.Expenses.Sum(x => x.Amount).ToString("N2");
            }
            if (IncomeStatement.Income.Sum(x => x.Amount) < 0)
            {
                xrLabel8.Text = (IncomeStatement.Income.Sum(x => x.Amount) * -1).ToString("N2");
            }
            else
            {

                xrLabel8.Text = IncomeStatement.Income.Sum(x => x.Amount) .ToString("N2");
            }
            if (IncomeStatement.Income.Sum(x => x.Amount) + IncomeStatement.Expenses.Sum(x => x.Amount) < 0)
            {
                xrLabel15.Text = ((IncomeStatement.Income.Sum(x => x.Amount) + IncomeStatement.Expenses.Sum(x => x.Amount))*-1).ToString("N2");
            }
            else
            {
                xrLabel15.Text = (IncomeStatement.Income.Sum(x => x.Amount) + IncomeStatement.Expenses.Sum(x => x.Amount)).ToString("N2");
            }
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
