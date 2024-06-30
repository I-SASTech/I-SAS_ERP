using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;
using System.Linq;

namespace Noble.Report.Reports.Invoice
{
    public partial class SA_A4_Templet3 : DevExpress.XtraReports.UI.XtraReport
    {
        public SA_A4_Templet3(CompanyDto companyDtl,SaleDetailLookupModel saleDtl)
        {
            InitializeComponent();
            CompanyInfo.DataSource=companyDtl;
            xrTableCell18.Text = saleDtl.TaxMethod == "Exclusive" ? "0.00" : saleDtl.SaleItems.Sum(x => x.TaxRate).ToString("0.00");
            xrLabel11.Text = (saleDtl.SaleItems.Sum(x => x.Total) + Convert.ToDecimal(xrTableCell18.Text)).ToString("0.00");
            saleDtl.SaleItems.Where(x => x.TaxMethod != null).ToList().ForEach(b => b.TaxMethod = (b.TaxMethod == "Exclusive") ? "2" : "1");
            SaleInfo.DataSource=saleDtl;
            xrLabel53.Text = saleDtl.InvoiceNote;
        }

    }
}
