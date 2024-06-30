using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Invoice.ServiceSaleOrder
{
    public partial class SaleOrder_SATemplate6 : DevExpress.XtraReports.UI.XtraReport
    {
        public SaleOrder_SATemplate6(CompanyDto companyDto, PrintSetting printSetting, SaleOrderDetailLookUpModel saleOrderDetail)
        {
            InitializeComponent();
            PrintSetting.DataSource = printSetting;
            CompanyDetail.DataSource = companyDto;
            SaleOrderDetail_SATemplate6.DataSource = saleOrderDetail;
            var invoiceForEn = "";
            var invoiceForAr = "";
            var invoiceToEn = "";
            var invoiceToAr = "";
            var invoiceNoEn = "";
            var invoiceNoAr = "";

            if (saleOrderDetail.IsQuotation)
            {
                invoiceToEn = "/Quotation To:";
                invoiceToAr = "عرض سعرإلى";
                invoiceForEn = "/Quotation for";
                invoiceForAr = "عرض سعر لـ";
                invoiceNoEn = "Quotation No.";
                invoiceNoAr = "الرقم عرض الأسعار";
            }
            else
            {
                invoiceToEn = "/Sale Order To";
                invoiceToAr = "أمر بيع ل";
                invoiceForEn = "/Sale Order for";
                invoiceForAr = "أمر بيع لـ";
                invoiceNoEn = "Sale Order No.";
                invoiceNoAr = "الرقم طلب المبيعات";
            }

            xrLabel15.Text = invoiceToEn;
            xrLabel16.Text = invoiceToAr;
            xrLabel36.Text = saleOrderDetail.InvoiceNote;
            xrLabel31.Text = invoiceForEn;
            xrLabel30.Text = invoiceForAr;
            xrLabel13.Text = invoiceNoEn;
            xrLabel12.Text = invoiceNoAr;

            var removeinvoicetagesEn = "";
            var removeinvoicetagesAr = "";
            if ((saleOrderDetail.Note != "" && saleOrderDetail.Note != null) || (saleOrderDetail.NoteAr != "" && saleOrderDetail.NoteAr != null))
            {
                removeinvoicetagesEn = (saleOrderDetail.Note != "" && saleOrderDetail.Note != null) ? saleOrderDetail.Note.Replace("<p>", " ").Replace("</p>", " ") : "";
                removeinvoicetagesAr = (saleOrderDetail.NoteAr != "" && saleOrderDetail.NoteAr != null) ? saleOrderDetail.NoteAr.Replace("<p>", " ").Replace("</p>", " ") : "";

            }
            else
            {
                removeinvoicetagesEn = (printSetting.TermsInEng != "" && printSetting.TermsInEng != null) ? printSetting.TermsInEng.Replace("<p>", " ").Replace("</p>", " ") : "";
                removeinvoicetagesAr = (printSetting.TermsInAr != "" && printSetting.TermsInAr != null) ? printSetting.TermsInAr.Replace("<p>", " ").Replace("</p>", " ") : "";
            }
            xrLabel54.Text = removeinvoicetagesEn.ToString();
            xrLabel55.Text = removeinvoicetagesAr.ToString();

            if (saleOrderDetail.WarrantyLogoPath != null)
            {
                if (saleOrderDetail.WarrantyLogoPath != null && saleOrderDetail.WarrantyLogoPath != "" && saleOrderDetail.WarrantyLogoPath != string.Empty)
                {
                    byte[] imageData = Convert.FromBase64String(saleOrderDetail.WarrantyLogoPath);
                    MemoryStream ms = new MemoryStream(imageData);
                    Bitmap image = new Bitmap(ms);
                    XRPictureBox pictureBox = new XRPictureBox();
                    WarrantyImage.Image = image;
                }
            }

            if (saleOrderDetail.BarCode != null && saleOrderDetail.BarCode != "" && saleOrderDetail.BarCode != string.Empty)
            {
                byte[] barCode = Convert.FromBase64String(saleOrderDetail.BarCode);
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
