using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Reports.InventoryReport
{
    public partial class ProductComparisionReport : XtraReport
    {
        public ProductComparisionReport(CompanyDto companyDtl, List<ProductList> productLedger, DateTime fromTime, DateTime toTime)
        {
            InitializeComponent();
            companyDetails.DataSource = companyDtl;
            productLedgerDtl.DataSource = productLedger;
            xrLabel3.Text = fromTime.ToString("MM-dd-yyyy");
            xrLabel4.Text = toTime.ToString("MM-dd-yyyy");
            xrLabel24.Text = DateTime.Now.ToString("MM-dd-yyyy");
            if (!string.IsNullOrEmpty(companyDtl.Base64Logo) && companyDtl.Base64Logo != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(companyDtl.Base64Logo);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                xrPictureBox1.Image = image;
            }
        }

    }
}
