using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice.PurchaseReturn
{
    public partial class PurchaseReturnTemplate6 : DevExpress.XtraReports.UI.XtraReport
    {
        public PurchaseReturnTemplate6(CompanyDto companyDto, PrintSetting printSetting, PurchasePostLookupModel saleDetail)
        {
            InitializeComponent();

            PrintSetting.DataSource = printSetting;
            CompanyDetail.DataSource = companyDto;
            SaleDetaill.DataSource = saleDetail;
            SaleItemLookUpModel.DataSource = saleDetail.PurchasePostItems;

            if (saleDetail.QrCode != null && saleDetail.QrCode != "" && saleDetail.QrCode != string.Empty)
            {
                byte[] Qrcode = Convert.FromBase64String(saleDetail.QrCode);
                MemoryStream QrCodems = new MemoryStream(Qrcode);
                Bitmap QrCodeImage = new Bitmap(QrCodems);
                QrCode.Image = QrCodeImage;
            }


            if (printSetting.HeaderImageForPrint != null && printSetting.HeaderImageForPrint != "" && printSetting.HeaderImageForPrint != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(printSetting.HeaderImageForPrint);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                xrPictureBox1.Image = image;
            }
            if (printSetting.FooterImageForPrint != null && printSetting.FooterImageForPrint != "" && printSetting.FooterImageForPrint != string.Empty)
            {
                byte[] footerData = Convert.FromBase64String(printSetting.FooterImageForPrint);
                MemoryStream Footerms = new MemoryStream(footerData);
                Bitmap FooterImg = new Bitmap(Footerms);
                xrPictureBox2.Image = FooterImg;
                xrPictureBox3.Image = FooterImg;
            }
        }

    }
}
