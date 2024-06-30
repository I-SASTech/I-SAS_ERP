using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace Noble.Report.Reports.TuchInvoice
{
    public partial class SA_Default_DayEnd : DevExpress.XtraReports.UI.XtraReport
    {
        public SA_Default_DayEnd(CompanyDto companyDto, PrintSetting printSetting, OpeningBalanceLookUpModel openingBalance, DayStartLookUpModel IsDayStartLookupModel)
        {
            InitializeComponent();
            CompanyDto.DataSource = companyDto;
            openingBalanceDtl.DataSource = openingBalance;
            BankDtlItem.DataSource = openingBalance.BankDetails;
            IsDayStar.DataSource = IsDayStartLookupModel;
            decimal banktotal = openingBalance.BankDetails.Sum(x => decimal.Parse(x.TotalAmount));
            xrLabel24.Text = banktotal.ToString("#.##");
            xrLabel26.Text = (banktotal + IsDayStartLookupModel.TotalCash).ToString("#.##");
            xrTableCell9.Text=(IsDayStartLookupModel.VerifyCash-IsDayStartLookupModel.CarryCash).ToString("#.##");
        }

    }
}
