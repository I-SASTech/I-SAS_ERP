using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Noble.Report.Reports.PurchaseModule.PurchaseOrder
{
    public partial class PurchaseOrderGeneralTemplate : DevExpress.XtraReports.UI.XtraReport
    {
        public PurchaseOrderGeneralTemplate(CompanyDto companyDto, PrintSetting printSetting, PurchaseOrderLookupModel saleDetail)
        {
            InitializeComponent();
            SaleDetails.DataSource = saleDetail;
            CompanyDto.DataSource = companyDto;
            PrintSetting.DataSource = printSetting;

            //Company Phone and LandLine Numbers
            xrLabel4.Text = "PHONE:" + " " + companyDto.PhoneNo + " - " + companyDto.LandLine;

            //Company NTN Number
            xrLabel5.Text = "NTN # " + " " + companyDto.VatRegistrationNo;


            xrLabel26.Text = "GST" + " " + saleDetail.TaxRateName;
            xrLabel62.Text = "GST" + " " + saleDetail.TaxRateName;
            xrLabel68.Text = saleDetail.Note;

            if(saleDetail.DisplatName == "SupplierQuotation")
            {
                xrLabel3.Text = "Supplier Quotation";
                xrLabel17.Text = "Supplier Quotation #";
            }
            else
            {
                xrLabel3.Text = "Purchase Order";
                xrLabel17.Text = "Purchase Order #";

            }

            xrLabel73.Text = companyDto.CompanyNameEnglish + " - " + companyDto.PhoneNo + " - " + companyDto.CompanyEmail;

        }
        public static string ConvertNumberToWords(int number)
        {

            if (number == 0)
            {
                return "zero";
            }

            if (number < 0)
            {
                return "minus " + ConvertNumberToWords(Math.Abs(number));
            }

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += ConvertNumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertNumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertNumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                {
                    words += "and ";
                }

                string[] unitsMap = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                string[] tensMap = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                {
                    words += unitsMap[number];
                }
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                    {
                        words += "-" + unitsMap[number % 10];
                    }
                }
            }

            return words;
        }
    }
}
