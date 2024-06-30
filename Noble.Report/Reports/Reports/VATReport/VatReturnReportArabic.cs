using DevExpress.XtraReports.UI;
using System;
using System.Drawing;
using System.IO;
using Noble.Report.Models;

namespace Noble.Report.Reports.Reports.VATReport
{
    public partial class VatReturnReportArabic : DevExpress.XtraReports.UI.XtraReport
    {
        public VatReturnReportArabic(CompanyDto companyDtl, VatReturnLookUpModel vatReturnLook, DateTime fromTime, DateTime toTime,string currency)
        {
            InitializeComponent();
            companyInfo.DataSource = companyDtl;
            xrLabel4.Text = fromTime.ToString("MM-dd-yyyy");
            xrLabel5.Text = DateTime.Now.ToString("MM-dd-yyyy");
            //xrLabel10.Text = fromTime.ToString("MMMM");


            SalesAmount.Text = currency + " " + (vatReturnLook.TotalSale < 0 ? (vatReturnLook.TotalSale * -1).ToString("N2") : vatReturnLook.TotalSale.ToString("N2"));
            SumOfAmount.Text = SalesAmount.Text;
            SaleVatAmount.Text = currency + " " + (vatReturnLook.TotalSaleVat < 0 ? (vatReturnLook.TotalSaleVat * -1).ToString("N2") : vatReturnLook.TotalSaleVat.ToString("N2"));
            SumOfVat.Text = SaleVatAmount.Text;
            Adjustment.Text = (0).ToString("N2");


            PurchaseAmount.Text = currency + " " + (vatReturnLook.TotalPurchase < 0 ? (vatReturnLook.TotalPurchase * -1).ToString("N2") : vatReturnLook.TotalPurchase.ToString("N2"));
            TotalPurchaseAmount.Text = PurchaseAmount.Text;
            PurchaseVatAmount.Text = currency + " " + (vatReturnLook.TotalPurchaseVat < 0 ? (vatReturnLook.TotalPurchaseVat * -1).ToString("N2") : vatReturnLook.TotalPurchaseVat.ToString("N2"));
            TotalVatAmount.Text = PurchaseVatAmount.Text;


            NetVat.Text = currency + " " + (vatReturnLook.TotalSaleVat - vatReturnLook.TotalPurchaseVat).ToString("N2");
            SumNetVat.Text = NetVat.Text;


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
