using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Reports.SalePurchaseReport.AdvancePurchaseInvoiceReport
{
    public partial class PurchaseInvoiceSummary : DevExpress.XtraReports.UI.XtraReport
    {
        public PurchaseInvoiceSummary(CompanyDto company, List<PurchaseInvoiceSummaryLookupModel> Purchase, string compairWith, string comparePeriod)
        {
            InitializeComponent();
            CompanyInfo.DataSource = company;
            Purchaseinfo.DataSource = Purchase;
            DateTime fromDates = DateTime.Now;
            DateTime toDates = DateTime.Now;
            if (compairWith == "Previous Month(s)")
            {
                for (int i = 1; i <= Convert.ToInt32(comparePeriod); i++)
                {
                    fromDates = DateTime.Now.AddMonths(-i);
                }
                xrLabel9.Text = fromDates.ToString("dd MMMM-yyyy");
                xrLabel7.Text = toDates.ToString("dd MMMM-yyyy");
            }
            else if (compairWith == "Previous Year(s)")
            {
                for (int i = 0; i < Convert.ToInt32(comparePeriod); i++)
                {
                    int year = DateTime.Now.Year;
                    int years = year - i;
                    fromDates = new DateTime(years, 1, 1);
                }
                xrLabel9.Text = fromDates.ToString("yyyy");
                xrLabel7.Text = toDates.ToString("yyyy");
            }
            else if (compairWith == "Previous Period(s)")
            {
                xrLabel9.Text = comparePeriod;
                xrLabel7.Visible = false;
                xrLabel8.Visible = false;
                xrLabel11.Text = "Period";

            }
            else if (compairWith == "Previous Quarter(s)")
            {

                for (int q = 1; q <= Convert.ToInt32(comparePeriod); q++)
                {
                    int year = fromDates.Year;
                    int quarter = (fromDates.Month - 1) / 3 + 1;

                    if (quarter - q < 0)
                    {
                        year--;
                        quarter = quarter + 4 - q;
                    }
                    else
                    {
                        quarter = quarter - q;
                    }

                    fromDates = new DateTime(year, 3 * quarter + 1, 1);
                    if (q == 1)
                    {
                        toDates = fromDates.AddMonths(3).AddDays(-1);
                    }


                }
                xrLabel9.Text = fromDates.ToString("dd MMMM-yyyy");
                xrLabel7.Text = toDates.ToString("dd MMMM-yyyy");
            }
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
