using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Noble.Report.Reports.ProductModule.BOM
{
    public partial class BOMGeneralReport : DevExpress.XtraReports.UI.XtraReport
    {
        public BOMGeneralReport(CompanyDto companyDto, PrintSetting printSetting, BomsLookupModel bom)
        {
            InitializeComponent();
            CompanyDto.DataSource = companyDto;
            PrintSetting.DataSource = printSetting;
            Bom.DataSource = bom;

            //Company Phone and LandLine Numbers
            xrLabel4.Text = "PHONE:" + " " + companyDto.PhoneNo + " - " + companyDto.LandLine;

            //Company NTN Number
            xrLabel5.Text = "NTN # " + " " + companyDto.VatRegistrationNo;

            xrLabel9.Text = bom.CreatedDate.ToString();
            xrLabel11.Text = bom.SaleOrder;
            xrLabel10.Text = bom.WareHouseName;



        }
    }
}
