using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DevExpress.Utils.Extensions;

namespace Noble.Report.Reports.Reports.LedgerReport
{
    public partial class CustomerLedgerReport : DevExpress.XtraReports.UI.XtraReport
    {
        public CustomerLedgerReport(CompanyDto compantdtl, List<CustomerLedgerModel> customerdtl, DateTime fromTime, DateTime toTime, string fromName)
        {
            InitializeComponent();
            xrLabel1.Text = fromName;
            //customerdtl.Where(x=>x.Amount!=null).ForEach(y=>y.Amount=Convert.ToDecimal(y.Amount.ToString("#.##")));
            decimal debit = customerdtl.Where(x => x.Amount > 0).Sum(y => y.Amount);
            xrLabel7.Text = debit.ToString("N2");
            decimal credit = customerdtl.Where(x => x.Amount <= 0).Sum(y => y.Amount);
            xrLabel9.Text = (credit * -1).ToString("N2");
            xrLabel12.Text = (customerdtl.Sum(x => x.Amount)).ToString("N2");
            customerdtl.ForEach(x => x.Amount = Math.Round(x.Amount, 2));
            CompanyDetail.DataSource = compantdtl;
            CustomerLedger.DataSource = customerdtl;
            if(fromName == "Supplier Ledger Report")
            {
                xrTableCell3.Text = "Supplier Name";
            }
            else
            {

                xrTableCell3.Text = "Customer Name";
            }
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
