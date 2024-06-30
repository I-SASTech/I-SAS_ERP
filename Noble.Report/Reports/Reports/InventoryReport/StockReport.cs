using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.IO;

namespace Noble.Report.Reports.Reports.InventoryReport
{
    public partial class StockReport : DevExpress.XtraReports.UI.XtraReport
    {
        public StockReport (CompanyDto companyDtl, StockListLookUpModel stockDtl,DateTime fromDate,DateTime toDate)
        {
            InitializeComponent();
            companyDetails.DataSource = companyDtl;
            Stock.DataSource = stockDtl;
            xrLabel3.Text = fromDate.ToString("MM-dd-yyyy");
            xrLabel4.Text = toDate.ToString("MM-dd-yyyy");
            if (companyDtl.Base64Logo != null && companyDtl.Base64Logo != "" && companyDtl.Base64Logo != string.Empty)
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
