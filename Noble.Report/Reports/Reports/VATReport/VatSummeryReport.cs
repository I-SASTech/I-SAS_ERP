using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.Collections.Generic;
using System.Linq;

namespace Noble.Report.Reports.Reports.VATReport
{
    public partial class VatSummeryReport : DevExpress.XtraReports.UI.XtraReport
    {
        public VatSummeryReport (CompanyDto companyInfo, List<VatSalePurchaseModel> vatSummery)
        {
            InitializeComponent();
            CompanyIDtl.DataSource = companyInfo;
            Vat.DataSource = vatSummery;

        }
    }
}
