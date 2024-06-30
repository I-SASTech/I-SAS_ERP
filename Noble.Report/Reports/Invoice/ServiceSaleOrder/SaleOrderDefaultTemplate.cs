using DevExpress.XtraReports.UI;
using Noble.Report.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Noble.Report.Reports.Invoice.ServiceSaleOrder
{
    public partial class SaleOrderDefaultTemplate : DevExpress.XtraReports.UI.XtraReport
    {

        public static string ConvertNumberToWords(double number)
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
                    words += unitsMap[Convert.ToInt64(number)];
                }
                else
                {
                    words += tensMap[Convert.ToInt64(number) / 10];
                    if ((number % 10) > 0)
                    {
                        words += "-" + unitsMap[Convert.ToInt64(number) % 10];
                    }
                }
            }

            return words;
        }
        public SaleOrderDefaultTemplate(CompanyDto companyDto, PrintSetting printSetting, SaleOrderDetailLookUpModel saleOrderDetail)
        {
            try
            {
                InitializeComponent();
                A4_Default_PrintSetting.DataSource = printSetting;
                A4_Default_CompanyDto.DataSource = companyDto;
                A4_Default_SaleDetails.DataSource = saleOrderDetail;
                saleOrderDetail.SaleOrderItems.Where(x => x.Code == null).ToList().ForEach(b => b.Code = b.StyleNumber);
                A4_Default_SaleItem.DataSource = saleOrderDetail.SaleOrderItems;
                xrLabel8.Text = saleOrderDetail.InvoiceNote;
                var total = saleOrderDetail.SaleOrderItems.Sum(b => b.TotalAmount);
                var reportName = "";
                var invoiceType = "";

                if (saleOrderDetail.IsQuotation)
                {
                    reportName = "Quotation Report";
                    invoiceType = "Quotation";
                }
                else
                {
                    reportName = "Sale Order Report";
                    invoiceType = "Sale Order";
                }
                report.Text = reportName;
                invoice.Text = invoiceType;


                var qty = 0;
                foreach (var item in saleOrderDetail.SaleOrderItems)
                {
                    qty = qty + Convert.ToInt32(item.Quantity);
                }

                xrLabel10.Text = qty + ":الكمية الإجمالية";

                if (saleOrderDetail.BarCode != null && saleOrderDetail.BarCode != "" && saleOrderDetail.BarCode != string.Empty)
                {
                    byte[] barCode = Convert.FromBase64String(saleOrderDetail.BarCode);
                    MemoryStream barCodes = new MemoryStream(barCode);
                    Bitmap barCodesImage = new Bitmap(barCodes);
                    xrPictureBox2.Image = barCodesImage;
                }
                if (companyDto.Base64Logo != null && companyDto.Base64Logo != "" && companyDto.Base64Logo != string.Empty)
                {
                    byte[] imageData = Convert.FromBase64String(companyDto.Base64Logo);
                    MemoryStream ms = new MemoryStream(imageData);
                    Bitmap image = new Bitmap(ms);
                    XRPictureBox pictureBox = new XRPictureBox();
                    xrPictureBox1.Image = image;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }


    }
}
