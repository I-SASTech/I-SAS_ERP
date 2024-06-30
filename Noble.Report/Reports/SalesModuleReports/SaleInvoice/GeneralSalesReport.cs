using Noble.Report.Models;
using System;

namespace Noble.Report.Reports.SalesModuleReports.SaleInvoice
{
    public partial class GeneralSalesReport : DevExpress.XtraReports.UI.XtraReport
    {
        public GeneralSalesReport(CompanyDto companyDto, PrintSetting printSetting, SaleDetailLookupModel saleDetail)
        {
            InitializeComponent();
            SaleDetails.DataSource = saleDetail;
            CompanyDto.DataSource = companyDto;
            PrintSetting.DataSource = printSetting;

            //Company Phone and LandLine Numbers
            xrLabel4.Text = "PHONE:" + " " + companyDto.PhoneNo + " - " + companyDto.LandLine;

            //Company NTN Number
            xrLabel5.Text = "NTN # " + " " + companyDto.VatRegistrationNo;

            xrLabel41.Text = "AMOUNT IN WORDS:" + "  " + ConvertNumberToWords(Convert.ToInt32(saleDetail.TotalAmount));

            xrLabel42.Text = "For" + "  " + companyDto.CompanyNameEnglish;

            xrLabel29.Text = "GST" + " " + saleDetail.TaxRateName;
            xrLabel35.Text = "GST" + " " + saleDetail.TaxRateName;

            if(saleDetail.InvoiceType ==  Enums.InvoiceType.Hold || saleDetail.InvoiceType == Enums.InvoiceType.Paid || saleDetail.InvoiceType == Enums.InvoiceType.Credit)
            {
                xrLabel3.Text = "SALES TAX INVOICE";
                xrLabel17.Text = "Invoice #";
            }
            else if (saleDetail.InvoiceType == Enums.InvoiceType.ProformaInvoice)
            {
                xrLabel3.Text = "PROFORMA INVOICE";
                xrLabel17.Text = "Proforma Invoice #";
            }
            else if(saleDetail.InvoiceType == Enums.InvoiceType.PurchaseOrder)
            {
                xrLabel3.Text = "CUSTOMER PURCHASE ORDER";
                xrLabel17.Text = "Customer PO #";
            }

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
