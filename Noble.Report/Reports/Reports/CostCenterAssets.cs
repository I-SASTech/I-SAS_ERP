using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.Linq;

namespace Noble.Report.Reports.Reports
{
    public partial class CostCenterAssets : DevExpress.XtraReports.UI.XtraReport
    {
        public CostCenterAssets (AdvanceCostCenterLookupModel account)
        {
            InitializeComponent();
            accountinfo.DataSource = account.Assets.OrderBy(y=>y.Code).ToList();
        }
    }
}
