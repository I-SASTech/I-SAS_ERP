using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Invoice.ReturnInvoice
{
    public partial class ReturnInvoice_SATemplate6 : DevExpress.XtraReports.UI.XtraReport
    {
        public ReturnInvoice_SATemplate6(CompanyDto companyDto, PrintSetting printSetting, SaleDetailLookupModel saleDetail)
        {
            InitializeComponent();
            PrintSetting.DataSource = printSetting;
            CompanyDetail.DataSource = companyDto;
            SaleDetaill.DataSource = saleDetail;
            SaleItemLookUpModel.DataSource = saleDetail.SaleItems;

            if (saleDetail.WarrantyLogoPath != null && saleDetail.WarrantyLogoPath != "" && saleDetail.WarrantyLogoPath != string.Empty)
            {
                byte[] imageData = Convert.FromBase64String(saleDetail.WarrantyLogoPath);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                WarrantyImage.Image = image;
            }
            //if (saleDetail.QRCode != null && saleDetail.QRCode != "" && saleDetail.QRCode != string.Empty)
            //{
            //    byte[] Qrcode = Convert.FromBase64String(saleDetail.QRCode);
            //    MemoryStream QrCodems = new MemoryStream(Qrcode);
            //    Bitmap QrCodeImage = new Bitmap(QrCodems);
            //    QrCode.Image = QrCodeImage;
            //}

            var removeinvoicetagesEn = "";
            var removeinvoicetagesAr = "";
            if ((saleDetail.TermAndCondition != "" && saleDetail.TermAndCondition != null) || (saleDetail.TermAndConditionAr != "" && saleDetail.TermAndConditionAr != null))
            {
                removeinvoicetagesEn = (saleDetail.TermAndCondition != "" && saleDetail.TermAndCondition != null) ? saleDetail.TermAndCondition.Replace("<p>", " ").Replace("</p>", " ") : "";
                removeinvoicetagesAr = (saleDetail.TermAndConditionAr != "" && saleDetail.TermAndConditionAr != null) ? saleDetail.TermAndConditionAr.Replace("<p>", " ").Replace("</p>", " ") : "";

            }
            else
            {
                removeinvoicetagesEn = (printSetting.TermsInEng != "" && printSetting.TermsInEng != null) ? printSetting.TermsInEng.Replace("<p>", " ").Replace("</p>", " ") : "";
                removeinvoicetagesAr = (printSetting.TermsInAr != "" && printSetting.TermsInAr != null) ? printSetting.TermsInAr.Replace("<p>", " ").Replace("</p>", " ") : "";
            }
            xrLabel55.Text = removeinvoicetagesEn.ToString();
            xrLabel54.Text = removeinvoicetagesAr.ToString();

            if (saleDetail.BarCode != null && saleDetail.BarCode != "" && saleDetail.BarCode != string.Empty)
            {
                byte[] barCode = Convert.FromBase64String(saleDetail.BarCode);
                MemoryStream barCodes = new MemoryStream(barCode);
                Bitmap barCodesImage = new Bitmap(barCodes);
                BarCode.Image = barCodesImage;
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
