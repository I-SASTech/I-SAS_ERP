using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.Collections.Generic;
using System.IO;

namespace Noble.Report.Reports.Reports.VATReport
{
    public partial class PurchaseVatReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PurchaseVatReport (CompanyDto companyDtl,List<SaleMonthWiseLookUpModel> saleDtl,DateTime fromTime,DateTime toTime,string formName)
        {
            InitializeComponent();
            companyInfo.DataSource=companyDtl;
            invoiceInfo.DataSource=saleDtl;
            xrLabel3.Text = fromTime.ToString("MMMM yy") + " To " + toTime.ToString("MMMM yy");
            if (formName == "PurchaseMonth" || formName == "SaleMonth")
            {
                if (formName == "SaleMonth")
                {
                    xrTableCell3.Text = "Customer Name";
                    xrLabel2.Text = " VAT SALE FOR PERIOD  ";
                    xrLabel6.Text = "Sale Vat Detail Report";
                    xrTableCell13.Text = "Sale No";
                    xrTableCell17.Text = "Customer VAT No.";
                }
                else
                {
                    xrTableCell3.Text = "Supplier Name";
                    xrLabel2.Text = " VAT PURCHASE FOR PERIOD  ";
                    xrLabel6.Text = "Purchase Vat Detail Report";
                    xrTableCell13.Text = "Purchase No";
                    xrTableCell17.Text = "Supplier VAT No.";
                }
            }
            else
            {
                xrTableCell17.Text = "";
                xrTableCell11.Text = "";
                xrTableCell2.BackColor =Color.DarkGray;
                xrLabel2.Text = " VAT FOR DDAILY ECPENSE INV# OF  ";
                xrLabel6.Text = "Daily Ecpense Report";
                xrTableCell13.Text = "Invoice No";
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
