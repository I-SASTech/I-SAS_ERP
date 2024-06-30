using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Invoice
{
    public partial class SA_A4_Template4 : DevExpress.XtraReports.UI.XtraReport
    {
        public SA_A4_Template4(CompanyDto companyDto, PrintSetting printSetting, SaleDetailLookupModel saleDetail)
        {
            InitializeComponent();
            SA_A4_Temp2_SaleDetail.DataSource = saleDetail;


            saleDetail.SaleItems.Where(x => x.Code == null).ToList().ForEach(b => b.Code = b.StyleNumber);
            SA_A4_Temp2_SaleItem.DataSource = saleDetail.SaleItems;

            byte[] Qrcode = Convert.FromBase64String(saleDetail.QRCode);
            MemoryStream QrCodems = new MemoryStream(Qrcode);
            Bitmap QrCodeImage = new Bitmap(QrCodems);
            xrPictureBox2.Image = QrCodeImage;

        }
    }
}