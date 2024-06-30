using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Reports.LedgerReport.AdvanceCustomerLedgerReport
{
    public partial class AdvanceCustomerSupplierReport : DevExpress.XtraReports.UI.XtraReport
    {
        public AdvanceCustomerSupplierReport(CompanyDto company, List<AdvanceCustomerLedgerLookupModel> Customer, DateTime fromTime, DateTime totime,bool iscustomer)
        {
            InitializeComponent();
            cousomerInfo.DataSource = Customer;
            CompanyInfo.DataSource = company;
            if (iscustomer)
            {
                xrLabel1.Text = "Advance Customer Ledger Report";
                xrTableCell3.Text = "Customer Name";
            }
            else
            {
                xrLabel1.Text = "Advance Supplier Ledger Report";
                xrTableCell3.Text = "Supplier Name";
            }
            xrLabel3.Text = fromTime.ToString("dd MMMM yyyy");
            xrLabel4.Text = totime.ToString("dd MMMM yyyy");
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
