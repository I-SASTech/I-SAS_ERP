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
    public partial class Sale_PurchaseInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        public Sale_PurchaseInvoice(CompanyDto compantdtl, List<SaleInvoiceModel> saleDtl, string fromTime,string toTime,string formName)
        {
            InitializeComponent();
            companyInfo.DataSource = compantdtl;
            SaleInvoiceInfo.DataSource= saleDtl;
            xrLabel1.Text=formName+" Report";
            if(formName == "Sale")
            {
                xrTableCell4.Text = "Customer Name";
            }
            else
            {
                xrTableCell4.Text = "Supplier Name";
            }
            xrLabel3.Text = Convert.ToDateTime(fromTime).ToString("dd MMMM yyyy");
            xrLabel4.Text = Convert.ToDateTime(toTime).ToString("dd MMMM yyyy");
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
