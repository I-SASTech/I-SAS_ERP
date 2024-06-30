using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using Noble.Report.Models;
using System.Linq;

namespace Noble.Report.Reports.Invoice
{
    public partial class A4_Templet3 : DevExpress.XtraReports.UI.XtraReport
    {
        public A4_Templet3(CompanyDto companyDto, PrintSetting printSetting, SaleDetailLookupModel saleDetail)
        {
            InitializeComponent();

            PrintSetting.DataSource = printSetting;
            CompanyDto.DataSource = companyDto;
            SaleDetail.DataSource = saleDetail;


            saleDetail.SaleItems.Where(x => x.Code == null).ToList().ForEach(b => b.Code = b.StyleNumber);
            SaleItem.DataSource = saleDetail.SaleItems;
            var invoicenameenglish = "";
            var invoicenameengarabic = "";
            if (saleDetail.CustomerCategory == "B2B – Business to Business")
            {
                invoicenameenglish = "Tax Invoice";
                invoicenameengarabic = "فاتورة ضريبية";
            }
            else if (saleDetail.CustomerCategory == "B2C – Business to Client")
            {

                invoicenameenglish = "Simplified Tax Invoice";
                invoicenameengarabic = "فاتورة ضريبية مبسطة";
            }
            //BankDetails.Text = printSetting.BankIcon1 + "   " + printSetting.BankAccount1;
            byte[] imageData = Convert.FromBase64String(printSetting.HeaderImageForPrint);
            MemoryStream ms = new MemoryStream(imageData);
            Bitmap image = new Bitmap(ms);
            XRPictureBox pictureBox = new XRPictureBox();
            byte[] footerData = Convert.FromBase64String(printSetting.FooterImageForPrint);
            MemoryStream Footerms = new MemoryStream(footerData);
            Bitmap FooterImg = new Bitmap(Footerms);
            byte[] Qrcode = Convert.FromBase64String(saleDetail.QRCode);
            MemoryStream QrCodems = new MemoryStream(Qrcode);
            Bitmap QrCodeImage = new Bitmap(QrCodems);
            QrCode.Image = QrCodeImage;
        }
    }

    
}
