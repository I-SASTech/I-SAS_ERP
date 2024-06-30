using Noble.Report.Models;
using System;

namespace Noble.Report.Reports.SalesModuleReports.CreditNote
{
    public partial class CreditNotesGeneralTemplate : DevExpress.XtraReports.UI.XtraReport
    {
        public CreditNotesGeneralTemplate(CompanyDto companyDto, PrintSetting printSetting, CreditNotesModel saleDetail)
        {
            InitializeComponent();

            CompanyDto.DataSource = companyDto;
            PrintSetting.DataSource = printSetting;
            CreditNote.DataSource = saleDetail;

            //Company Phone and LandLine Numbers
            xrLabel4.Text = "PHONE:" + " " + companyDto.PhoneNo + " - " + companyDto.LandLine;

            //Company NTN Number
            xrLabel5.Text = "NTN # " + " " + companyDto.VatRegistrationNo;

            xrLabel24.Text = saleDetail.SaleNo;

            if(saleDetail.IsCreditNote)
            {
                xrLabel3.Text = "Credit Note";
                xrLabel18.Text = "Credit Note #";

            }
            else
            {
                xrLabel3.Text = "Debit Note";
                xrLabel18.Text = "Debit Note #";

            }
            string convertedAmount = ConvertNumberToWords(Convert.ToInt32(saleDetail.Amount));

            details.Text = "AMOUNT IN WORDS:" + "  " + convertedAmount;

            xrLabel42.Text = "For" + "  " + companyDto.CompanyNameEnglish;

            xrLabel29.Text = "GST" + " " + saleDetail.TaxRateName;
            xrLabel35.Text = "GST" + " " + saleDetail.TaxRateName;

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
