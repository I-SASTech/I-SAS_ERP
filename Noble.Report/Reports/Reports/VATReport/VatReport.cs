using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.IO;
using System.Linq;
using DevExpress.Utils.Extensions;

namespace Noble.Report.Reports.Reports.VATReport
{
    public partial class VatReport : DevExpress.XtraReports.UI.XtraReport
    {
        public VatReport(CompanyDto companyDtl,VatPayableListModel vatPayableList,DateTime fromTime,DateTime toTime)
        {
            InitializeComponent();
             decimal totalvatpaid= vatPayableList.VatPaid.Sum(x => x.Amount);
            xrLabel9.Text = totalvatpaid < 0 ? (totalvatpaid * -1).ToString("N2") : totalvatpaid.ToString("N2");
            decimal totalvatPayable = vatPayableList.VatPayable.Sum(x => x.Amount);
            xrLabel12.Text = totalvatPayable < 0 ? (totalvatPayable * -1).ToString("N2") : totalvatPayable.ToString("N2");
            decimal totalvat = totalvatpaid + totalvatPayable;
            xrLabel15.Text = totalvat < 0 ? (totalvat * -1).ToString("N2") : totalvat.ToString("N2");
            vatPayableList.VatPaid.Where(x=> x.Amount !=null).ForEach(y=>y.Amount = Convert.ToDecimal(y.Amount.ToString("N2")));
            vatPayableList.VatPayable.Where(x=> x.Amount !=null).ForEach(y=>y.Amount = Convert.ToDecimal(y.Amount.ToString("N2")));
            CompanyDtl.DataSource=companyDtl;
            VatPayableDtl.DataSource = vatPayableList;
            xrLabel16.Text = fromTime.ToString("MM-dd-yyyy");
            xrLabel2.Text = toTime.ToString("MM-dd-yyyy");
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
