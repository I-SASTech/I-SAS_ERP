using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;

namespace Noble.Report.Reports.Reports.SalePurchaseReport.AdvanceSalePurchase
{
    public partial class AdvanceSaleInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        public AdvanceSaleInvoice(CompanyDto company,List<AdvanceSalesInvoiceLookupModel>sale,DateTime fromTime, DateTime toTime)
        {
            InitializeComponent();
            CompanyInfo.DataSource=company;
            SaleInfo.DataSource=sale;
            xrLabel3.Text = fromTime.ToString("dd MMMM yyyy");
            xrLabel4.Text = toTime.ToString("dd MMMM yyyy");
            if (company.Base64Logo != null && company.Base64Logo != "" && company.Base64Logo != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(company.Base64Logo);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                xrPictureBox1.Image = image;
            }
        }

    }
}
