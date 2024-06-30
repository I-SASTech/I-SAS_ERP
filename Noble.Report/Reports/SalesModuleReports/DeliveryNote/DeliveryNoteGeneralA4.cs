using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Noble.Report.Models;

namespace Noble.Report.Reports.SalesModuleReports.DeliveryNote
{
    public partial class DeliveryNoteGeneralA4 : DevExpress.XtraReports.UI.XtraReport
    {
        public DeliveryNoteGeneralA4(CompanyDto companyDto, PrintSetting printSetting, DeliveryChallanLookUp saleDetail)
        {
            InitializeComponent();
            CompanyDto.DataSource = companyDto;
            PrintSetting.DataSource = printSetting;
            DeliveryChallan.DataSource = saleDetail;

            //Company Phone and LandLine Numbers
            xrLabel4.Text = "PHONE:" + " " + companyDto.PhoneNo + " - " + companyDto.LandLine;

            //Company NTN Number
            xrLabel5.Text = "NTN # " + " " + companyDto.VatRegistrationNo;


            xrLabel31.Text = saleDetail.Description;
            
        }
    }
}
