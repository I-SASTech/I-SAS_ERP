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
    public partial class VatExpenseReport : DevExpress.XtraReports.UI.XtraReport
    {
        public VatExpenseReport(CompanyDto compantdtl, List<DailyExpenseLookUpModel> dailyExpense,DateTime fromTime,DateTime toTime)
        {
            InitializeComponent();
            companyDto.DataSource = compantdtl;
            
            vatExpenseDto.DataSource = dailyExpense;
            xrLabel3.Text = fromTime.ToString("dd MMMM yyyy");
            xrLabel4.Text = toTime.ToString("dd MMMM yyyy");
            xrLabel24.Text = DateTime.Now.ToString("dd MMMM yyyy");
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
