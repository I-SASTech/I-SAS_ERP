using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice.PurchaseOrder
{
    public partial class PurchaseOrderTemplate6 : DevExpress.XtraReports.UI.XtraReport
    {
        public PurchaseOrderTemplate6(CompanyDto companyDto, PrintSetting printSetting, PurchaseOrderLookupModel saleDetail)
        {
            InitializeComponent();
            PrintSetting.DataSource = printSetting;
            CompanyDetail.DataSource = companyDto;
            SaleDetaill.DataSource = saleDetail;
            SaleItemLookUpModel.DataSource = saleDetail.PurchaseOrderItems;

            if (saleDetail.QrCode != null && saleDetail.QrCode != "" && saleDetail.QrCode != string.Empty)
            {
                byte[] Qrcode = Convert.FromBase64String(saleDetail.QrCode);
                MemoryStream QrCodems = new MemoryStream(Qrcode);
                Bitmap QrCodeImage = new Bitmap(QrCodems);
                QrCode.Image = QrCodeImage;
            }

            SupplierAddress.Text = saleDetail.CustomerAddress;


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
