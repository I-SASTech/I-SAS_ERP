using DevExpress.XtraReports.UI;
using Noble.Report.Enums;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

namespace Noble.Report.Reports.Invoice.CustomerPayReciept.AdvanceCustomerPaymentVocher
{
    public partial class Templet2 : DevExpress.XtraReports.UI.XtraReport
    {
        public Templet2(CompanyDto company, PaymentVoucherLookUpModel paymentVoucherLookUpModel, PrintSetting printSetting)
        {
            InitializeComponent();
            Company.DataSource = company;
            Payment.DataSource = paymentVoucherLookUpModel;
            if (paymentVoucherLookUpModel.DocumentId != null)
            {
                xrLabel1.Visible = true;
                xrLabel1.Text = paymentVoucherLookUpModel.DocumentName;
            }
            if (paymentVoucherLookUpModel.PaymentMode == SalePaymentType.Bank)
            {
                xrLabel33.Text = "Bank Account: ";
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
