 using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice.CustomerPayReciept
{
    public partial class A5_paymentVoucherList : DevExpress.XtraReports.UI.XtraReport
    {
        public A5_paymentVoucherList(CompanyDto companyDto, PrintSetting printSetting, PaymentVoucherLookUpModel paymentVoucherLookUpModel)
        {
            InitializeComponent();
            Date.Text= paymentVoucherLookUpModel.Date.ToString("dd MMMM yyyy");
            VocherNo.Text= paymentVoucherLookUpModel.VoucherNumber.ToString();
            SumofSR.Text="SAR  "+paymentVoucherLookUpModel.Amount.ToString();
            NameCust.Text=paymentVoucherLookUpModel.ContactAccountName.ToString();
            CashAccount.Text=paymentVoucherLookUpModel.BankCashAccountName.ToString();
            Balance.Text=paymentVoucherLookUpModel.Amount.ToString();
            if (paymentVoucherLookUpModel.DocumentId != null)
            {
                xrLabel13.Visible = true;
                xrLabel9.Visible = true;
                xrLabel6.Visible = true;
                xrLabel6.Text = paymentVoucherLookUpModel.DocumentName.ToString();
                xrLabel1.Visible = true;
                xrLabel14.Visible = true;
                xrLabel3.Visible = true;
                xrLabel14.Text = paymentVoucherLookUpModel.DocumentType.ToString();
            }
            if (paymentVoucherLookUpModel.QRCode != null && paymentVoucherLookUpModel.QRCode != "" && paymentVoucherLookUpModel.QRCode != string.Empty)
            {
                byte[] Qrcode = Convert.FromBase64String(paymentVoucherLookUpModel.QRCode);
                MemoryStream QrCodems = new MemoryStream(Qrcode);
                Bitmap QrCodeImage = new Bitmap(QrCodems);
                QRCode.Image = QrCodeImage;
            }
            if (printSetting.FooterImageForPrint != null && printSetting.FooterImageForPrint != "" && printSetting.FooterImageForPrint != string.Empty)
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
                Footer.Image = FooterImg;
            }

        }

    }
}
