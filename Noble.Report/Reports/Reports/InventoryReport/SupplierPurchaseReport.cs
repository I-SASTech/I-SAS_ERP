using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using Noble.Report.Models;
using System.Collections.Generic;

namespace Noble.Report.Reports.Reports.InventoryReport
{
    public partial class SupplierPurchaseReport : DevExpress.XtraReports.UI.XtraReport
    {
        public SupplierPurchaseReport(CompanyDto companyDtl, List<PurchasePostLookupModel> supplierDtl, DateTime fromTime,DateTime toTime)
        {
            InitializeComponent();
            companyDetails.DataSource = companyDtl;
            SupplierPurchaseDtl.DataSource = supplierDtl;
            xrLabel3.Text = fromTime.ToString("MM-dd-yyyy");
            xrLabel4.Text = toTime.ToString("MM-dd-yyyy");
            xrLabel24.Text = DateTime.Now.ToString("MM-dd-yyyy");
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
