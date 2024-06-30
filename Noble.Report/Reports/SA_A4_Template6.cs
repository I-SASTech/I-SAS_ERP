﻿using DevExpress.Utils.Extensions;
using DevExpress.XtraReports.UI;
using iTextSharp.text.pdf;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Noble.Report.Reports
{
    public partial class SA_A4_Template6 : DevExpress.XtraReports.UI.XtraReport
    {
        public SA_A4_Template6(CompanyDto companyDto, PrintSetting printSetting, SaleDetailLookupModel saleDetail)
        {
             InitializeComponent();
            
            PrintSetting.DataSource = printSetting;
            CompanyDetail.DataSource = companyDto;
            SaleDetaill.DataSource = saleDetail;
            saleDetail.SaleItems.Where(x => x.Code == null).ToList().ForEach(b => b.Code = b.StyleNumber);
            SaleItemLookUpModel.DataSource = saleDetail.SaleItems;
            InvoiceTypeArabic.Text = "";

            var invoiceType = "";
            var invoiceTypeAr = "";
            if(saleDetail.InvoiceType==Enums.InvoiceType.Credit)
            {
                invoiceType = "";
                invoiceTypeAr = "Credit Invoice   فاتورة أجلة";

            }
            else if (saleDetail.InvoiceType == Enums.InvoiceType.Paid)
            {
               invoiceType = "";
                invoiceTypeAr = "Cash Invoice   الفاتورة النقدية";

            }
            else if (saleDetail.InvoiceType == Enums.InvoiceType.Hold)
            {
                invoiceType = "";
                invoiceTypeAr = "Hold Invoice  عقد الفاتورة";

            }
            InvoiceTypeArabic.Text = invoiceTypeAr;

           var invoicenameenglish = "";
            var invoicenameengarabic = "";
            if (saleDetail.CustomerCategory == "B2B – Business to Business")
            {
                invoicenameenglish = "";
                invoicenameengarabic = "B2B TAX INVOICE -  فاتورة ضريبية";
            }
            else if (saleDetail.CustomerCategory == "B2C – Business to Client")
            {

                invoicenameenglish = "";
                invoicenameengarabic = "B2C INVOICE  - فاتورة ضريبية مبسطة";
            }
            TaxInvoiceArabic.Text = invoicenameengarabic;

            var removeinvoicetagesEn = "";
            var removeinvoicetagesAr = "";
            if ((saleDetail.TermAndCondition != "" && saleDetail.TermAndCondition != null) || (saleDetail.TermAndConditionAr != "" && saleDetail.TermAndConditionAr != null))
            {
                removeinvoicetagesEn = (saleDetail.TermAndCondition != "" && saleDetail.TermAndCondition != null) ? saleDetail.TermAndCondition.Replace("<p>", " ").Replace("</p>", " ") : "";
                removeinvoicetagesAr = (saleDetail.TermAndConditionAr != "" && saleDetail.TermAndConditionAr != null) ?  saleDetail.TermAndConditionAr.Replace("<p>", " ").Replace("</p>", " ") : "" ;

            }
            else
            {
                removeinvoicetagesEn = (printSetting.TermsInEng != "" && printSetting.TermsInEng != null) ? printSetting.TermsInEng.Replace("<p>", " ").Replace("</p>", " ") : "";
                removeinvoicetagesAr = (printSetting.TermsInAr != "" && printSetting.TermsInAr != null) ? printSetting.TermsInAr.Replace("<p>", " ").Replace("</p>", " ") : "";
            }


            {
                

                Address.Text = saleDetail.CustomerAddress;


            }
            


            xrLabel54.Text = removeinvoicetagesEn.ToString();
            xrLabel55.Text = removeinvoicetagesAr.ToString();


            if (saleDetail.WarrantyLogoPath != null)
            {
                if (saleDetail.WarrantyLogoPath != null && saleDetail.WarrantyLogoPath != "" && saleDetail.WarrantyLogoPath != string.Empty)
                {
                    byte[] imageData = Convert.FromBase64String(saleDetail.WarrantyLogoPath);
                    MemoryStream ms = new MemoryStream(imageData);
                    Bitmap image = new Bitmap(ms);
                    XRPictureBox pictureBox = new XRPictureBox();
                    WarrantyImage.Image = image;
                }
            }
            
            if(saleDetail.InvoiceType == Enums.InvoiceType.Paid || saleDetail.InvoiceType == Enums.InvoiceType.Credit)
            {
                if (saleDetail.QRCode != null && saleDetail.QRCode != "" && saleDetail.QRCode != string.Empty)
                {
                    byte[] Qrcode = Convert.FromBase64String(saleDetail.QRCode);
                    MemoryStream QrCodems = new MemoryStream(Qrcode);
                    Bitmap QrCodeImage = new Bitmap(QrCodems);
                    QrCode.Image = QrCodeImage;
                }
            }

           
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
            //if (saleDetail.QRCode != null && saleDetail.QRCode != "" && saleDetail.QRCode != string.Empty)
            //{
            //    byte[] Qrcode = Convert.FromBase64String(saleDetail.QRCode);
            //    MemoryStream QrCodems = new MemoryStream(Qrcode);
            //    Bitmap QrCodeImage = new Bitmap(QrCodems);
            //    QrCode.Image = QrCodeImage;
            //}

        }

        private void ReportHeader_BeforePrint(object sender, CancelEventArgs e)
        {

        }

        private void SA_A4_Template6_BeforePrint(object sender, CancelEventArgs e)
        {
        
        }
    }
}
