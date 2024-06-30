using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Noble.Report.Reports.PurchaseModule.PurchaseInvoice
{
    public partial class PurchaseInvoiceGeneralTemplate : DevExpress.XtraReports.UI.XtraReport
    {
        public PurchaseInvoiceGeneralTemplate(CompanyDto companyDto, PrintSetting printSetting, PurchasePostLookupModel purchaseDetails)
        {
            InitializeComponent();

            CompanyDto.DataSource = companyDto;
            PrintSetting.DataSource = printSetting;
            PurchaseDetail.DataSource = purchaseDetails;

            xrLabel1.Text = companyDto.CompanyNameEnglish;
            xrLabel8.Text = companyDto.AddressEnglish;
            xrLabel9.Text = companyDto.VatRegistrationNo;
            xrLabel14.Text = companyDto.VatRegistrationNo;
            xrLabel14.Text = companyDto.PhoneNo;
            xrLabel13.Text = companyDto.VatRegistrationNo;
            xrLabel11.Text = companyDto.CompanyEmail;
            xrLabel10.Text = companyDto.CompanyRegNo;

            xrLabel20.Text = purchaseDetails.RegistrationNo;
            xrLabel32.Text = purchaseDetails.SupplierAddress;
            xrLabel36.Text = purchaseDetails.SupplierCNIC;
            xrLabel33.Text = purchaseDetails.SupplierRegNo;
            xrLabel34.Text = purchaseDetails.SupplierCRN;
            xrLabel31.Text = purchaseDetails.SupplierContact;
            xrLabel36.Text = purchaseDetails.SupplierCode;
            xrLabel35.Text = purchaseDetails.SupplierRegNo;

            xrLabel41.Text = companyDto.CompanyNameEnglish;
            xrLabel42.Text = companyDto.NameEnglish;
            xrLabel43.Text = companyDto.AddressEnglish;
            xrLabel44.Text = companyDto.PhoneNo;
            xrLabel46.Text = companyDto.VatRegistrationNo;

            xrLabel47.Text = "TAX" + " " + purchaseDetails.TaxRatesName;
            xrLabel62.Text = "TAX" + " " + purchaseDetails.TaxRatesName;

            xrLabel68.Text = purchaseDetails.Note;

            xrLabel73.Text = companyDto.NameEnglish + " " + companyDto.PhoneNo + " " + companyDto.CompanyEmail;
        }
    }
}
