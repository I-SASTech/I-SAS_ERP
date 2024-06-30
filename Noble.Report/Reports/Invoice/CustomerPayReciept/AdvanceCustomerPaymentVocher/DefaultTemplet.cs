using DevExpress.XtraReports.UI;
using Noble.Report.Enums;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice.CustomerPayReciept.AdvanceCustomerPaymentVocher
{
    public partial class DefaultTemplet : DevExpress.XtraReports.UI.XtraReport
    {
        public DefaultTemplet(CompanyDto company, PaymentVoucherLookUpModel paymentVoucherLookUpModel)
        {
            InitializeComponent();
            Company.DataSource= company;
            Payment.DataSource= paymentVoucherLookUpModel;
            if (paymentVoucherLookUpModel.DocumentId != null)
            {
                xrLabel1.Visible = true;
                xrLabel1.Text = paymentVoucherLookUpModel.DocumentName;
            }
            if (paymentVoucherLookUpModel.PaymentMode == SalePaymentType.Bank)
            {
                xrLabel33.Text = "Bank Account: ";
            }
            if (company.Base64Logo != null && company.Base64Logo != "" && company.Base64Logo != string.Empty)
            {

                byte[] imageData = Convert.FromBase64String(company.Base64Logo);
                MemoryStream ms = new MemoryStream(imageData);
                Bitmap image = new Bitmap(ms);
                XRPictureBox pictureBox = new XRPictureBox();
                // Assuming "Image" is a property or variable that references the picture box control
                Image.Image = image;
            }
        }

    }
}
