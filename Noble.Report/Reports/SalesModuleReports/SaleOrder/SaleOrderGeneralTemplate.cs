using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Noble.Report.Reports.SalesModuleReports.SaleOrder
{
    public partial class SaleOrderGeneralTemplate : DevExpress.XtraReports.UI.XtraReport
    {
        public SaleOrderGeneralTemplate(CompanyDto companyDto, PrintSetting printSetting, SaleOrderDetailLookUpModel saleDetails)
        {
            InitializeComponent();
            PrintSetting.DataSource = printSetting;
            CompanyDto.DataSource = companyDto;
            SaleDetails.DataSource = saleDetails;

            //Company Phone and LandLine Numbers
            xrLabel4.Text = "PHONE:" + " " + companyDto.PhoneNo + " - " + companyDto.LandLine;

            //Company NTN Number
            xrLabel5.Text = "NTN # " + " " + companyDto.VatRegistrationNo;

            xrLabel41.Text = "AMOUNT IN WORDS:" + "  " + ConvertNumberToWords(Convert.ToInt32(saleDetails.TotalAmount));

            xrLabel42.Text = "For" + "  " + companyDto.CompanyNameEnglish;

            xrLabel29.Text = "GST" + " " + saleDetails.TaxRateName;
            xrLabel35.Text = "GST" + " " + saleDetails.TaxRateName;
            if (saleDetails.IsQuotation)
            {
                xrLabel3.Text = "Quotation Invoice";
                xrLabel17.Text = "Quotation #";
            }
            else
            {
                xrLabel3.Text = "Sale Order Invoice";
                xrLabel17.Text = "Sale Order #";
            }
            xrLabel14.Text = saleDetails.CustomerCRN;
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
