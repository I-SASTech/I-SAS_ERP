using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Noble.Report.Reports.Vochers
{
    public partial class PaymentVocher : DevExpress.XtraReports.UI.XtraReport
    {
        public PaymentVocher(CompanyDto companyDto, PrintSetting printSetting, PaymentVoucherLookUpModel paymentVoucherLookUpModel)
        {
            InitializeComponent();
            Payment.DataSource = paymentVoucherLookUpModel;
        }

    }
}
