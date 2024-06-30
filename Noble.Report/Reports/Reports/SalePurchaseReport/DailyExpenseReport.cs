using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.Collections.Generic;
using System.IO;

namespace Noble.Report.Reports.Reports.SalePurchaseReport
{
    public partial class DailyExpenseReport : DevExpress.XtraReports.UI.XtraReport
    {
        public DailyExpenseReport (CompanyDto compantdtl, List<MonthWiseSaleLookUpModel> expense, DateTime fromDate,DateTime toDate)
        {
            InitializeComponent();
            companyInfo.DataSource = compantdtl;
            ExpenseInfo.DataSource = expense;
            xrLabel3.Text = Convert.ToDateTime(fromDate).ToString("dd MMMM yyyy");
            xrLabel4.Text = Convert.ToDateTime(toDate).ToString("dd MMMM yyyy");
            if (compantdtl.Base64Logo != null && compantdtl.Base64Logo != "" && compantdtl.Base64Logo != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(compantdtl.Base64Logo);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                xrPictureBox1.Image = image;
            }
        }
    }
}