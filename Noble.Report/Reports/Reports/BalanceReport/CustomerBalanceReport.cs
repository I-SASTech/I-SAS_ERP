using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Reports.BalanceReport
{
    public partial class CustomerBalanceReport : DevExpress.XtraReports.UI.XtraReport
    {
        public CustomerBalanceReport(CompanyDto companyDtl, ContactLookUpModel contactLookUp, DateTime fromTime, DateTime toTime,string formName)
        {
            InitializeComponent();

            CompanyDto.DataSource = companyDtl;
            Contact.DataSource = contactLookUp;
            xrLabel3.Text = fromTime.ToString("dd MMMM yyyy");
            xrLabel4.Text = toTime.ToString("dd MMMM yyyy");
            xrLabel24.Text = DateTime.Now.ToString("dd MMMM yyyy");
            if (formName == "Customer")
            {
                xrLabel1.Text = "Customer BalanceSheet Reprot";
            }
            else
            {
                xrLabel1.Text = "Supplier BalanceSheet Reprot";
            }

            

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
