using DevExpress.XtraEditors.Filtering.Templates;
using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace Noble.Report.Reports.Invoice.CustomerPayReciept
{
    public partial class Custom_PaymentReciept : DevExpress.XtraReports.UI.XtraReport
    {
        public Custom_PaymentReciept (CompanyDto company, PaymentVoucherLookUpModel paymentVoucherLookUpModel)
        {
            InitializeComponent();
            CompanyInfo.DataSource=company;
            Payment.DataSource=paymentVoucherLookUpModel;

            Date.Text = paymentVoucherLookUpModel.Date.ToString("dd MMMM yyyy");
            VocherNo.Text = paymentVoucherLookUpModel.VoucherNumber.ToString();
            NameCust.Text = paymentVoucherLookUpModel.ContactAccountName.ToString();
            CashAccount.Text = paymentVoucherLookUpModel.BankCashAccountName.ToString();
            Balance.Text = paymentVoucherLookUpModel.Amount.ToString("N2");
            if (paymentVoucherLookUpModel.DocumentId != null)
            {
                xrLabel13.Visible = true;
                xrLabel6.Visible = true;
                xrLabel6.Text = paymentVoucherLookUpModel.DocumentName.ToString();
            }
            if (paymentVoucherLookUpModel.QRCode != null && paymentVoucherLookUpModel.QRCode != "" && paymentVoucherLookUpModel.QRCode != string.Empty)
            {
                byte[] Qrcode = Convert.FromBase64String(paymentVoucherLookUpModel.QRCode);
                MemoryStream QrCodems = new MemoryStream(Qrcode);
                Bitmap QrCodeImage = new Bitmap(QrCodems);
                QRCode.Image = QrCodeImage;
            }
        }
    }
}
