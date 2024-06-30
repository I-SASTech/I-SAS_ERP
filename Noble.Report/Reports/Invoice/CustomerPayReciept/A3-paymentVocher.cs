using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Noble.Report.Reports.Invoice.CustomerPayReciept
{
    public partial class A3_paymentVocher : DevExpress.XtraReports.UI.XtraReport
    {
        public A3_paymentVocher (CompanyDto companyDto, PrintSetting printSetting, PaymentVoucherLookUpModel paymentVoucherLookUpModel)
        {
            InitializeComponent();
        }
    }
}
