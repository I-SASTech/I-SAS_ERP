using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Noble.Report.Reports.Reports.LedgerReport.AdvanceCustomerLedgerReport
{
    public partial class AdvanceCustomerSupplierComparisonReport : DevExpress.XtraReports.UI.XtraReport
    {
        public AdvanceCustomerSupplierComparisonReport(CompanyDto company, List<AdvanceCustomerLedgerLookupModel> Customer, string fromTime, string totime, bool iscustomer)
        {
            InitializeComponent();
            Company.DataSource= company;
            LedgerInfo.DataSource = Customer;
        }
    }
}
