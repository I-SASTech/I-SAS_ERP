using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.Linq;

namespace Noble.Report.Reports.TuchInvoice
{
    public partial class SA_TP_RetailSaTemplate1 : DevExpress.XtraReports.UI.XtraReport
    {
        public SA_TP_RetailSaTemplate1(CompanyDto companyDtl, SaleDetailLookupModel saleDtl)
        {
            InitializeComponent();
            CompanyInfo.DataSource = companyDtl;
            xrTableCell18.Text = saleDtl.TaxMethod == "Exclusive" ? "0.00" : saleDtl.SaleItems.Sum(x => x.TaxRate).ToString("N2");
            xrLabel10.Text = (saleDtl.SaleItems.Sum(x => x.Total) + Convert.ToDecimal(xrTableCell18.Text)).ToString("#.##");
            saleDtl.SaleItems.Where(x => x.TaxMethod != null).ToList().ForEach(b => b.TaxMethod = (b.TaxMethod == "Exclusive") ? "2" : "1");
            SaleInfo.DataSource = saleDtl;
        }
    }
}
